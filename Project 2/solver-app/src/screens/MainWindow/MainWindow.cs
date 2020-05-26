using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;
using EvtSourceTDIN;

namespace Solver {
    public class MainWindow : Window {
        [UI] public ListBox myIssuesList = null;

        [UI] public ListBox unassignedIssues = null;

        Dictionary<String, Issue> issues;

        public IssueWindow issueWindow = null;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle) {
            builder.Autoconnect(this);

            issues = new Dictionary<string, Issue>();

            DeleteEvent += Window_DeleteEvent;

            myIssuesList.RowSelected += (object sender, RowSelectedArgs args) => {
                if (args.Row == null) return;

                var label = (Label)args.Row.Child;
                LaunchIssueWindow(label.Text, args.Row);
            };

            unassignedIssues.RowSelected += (object sender, RowSelectedArgs args) => {
                if (args.Row == null) return;

                var label = (Label)args.Row.Child;
                LaunchIssueWindow(label.Text, args.Row);
            };

            LoadMainWindow();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            Application.Quit();
        }

        private void LoadMainWindow() {
            var task = FetchIssues();
            subscribeServerEvents();
        }

        private async Task FetchIssues() {
            var response = await SolverApp.GetRequest("/api/solver/");

            if (response["issues"] == null) return;
            var fetchedIssues = response["issues"].ToObject<JArray>();

            for (int i = 0; i < fetchedIssues.Count; i++) {
                var issue = fetchedIssues[i];
                if (issue["assignee"].ToObject<String>() == null) {
                    InsertUnassignedIssue(issue);
                } else if (issue["assignee"].ToObject<String>() == SolverApp.GetEmail()
                && issue["state"].ToObject<String>() != "solved") {
                    InsertSolverIssues(issue);
                }
            }

        }

        private void InsertUnassignedIssue(JToken issue) {
            issues[issue["title"].ToString()] = new Issue() {
                Title = issue["title"].ToString(),
                ID = issue["_id"].ToString(),
                Description = issue["description"].ToString(),
                State = issue["state"].ToString(),
                Date = issue["createdAt"].ToString(),
                Creator = issue["creator"].ToString()
            };

            var listBoxRow = new ListBoxRow();

            listBoxRow.Add(new Label { Text = issue["title"].ToString(), Expand = true });

            unassignedIssues.Add(listBoxRow);
            listBoxRow.ShowAll();
        }

        private void InsertSolverIssues(JToken issue) {
            issues[issue["title"].ToString()] = new Issue() {
                Title = issue["title"].ToString(),
                ID = issue["_id"].ToString(),
                Description = issue["description"].ToString(),
                State = issue["state"].ToString(),
                Date = issue["createdAt"].ToString(),
                Creator = issue["creator"].ToString()
            };

            var listBoxRow = new ListBoxRow();
            listBoxRow.Add(new Label { Text = issue["title"].ToString(), Expand = true });

            myIssuesList.Add(listBoxRow);
            listBoxRow.ShowAll();
        }

        private void subscribeServerEvents() {
            var host = DotNetEnv.Env.GetString("SERVER_ADDRESS") + "/api/stream/solver";

            var evt = new EventSourceReader(new Uri(host)).Start();
            evt.MessageReceived += (object sender, EventSourceMessageEventArgs e) => {
                Task.Run(() => {
                    var parsedMessage = JObject.Parse(e.Message);

                    var type = parsedMessage["type"];

                    if (type == null) return;

                    switch (type.ToString()) {
                        case "issue":
                            InsertUnassignedIssue(parsedMessage["issue"]);
                            break;
                        case "question":
                            UpdateQuestion(parsedMessage["question"]);
                            break;
                        case "assign":
                            RemoveIssue(parsedMessage["issue"]);
                            break;
                        default:
                            break;
                    }
                });
            };
            evt.Disconnected += async (object sender, DisconnectEventArgs e) => {
                if (e.ReconnectDelay != 3000) {
                    Console.WriteLine($"Retry: {e.ReconnectDelay} - Error: {e.Exception.Message}");
                }
                await Task.Delay(e.ReconnectDelay);
                evt.Start(); // Reconnect to the same URL
            };
        }

        private void LaunchIssueWindow(string issueID, ListBoxRow row) {
            issueWindow = new IssueWindow(issues[issueID], this, row);
            SolverApp.GetApp().AddWindow(issueWindow);
            issueWindow.ShowAll();
        }

        private void UpdateQuestion(JToken question) {
            try {
                if (question["answer"] == null) return;

                foreach (var pair in issueWindow.questions) {
                    if (pair.Key == question["question"].ToString()) {
                        var q = (Question)pair.Value;
                        q.Answer = question["answer"].ToString();
                    }
                }
                if (issueWindow.answerDialog != null) {
                    issueWindow.answerDialog.UpdateQuestion(question["answer"].ToString());
                }
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        private void RemoveIssue(JToken issue) {
            if (issue["title"] == null) return;

            foreach (Widget child in unassignedIssues.Children) {
                var row = (ListBoxRow) child;
                var label = (Label) row.Child;

                if (label.Text == issue["title"].ToString()) {
                    unassignedIssues.Remove(row);
                }
            }
        }
    }
}

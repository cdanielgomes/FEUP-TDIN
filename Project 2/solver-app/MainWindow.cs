using System;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;

namespace Solver {
    class MainWindow : Window {
        [UI]
        ListBox myIssuesList = null;

        [UI]
        ListBox unassignedIssues = null;

        int userIssuesCounter = 0;
        int unassignedIssuesCounter = 0;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle) {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            LoadMainWindow();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            Application.Quit();
        }

        private void LoadMainWindow() {
            var task = FetchIssues();
        }

        private async Task FetchIssues() {
            var response = await SolverApp.GetRequest("/api/solver/");

            if (response["issues"] == null) return;

            var issues = response["issues"].ToObject<JArray>();
            
            for(int i = 0; i < issues.Count; i++) {
                var issue = issues[i];

                if (issue["assignee"].ToObject<String>() == null) {
                    InsertUnassignedIssue(issue);
                }
                else if (issue["assignee"].ToObject<String>() == SolverApp.GetEmail()) {
                    InsertSolverIssues(issue);
                }
            }

        }

        private void InsertUnassignedIssue(JToken issue) {
            var listBoxRow = new ListBoxRow();
            var title = issue["title"].ToObject<String>();

            listBoxRow.Add(new Label { Text = title, Expand = true });
            unassignedIssues.Insert(listBoxRow, unassignedIssuesCounter);
            listBoxRow.ShowAll();

            unassignedIssuesCounter++;
        }

        private void InsertSolverIssues(JToken issue) {
            var listBoxRow = new ListBoxRow();
            listBoxRow.Add(new Label { Text = issue["title"].ToString(), Expand = true });

            myIssuesList.Insert(listBoxRow, userIssuesCounter);
            listBoxRow.ShowAll();

            userIssuesCounter++;
        }
    }
}

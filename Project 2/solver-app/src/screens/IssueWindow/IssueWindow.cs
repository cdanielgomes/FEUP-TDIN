using System;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;

namespace Solver {
    class IssueWindow : Window {
        [UI] TextView descriptionBox = null;
        [UI] TextView answerBox = null;
        [UI] Label issueTitle = null;
        [UI] Label authorDate = null;
        [UI] Button solveButton = null;

        Issue issue;
        MainWindow mainWindow;
        ListBoxRow row;

        public IssueWindow(Issue issue, MainWindow mainWindow, ListBoxRow row) : this(new Builder("IssueWindow.glade"), issue, mainWindow, row) { }

        private IssueWindow(Builder builder, Issue _issue, MainWindow _mainWindow, ListBoxRow _row) : base(builder.GetObject("IssueWindow").Handle) {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            issue = _issue;
            mainWindow = _mainWindow;
            row = _row;

            issueTitle.Text = issue.Title;
            descriptionBox.Buffer.Text = issue.Description;
            authorDate.Text = $"Submited by {issue.Creator}, at {issue.Date}";

            if (issue.State == "unassigned") {
                solveButton.Label = "Assign";
                solveButton.Clicked += AssignButton_Clicked;
            } else {
                solveButton.Clicked += SolveButton_Clicked;
            }
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {

        }

        private void AssignButton_Clicked(object sender, EventArgs a) {
            var task = Assign();
        }


        private async Task Assign() {
            var endpoint = $"/api/solver/{issue.ID}/assigned";
            var requestBody = new JObject();
            var response = await SolverApp.PutRequest(endpoint, requestBody);

            if (response["issue"] != null) {
                solveButton.Label = "Solve";
                solveButton.Clicked -= AssignButton_Clicked;
                solveButton.Clicked += SolveButton_Clicked;

                mainWindow.unassignedIssues.Remove(row);
                mainWindow.myIssuesList.Add(row);
            }
        }

        private void SolveButton_Clicked(object sender, EventArgs a) {
            var task = Solve();
        }

        private async Task Solve() {
            var endpoint = $"/api/solver/{issue.ID}/assigned";
            var requestBody = new JObject();
            requestBody["answer"] = answerBox.Buffer.Text;
            var response = await SolverApp.PutRequest(endpoint, requestBody);

            if (response["issue"] != null) {
                mainWindow.myIssuesList.Remove(row);
                this.Hide();
                SolverApp.GetApp().RemoveWindow(this);
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;

namespace Solver {
    class QuestionDialog : Window {
        [UI] Entry departmentEntry = null;
        [UI] TextView questionText = null;
        [UI] Button sendButton = null;
        [UI] Button cancelButton = null;

        IssueWindow issueWindow;
        String issueID;

        public QuestionDialog(IssueWindow issueWindow, string issueID) : this(new Builder("QuestionDialog.glade"), issueWindow, issueID) { }

        private QuestionDialog(Builder builder, IssueWindow _issueWindow, string _issueID) : base(builder.GetObject("QuestionDialog").Handle) {
            builder.Autoconnect(this);

            issueWindow = _issueWindow;
            issueID = _issueID;

            DeleteEvent += Window_DeleteEvent;
            sendButton.Clicked += SendButton_Clicked;
            cancelButton.Clicked += CancelButton_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs args) {
            this.Hide();
            SolverApp.GetApp().RemoveWindow(this);
        }

        private void CancelButton_Clicked(object sender, EventArgs args) {
            this.Hide();
            SolverApp.GetApp().RemoveWindow(this);
        }

        private void SendButton_Clicked(object sender, EventArgs args) {
            var task = SendQuestion();
        }

        private async Task SendQuestion() {
            try {
                Console.WriteLine("coiso");
                var endpoint = $"http://localhost:3000/api/solver/{issueID}/question";
                var requestBody = new JObject();
                requestBody["question"] = questionText.Buffer.Text;
                requestBody["department"] = departmentEntry.Text;

                var response = await SolverApp.PostRequest(endpoint, requestBody);

                if (response["question"] == null) return;

                var responseQuestion = response["question"];

                issueWindow.questions[responseQuestion["question"].ToString()] = new Question() {
                    Text = responseQuestion["question"].ToString(),
                    Department = responseQuestion["department"].ToString(),
                    State = responseQuestion["state"].ToString(),
                    Date = responseQuestion["createdAt"].ToString()
                };

                var questionRow = new ListBoxRow();
                questionRow.Add(new Label { Text = responseQuestion["question"].ToString(), Expand = true });
                issueWindow.questionsList.Add(questionRow);
                questionRow.ShowAll();

                this.Hide();
                SolverApp.GetApp().RemoveWindow(this);
            }
            catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

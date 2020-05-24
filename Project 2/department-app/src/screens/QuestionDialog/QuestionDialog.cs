using System;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;

namespace Department {
    class QuestionDialog : Window {
        [UI] TextView questionBox = null;
        [UI] TextView answerBox = null;
        [UI] Button answerButton = null;
        [UI] Button closeButton = null;

        MainWindow mainWindow;
        Question question;
        ListBoxRow row;

        public QuestionDialog(Question question, MainWindow mainWindow, ListBoxRow row) : this(new Builder("QuestionDialog.glade"), question, mainWindow, row) { }

        private QuestionDialog(Builder builder, Question _question, MainWindow _mainWindow, ListBoxRow _row) : base(builder.GetObject("QuestionDialog").Handle) {
            builder.Autoconnect(this);

            mainWindow = _mainWindow;
            question = _question;
            row = _row;

            DeleteEvent += Window_DeleteEvent;
            questionBox.Buffer.Text = question.Text;
            answerBox.Buffer.Text = question.Answer;
            answerButton.Clicked += AnswerButton_Clicked;
            closeButton.Clicked += CloseButton_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs args) {
            DepartmentApp.GetApp().RemoveWindow(this);
        }

        private void AnswerButton_Clicked(object sender, EventArgs args) {
            var task = sendAnswer();
        }

        private void CloseButton_Clicked(object sender, EventArgs args) {
            this.Hide();
            DepartmentApp.GetApp().RemoveWindow(this);
        }

        private async Task sendAnswer() {
            var endpoint = $"/api/solver/{question.issueID}/questions/{question.ID}";
            var answer = answerBox.Buffer.Text;
            var requestBody = new JObject();

            var response = await DepartmentApp.PutRequest(endpoint, requestBody);

            if (response["message"] != null) return;

            mainWindow.questionsList.Remove(row);

            mainWindow.SaveData();

            mainWindow.questions.Remove(question.Text);
            this.Hide();
            DepartmentApp.GetApp().RemoveWindow(this);
        }
    }
}

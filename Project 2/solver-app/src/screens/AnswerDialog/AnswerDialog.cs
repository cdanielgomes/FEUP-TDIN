using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Solver {
    class AnswerDialog : Window {
        [UI] TextView questionBox = null;
        [UI] TextView answerBox = null;

        IssueWindow issueWindow;
        Question question;

        public AnswerDialog(Question question, IssueWindow issueWindow) : this(new Builder("AnswerDialog.glade"), question, issueWindow) { }

        private AnswerDialog(Builder builder, Question _question, IssueWindow _issueWindow) : base(builder.GetObject("AnswerDialog").Handle) {
            builder.Autoconnect(this);

            issueWindow = _issueWindow;
            question = _question;

            questionBox.Buffer.Text = question.Text;
            answerBox.Buffer.Text = question.Answer;
        }
    }
}

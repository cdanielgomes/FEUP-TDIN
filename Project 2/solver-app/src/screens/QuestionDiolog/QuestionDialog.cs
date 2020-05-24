using System;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;

namespace Solver {
    class QuestionDialog : Window {
        IssueWindow issueWindow;

        public QuestionDialog(Question question, IssueWindow issueWindow) : this(new Builder("QuestionWindow.glade"), question, issueWindow) { }

        private QuestionDialog(Builder builder, Question _question, IssueWindow _issueWindow) : base(builder.GetObject("QuestionWindow").Handle) {
            builder.Autoconnect(this);

            issueWindow = _issueWindow;
        }
    }
}

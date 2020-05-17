using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Department {
    class MainWindow : Window {
        [UI]
        ListBox myQuestionsList = null;

        [UI]
        ListBox unassignedQuestions = null;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle) {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            LoadUnassignedQuestions();
            LoadQuestionsList();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            Application.Quit();
        }

        private void LoadUnassignedQuestions() {
            var listBoxRow = new ListBoxRow();
            listBoxRow.Add(new Label { Text = "Unassigned Question", Expand = true });

            unassignedQuestions.Insert(listBoxRow, 0);
            listBoxRow.ShowAll();
        }

        private void LoadQuestionsList() {
            var listBoxRow = new ListBoxRow();
            listBoxRow.Add(new Label { Text = "My Question", Expand = true });

            myQuestionsList.Insert(listBoxRow, 0);
            listBoxRow.ShowAll();
        }
    }
}

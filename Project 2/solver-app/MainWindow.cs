using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Solver {
    class MainWindow : Window {

        [UI]
        ListBox myIssuesList = null;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle) {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            LoadIssuesList();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            Application.Quit();
        }

        private void LoadIssuesList() {
            var listBoxRow = new ListBoxRow();
            listBoxRow.Add(new Label { Text="Test Element", Expand=true});

            myIssuesList.Insert(listBoxRow,0);
        }
    }
}

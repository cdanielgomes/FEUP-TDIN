using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Solver {
    class MainWindow : Window {
        [UI]
        ListBox myIssuesList = null;

        [UI]
        ListBox unassignedIssues = null;
        QueueListener _queue = null;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle) {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            InitQueue();
            LoadUnassignedIssues();
            LoadIssuesList();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            Application.Quit();
        }

        private void LoadUnassignedIssues() {
            var listBoxRow = new ListBoxRow();
            listBoxRow.Add(new Label { Text = "Unassigned Issue", Expand = true });

            unassignedIssues.Insert(listBoxRow, 0);
            listBoxRow.ShowAll();
        }

        private void LoadIssuesList() {
            var listBoxRow = new ListBoxRow();
            listBoxRow.Add(new Label { Text = "My issue", Expand = true });

            myIssuesList.Insert(listBoxRow, 0);
            listBoxRow.ShowAll();
        }

        void InitQueue() {
            _queue = new QueueListener();
            _queue.Received += (message) => {
                var listBoxRow = new ListBoxRow();
                listBoxRow.Add(new Label { Text = "My issue", Expand = true });

                unassignedIssues.Insert(listBoxRow, 0);
                listBoxRow.ShowAll();
            };
        }
    }
}

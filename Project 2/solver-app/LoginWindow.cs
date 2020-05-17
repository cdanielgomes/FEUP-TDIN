using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Solver {
    class LoginWindow : Window {
        [UI]
        Button loginButton = null;

        [UI]
        Button registerButton = null;

        [UI]
        Button closeButton = null;

        public LoginWindow() : this(new Builder("LoginWindow.glade")) { }

        private LoginWindow(Builder builder) : base(builder.GetObject("LoginWindow").Handle) {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            loginButton.Clicked += LoginButton_Clicked;
            registerButton.Clicked += RegisterButton_Clicked;
            closeButton.Clicked += CloseButton_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            Application.Quit();
        }

        private void LoginButton_Clicked(object sender, EventArgs a) {
            
        }

        private void RegisterButton_Clicked(object sender, EventArgs a) {
            var registerWindow = new RegisterWindow();

            SolverApp.GetApp().AddWindow(registerWindow);
            registerWindow.ShowAll();

            SolverApp.GetApp().RemoveWindow(this);
            this.Hide();
        }

        private void CloseButton_Clicked(object sender, EventArgs a) {
            Application.Quit();
        }
    }
}

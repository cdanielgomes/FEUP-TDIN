using System;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;

namespace Department {
    class LoginWindow : Window {
        [UI] Button loginButton = null;
        [UI] Button registerButton = null;
        [UI] Button closeButton = null;
        [UI] Entry emailBox = null;
        [UI] Entry passwordBox = null;

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
            var task = AuthenticateUser();
        }

        private void RegisterButton_Clicked(object sender, EventArgs a) {
            var registerWindow = new RegisterWindow();

            DepartmentApp.GetApp().AddWindow(registerWindow);
            registerWindow.ShowAll();

            DepartmentApp.GetApp().RemoveWindow(this);
            this.Hide();
        }

        private void CloseButton_Clicked(object sender, EventArgs a) {
            Application.Quit();
        }

        private async Task AuthenticateUser() {
            var email = emailBox.Text;
            var pass = passwordBox.Text;

            var loginBody = new JObject();
            loginBody["email"] = email;
            loginBody["password"] = pass;

            var response = await DepartmentApp.PostRequest("/api/auth/login", loginBody);

            if (response == null) return;

            DepartmentApp.SetJwt(response["auth_token"].ToString());
            DepartmentApp.SetEmail(response["email"].ToString());

            var mainWindow = new MainWindow();
            DepartmentApp.GetApp().AddWindow(mainWindow);
            mainWindow.ShowAll();

            DepartmentApp.GetApp().RemoveWindow(this);
            this.Hide();
        }
    }
}

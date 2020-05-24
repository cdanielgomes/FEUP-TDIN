using System;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;

namespace Solver {
    class RegisterWindow : Window {
        [UI] Button registerButton = null;
        [UI] Button loginButton = null;
        [UI] Button closeButton = null;
        [UI] Entry emailBox = null;
        [UI] Entry nameBox = null;
        [UI] Entry passBox = null;
        [UI] Entry confBox = null;

        public RegisterWindow() : this(new Builder("RegisterWindow.glade")) { }

        private RegisterWindow(Builder builder) : base(builder.GetObject("RegisterWindow").Handle) {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            registerButton.Clicked += RegisterButton_Clicked;
            loginButton.Clicked += LoginButton_Clicked;
            closeButton.Clicked += CloseButton_Clicked;
        }

        private void Window_DeleteEvent(object sender, EventArgs a) {
            Application.Quit();
        }

        private void RegisterButton_Clicked(object sender, EventArgs a) {
            var task = AutheticateSolver();
        }

        private void LoginButton_Clicked(object sender, EventArgs a) {
            var loginWindow = new LoginWindow();

            SolverApp.GetApp().AddWindow(loginWindow);
            loginWindow.ShowAll();

            SolverApp.GetApp().RemoveWindow(this);
            this.Hide();
        }

        private void CloseButton_Clicked(object sender, EventArgs a) {
            Application.Quit();
        }

        private async Task AutheticateSolver() {
            var email = emailBox.Text;
            var name = nameBox.Text;
            var pass = passBox.Text;
            var conf = confBox.Text;

            var registerBody = new JObject();
            registerBody["email"] = email;
            registerBody["username"] = name;
            registerBody["password"] = pass;
            registerBody["passwordConf"] = conf;
            registerBody["role"] = "solver";

            var response = await SolverApp.PostRequest("/api/users/", registerBody);

            if (response == null) return;

            var loginBody = new JObject();
            loginBody["email"] = email;
            loginBody["password"] = pass;

            var loginResponse = await SolverApp.PostRequest("/api/auth/login", loginBody);

            if (loginResponse == null) return;

            SolverApp.SetJwt(response["auth_token"].ToString());
            SolverApp.SetEmail(response["email"].ToString());

            var mainWindow = new MainWindow();
            SolverApp.GetApp().AddWindow(mainWindow);
            mainWindow.ShowAll();

            SolverApp.GetApp().RemoveWindow(this);
            this.Hide();
        }
    }
}

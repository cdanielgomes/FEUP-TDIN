using System;
using System.Threading.Tasks;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json.Linq;

namespace Solver {
    class RegisterWindow : Window {
        [UI]
        Button registerButton = null;

        [UI]
        Button loginButton = null;

        [UI]
        Button closeButton = null;

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
            var json = JObject.Parse(@"{
                'email':'nome@mail.com',
                'username': 'nome',
                'password': '123456',
                'passwordConf': '123456',
                'role': 'solver'
            }");
            await SolverApp.PostRequest("/api/users/", json);
        }
    }
}

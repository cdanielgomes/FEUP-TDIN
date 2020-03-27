using System;
using System.Windows.Forms;
using Common;

namespace Client {
    public partial class RegisterWindow : Form {
        public RegisterWindow() {
            InitializeComponent();
        }
        
        private void registerButton_Click(object sender, EventArgs e) {
                        
            if (ClientApp.GetServer().RegisterUser(nicknameBox.Text, passwordBox.Text)) {
                Console.WriteLine("Registration worked");

                if (ClientApp.GetServer().LoginUser(nicknameBox.Text, passwordBox.Text, "address")) {
                    ClientApp.SetLoggedUser(new ActiveUser(nicknameBox.Text, "address"));
                    Console.WriteLine("Login worked");
                    this.Close();
                    Application.Run(new MainWindow());
                }
            }
            else {
                Console.WriteLine("Registration failed");
            }
        }
        
        private void loginButton_Click(object sender, EventArgs e) {
            this.Close();
            Application.Run(new LoginWindow());
        }
    }
}
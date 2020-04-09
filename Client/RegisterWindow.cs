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

                if (ClientApp.GetServer().LoginUser(nicknameBox.Text, passwordBox.Text, ClientApp.GetInstance().Address)) {
                    ClientApp.SetLoggedUser(new ActiveUser(nicknameBox.Text, ClientApp.GetInstance().Address));
                    Console.WriteLine("Login worked");
                    this.Hide();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.ShowDialog();
                }
            }
            else {
                Console.WriteLine("Registration failed");
            }
        }
        
        private void loginButton_Click(object sender, EventArgs e) {
            this.Hide();
            LoginWindow loginWin = new LoginWindow();
            loginWin.ShowDialog();
        }
    }
}
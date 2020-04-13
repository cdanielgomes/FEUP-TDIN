using System;
using System.Windows.Forms;
using Common;

namespace Client {
    public partial class LoginWindow : Form {
        
        
        public LoginWindow() {
            
            InitializeComponent();

        }

        private void registerButton_Click(object sender, EventArgs e) {
            this.Hide();
            RegisterWindow regWindow = new RegisterWindow();
            regWindow.ShowDialog();
           
            
        }

        private void loginButton_Click(object sender, EventArgs e) {
            try
            {
                ActiveUser newUser = ClientApp.GetServer().LoginUser(nicknameBox.Text, passwordBox.Text, ClientApp.GetInstance().Address);
                if (newUser != null)
                {
                    ClientApp.SetLoggedUser(newUser);
                    Console.WriteLine(@"Login worked");
                    this.Hide();
                    MainWindow mainWin = new MainWindow(newUser);
                    mainWin.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Login Failed: Be sure that you were registred with those credentials",
                        "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nicknameBox.Text = "";
                    passwordBox.Text = "";
                }
            }
            catch (Exception ex)
            {

                ClientApp.LaunchServerError(ex.Message);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
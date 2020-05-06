using System;
using System.Windows.Forms;
using Common;

namespace Client {
    public partial class RegisterWindow : Form {
        public RegisterWindow() {
            InitializeComponent();
        }
        
        private void registerButton_Click(object sender, EventArgs e) {

            try
            {
                if (passwordBox.Text != textBox2.Text)
                {
                    MessageBox.Show("Passwords do not match!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ClientApp.GetServer().RegisterUser(nicknameBox.Text, textBox2.Text, passwordBox.Text))
                {
                    Console.WriteLine(@"Registration worked");
                    ActiveUser newUser = ClientApp.GetServer().LoginUser(nicknameBox.Text, passwordBox.Text, ClientApp.GetInstance().Address);
                    if (newUser != null)
                    {
                        ClientApp.SetLoggedUser(newUser);
                        Console.WriteLine(@"Login worked");
                        this.Hide();
                        ClientApp.SetMainWindow(new MainWindow(newUser));
                        ClientApp.GetMainWindow().ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("8 characters minimum;" + Environment.NewLine + "Can not have white spaces, <, >, \' or \"", "Password Rules", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                ClientApp.LaunchServerError(ex.Message);
            }
        }
        
        private void loginButton_Click(object sender, EventArgs e) {
            this.Hide();
            LoginWindow loginWin = new LoginWindow();
            loginWin.ShowDialog();
        }

        private void nicknameLabel_Click(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
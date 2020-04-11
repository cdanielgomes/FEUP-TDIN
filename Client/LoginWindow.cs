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
            if (ClientApp.GetServer().LoginUser(nicknameBox.Text, passwordBox.Text, ClientApp.GetInstance().Address)) {
                ClientApp.SetLoggedUser(new ActiveUser(nicknameBox.Text, "Realname", ClientApp.GetInstance().Address));
                Console.WriteLine(@"Login worked");
                this.Hide();
                MainWindow mainWin = new MainWindow();
                mainWin.ShowDialog();
            }
        }
    }
}
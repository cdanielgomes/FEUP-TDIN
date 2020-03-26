using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Windows.Forms;
using Common;

namespace Client {
    public partial class LoginWindow : Form {
        
        private HashSet<ActiveUser> _friends;
        public LoginWindow() {
            
            InitializeComponent();

        }

        private void registerButton_Click(object sender, EventArgs e) {
            this.Close();
            Application.Run(new RegisterWindow());
        }

        private void loginButton_Click(object sender, EventArgs e) {
            if (ClientApp.GetServer().LoginUser(nicknameBox.Text, passwordBox.Text, "address")) {
                this.Close();
                Application.Run(new MainWindow());
            }
        }
    }
}
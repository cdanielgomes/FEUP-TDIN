using System;
using System.Windows.Forms;

namespace Client {
    public partial class RegisterWindow : Form {
        public RegisterWindow() {
            InitializeComponent();
        }
        
        private void registerButton_Click(object sender, EventArgs e) {
                        
            if (ClientApp.GetServer().RegisterUser("Carlos", "myAddress")) {
                Console.WriteLine("Registration worked");

                if (ClientApp.GetServer().LoginUser("Carlos", "myAddress", "address"))
                {
                  
                }
                // Launch everything else
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
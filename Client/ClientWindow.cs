using System;
using System.Runtime.Remoting;
using System.Windows.Forms;
using Common;

namespace Client {
    public partial class ClientWindow : Form {
        private IServer ChatServer;
        public ClientWindow() {
            InitializeServerConnection();
            InitializeComponent();
            
            /*if (ChatServer.RegisterUser("Random User")) {
                Console.WriteLine("Registration worked");
            }
            else {
                Console.WriteLine("Registration failed");
            }*/

        }

        private void InitializeServerConnection() {
            try {
                RemotingConfiguration.Configure("Client.exe.config", false);
                ChatServer = (IServer) RemoteNew.New(typeof(IServer));
            }
            catch (RemotingException e) {
                Console.WriteLine("Failed to connect to Server:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
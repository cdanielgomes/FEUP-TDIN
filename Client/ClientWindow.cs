using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Windows.Forms;
using Common;

namespace Client {
    public partial class ClientWindow : Form , IClientRem{
        private IServer ChatServer;
        private HashSet<ActiveUser> _friends;
        public ClientWindow() {
            InitializeServerConnection();
            InitializeComponent();
            
            if (ChatServer.RegisterUser("Carlos", "myAddress")) {
                Console.WriteLine("Registration worked");

              if (ChatServer.LoginUser("Carlos", "myAddress", "address"))
              {
                  
                }
                // Launch everything else
            }
            else {
                Console.WriteLine("Registration failed");
            }

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

        public void OnlineUsers(HashSet<ActiveUser> onlineUsers)
        {
            _friends = onlineUsers;
            Console.WriteLine("Receive stuff");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Windows.Forms;
using Common;

namespace Client {
    public class ClientApp {
        private static ClientApp _instance;
        private IServer _chatServer;
        private ActiveUser _loggedUser;
        private HashSet<ChatBox> _chat;

        private ClientApp() {
            InitializeServerConnection();
            _chat = new HashSet<ChatBox>();
        }

        public static void Init(string address)
        {
            _instance ??= new ClientApp {Address = address};
        }

        void InitializeServerConnection() {
            try {
                RemotingConfiguration.Configure("Client.exe.config", false);
                _chatServer = (IServer) RemoteNew.New(typeof(IServer));
            }
            catch (Exception e) {
                Console.WriteLine(@"Failed to connect to Server:");
                Console.WriteLine(e.Message);
                LaunchServerError(e.Message);
            }
        }

        public static ClientApp GetInstance() {
            return _instance;
        }

        public static IServer GetServer() {
            return _instance._chatServer;
        }
        
        public static ActiveUser GetLoggedUser() {
            return _instance._loggedUser;
        }

        public static void SetLoggedUser(ActiveUser user) {
            _instance._loggedUser = user;
        }

        public string Address { get; private set; }

        public HashSet<ChatBox> GetChats()
        {
            return _chat;
        }

        public static void LaunchServerError(string message)
        {
            MessageBox.Show(message,
                "Server does not respond",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
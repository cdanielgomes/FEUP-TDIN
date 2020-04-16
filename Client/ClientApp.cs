using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Windows.Forms;
using Common;

namespace Client {
    public class ClientApp {
        private static ClientApp _instance;
        private IServer _chatServer;
        private ActiveUser _loggedUser;
        private Dictionary<string, ChatBox> _chat;
        private HashSet<string> _pendingChats;
        private MainWindow mainWin;

        private ClientApp() {
            InitializeServerConnection();
            _pendingChats = new HashSet<string>();
            _chat = new Dictionary<string, ChatBox>();

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

        public Dictionary<string, ChatBox> GetChats()
        {
            return _chat;
        }

        public HashSet<string> GetPendingChats()
        {
            return _pendingChats;
        }
        public static void LaunchServerError(string message)
        {
            MessageBox.Show(message,
                "Server does not respond",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void SetMainWindow(MainWindow window) {
            _instance.mainWin = window;
        }

        public static MainWindow GetMainWindow()
        {
            return _instance.mainWin;
        }
    }
}
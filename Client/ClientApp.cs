using System;
using System.Runtime.Remoting;
using Common;

namespace Client {
    public class ClientApp {
        private static ClientApp _instance;
        private IServer _chatServer;
        private ActiveUser _loggedUser;

        private ClientApp() {
            InitializeServerConnection();
        }

        public static void Init() {
            if (_instance == null) {
                _instance = new ClientApp();
            }
        }

        void InitializeServerConnection() {
            try {
                RemotingConfiguration.Configure("Client.exe.config", false);
                _chatServer = (IServer) RemoteNew.New(typeof(IServer));
            }
            catch (RemotingException e) {
                Console.WriteLine("Failed to connect to Server:");
                Console.WriteLine(e.Message);
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
    }
}
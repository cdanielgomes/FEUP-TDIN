using System;
using System.Runtime.Remoting;
using Common;

namespace Client {
    internal static class Program {
        public static void Main(string[] args) {
            RemotingConfiguration.Configure("Client.exe.config", false);
            IServer server = (IServer) RemoteNew.New(typeof(IServer));
            if (server.RegisterUser("Random User")) {
                Console.WriteLine("Registration worked");
            }
            else {
                Console.WriteLine("Registration failed");
            }
        }
    }
}
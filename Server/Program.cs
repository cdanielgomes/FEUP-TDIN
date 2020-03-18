using System;
using System.Runtime.Remoting;

namespace Server
{
    internal static class Program {
        public static void Main(string[] args) {
            RemotingConfiguration.Configure("Server.exe.config", false);
            Console.WriteLine("Press Return to terminate.");
            Console.ReadLine();
        }
    }
}
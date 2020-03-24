using System;
using System.Collections.Generic;
using Common;

namespace Server
{
    public class Server: MarshalByRefObject, IServer
    {
        private Dictionary<String, String> usersMap; 
        public Server() {
            usersMap = new Dictionary<string, string>();
        }
        
        public override object InitializeLifetimeService() {
            
            return null;
        }

        public bool RegisterUser(String user) {
            if (usersMap.ContainsKey(user)) {
                Console.WriteLine("[Server]: User {0} is already registered", user);
                return false;
            }
            usersMap[user] = user;
            Console.WriteLine("[Server]: User {0} registered with success", user);
            return true;
        }
    }
}
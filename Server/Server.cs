using System;
using System.Collections.Generic;
using Common;

namespace Server
{
    public class Server : MarshalByRefObject, IServer
    {
        private Dictionary<string, RegisteredUser> _usersRegistered;
        private HashSet<ActiveUser> _onlineUsers;
        private IClientRem _clientRem;

        public Server()
        {
            _usersRegistered = new Dictionary<string, RegisteredUser>();
            _onlineUsers = new HashSet<ActiveUser>();
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public bool RegisterUser(string username, string password)
        {
            try
            {
                RegisteredUser newUser = new RegisteredUser(username, password);
                _usersRegistered.Add(username, newUser);

                Console.WriteLine("[Server]: User {0} registered with success", username);
                return true;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("[Server]: Username can not be null");
                return false;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("[Server]: User {0} is already registered", username);
                return false;
            }
        }

        public bool LoginUser(string username, string password, string address)
        {
            
            if (!_usersRegistered.ContainsKey(username) ||
                !_usersRegistered[username].CheckPassword((password))) return false;
            if (!_onlineUsers.Add(new ActiveUser(username, address))) return false;


          //  _clientRem.OnlineUsers(_onlineUsers);
            return true;
        }

        public bool LogoutUser(ActiveUser user)
        {
            return _onlineUsers.Remove(user);
        }
    }
}
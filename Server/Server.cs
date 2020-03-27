using System;
using System.Collections.Generic;
using Common;

namespace Server
{
    public class Server : MarshalByRefObject, IServer
    {
        private Dictionary<string, RegisteredUser> _usersRegistered;
        private HashSet<ActiveUser> _onlineUsers;
        public event NewActiveUser NewUserHandler; 
        public event LogoutActiveUser LogoutUserHandler; 

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
            ActiveUser newUser = new ActiveUser(username, address);
            if (!_onlineUsers.Add(newUser)) return false;


            NotifyActiveUser(newUser);
            return true;
        }

        public bool LogoutUser(ActiveUser user)
        {
            NotifyLogoutUser(user);
            return _onlineUsers.Remove(user);
        }

        public HashSet<ActiveUser> getOnlineUsers() {
            return _onlineUsers;
        }
        
        void NotifyActiveUser(ActiveUser user) {
            if (NewUserHandler != null) {
                Delegate[] invkList = NewUserHandler.GetInvocationList();

                foreach (NewActiveUser handler in invkList) {
                    try {
                        IAsyncResult ar = handler.BeginInvoke(user, null, null);
                        Console.WriteLine("User {0} Logged in",user.Username);
                    }
                    catch (Exception e) {
                        NewUserHandler -= handler;
                    }
                }
            }
        }
        
        void NotifyLogoutUser(ActiveUser user) {
            if (LogoutUserHandler != null) {
                Delegate[] invkList = LogoutUserHandler.GetInvocationList();

                foreach (LogoutActiveUser handler in invkList) {
                    try {
                        IAsyncResult ar = handler.BeginInvoke(user, null, null);
                        Console.WriteLine("User {0} Logged out",user.Username);
                    }
                    catch (Exception e) {
                        LogoutUserHandler -= handler;
                    }
                }
            }
        }
    }
}
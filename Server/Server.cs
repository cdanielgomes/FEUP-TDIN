using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Common;

namespace Server
{
    public class Server : MarshalByRefObject, IServer
    {
        const string FileName = "StoredUsers.bin";
        private Dictionary<string, RegisteredUser> _usersRegistered;
        private HashSet<ActiveUser> _onlineUsers;
        public event NewActiveUser NewUserHandler; 
        public event LogoutActiveUser LogoutUserHandler; 

        public Server()
        {
            LoadData();
            _onlineUsers = new HashSet<ActiveUser>();
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public bool RegisterUser(string username, string realname, string password)
        {
            try
            {
                RegisteredUser newUser = new RegisteredUser(username, realname, password);
                _usersRegistered.Add(username, newUser);
                SaveData();
                Console.WriteLine("[Server]: User {0} registered with success", username);
                return true;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(@"[Server]: Username can not be null");
                return false;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("[Server]: User {0} is already registered", username);
                return false;
            }
        }

        public ActiveUser LoginUser(string username, string password, string address)
        {
            
            if (!_usersRegistered.ContainsKey(username) ||
                !_usersRegistered[username].CheckPassword((password))) return null;

            RegisteredUser regUser = _usersRegistered[username];
            ActiveUser newUser = new ActiveUser(regUser.Username, regUser.RealName, address);

            if (!_onlineUsers.Contains(newUser)) {
                if (!_onlineUsers.Add(newUser)) return null;
                NotifyActiveUser(newUser);
            }
            
            return newUser;
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
                        Console.WriteLine("[Server]: User {0} Logged in",user.Username);
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
                        Console.WriteLine("[Server]: User {0} Logged out",user.Username);
                    }
                    catch (Exception e) {
                        LogoutUserHandler -= handler;
                    }
                }
            }
        }

        void LoadData()
        {
            if (File.Exists(FileName))
            {
                Console.WriteLine("[Server]: Reading saved file");
                Stream openFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                this._usersRegistered = (Dictionary<string, RegisteredUser>)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
            }
            else
            {
                _usersRegistered = new Dictionary<string, RegisteredUser>();
            }
        }

        void SaveData()
        {
            Stream SaveFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, this._usersRegistered);
            SaveFileStream.Close();
        }
    }
}
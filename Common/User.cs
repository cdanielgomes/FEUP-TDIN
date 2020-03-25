using System;

namespace Common
{
    [Serializable]
    public abstract class User
    {
        protected User(string username)
        {
            Username = username;
        }

        public string Username { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            User r = (User) obj;
            return Username.Equals(r.Username);
        }

        public override int GetHashCode()
        {
            return (Username != null ? Username.GetHashCode() : 0);
        }
    }

    [Serializable]
    public sealed class ActiveUser : User
    {
        public ActiveUser(string username, string address) : base(username)
        {
            Address = address;
        }

        public string Address { get; set; }
    }

    [Serializable]
    public sealed class RegisteredUser : User
    {
        public RegisteredUser(string username, string password) : base(username)
        {
            Password = password;
        }

        private string Password { get; set; }

        public bool CheckPassword(string pass)
        {
            return Password.Equals(pass);
        }
    }
}
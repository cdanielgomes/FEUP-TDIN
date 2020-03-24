using System;

namespace Common
{
    public abstract class User
    {
        protected User(string username, string address)
        {
            Username = username;
            Address = address;
        }
        
        public string Username { get; set; }

        public string Address { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) 
                return false;
            User r = (User)obj;
            return Username.Equals(r.Username) || Address.Equals(r.Address);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Username != null ? Username.GetHashCode() : 0) * 397) ^ (Address != null ? Address.GetHashCode() : 0);
            }
        }
    }


    public sealed class ActiveUsers : User
    {
        public ActiveUsers(string username, string address) : base(username, address)
        {}
    }

    public sealed class RegisteredUsers : User
    {
        public RegisteredUsers(string username, string address, string password) : base(username, address)
        {
            Password = password;
        }
        private string Password { get; set; }
    }
    
    
    
}
using System;
using System.Collections.Generic;

namespace Common
{
    public interface IServer {
        bool RegisterUser(string username, string realname, string password);

        ActiveUser LoginUser(string username, string password, string address);
        bool LogoutUser(ActiveUser user);

        HashSet<ActiveUser> getOnlineUsers();
        
        event NewActiveUser NewUserHandler; 
        event LogoutActiveUser LogoutUserHandler; 
    }
}
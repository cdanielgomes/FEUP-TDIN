using System.Collections.Generic;

namespace Common
{
    public interface IClientRem
    {
       void OnlineUsers( HashSet<ActiveUser> onlineUsers);
    }
}
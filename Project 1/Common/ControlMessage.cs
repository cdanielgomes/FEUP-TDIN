using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class ControlMessage : Message
    {

        public RemoteChat Chat;

        public ControlMessage(ActiveUser user, string chatName, RemoteChat chat, string id): base(user, "control", chatName, id)
        {
            Chat = chat;
        }
    }
}

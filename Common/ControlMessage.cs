using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ControlMessage : Message
    {

        public IChat Chat;

        public ControlMessage(ActiveUser user, string chatName, IChat chat): base(user, "invite", chatName)
        {
            Chat = chat;
        }
    }
}

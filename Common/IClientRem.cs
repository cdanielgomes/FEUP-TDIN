using System.Collections.Generic;

namespace Common
{
    public interface IClientRem
    {
        void SendMessage(Message message);

        void AcceptChat(ActiveUser user, string chatName);

        void RejectChat(ActiveUser user, string chatName);

        void Invite(Message m);

    }



}
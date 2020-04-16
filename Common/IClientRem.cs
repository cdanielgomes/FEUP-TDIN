using System.Collections.Generic;

namespace Common
{
    public interface IClientRem
    {

        void AcceptChat(ActiveUser user, string chatName, IChat chat);

        void RejectChat(ActiveUser user, string chatName);

        void Invite(InviteMessage m);

        void CloseChat(InviteMessage m);

    }



}
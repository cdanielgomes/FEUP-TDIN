using System.Collections.Generic;

namespace Common
{
    public interface IClientRem
    {

        void AcceptChat(ActiveUser user, string chatName, RemoteChat chat);

        void RejectChat(ActiveUser user, string chatName);

        void Invite(ControlMessage m);

        void CloseChat(ControlMessage m);

    }



}
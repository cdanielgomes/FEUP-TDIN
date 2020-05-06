using System;
using System.Collections.Generic;

namespace Common
{
    public interface IClientRem
    {

        void AcceptChat(ActiveUser user, string chatName, RemoteChat chat, string id);

        void RejectChat(ActiveUser user, string chatName, string id);

        void Invite(ControlMessage m);

    }



}
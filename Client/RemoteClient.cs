using System;
using System.Threading;
using System.Windows.Forms;
using Common;
using Message = Common.Message;

namespace Client
{
    public class RemoteClient : MarshalByRefObject, IClientRem
    {
        public void AcceptChat(ActiveUser user, string chatName, RemoteChat chat, string number)
        {
            if (ClientApp.GetInstance().GetChats().ContainsKey(number)) return;
            if (!ClientApp.GetInstance().GetPendingChats().Remove(number)) Console.WriteLine("Not found  element");
            ClientApp.GetMainWindow().StartChatBox(user, chatName, chat, number);
        }

        public void CloseChat(ControlMessage m)
        {
           ChatBox b =  ClientApp.GetInstance().GetChats()[m.ID];
            b.Close();
            ClientApp.GetInstance().GetChats().Remove(m.ID);
            b.Dispose();
        }

        public void Invite(ControlMessage m)
        {
            if (ClientApp.GetInstance().GetChats().ContainsKey(m.ID)) return;

            Console.WriteLine(@"Invitation received from " + m.Sender + " to join " + m.ID);
            ClientApp.GetMainWindow().LaunchInviteWindow(m);

        }

        public void RejectChat(ActiveUser user, string chatName, string id)
        {
            ClientApp.GetInstance().GetPendingChats().Remove(id);
            ClientApp.GetMainWindow().RejectedChat(user, chatName);
            //say that was reject 
        }
    }
}
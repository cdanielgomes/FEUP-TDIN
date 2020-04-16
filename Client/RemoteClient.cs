using System;
using System.Threading;
using System.Windows.Forms;
using Common;
using Message = Common.Message;

namespace Client
{
    public class RemoteClient : MarshalByRefObject, IClientRem
    {
        public void AcceptChat(ActiveUser user, string chatName, IChat chat)
        {
            ClientApp.GetInstance().GetPendingChats().Remove(chatName + user.Username);
            ClientApp.GetMainWindow().StartChatBox(user, chatName, chat);
        }

        public void CloseChat(ControlMessage m)
        {
           ChatBox b =  ClientApp.GetInstance().GetChats()[m.ChatName + m.SentUser.Username];
            b.Close();
            ClientApp.GetInstance().GetChats().Remove(m.ChatName + m.SentUser.Username);
            b.Dispose();
        }

        public void Invite(ControlMessage m)
        {
            Console.WriteLine(@"Invitation received from " + m.SentUser + " to join " + m.ChatName);
            ClientApp.GetMainWindow().LaunchInviteWindow(m);

        }

        public void RejectChat(ActiveUser user, string chatName)
        {
            ClientApp.GetInstance().GetPendingChats().Remove(chatName+user.Username);
            //say that was reject 
        }
    }
}
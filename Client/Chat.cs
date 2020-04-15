using System;
using System.Threading;
using System.Windows.Forms;
using Common;
using Message = Common.Message;

namespace Client
{
    public class Chat : MarshalByRefObject, IClientRem
    {
        public void AcceptChat(ActiveUser user, string chatName)
        {
            ClientApp.GetInstance().GetPendingChats().Remove(chatName + user.Username);
            ClientApp.GetMainWindow().StartChatBox(user, chatName);
        }

        public void CloseChat(Message m)
        {
           ChatBox b =  ClientApp.GetInstance().GetChats()[m.ChatName + m.SentUser.Username];
            b.Close();
            ClientApp.GetInstance().GetChats().Remove(m.ChatName + m.SentUser.Username);
            b.Dispose();
        }

        public void Invite(Message m)
        {
            ClientApp.GetMainWindow().LaunchChatWindow(m);

        }

        public void RejectChat(ActiveUser user, string chatName)
        {
            ClientApp.GetInstance().GetPendingChats().Remove(chatName+user.Username);
            //say that was reject 
        }

        public void SendMessage(Message message)
        {
            // Ver se existe
            // Caso nao exista Criar a ChatBox
            // Direcionar a Mensagem para a ChatBox correta
            var chats = ClientApp.GetInstance().GetChats();
            ChatBox chat = ClientApp.GetInstance().GetChats()[message.ChatName + message.SentUser.Username];

            chat.AddMessage(message);
            
        }

        
    }
}
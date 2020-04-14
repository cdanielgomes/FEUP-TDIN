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
            ClientApp.GetInstance().GetPendingChats().Remove(user.Username);
            ClientApp.GetInstance().GetPendingChats().Remove(user.Username);
            ClientApp.GetInstance().GetChats().Add(user.Username, new ChatBox(user, chatName));

            throw new NotImplementedException();
        }

        public void Invite(Message m)
        {

            Console.WriteLine(m.MessageSent);
            
               InviteWindow c = new InviteWindow(m.SentUser, m.ChatName);
                c.Show();

              Console.WriteLine("Shown");
        }

        public void RejectChat(ActiveUser user, string chatName)
        {
            ClientApp.GetInstance().GetPendingChats().Remove(user.Username);
            //say that was reject 
        }

        public void SendMessage(Message message)
        {
            // Ver se existe
            // Caso nao exista Criar a ChatBox
            // Direcionar a Mensagem para a ChatBox correta
            var chats = ClientApp.GetInstance().GetChats();
            ChatBox chat = ClientApp.GetInstance().GetChats()[message.SentUser.Username];
            Console.WriteLine(@"TRY TO SEND MESSAGE");

            chat.AddMessage(message);
            
        }

        
    }
}
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
            Console.WriteLine("Hey irmao, tentei receber o teu acceptance ");
            ClientApp.GetInstance().GetPendingChats().Remove(user.Username);
            Console.WriteLine("Try to start chat on " + ClientApp.GetLoggedUser().Username + " for an acceptance by " + user.Username);

            ClientApp.GetMainWindow().StartChatBox(user, chatName);
            Console.WriteLine("Open chat on  " + ClientApp.GetLoggedUser().Username + " with " + user.Username);
        }

        public void Invite(Message m)
        {
            ClientApp.GetMainWindow().LaunchChatWindow(m);

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
            Console.WriteLine(@"TRY TO RECEIVE MESSAGE");

            chat.AddMessage(message);
            
        }

        
    }
}
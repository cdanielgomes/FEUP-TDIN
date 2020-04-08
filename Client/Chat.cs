using System;
using System.Windows.Forms;
using Common;
using Message = Common.Message;

namespace Client
{
    public class Chat : MarshalByRefObject, IClientRem
    {
        
        public void SendMessage(Message message)
        {
            // Ver se existe
            // Caso nao exista Criar a ChatBox
            // Direcionar a Mensagem para a ChatBox correta
            var chats = ClientApp.GetInstance().GetChats();
            ChatBox chat = null;
            Console.WriteLine(@"TRY TO SEND MESSAGE");
            foreach (var chatBox in chats)
            {
                if (chatBox.GetFriend().Address == message.SentUser.Address)
                {
                    chat = chatBox;
                    break;
                }
            }

            if (chat != null)
            {
                Console.WriteLine(@"Already Created");

                chat.AddMessage(message);
            }
            else
            {
                Console.WriteLine(@"Start Chat");
                var c = new ChatBox(message.SentUser);
                ClientApp.GetInstance().GetChats().Add(c);
                Application.Run(c);
            }
        }
        
        
    }
}
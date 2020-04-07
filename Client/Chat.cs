using System;
using Common;

namespace Client
{
    public class Chat : MarshalByRefObject, IClientRem
    {
        
        public void SendMessage(Message message)
        {
            // Ver se existe
            // Caso nao exista Criar a ChatBox
            // Direcionar a Mensagem para a ChatBox correta
            ClientApp.GetInstance();
        }
        
        
    }
}
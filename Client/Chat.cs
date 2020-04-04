using System;
using Common;

namespace Client
{
    public class Chat : MarshalByRefObject, IClientRem
    {
        public string SayI(string message)
        {
                Console.WriteLine(message + " \n on the user:");
                Console.WriteLine(ClientApp.GetLoggedUser().Username);
                return "olá";
        }
        
        
    }
}
using System;

namespace Common
{
    public class Message
    {
        
        public Message(ActiveUser user, string message)
        {
            MessageSent = message;
            SentUser = user;
        }

        public ActiveUser SentUser { get; }

        public DateTime MessageDate { get; } = DateTime.Now;
        public string MessageSent { get; }
    }
}
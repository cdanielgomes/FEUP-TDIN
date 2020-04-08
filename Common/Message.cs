using System;

namespace Common
{
    [Serializable]
    public class Message : IComparable
    {
        
        public Message(ActiveUser user, string message)
        {
            MessageSent = message;
            SentUser = user;
        }

        public ActiveUser SentUser { get; }

        public DateTime MessageDate { get; } = DateTime.Now;
        public string MessageSent { get; }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
        
            Message otherMessage = obj as Message;
            if (otherMessage != null) 
                return MessageDate.CompareTo(otherMessage.MessageDate);
            throw new ArgumentException("Object is not a Temperature");
        }
    }
}
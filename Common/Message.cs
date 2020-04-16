using System;

namespace Common
{
    [Serializable]
    public class Message : IComparable
    {
        private Guid _id = Guid.NewGuid();
        
        public Message(ActiveUser user, string message, string chatName)
        {
            MessageSent = message;
            Sender = user;
            ChatName = chatName;
        }
        public ActiveUser Sender { get; }

        public DateTime MessageDate { get; } = DateTime.Now;
        public string MessageSent { get; }

        public string ChatName { get; }

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
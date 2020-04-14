using System;

namespace Common
{
    [Serializable]
    public class Message : IComparable
    {
        private bool _invite = false;
        public Message(ActiveUser user, string message)
        {
            MessageSent = message;
            SentUser = user;
            ChatName = "Random";    
        }
        public Message(ActiveUser user, string message, string chatName)
        {
            MessageSent = message;
            SentUser = user;
            ChatName = chatName;
        }
        public Message(ActiveUser user, string chatName, bool invite)
        {
            _invite = true;
            SentUser = user;
            ChatName = chatName;
        }
        public ActiveUser SentUser { get; }

        public DateTime MessageDate { get; } = DateTime.Now;
        public string MessageSent { get; }

        public string ChatName { get; }

        public bool IsInvite()
        {
            return _invite;
        }
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
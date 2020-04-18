using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class Message : IComparable
    {
        private Guid _id = Guid.NewGuid();

        private List<byte[]> files;
        private List<string> filesName;

        public Message(ActiveUser user, string message, string chatName)
        {
            MessageSent = message;
            Sender = user;
            ChatName = chatName;
        }
        public Message(ActiveUser user, string chatName)
        {
            Sender = user;
            ChatName = chatName;
            files = new List<byte[]>();
            filesName = new List<string>();
            MessageSent = "";

        }

        public ActiveUser Sender { get; }

        public DateTime MessageDate { get; } = DateTime.Now;
        public string MessageSent { get; set; }

        public string ChatName { get; }

        public void AddFile(byte[] file, string filename)
        {
            files.Add(file);
            filesName.Add(filename);
        }

        public List<byte[]> GetFiles()
        {
            return files;
        }

        public List<string> GetFilesName()
        {
            return filesName;
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
        
            Message otherMessage = obj as Message;
            if (otherMessage != null) 
                return MessageDate.CompareTo(otherMessage.MessageDate);
            throw new ArgumentException("Object is not a Temperature");
        }

        public void DeleteAll()
        {
            files.Clear();
            filesName.Clear();
        }
    }
}
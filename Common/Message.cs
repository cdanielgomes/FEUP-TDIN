using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class Message : IComparable
    {

        private List<byte[]> files = new List<byte[]>();
        private List<string> filesName = new List<string>();

        public Message(ActiveUser user, string message, string chatName, string chatId)
        {
            MessageSent = message;
            Sender = user;
            ChatName = chatName;
            ID = chatId;
        }
        public Message(ActiveUser user, string message, string chatName)
        {
            MessageSent = message;
            Sender = user;
            ChatName = chatName;
        }
        public ActiveUser Sender { get; }

        public DateTime MessageDate { get; } = DateTime.Now;
        public string MessageSent { get; set; }

        public string ChatName { get; }
        public string ID { get; }

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
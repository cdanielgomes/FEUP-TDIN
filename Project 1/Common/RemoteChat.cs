using System;
using System.Runtime.Serialization;
using Common;

namespace Common
{
    [Serializable]
    public class RemoteChat : MarshalByRefObject
    {
        public event NewMessage NewMessageHandler;
        public event CloseChat CloseChatHandler;

        public void WriteMessage(Message msg)
        {
            NewMessageHandler(msg);
        }

        public void CloseChat()
        {
            CloseChatHandler();
        }
    }
}

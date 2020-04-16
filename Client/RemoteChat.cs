using System;
using Common;

namespace Common
{
    class RemoteChat : MarshalByRefObject, IChat
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

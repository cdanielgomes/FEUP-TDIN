namespace Common
{
    public interface IChat
    {

        void WriteMessage(Message msg);

        void CloseChat();

        event NewMessage NewMessageHandler;
        event CloseChat CloseChatHandler;
    }
}

using System;

namespace Common {
    public delegate void NewActiveUser(ActiveUser user);
    public delegate void LogoutActiveUser(ActiveUser user);
    public delegate void NewMessage(Message msg);
    public delegate void CloseChat();

    public class NewUserEventRepeater : MarshalByRefObject {
        public event NewActiveUser Handler;

        public override object InitializeLifetimeService() {
            return null;
        }

        public void Repeater(ActiveUser user) {
            Handler?.Invoke(user);
        }
    }
    
    public class LogoutUserEventRepeater : MarshalByRefObject {
        public event LogoutActiveUser Handler;

        public override object InitializeLifetimeService() {
            return null;
        }

        public void Repeater(ActiveUser user) {
            Handler?.Invoke(user);
        }
    }

    public class MessageEventRepeater : MarshalByRefObject
    {
        public event NewMessage Handler;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(Message msg)
        {
            Handler?.Invoke(msg);
        }
    }

    public class CloseEventRepeater : MarshalByRefObject
    {
        public event CloseChat Handler;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater()
        {
            Handler?.Invoke();
        }
    }
}

using System;

namespace Common {
    public delegate void NewActiveUser(ActiveUser user);
    public delegate void LogoutActiveUser(ActiveUser user);

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
}

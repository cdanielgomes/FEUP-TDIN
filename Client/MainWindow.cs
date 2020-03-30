using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Windows.Forms;
using Common;

namespace Client {
    public partial class MainWindow : Form {
        private HashSet<ActiveUser> _onlineUsers;
        private NewUserEventRepeater _newUserRepeater;
        private LogoutUserEventRepeater _logoutUserRepeater;
        public MainWindow() {
            InitializeComponent();
            fetchOnlineUsers();
            SubscribeServerNotifications();
        }

        private void fetchOnlineUsers()
        {
            try {
                _onlineUsers = ClientApp.GetServer().getOnlineUsers();
                Console.WriteLine("Online users received");
            }
            catch (RemotingException e) {
                Console.WriteLine("Failed to fetch online users");
            }
            
        }

        public void AddActiveUser(ActiveUser user) {
            _onlineUsers.Add(user);
            ListViewItem lvItem = new ListViewItem(user.Username);
            userListView.Items.Add(lvItem);
            lvItem.ImageIndex = 0;
            //Application.DoEvents();
            Console.WriteLine("User {0} Logged in",user.Username);
        }

        public void RemoveActiveUser(ActiveUser user) {
            _onlineUsers.Remove(user);

            foreach (ListViewItem lvItem in this.userListView.Items) {
                if (lvItem.Text.Equals(user.Username)) {
                    lvItem.Remove();
                }
            }
            
            Console.WriteLine("User {0} Logged out",user.Username);
        }

        private void SubscribeServerNotifications() {
            _newUserRepeater = new NewUserEventRepeater();
            _logoutUserRepeater = new LogoutUserEventRepeater();
            _newUserRepeater.Handler += new NewActiveUser(AddActiveUser);
            _logoutUserRepeater.Handler += new LogoutActiveUser(RemoveActiveUser);
            ClientApp.GetServer().NewUserHandler += new NewActiveUser(_newUserRepeater.Repeater);
            ClientApp.GetServer().LogoutUserHandler += new LogoutActiveUser(_logoutUserRepeater.Repeater);
            Console.WriteLine("Client App subscribed with success");
        }
        
        private void UnsubscribeServerNotifications() {
            ClientApp.GetServer().NewUserHandler -= new NewActiveUser(_newUserRepeater.Repeater);
            ClientApp.GetServer().LogoutUserHandler -= new LogoutActiveUser(_logoutUserRepeater.Repeater);
            Console.WriteLine("Client App unsubscribed with success");
        }

        private void LogoutSession() {
            UnsubscribeServerNotifications();
            ClientApp.GetServer().LogoutUser(ClientApp.GetLoggedUser());
        }

        private void MainWindow_FormClosed(Object sender, FormClosedEventArgs e) {
            LogoutSession();
            Application.Exit();
        }

        private void ClientWindow_Load(object sender, EventArgs e) {
            foreach (ActiveUser user in _onlineUsers) {
                ListViewItem lvItem = new ListViewItem(user.Username);
                userListView.Items.Add(lvItem);
                lvItem.ImageIndex = 0;
            }
        }
    }
}
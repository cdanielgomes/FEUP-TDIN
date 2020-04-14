using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainWindow : Form
    {

        private HashSet<ActiveUser> _onlineUsers;
        private NewUserEventRepeater _newUserRepeater;
        private LogoutUserEventRepeater _logoutUserRepeater;

        public MainWindow(ActiveUser user)
        {
            InitializeComponent();
            label2.Text += user.Username;
            fetchOnlineUsers();
            SubscribeServerNotifications();
        }

        private void fetchOnlineUsers()
        {
            try
            {
                _onlineUsers = ClientApp.GetServer().getOnlineUsers();
                Console.WriteLine(@"Online users received");
            }
            catch (RemotingException e)
            {
                Console.WriteLine(@"Failed to fetch online users");
                Console.WriteLine(e);
            }
        }

        public void AddActiveUser(ActiveUser user)
        {
            Console.WriteLine("User {0} Logged in", user.Username);

            _onlineUsers.Add(user);
            ListViewItem lvItem = new ListViewItem(user.Username);
            listView1.Items.Add(lvItem);
            lvItem.ImageIndex = 0;
            //Application.DoEvents();
            Console.WriteLine("User {0} Logged in", user.Username);
        }

        public void RemoveActiveUser(ActiveUser user)
        {
            _onlineUsers.Remove(user);

            foreach (ListViewItem lvItem in this.listView1.Items)
            {
                if (lvItem.Text.Equals(user.Username))
                {
                    lvItem.Remove();
                }
            }

            Console.WriteLine("User {0} Logged out", user.Username);
        }

        private void SubscribeServerNotifications()
        {
            _newUserRepeater = new NewUserEventRepeater();
            _logoutUserRepeater = new LogoutUserEventRepeater();
            _newUserRepeater.Handler += new NewActiveUser(AddActiveUser);
            _logoutUserRepeater.Handler += new LogoutActiveUser(RemoveActiveUser);
            ClientApp.GetServer().NewUserHandler += new NewActiveUser(_newUserRepeater.Repeater);
            ClientApp.GetServer().LogoutUserHandler += new LogoutActiveUser(_logoutUserRepeater.Repeater);
            Console.WriteLine(@"Client App subscribed with success");
        }

        private void UnsubscribeServerNotifications()
        {
            ClientApp.GetServer().NewUserHandler -= new NewActiveUser(_newUserRepeater.Repeater);
            ClientApp.GetServer().LogoutUserHandler -= new LogoutActiveUser(_logoutUserRepeater.Repeater);
            Console.WriteLine(@"Client App unsubscribed with success");
        }

        private void LogoutSession()
        {
            UnsubscribeServerNotifications();
            ClientApp.GetServer().LogoutUser(ClientApp.GetLoggedUser());
        }

        private void CreateChat_Click(object sender, EventArgs e)
        {

            //List<ActiveUser> users = null;
            ActiveUser user = null;

            var selectedUsers = listView1.SelectedItems;
            for (var index = 0; index < selectedUsers.Count; index++)
            {
                foreach (var contact in _onlineUsers)
                {

                    if (contact.Username.Equals(selectedUsers[index].Text))
                    {
                        Console.WriteLine(@"Made contact with {0}", contact.Username);

                        // users.Add(contact);
                        user = contact;
                        break;
                    }
                }

                if (user != null)
                {

                    IClientRem friend = (IClientRem)RemotingServices.Connect(typeof(IClientRem), user.Address);

                    // 

                    ClientApp.GetInstance().GetPendingChats().Add(user.Username);

                   Thread a = new Thread(() => { friend.Invite(new Common.Message(ClientApp.GetLoggedUser(), "Random", true)); });
                    a.Start();

                   // var chat = new ChatBox(user, "Random");
                    // ClientApp.GetInstance().GetChats().Add(chat);
                    //chat.Show();
                }
                else
                {
                    Console.WriteLine(@"NULL");
                }
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {

            LogoutSession();
            Application.Exit();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            foreach (ActiveUser user in _onlineUsers)
            {
                ListViewItem lvItem = new ListViewItem(user.Username);
                listView1.Items.Add(lvItem);
                lvItem.ImageIndex = 0;
            }
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogoutSession();
            Application.Exit();
        }
    }
}

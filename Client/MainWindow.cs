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

            List<ActiveUser> users = new List<ActiveUser>();

            var selectedUsers = listView1.SelectedItems;
            bool groupChat = selectedUsers.Count > 1;

            for (var index = 0; index < selectedUsers.Count; index++)
            {
                foreach (var contact in _onlineUsers)
                {

                    if (contact.Username.Equals(selectedUsers[index].Text))
                    {
                        users.Add(contact);
          
                        break;
                    }
                }
            }

            if (users.Count == 1)
            {

                IClientRem friend = (IClientRem)RemotingServices.Connect(typeof(IClientRem), users[0].Address);
                ClientApp.GetInstance().GetPendingChats().Add(users[0].ToString());
                RemoteChat chat = new RemoteChat();

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine(@"Invitation sent to " + users[0].Username + " to join the mutual chat");
                    try
                    {
                        friend.Invite(new ControlMessage(ClientApp.GetLoggedUser(), ClientApp.GetLoggedUser().Username, chat));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invitation Failed");
                        Console.WriteLine(e);
                    }
                    Console.WriteLine("success");
                });
            }
            else if (users.Count > 1)
            {
                Form1 a = new Form1();
                a.ShowDialog();
                RemoteChat chat = new RemoteChat();

                foreach (var user in users)
                {
                    IClientRem friend = (IClientRem)RemotingServices.Connect(typeof(IClientRem), user.Address);
                  
                    Task.Factory.StartNew(() =>
                    {
                        Console.WriteLine(@"Invitation sent to " + user.Username + " to join " + a.GetText);
                        try
                        {
                            friend.Invite(new ControlMessage(ClientApp.GetLoggedUser(), a.GetText, chat));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invitation Failed");
                            Console.WriteLine(e);
                        }
                        Console.WriteLine("success");
                    });
                }
            }
            else
            {

                MessageBox.Show("Please select a user of a group of users",
                "No user was selected",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        public void LaunchInviteWindow(ControlMessage msg)
        {
            InviteWindow inviteWin = new InviteWindow(msg.Sender, msg.ChatName, msg.Chat);
            this.BeginInvoke((MethodInvoker)delegate () {
                inviteWin.Show();
            });
        }

        public void StartChatBox(ActiveUser user, string chatName, RemoteChat chat )
        {

            if (InvokeRequired)
            {

                BeginInvoke((MethodInvoker)delegate { StartChatBox(user, chatName, chat); });
            }
            else
            {
                ChatBox box = new ChatBox(user, chatName, chat);
                // TODO: username or 
                ClientApp.GetInstance().GetChats().Add(chatName+user.Username, box);
                box.Show();
            }
          
        }
    }
}

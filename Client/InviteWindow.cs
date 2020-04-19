using Common;
using System;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Client
{
    public partial class InviteWindow : Form
    {
        private IClientRem _iFriend;
        private RemoteChat _chat;
        private ActiveUser _user;
        private string _chatName;
        public InviteWindow(ActiveUser user, string chatName, RemoteChat chat)
        {
            try
            {
                Console.WriteLine(user.Username, user.Address);
                InitializeComponent();
                label1.Text += ClientApp.GetLoggedUser().Username;
                nameOfTheChat.Text += chatName;
                _iFriend = (IClientRem)RemotingServices.Connect(typeof(IClientRem), user.Address);
                _user = user;
                _chatName = chatName;
                _chat = chat;
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AcceptInvationButton_Click(object sender, System.EventArgs e)
        {
            string username = ClientApp.GetLoggedUser().Username;
            string chatName;

            ClientApp.GetInstance().GetPendingChats().Add(_chatName);

            if (_chatName != username)
            {
                chatName = _chatName;
            }
            else
            {
                chatName = username;
            }

            Task.Factory.StartNew(() => { _iFriend.AcceptChat(ClientApp.GetLoggedUser(), chatName, _chat); });
            
            Console.WriteLine(ClientApp.GetLoggedUser().Username + " tries to start a chat by accept request of " + _user.Username);

            ClientApp.GetMainWindow().StartChatBox(_user, chatName, _chat);
            Console.WriteLine(@"start chat on " + username + " with " + _user.Username);
            this.Close();
        }

        private void RejectInvitationButton_Click(object sender, System.EventArgs e)
        {
            _iFriend.RejectChat(ClientApp.GetLoggedUser(), _chatName);
            this.Close();
        }

        private void InviteWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate ()
            {
                _iFriend.RejectChat(ClientApp.GetLoggedUser(), _chatName);
                this.Close();
            });
        }
    }
}
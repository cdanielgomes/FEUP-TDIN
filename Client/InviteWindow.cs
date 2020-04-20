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
        private string _id;
        public InviteWindow(ActiveUser user, string chatName, RemoteChat chat, string id)
        {
            try
            {
                Console.WriteLine(user.Username, user.Address);
                InitializeComponent();
                label1.Text += user.Username;
                nameOfTheChat.Text = user.Username != chatName ?  "Chat Name " + chatName : "";
                _iFriend = (IClientRem)RemotingServices.Connect(typeof(IClientRem), user.Address);
                _user = user;
                _chatName = chatName;
                _chat = chat;
                _id = id;
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AcceptInvationButton_Click(object sender, System.EventArgs e)
        {
            string username = ClientApp.GetLoggedUser().Username;
            string chatName;

            if (_chatName != _user.Username)
            {
                chatName = _chatName;
            }
            else
            {
                chatName = username;
            }

            Task.Factory.StartNew(() => { _iFriend.AcceptChat(ClientApp.GetLoggedUser(), chatName, _chat, _id); });
            
            
            ClientApp.GetMainWindow().StartChatBox(_user, chatName, _chat, _id);
            this.Close();
        }

        private void RejectInvitationButton_Click(object sender, System.EventArgs e)
        {
            _iFriend.RejectChat(ClientApp.GetLoggedUser(), _chatName, _id);
            this.Close();
        }

        private void InviteWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate ()
            {
                _iFriend.RejectChat(ClientApp.GetLoggedUser(), _chatName, _id);
                this.Close();
            });
        }
    }
}
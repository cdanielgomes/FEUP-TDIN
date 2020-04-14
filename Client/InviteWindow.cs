using Common;
using System;
using System.Runtime.Remoting;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Client
{
    public partial class InviteWindow : Form
    {
        private IClientRem _iFriend;
        private ActiveUser _user;
        private string _chatName;
        public InviteWindow(ActiveUser user, string chatName)
        {
            InitializeComponent();
            label1.Text += user.Username;
            nameOfTheChat.Text += chatName;
            _iFriend = (IClientRem)RemotingServices.Connect(typeof(IClientRem), user.Address);
            _user = user;
            _chatName = chatName;
        }

        private void AcceptInvationButton_Click(object sender, System.EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                _iFriend.AcceptChat(ClientApp.GetLoggedUser(), _chatName);
            }));

            BeginInvoke(new Action(() => {

                ChatBox box = new ChatBox(_user, _chatName);
                // TODO: username or 
                ClientApp.GetInstance().GetChats().Add(_user.Username, box);
                box.Show();
                
            }));
        }

        private void RejectInvitationButton_Click(object sender, System.EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                _iFriend.RejectChat(ClientApp.GetLoggedUser(), _chatName);
            }));

        }

        private void InviteWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                _iFriend.RejectChat(ClientApp.GetLoggedUser(), _chatName);
            }));
        }
    }
}
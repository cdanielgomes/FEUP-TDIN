using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Windows.Forms;
using Common;
using Message = Common.Message;

namespace Client
{
    public partial class ChatBox : Form
    {
        private readonly IClientRem _iFriend;
        private ActiveUser _user;
        private SortedSet<Message> _messages;

        public ChatBox(ActiveUser user)
        {
            _user = user;
            _iFriend = (IClientRem) RemotingServices.Connect(typeof(IClientRem), user.Address);
            _messages = new SortedSet<Message>();
            InitializeComponent(user.Username);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            char[] charsToTrim = {' '};
            string test = inputMessage.Text.Trim(charsToTrim);
            if (test.Equals(""))
            {
                Message m = new Message(_user, inputMessage.Text);
                _iFriend.SendMessage(m);

            }
        }
    }
}
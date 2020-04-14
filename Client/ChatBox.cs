using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting;
using System.Threading;
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
        private string chatName;

        public ChatBox(ActiveUser user, string chatName)
        {
            _user = user;
            _iFriend = (IClientRem) RemotingServices.Connect(typeof(IClientRem), user.Address);
            _messages = new SortedSet<Message>();
            InitializeComponent();
            friendLabel.Text = user.Username;
            nameOfTheChat.Text = chatName;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            char[] charsToTrim = {' '};
            string test = inputMessage.Text.Trim(charsToTrim);
            if (!test.Equals(""))
            {
                Message m = new Message(ClientApp.GetLoggedUser(), inputMessage.Text);
                _messages.Add(m);
                InsertText(m);

                Thread t = new Thread(() =>
                {
                    _iFriend.SendMessage(m);
                }); 
                t.Start();

                inputMessage.Text = "";
            }
        }

        public ActiveUser GetFriend()
        {
            return _user;
        }

        public void AddMessage(Message message)
        {
            _messages.Add(message);
            InsertText(message);
        }

        private void InsertText(Message m)
        {
            string dateTime = m.MessageDate.ToString("g",  CultureInfo.CreateSpecificCulture("fr-FR"));
            string message = m.MessageSent;
            chatMessages.Text += message + '\t' + dateTime + '\n';
        }
        
    }
}
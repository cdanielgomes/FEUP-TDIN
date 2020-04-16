using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
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
        private string _chatName;
        private RemoteChat _chat;

        private MessageEventRepeater _messageRepeater;
        private CloseEventRepeater _closeRepeater;
        
        public ChatBox(ActiveUser user, string chatName, RemoteChat chat)
        {
            _user = user;
            _messages = new SortedSet<Message>();
            InitializeComponent();
            friendLabel.Text = user.Username;
            nameOfTheChat.Text = chatName;
            _chatName = chatName;
            _chat = chat;
            SubscribeChat();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            char[] charsToTrim = {' '};
            string test = inputMessage.Text.Trim(charsToTrim);
            if (!test.Equals(""))
            {
                Message m = new Message(ClientApp.GetLoggedUser(), inputMessage.Text, _chatName);
                _messages.Add(m);

                Thread t = new Thread(() =>
                {
                    _chat.WriteMessage(m);
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
            string message = "";

            if (m.SentUser.Username == ClientApp.GetLoggedUser().Username)
            {
                chatMessages.SelectionAlignment = HorizontalAlignment.Right;
                message += m.SentUser.Username + "(Me)";
            }
            else
            {
                chatMessages.SelectionAlignment = HorizontalAlignment.Left;
                message += m.SentUser.Username;
            }

            message += " - " + dateTime + Environment.NewLine + m.MessageSent + Environment.NewLine + Environment.NewLine;

            chatMessages.SelectionFont = new Font(chatMessages.Font, FontStyle.Regular);
            chatMessages.AppendText(message);
        }

        private void CloseChat()
        {
            this.Close();
        }

        private void ChatBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _iFriend.CloseChat(new ControlMessage(ClientApp.GetLoggedUser(), _chatName, _chat));
            }
            catch (Exception ex)
            {

            }
            UnsubscibeChat();
        }

        private void SubscribeChat()
        {
            _messageRepeater = new MessageEventRepeater();
            _closeRepeater = new CloseEventRepeater();
            _messageRepeater.Handler += new NewMessage(InsertText);
            _closeRepeater.Handler += new CloseChat(CloseChat);
            _chat.NewMessageHandler += new NewMessage(_messageRepeater.Repeater);
            _chat.CloseChatHandler += new CloseChat(_closeRepeater.Repeater);
            Console.WriteLine(@"Client App subscribed to the chat with success");
        }

        private void UnsubscibeChat()
        {
            _chat.NewMessageHandler -= new NewMessage(_messageRepeater.Repeater);
            _chat.CloseChatHandler -= new CloseChat(_closeRepeater.Repeater);
            Console.WriteLine(@"Client App unsubscribed to the chat with success");
        }
    }
}
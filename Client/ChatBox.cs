using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Syroot.Windows.IO;
using Message = Common.Message;

namespace Client
{
    public partial class ChatBox : Form
    {
        private ActiveUser _user;
        private SortedSet<Message> _messages;
        private List<byte[]> files;
        private string _chatName;
        private RemoteChat _chat;

        public string ID { get; }

        private MessageEventRepeater _messageRepeater;
        private CloseEventRepeater _closeRepeater;
       
        
        public ChatBox(ActiveUser user, string chatName, RemoteChat chat, string id)
        {
            _user = user;
            _messages = new SortedSet<Message>();
            files = new List<byte[]>();
            InitializeComponent();
            nameOfTheChat.Text = chatName;
            _chatName = chatName;
            _chat = chat;
            ID = id;
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

        private void CloseChat()
        {
            this.Close();
        }

        private void ChatBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logout();
        }

        private void SubscribeChat()
        {
            _messageRepeater = new MessageEventRepeater();
            _closeRepeater = new CloseEventRepeater();
            _messageRepeater.Handler += new NewMessage(InsertMessage);
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

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                try
                {
                    string filename = dialog.FileName;
                    byte[] file = File.ReadAllBytes(filename);
                    var ans = MessageBox.Show("Add the file " + Path.GetFileName(filename) + " to be sent", "Confirm", MessageBoxButtons.YesNo);
                    if (ans == DialogResult.Yes)
                    {
                        Task t = Task.Factory.StartNew(() => {
                            Message message = new Message(ClientApp.GetLoggedUser(), "Sent file " + filename , _chatName);
                            message.AddFile(file, Path.GetFileName(filename));
                            _chat.WriteMessage(message);
                        });
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Unable to upload the file", "Error", MessageBoxButtons.OK);
                    Console.WriteLine(error.Message);
                }
            }
        }

        void InsertMessage(Message m)
        {
            string dateTime = m.MessageDate.ToString("g", CultureInfo.CreateSpecificCulture("fr-FR"));
            

            if (m.GetFiles().Count > 0)
            {
                for (int i = 0; i < m.GetFiles().Count; i++)
                {
                    ListViewItem s = new ListViewItem();
                    s.Text = m.GetFilesName()[i];
                    files.Add(m.GetFiles()[i]);
                    filesShared.Items.Add(s);
                }
            }
            string message = "";
            if (m.MessageSent == null) return;

            if (m.Sender.Username == ClientApp.GetLoggedUser().Username)
            {
                chatMessages.SelectionAlignment = HorizontalAlignment.Right;
                message += m.Sender.Username + " (Me)";
            }
            else
            {
                chatMessages.SelectionAlignment = HorizontalAlignment.Left;
                message += m.Sender.Username;
            }

            message += " - " + dateTime + Environment.NewLine + m.MessageSent + Environment.NewLine + Environment.NewLine;

            chatMessages.SelectionFont = new Font(chatMessages.Font, FontStyle.Regular);
            chatMessages.AppendText(message);
        }

        private void SaveFile(byte[] file, string fileName)
        {
            string downloadsPath = KnownFolders.Downloads.Path;
            string name = fileName;

            while(File.Exists(downloadsPath + "/" + name))
            {
                name = name.Insert(name.IndexOf('.'), "_new");
            }

            File.WriteAllBytes(downloadsPath + "/" + name,file);
        }

        private void filesShared_Click(object sender, EventArgs e)
        {
            try { 
            byte[] file = files[filesShared.SelectedItems[0].Index];
            SaveFile(file, filesShared.SelectedItems[0].Text);
            }
            catch (Exception error)
            {
                MessageBox.Show("Download Failed: " + error.Message, "Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Logout()
        {
            ClientApp.GetInstance().GetChats().Remove(this.ID);
            UnsubscibeChat();
            try
            {
                _chat.CloseChat();
            }
            catch (Exception ex)
            {

            }

        }

    }
}
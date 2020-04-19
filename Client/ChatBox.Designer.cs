using System.ComponentModel;

namespace Client
{
    partial class ChatBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatBox));
            this.inputMessage = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.friendLabel = new System.Windows.Forms.Label();
            this.nameOfTheChat = new System.Windows.Forms.Label();
            this.chatMessages = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.filesShared = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // inputMessage
            // 
            this.inputMessage.Location = new System.Drawing.Point(46, 362);
            this.inputMessage.Margin = new System.Windows.Forms.Padding(2);
            this.inputMessage.Multiline = true;
            this.inputMessage.Name = "inputMessage";
            this.inputMessage.Size = new System.Drawing.Size(500, 43);
            this.inputMessage.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(656, 362);
            this.sendButton.Margin = new System.Windows.Forms.Padding(2);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(61, 41);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // friendLabel
            // 
            this.friendLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.friendLabel.Location = new System.Drawing.Point(42, 71);
            this.friendLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.friendLabel.Name = "friendLabel";
            this.friendLabel.Size = new System.Drawing.Size(380, 37);
            this.friendLabel.TabIndex = 3;
            this.friendLabel.Text = "Chat with your friends here:";
            // 
            // nameOfTheChat
            // 
            this.nameOfTheChat.AutoSize = true;
            this.nameOfTheChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameOfTheChat.Location = new System.Drawing.Point(254, 31);
            this.nameOfTheChat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameOfTheChat.Name = "nameOfTheChat";
            this.nameOfTheChat.Size = new System.Drawing.Size(104, 24);
            this.nameOfTheChat.TabIndex = 4;
            this.nameOfTheChat.Text = "Chat Name";
            // 
            // chatMessages
            // 
            this.chatMessages.Location = new System.Drawing.Point(46, 102);
            this.chatMessages.Margin = new System.Windows.Forms.Padding(2);
            this.chatMessages.Name = "chatMessages";
            this.chatMessages.ReadOnly = true;
            this.chatMessages.Size = new System.Drawing.Size(500, 236);
            this.chatMessages.TabIndex = 5;
            this.chatMessages.TabStop = false;
            this.chatMessages.Text = "";
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(584, 362);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 42);
            this.button2.TabIndex = 7;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(580, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Files Shared";
            // 
            // filesShared
            // 
            this.filesShared.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.filesShared.HideSelection = false;
            this.filesShared.Location = new System.Drawing.Point(584, 102);
            this.filesShared.Margin = new System.Windows.Forms.Padding(2);
            this.filesShared.MultiSelect = false;
            this.filesShared.Name = "filesShared";
            this.filesShared.Size = new System.Drawing.Size(133, 236);
            this.filesShared.TabIndex = 9;
            this.filesShared.UseCompatibleStateImageBehavior = false;
            this.filesShared.View = System.Windows.Forms.View.List;
            this.filesShared.Click += new System.EventHandler(this.filesShared_Click);
            // 
            // ChatBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(753, 424);
            this.Controls.Add(this.filesShared);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chatMessages);
            this.Controls.Add(this.nameOfTheChat);
            this.Controls.Add(this.friendLabel);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.inputMessage);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ChatBox";
            this.Text = "ChatBox";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatBox_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label friendLabel;
        private System.Windows.Forms.TextBox inputMessage;
        private System.Windows.Forms.Button sendButton;

        #endregion

        private System.Windows.Forms.Label nameOfTheChat;
        private System.Windows.Forms.RichTextBox chatMessages;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView filesShared;
    }
}
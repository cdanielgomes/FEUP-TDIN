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
        private void InitializeComponent(string friend)
        {
            this.inputMessage = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.chatMessages = new System.Windows.Forms.TextBox();
            this.friendLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputMessage
            // 
            this.inputMessage.Location = new System.Drawing.Point(93, 558);
            this.inputMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputMessage.Multiline = true;
            this.inputMessage.Name = "inputMessage";
            this.inputMessage.Size = new System.Drawing.Size(684, 64);
            this.inputMessage.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(838, 558);
            this.sendButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(81, 64);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // chatMessages
            // 
            this.chatMessages.Location = new System.Drawing.Point(93, 102);
            this.chatMessages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chatMessages.Multiline = true;
            this.chatMessages.Name = "chatMessages";
            this.chatMessages.Size = new System.Drawing.Size(826, 396);
            this.chatMessages.TabIndex = 2;
            // 
            // friendLabel
            // 
            this.friendLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.friendLabel.Location = new System.Drawing.Point(87, 42);
            this.friendLabel.Name = "friendLabel";
            this.friendLabel.Size = new System.Drawing.Size(506, 58);
            this.friendLabel.TabIndex = 3;
            this.friendLabel.Text = friend;
            // 
            // ChatBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 652);
            this.Controls.Add(this.friendLabel);
            this.Controls.Add(this.chatMessages);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.inputMessage);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChatBox";
            this.Text = "ChatBox";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox chatMessages;
        private System.Windows.Forms.Label friendLabel;
        private System.Windows.Forms.TextBox inputMessage;
        private System.Windows.Forms.Button sendButton;

        #endregion
    }
}
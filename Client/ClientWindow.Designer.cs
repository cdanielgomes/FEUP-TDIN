using System.ComponentModel;
using System.Windows.Forms;

namespace Client {
    partial class ClientWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.nicknameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.nicknameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

         
            // 
            // nicknameLabel
            // 
            this.nicknameLabel.AutoSize = true;
            this.nicknameLabel.Location = new System.Drawing.Point(154, 202);
            this.nicknameLabel.Name = "nicknameLabel";
            this.nicknameLabel.Size = new System.Drawing.Size(38, 13);
            this.nicknameLabel.TabIndex = 0;
            this.nicknameLabel.Text = "Name:";
            
            //
            // nicknameBox
            //
            this.nicknameBox.Location = new System.Drawing.Point(198, 199);
            this.nicknameBox.Name = "nicknameBox";
            this.nicknameBox.Size = new System.Drawing.Size(270, 20);
            this.nicknameBox.TabIndex = 3;
            
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(154, 251);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(38, 13);
            this.passwordLabel.TabIndex = 0;
            this.passwordLabel.Text = "Pass:";
            
            //
            // passwordBox
            //
            this.passwordBox.Location = new System.Drawing.Point(198, 249);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(270, 20);
            this.passwordBox.TabIndex = 3;

            // 
            // loginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(300, 395);
            this.LoginButton.Name = "loginButton";
            this.LoginButton.Size = new System.Drawing.Size(125, 27);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            //this.addItemButton.Click += new System.EventHandler(this.addItemButton_Click);
            //this.changeCommentButton.Click += new System.EventHandler(this.changeCommentButton_Click);
            
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(300, 446);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(119, 32);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            //this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            
            // 
            // ClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 508);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.nicknameLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.nicknameBox);
            this.Controls.Add(this.passwordBox);
            this.Name = "ClientWindow";
            this.Text = "ChatMessenger";
            //this.Load += new System.EventHandler(this.ClientWindow_Load);
            //this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nicknameLabel;
        private System.Windows.Forms.TextBox nicknameBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button closeButton;
    }
}
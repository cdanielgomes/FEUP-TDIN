using System.ComponentModel;

namespace Client {
    partial class RegisterWindow {
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

        private System.Windows.Forms.Label nicknameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox nicknameBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button loginButton;

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
            this.registerButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            // 
            // nicknameLabel
            // 
            this.nicknameLabel.AutoSize = true;
            this.nicknameLabel.Image = null;
            this.nicknameLabel.Text = "Name:";
            this.nicknameLabel.Location = new System.Drawing.Point(28, 26);
            this.nicknameLabel.Name = "nicknameLabel";
            this.nicknameLabel.Size = new System.Drawing.Size(40, 14);
            this.nicknameLabel.TabIndex = 0;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Image = null;
            this.passwordLabel.Text = "Pass:";
            this.passwordLabel.Location = new System.Drawing.Point(28, 78);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(33, 14);
            this.passwordLabel.TabIndex = 0;
            // 
            // nicknameBox
            // 
            this.nicknameBox.Text = "";
            this.nicknameBox.BackColor = System.Drawing.SystemColors.Window;
            this.nicknameBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.nicknameBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nicknameBox.Location = new System.Drawing.Point(79, 22);
            this.nicknameBox.Name = "nicknameBox";
            this.nicknameBox.Size = new System.Drawing.Size(314, 20);
            this.nicknameBox.TabIndex = 3;
            // 
            // passwordBox
            // 
            this.passwordBox.Text = "";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.BackColor = System.Drawing.SystemColors.Window;
            this.passwordBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.passwordBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passwordBox.Location = new System.Drawing.Point(79, 76);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(314, 20);
            this.passwordBox.TabIndex = 3;
            // 
            // RegisterButton
            // 
            this.registerButton.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Location = new System.Drawing.Point(142, 113);
            this.registerButton.Name = "RegisterButton";
            this.registerButton.Size = new System.Drawing.Size(146, 29);
            this.registerButton.TabIndex = 4;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Location = new System.Drawing.Point(144, 192);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(145, 34);
            this.closeButton.TabIndex = 6;
            // 
            // loginButton
            // 
            this.loginButton.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Location = new System.Drawing.Point(144, 152);
            this.loginButton.Name = "button1";
            this.loginButton.Size = new System.Drawing.Size(146, 29);
            this.loginButton.TabIndex = 7;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // ClientWindow
            // 
            this.ClientSize = new System.Drawing.Size(450, 241);
            this.Text = "ChatMessenger";
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.nicknameLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.nicknameBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.loginButton);
            this.Name = "ClientWindow";
            //this.Load += new System.EventHandler(this.ClientWindow_Load);
        }

        #endregion
    }
}
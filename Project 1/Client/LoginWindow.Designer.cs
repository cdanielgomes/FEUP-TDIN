//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client
{
    
    #region Windows Form Designer generated code
    public partial class LoginWindow
    {
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nicknameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.nicknameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nicknameLabel
            // 
            this.nicknameLabel.AutoSize = true;
            this.nicknameLabel.Location = new System.Drawing.Point(28, 83);
            this.nicknameLabel.Name = "nicknameLabel";
            this.nicknameLabel.Size = new System.Drawing.Size(74, 17);
            this.nicknameLabel.TabIndex = 0;
            this.nicknameLabel.Text = "Nickname:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(28, 122);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(73, 17);
            this.passwordLabel.TabIndex = 0;
            this.passwordLabel.Text = "Password:";
            // 
            // nicknameBox
            // 
            this.nicknameBox.BackColor = System.Drawing.SystemColors.Window;
            this.nicknameBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nicknameBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.nicknameBox.Location = new System.Drawing.Point(90, 79);
            this.nicknameBox.Name = "nicknameBox";
            this.nicknameBox.Size = new System.Drawing.Size(314, 22);
            this.nicknameBox.TabIndex = 3;
            // 
            // passwordBox
            // 
            this.passwordBox.BackColor = System.Drawing.SystemColors.Window;
            this.passwordBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passwordBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.passwordBox.Location = new System.Drawing.Point(90, 120);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(314, 22);
            this.passwordBox.TabIndex = 3;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(143, 167);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(146, 29);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(144, 246);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(145, 34);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(144, 206);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(146, 29);
            this.registerButton.TabIndex = 7;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(528, 36);
            this.label1.TabIndex = 8;
            this.label1.Text = "Login to start chatting with your friends";
            // 
            // LoginWindow
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(450, 296);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.nicknameLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.nicknameBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.registerButton);
            this.Name = "LoginWindow";
            this.Text = "ChatMessenger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TextBox nicknameBox;
        private System.Windows.Forms.Label nicknameLabel;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label label1;
    }
    #endregion
}
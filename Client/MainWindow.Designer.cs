using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Client
{
    
    #region Windows Form Designer generated code
    public partial class MainWindow
    {
        private void InitializeComponent(string name)
        {
            this.userListView = new System.Windows.Forms.ListView();
            this.userIcon = new System.Windows.Forms.ColumnHeader("");
            this.userNickname = new System.Windows.Forms.ColumnHeader("");
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userListView
            // 
            this.userListView.FullRowSelect = true;
            this.userListView.BackColor = System.Drawing.SystemColors.Window;
            this.userListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.userListView.Text = "userListView";
            this.userListView.UseCompatibleStateImageBehavior = false;
            this.userListView.Location = new System.Drawing.Point(72, 40);
            this.userListView.Name = "userListView";
            this.userListView.Size = new System.Drawing.Size(253, 295);
            this.userListView.TabIndex = 0;
            this.userListView.View = System.Windows.Forms.View.List;
            // 
            // button1
            // 
            this.button1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.button1.Text = "Create Chat";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Location = new System.Drawing.Point(144, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 33);
            this.button1.TabIndex = 1;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Location = new System.Drawing.Point(144, 400);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 33);
            this.button2.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 450);
            this.Text = name;
            this.Controls.Add(this.userListView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Load += new System.EventHandler(this.ClientWindow_Load);
            this.Name = "MainWindow";
            this.FormClosed += new FormClosedEventHandler(MainWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.ListView userListView;
        private System.Windows.Forms.ColumnHeader userIcon;
        private System.Windows.Forms.ColumnHeader userNickname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
    #endregion
}

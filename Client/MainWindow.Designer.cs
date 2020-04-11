using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Client
{
    
    #region Windows Form Designer generated code
    public partial class MainWindow
    {
        private void InitializeComponent(string name)
        {
            this.listView1 = new System.Windows.Forms.ListView();
            ImageList imageList = new ImageList();
            imageList.Images.Add("icon", Properties.Resources.userIcon);
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(45, 43);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(209, 171);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SmallImageList = imageList;
            // 
            // MainWindow
            // 
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.listView1);
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);

        }

        private ListView listView1;
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            GetText = ""; 
        }
        public string GetText { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!GetText.Equals("")) {
                this.Close();
            }
            else { 
                var x = MessageBox.Show("Give the Chat a Name", "Name", MessageBoxButtons.OK);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GetText == "") {
               var x = MessageBox.Show("Give the Chat a Name", "Name", MessageBoxButtons.OK);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetText = textBox1.Text.Trim();
        }
    }
}

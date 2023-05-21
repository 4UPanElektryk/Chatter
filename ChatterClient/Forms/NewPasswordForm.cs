using Chatter.Client.Transfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatter.Client
{
    public partial class NewPasswordForm : Form
    {
        string TOKEN;
        public NewPasswordForm(MainForm mainForm)
        {
            InitializeComponent();
            TOKEN = mainForm.TOKEN;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Passwords don't match");
                return;
            }
            if (textBox1.Text.Length < 8)
            {
                MessageBox.Show("Password needs at least 8 characters");
                return;
            }
            SimpleTCP.Message message = Program._Client.WriteLineAndGetReply("setpswd\n"+TOKEN+"\n"+JsonConvert.SerializeObject(textBox1.Text),TimeSpan.FromSeconds(5));
            if (message == null)
            {
                MessageBox.Show("Connection Error");
                return;
            }
            if (message.MessageString == "OK")
            {
                this.Close();
            }
            if (message.MessageString.StartsWith("E"))
            {
                MessageBox.Show("Error: " + message.MessageString);
                return;
            }
        }
    }
}

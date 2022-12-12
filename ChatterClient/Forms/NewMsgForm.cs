using Chatter.Client.Transfer;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace Chatter.Client
{
    public partial class NewMsgForm : Form
    {
        string Token;
        public NewMsgForm(MainForm main)
        {
            Token = main.TOKEN;
            InitializeComponent();
        }
        private void sendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrAddMsg tr = new TrAddMsg
            {
                Lines = textBox1.Lines,
            };
            SimpleTCP.Message response = Program._Client.WriteLineAndGetReply("addmsg\n" + Token + "\n" + JsonConvert.SerializeObject(tr), TimeSpan.FromSeconds(20));
            if (response.MessageString == "OK")
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Message sending error\n" + response.MessageString);
            }
        }
    }
}

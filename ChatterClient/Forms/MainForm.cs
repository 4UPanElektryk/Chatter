using Chatter.Client.Transfer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class MainForm : Form
    {
        public string TOKEN;
        public bool connected;
        public MainForm()
        {
            InitializeComponent();
            connected = false;
            TOKEN = string.Empty;
        }

        public void LogedIn()
        {
            newPostToolStripMenuItem.Enabled = true;
            copyUserTokenToClipboardToolStripMenuItem.Enabled = true;
            loginToolStripMenuItem.Text = "Logout";
            if (GetMsgs() != null)
            {
                richTextBox1 = MsgRenderer.RederMsgs(richTextBox1, GetMsgs());
            }
        }
        public List<UMsg> GetMsgs()
        {
            SimpleTCP.Message reply = Program._Client.WriteLineAndGetReply("getmsgs\n"+ TOKEN + "\n", TimeSpan.FromSeconds(20));
            try
            {
                return JsonConvert.DeserializeObject<TrGetMsgs>(reply.MessageString).msgs;
            }
            catch
            {
                return null;
            }
        }
        public void Logout()
        {
            newPostToolStripMenuItem.Enabled = false;
            copyUserTokenToClipboardToolStripMenuItem.Enabled = false;
            loginToolStripMenuItem.Text = "Login";
            TOKEN= string.Empty;
        }

        private void copyUserTokenToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TOKEN);
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                Connect_Form form = new Connect_Form();
                form.ShowDialog();
                if (form.failed)
                {
                    return;
                }
                connected = true;
            }

            if (TOKEN == string.Empty)
            {
                LoginForm form = new LoginForm();
                form.ShowDialog();
                if (form.Token != string.Empty)
                {
                    TOKEN = form.Token;
                    LogedIn();
                }
            }
            else
            {
                Logout();
            }
            refreshToolStripMenuItem.Enabled = connected;
        }

        private void newPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NewMsgForm(this).Show();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1 = MsgRenderer.RederMsgs(richTextBox1, GetMsgs());
        }
    }
}

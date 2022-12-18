using Chatter.Client.Transfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Chatter.Client
{
    public partial class MainForm : Form
    {
        public string TOKEN;
        public bool connected;
        public DateTime Time;
        public MainForm()
        {
            InitializeComponent();
            connected = false;
            TOKEN = string.Empty;
            Time = DateTime.UtcNow;
        }
        public void LogedIn()
        {
            newPostToolStripMenuItem.Enabled = true;
            copyUserTokenToClipboardToolStripMenuItem.Enabled = true;
            changeColorToolStripMenuItem.Enabled = true;
            changePasswordToolStripMenuItem.Enabled = true;
            loginToolStripMenuItem.Text = "Logout";
            richTextBox1 = MsgRenderer.RederMsgs(richTextBox1, GetMsgs());
        }
        public void Logout()
        {
            newPostToolStripMenuItem.Enabled = false;
            copyUserTokenToClipboardToolStripMenuItem.Enabled = false;
            changeColorToolStripMenuItem.Enabled = false;
            changePasswordToolStripMenuItem.Enabled = false;
            loginToolStripMenuItem.Text = "Login";
            TOKEN = string.Empty;
        }
        public List<UMsg> GetMsgs()
        {
            SimpleTCP.Message reply = Program._Client.WriteLineAndGetReply("getmsgs\n" + TOKEN + "\n", TimeSpan.FromSeconds(5));
            try
            {
                return JsonConvert.DeserializeObject<TrGetMsgs>(reply.MessageString).msgs;
            }
            catch
            {
                return null;
            }
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
            refreshToolStripMenuItem.Enabled = connected;
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
        }

        private void newPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NewMsgForm(this).Show();
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1 = MsgRenderer.RederMsgs(richTextBox1, GetMsgs());
            Time = DateTime.UtcNow;
        }
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            richTextBox1.Size = new Size(this.Size.Width - 16, this.Size.Height - 64);
        }

        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TrSetColor tr = new TrSetColor()
                {
                    color = colorDialog1.Color
                };
                SimpleTCP.Message message = Program._Client.WriteLineAndGetReply("setcolor\n" + TOKEN + "\n" + JsonConvert.SerializeObject(tr),TimeSpan.FromSeconds(10));
                if (message == null)
                {
                    return;
                }
                if (message.MessageString == "OK")
                {
                    return;
                }
                MessageBox.Show(message.MessageString, "Error");
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPasswordForm form = new NewPasswordForm(this);
            form.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!connected)
            {
                return;
            }
            TrRefresh tr = new TrRefresh
            {
                time = Time
            };
            SimpleTCP.Message message = Program._Client.WriteLineAndGetReply("refresh\n"+TOKEN+"\n"+JsonConvert.SerializeObject(tr), TimeSpan.FromSeconds(2));
            if (message == null)
            {
                richTextBox1.Text = "Connection Error";
                return;
            }
            if (message.MessageString == "NOCHANGES")
            {
                return;
            }
            if (message.MessageString == "CHANGES")
            {
                richTextBox1 = MsgRenderer.RederMsgs(richTextBox1, GetMsgs());
                Time = DateTime.UtcNow;
            }
        }
    }
}

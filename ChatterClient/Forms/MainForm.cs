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
        public SMsg[] GetMsgs()
        {
            SimpleTCP.Message reply = Program._Client.WriteLineAndGetReply("getmsgs\n" + TOKEN + "\n", TimeSpan.FromSeconds(5));
            try
            {
                return JsonConvert.DeserializeObject<SMsg[]>(reply.MessageString);
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
                timer1.Stop();
                LoginForm form = new LoginForm();
                form.ShowDialog();
                if (form.Token != string.Empty)
                {
                    TOKEN = form.Token;
                    LogedIn();
                }
                timer1.Stop();
            }
            else
            {
                Logout();
            }
        }

        private void newPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            new NewMsgForm(this).ShowDialog();
            timer1.Start();
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
                SimpleTCP.Message message = Program._Client.WriteLineAndGetReply("setcolor\n" + TOKEN + "\n" + JsonConvert.SerializeObject(colorDialog1.Color),TimeSpan.FromSeconds(10));
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
            SimpleTCP.Message message;
			try
            {
                message = Program._Client.WriteLineAndGetReply("refresh\n"+TOKEN+"\n"+JsonConvert.SerializeObject(Time), TimeSpan.FromSeconds(2));
            }
            catch
            {
                message = null;
            }
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

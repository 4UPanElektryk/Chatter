using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chatter.Client.Transfer;
using Newtonsoft.Json;

namespace Chatter.Client
{
    public partial class LoginForm : Form
    {
        public string Token;
        public LoginForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string login = TBLogin.Text;string password = TBPassword.Text;string token = TBToken.Text;
            TBLogin.Text = "";TBPassword.Text = ""; TBToken.Text = "";
            if (token != "")
            {
                SimpleTCP.Message reply = Program._Client.WriteLineAndGetReply("checktoken\n0\n" + token, TimeSpan.FromSeconds(20));
                if (bool.Parse(reply.MessageString))
                {
                    Token = reply.MessageString;
                    this.Close();
                }
            }
            else
            {
                LoginTransfer transfer = new LoginTransfer
                {
                    Login = login,
                    Password = password,
                };
                string data = JsonConvert.SerializeObject(transfer);
                SimpleTCP.Message reply = Program._Client.WriteLineAndGetReply("login\n0\n" + data, TimeSpan.FromSeconds(20));
                if (reply != null)
                {
                    if (reply.MessageString != "0")
                    {
                        Token = reply.MessageString;
                        this.Close();
                    }
                }
            }
        }
    }
}

using Chatter.AdminPanel.Transfer;
using Newtonsoft.Json;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.AdminPanel.Commands
{
    public class CmdAddUser : Command
    {
        public CmdAddUser(string name) : base(name) { }
        public override bool Execute(string text)
        {
            Console.Write("Login >");
            string login = Console.ReadLine();
            Console.Write("Color in HEX >");
            Color color = ColorTranslator.FromHtml(Console.ReadLine());
            Console.Write("User Type [a - admin, n - normal] >");
            bool isadmin = Console.ReadLine() == "a";
            Console.Write("Password >");
            Console.ForegroundColor = Console.BackgroundColor;
            string pass1 = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Confirm Password >");
            Console.ForegroundColor = Console.BackgroundColor;
            string pass2 = Console.ReadLine();
            Console.ResetColor();
            if (pass1 != pass2)
            {
                Console.WriteLine("Passwords do not match");
                return true;
            }
            if (color == Color.Empty)
            {
                color = Color.White;
            }
            TrAddUser tr = new TrAddUser
            {
                Name = login,
                Password = pass1,
                TextColor = color,
                IsAdmin = isadmin
            };
            Message message = Program._Client.WriteLineAndGetReply("adduser\n"+Program.Token+"\n"+JsonConvert.SerializeObject(tr), TimeSpan.FromSeconds(20));
            if (message.MessageString != "OK")
            {
                Program.ShowErr(message.MessageString);
                return true;
            }
            Console.WriteLine("User Added");
            return true;
        }
    }
}

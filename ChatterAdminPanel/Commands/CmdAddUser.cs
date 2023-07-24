using Chatter.AdminPanel.Transfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using IMTP.Client;
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
			Dictionary<string, object> data = new Dictionary<string, object>()
			{
				{ "Auth", Program.Token },
                { "IsAdmin", isadmin },
                { "Username", login },
                { "Password", pass1 },
                { "TextColor", color }
			};
			Task<IMTPResponse> task = Program._Client.SendRequest("/adduser", data);
			task.Wait();
			IMTPResponse response = task.Result;
			if (response.StatusCode != (int)IMTPStatusCode.OK)
            {
                Program.ShowErr(response);
                return true;
            }
            Console.WriteLine("User Added");
            return true;
        }
    }
}

using Chatter.Server.Transfer;
using Newtonsoft.Json;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.AdminPanel.Commands
{
    public class CmdGetUsers : Command
    {
        public CmdGetUsers(string name) : base(name) { }
        public override bool Execute(string text)
        {
            Message message = Program._Client.WriteLineAndGetReply("getusers\n"+Program.Token+"\n", TimeSpan.FromSeconds(20));
            if (message == null)
            {
                return true;
            }
			TrUser[] tr = JsonConvert.DeserializeObject<TrUser[]>(message.MessageString);
            foreach (TrUser item in tr)
            {
                if (item.IsAdmin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(item.Id + " " + item.Name);
                Console.ResetColor();
            }
            return base.Execute(text);
        }
    }
}

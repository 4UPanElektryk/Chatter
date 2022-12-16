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
            TrGetUsers tr = JsonConvert.DeserializeObject<TrGetUsers>(message.MessageString);
            foreach (TrUser item in tr.users)
            {
                if (item._IsAdmin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(item._Id + " " + item._Name);
                Console.ResetColor();
            }
            return base.Execute(text);
        }
    }
}

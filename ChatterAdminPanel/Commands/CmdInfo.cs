using Chatter.AdminPanel.Transfer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.AdminPanel.Commands
{
    public class CmdInfo : Command
    {
        public CmdInfo(string name) : base(name) { }
        public override bool Execute(string text)
        {
            TransferInfo data = JsonConvert.DeserializeObject<TransferInfo>(Program._Client.WriteLineAndGetReply("info\n" + Program.Token +"\n", TimeSpan.FromSeconds(300)).MessageString);
            Console.WriteLine("Client Username: " + data.Username);
            Console.WriteLine("Server: " + data.ServerName + "/" + data.ServerVersion);
            Console.WriteLine("Server Prot: " + data.Port);
            Console.WriteLine("Server Time: " + data.Time.ToString("dd/MM/yyyy HH:mm:ss"));
            return true;
        }
    }
}

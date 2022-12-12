using Chatter.AdminPanel.Transfer;
using Newtonsoft.Json;
using System;

namespace Chatter.AdminPanel.Commands
{
    public class CmdInfo : Command
    {
        public CmdInfo(string name) : base(name) { }
        public override bool Execute(string text)
        {
            TrInfo data = JsonConvert.DeserializeObject<TrInfo>(Program._Client.WriteLineAndGetReply("info\n" + Program.Token + "\n", TimeSpan.FromSeconds(300)).MessageString);
            Console.WriteLine("Client Username: " + data.Username);
            Console.WriteLine("Server: " + data.ServerName + "/" + data.ServerVersion);
            Console.WriteLine("Server Address: " + Program.Address + ":" + Program.Port);
            Console.WriteLine("Server Time: " + data.Time.ToString("dd/MM/yyyy HH:mm:ss"));
            return true;
        }
    }
}

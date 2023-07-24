using System;
using System.Threading.Tasks;
using IMTP.Client;
using System.Collections.Generic;

namespace Chatter.AdminPanel.Commands
{
    public class CmdInfo : Command
    {
        public CmdInfo(string name) : base(name) { }
        public override bool Execute(string text)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "Auth", Program.Token },
            };
			Task<IMTPResponse> task = Program._Client.SendRequest("/info", data);
			task.Wait();
			IMTPResponse response = task.Result;
            Console.WriteLine("Client Username: " + (string)response.Data["Username"]);
            Console.WriteLine("Server: " + (string)response.Data["ServerName"] + "/" + (string)response.Data["ServerVersion"]);
            Console.WriteLine("Server Address: " + Program.Address + ":" + Program.Port);
            Console.WriteLine("Server Time: " + ((DateTime)response.Data["Time"]).ToString("dd/MM/yyyy HH:mm:ss"));
            return true;
        }
    }
}

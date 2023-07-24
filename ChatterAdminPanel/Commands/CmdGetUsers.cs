using Chatter.Server.Transfer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMTP.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chatter.AdminPanel.Commands
{
    public class CmdGetUsers : Command
    {
        public CmdGetUsers(string name) : base(name) { }
        public override bool Execute(string text)
        {
			Dictionary<string, object> data = new Dictionary<string, object>()
			{
				{ "Auth", Program.Token },
			};
			Task<IMTPResponse> task = Program._Client.SendRequest("/getusers", data);
			task.Wait();
			IMTPResponse response = task.Result;
            if (response.StatusCode != (int)IMTPStatusCode.OK)
            {
                Program.ShowErr(response);
                return true;
            }
            foreach (TrUser item in JsonConvert.DeserializeObject<TrUser[]>((string)response.Data["Users"]))
            {
                if (item.IsAdmin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"{item.Id:D3} {item.Name}");
                Console.ResetColor();
            }
            return true;
        }
    }
}

using Chatter.Server.UserService;
using Chatter.Server.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chatter.Server.CommandService
{
    public class CmdInfo : Command
    {
        public CmdInfo(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            TransferInfo output = new TransferInfo 
            {
                Port = Program.Port,
                ServerName = Config.Data.ServerName,
                ServerVersion = Config.Data.ServerVersion,
                Time = DateTime.Now,
                Username = user._Name,
            };

            return JsonConvert.SerializeObject(output);
        }
    }
}

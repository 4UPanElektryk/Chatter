using Chatter.Server.Transfer;
using Chatter.Server.UserService;
using Newtonsoft.Json;
using System;

namespace Chatter.Server.CommandService
{
    public class CmdInfo : Command
    {
        public CmdInfo(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            TrInfo output = new TrInfo
            {
                ServerName = Config.Data.ServerName,
                ServerVersion = Config.Data.ServerVersion,
                Time = DateTime.Now,
                Username = user._Name,
            };

            return JsonConvert.SerializeObject(output);
        }
    }
}

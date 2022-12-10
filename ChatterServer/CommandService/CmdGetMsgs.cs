using Chatter.Server.MessageService;
using Chatter.Server.Transfer;
using Chatter.Server.UserService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.CommandService
{
    public class CmdGetMsgs : Command
    {
        public CmdGetMsgs(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            TrGetMsgs tr = new TrGetMsgs
            {
                msgs = MsgHandeler.GetLastSeveral(20),
            };
            return JsonConvert.SerializeObject(tr);
        }
    }
}

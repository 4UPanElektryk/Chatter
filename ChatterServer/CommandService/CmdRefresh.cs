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
    public class CmdRefresh : Command
    {
        public CmdRefresh(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            TrRefresh tr = JsonConvert.DeserializeObject<TrRefresh>(text);
            if (tr == null)
            {
                return "E-DAT";
            }
            if (tr.time < Program.LastChange)
            {
                return "CHANGES";
            }
            else
            {
                return "NOCHANGES";
            }
        }
    }
}

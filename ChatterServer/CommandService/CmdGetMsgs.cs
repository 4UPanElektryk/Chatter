using Chatter.Server.UserService;
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
            return base.Execute(text, user);
        }
    }
}

using Chatter.Server.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.CommandService
{
    public class CmdSetPswd : Command
    {
        public CmdSetPswd(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            if (user == null)
            {
                return "E-TKN";
            }



            return base.Execute(text, user);
        }
    }
}

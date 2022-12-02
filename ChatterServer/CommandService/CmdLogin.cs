using Chatter.Server.UserService;
using Chatter.Server.Transfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.CommandService
{
    public class CmdLogin : Command
    {
        public CmdLogin(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            LoginTransfer data = JsonConvert.DeserializeObject<LoginTransfer>(text);
            if (data == null)
            {
                return "0";
            }
            else
            {
                return TokenHandeler.AddToken(UserHandeler.GetUser(data.Login, data.Password));
            }
        }
    }
}

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
    public class CmdAddUser : Command
    {
        public CmdAddUser(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            if (user == null)
            {
                return "E-TKN";
            }
            TrAddUser tr = JsonConvert.DeserializeObject<TrAddUser>(text);
            if (tr == null)
            {
                return "E-DAT";
            }
            if (UserHandeler.LoginInUse(tr._Name))
            {
                return "E-NAM";
            }
            User user1 = new User
            {
                _Id = UserHandeler.GetNewId(),
                _IsAdmin = tr._IsAdmin,
                _Name = tr._Name,
                _Password = tr._Password,
                _TextColor = tr._TextColor,
            };
            UserHandeler.AddUser(user1);
            UserHandeler.Save();
            return "OK";
        }
    }
}

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
            if (UserHandeler.LoginInUse(tr.Name))
            {
                return "E-NAM";
            }
            User user1 = new User
            {
                _Id = UserHandeler.GetNewId(),
                _IsAdmin = tr.IsAdmin,
                _Name = tr.Name,
                _Password = tr.Password,
                _TextColor = tr.TextColor,
            };
            UserHandeler.AddUser(user1);
            UserHandeler.Save();
            return "OK";
        }
    }
}

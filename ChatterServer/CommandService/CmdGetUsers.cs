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
    public class CmdGetUsers : Command
    {
        public CmdGetUsers(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            TrGetUsers tr = new TrGetUsers();
            foreach (User item in UserHandeler.users)
            {
                TrUser tr1 = new TrUser
                {
                    _Id = item._Id,
                    _IsAdmin = item._IsAdmin,
                    _Name = item._Name,
                    _TextColor = item._TextColor
                };
                tr.users.Add(tr1);
            }
            return JsonConvert.SerializeObject(tr);
        }
    }
}

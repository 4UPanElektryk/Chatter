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
            List<TrUser> tr = new List<TrUser>();
            foreach (User item in UserHandeler.users)
            {
                TrUser tr1 = new TrUser
                {
                    Id = item._Id,
                    IsAdmin = item._IsAdmin,
                    Name = item._Name,
                    TextColor = item._TextColor
                };
                tr.Add(tr1);
            }
            return JsonConvert.SerializeObject(tr.ToArray());
        }
    }
}

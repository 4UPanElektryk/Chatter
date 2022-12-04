using Chatter.Server.UserService;
using Newtonsoft.Json;
using Chatter.Server.Transfer;

namespace Chatter.Server.CommandService
{
    public class CmdAdminLogin : Command
    {
        public CmdAdminLogin(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            TransferLogin data = JsonConvert.DeserializeObject<TransferLogin>(text);
            if (data != null && UserHandeler.GetUser(data.Login, data.Password)._IsAdmin)
            {
                return TokenHandeler.AddToken(UserHandeler.GetUser(data.Login, data.Password));
            }
            return "0";
        }
    }
}

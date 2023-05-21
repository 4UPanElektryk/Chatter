using Chatter.Server.Transfer;
using Chatter.Server.UserService;
using Newtonsoft.Json;

namespace Chatter.Server.CommandService
{
    public class CmdAdminLogin : Command
    {
        public CmdAdminLogin(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            TrLogin data = JsonConvert.DeserializeObject<TrLogin>(text);
            if (UserHandeler.GetUser(data.Login, data.Password)._IsAdmin)
            {
                return TokenHandeler.AddToken(UserHandeler.GetUser(data.Login, data.Password));
            }
            return "0";
        }
    }
}

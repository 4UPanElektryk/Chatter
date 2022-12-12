using Chatter.Server.Transfer;
using Chatter.Server.UserService;
using Newtonsoft.Json;

namespace Chatter.Server.CommandService
{
    public class CmdLogin : Command
    {
        public CmdLogin(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            TrLogin data = JsonConvert.DeserializeObject<TrLogin>(text);
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

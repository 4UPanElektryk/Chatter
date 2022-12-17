using Chatter.Server.UserService;
using Chatter.Server.Transfer;
using Newtonsoft.Json;

namespace Chatter.Server.CommandService
{
    public class CmdSetColor : Command
    {
        public CmdSetColor(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            if (user == null)
            {
                return "E-TKN";
            }
            TrSetColor tr = JsonConvert.DeserializeObject<TrSetColor>(text);
            if (tr == null)
            {
                return "E-DAT";
            }
            UserHandeler.SetUserColor(tr.color, user._Id);
            return "OK";
        }
    }
}

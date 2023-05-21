using Chatter.Server.UserService;
using Chatter.Server.Transfer;
using Newtonsoft.Json;
using System.Drawing;

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
            Color tr = JsonConvert.DeserializeObject<Color>(text);
            if (tr == null)
            {
                return "E-DAT";
            }
            UserHandeler.SetUserColor(tr, user._Id);
            return "OK";
        }
    }
}

using Chatter.Server.MessageService;
using Chatter.Server.Transfer;
using Chatter.Server.UserService;
using Newtonsoft.Json;

namespace Chatter.Server.CommandService
{
    public class CmdAddMsg : Command
    {
        public CmdAddMsg(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            if (user == null)
            {
                return "E-TKN";
            }
            TrAddMsg tr = JsonConvert.DeserializeObject<TrAddMsg>(text);
            if (tr == null)
            {
                return "E-DAT";
            }
            Msg msg = new Msg
            {
                _Message = tr.Lines,
                _UserID = user._Id,
            };
            MsgHandeler.AddMsg(msg);

            return "OK";
        }
    }
}

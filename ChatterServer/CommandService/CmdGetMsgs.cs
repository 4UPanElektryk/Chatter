using Chatter.Server.MessageService;
using Chatter.Server.Transfer;
using Chatter.Server.UserService;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chatter.Server.CommandService
{
    public class CmdGetMsgs : Command
    {
        public CmdGetMsgs(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            List<Msg> msgs = MsgHandeler.msgs;
            List<SMsg> umsgs = new List<SMsg>();
            foreach (Msg item in msgs)
            {
                SMsg temp = new SMsg
                {
                    Message = item._Message,
                    MessageID = item._MessageID,
                    Sent = item._Sent,
                    UserName = UserHandeler.GetUser(item._UserID)._Name,
                    Color = UserHandeler.GetUser(item._UserID)._TextColor,
                };
                umsgs.Add(temp);
            }
            return JsonConvert.SerializeObject(umsgs.ToArray());
        }
    }
}

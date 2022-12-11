using Chatter.Server.MessageService;
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
    public class CmdGetMsgs : Command
    {
        public CmdGetMsgs(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            List<Msg> msgs = MsgHandeler.GetLastSeveral(20);
            List<UMsg> umsgs = new List<UMsg>();
            foreach (Msg item in msgs)
            {
                UMsg temp = new UMsg
                {
                    _Message = item._Message,
                    _MessageID = item._MessageID,
                    _Sent = item._Sent,
                    _UserName = UserHandeler.GetUser(item._UserID)._Name,
                    _Color = UserHandeler.GetUser(item._UserID)._TextColor,
                };
                umsgs.Add(temp);
            }
            TrGetMsgs tr = new TrGetMsgs
            {
                msgs = umsgs,
            };
            return JsonConvert.SerializeObject(tr);
        }
    }
}

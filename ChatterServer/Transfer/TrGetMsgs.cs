using Chatter.Server.MessageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.Transfer
{
    public class TrGetMsgs
    {
        public List<UMsg> msgs { get; set; }
    }
    public class UMsg
    {
        public int _MessageID { get; set; }
        public string _UserName { get; set; }
        public DateTime _Sent { get; set; }
        public string[] _Message { get; set; }
    }
}

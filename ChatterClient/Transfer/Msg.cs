using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Client.Transfer
{
    public class Msg
    {
        public int _MessageID { get; set; }
        public int _UserID { get; set; }
        public DateTime _Sent { get; set; }
        public string[] _Message { get; set; }
    }
}

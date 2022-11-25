using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.MessageService
{
    public class Msg
    {
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public DateTime Sent { get; set; }
        public string[] Message { get; set; }
    }
}

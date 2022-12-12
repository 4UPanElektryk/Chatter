using System;

namespace Chatter.Server.MessageService
{
    public class Msg
    {
        public int _MessageID { get; set; }
        public int _UserID { get; set; }
        public DateTime _Sent { get; set; }
        public string[] _Message { get; set; }
    }
}

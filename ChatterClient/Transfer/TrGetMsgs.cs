using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chatter.Client.Transfer
{
    public class TrGetMsgs
    {
        public List<UMsg> msgs { get; set; }
    }
    public class UMsg
    {
        public Color _Color { get; set; }
        public int _MessageID { get; set; }
        public string _UserName { get; set; }
        public DateTime _Sent { get; set; }
        public string[] _Message { get; set; }
    }
}

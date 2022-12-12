using System;

namespace Chatter.Server.Transfer
{
    public class TrInfo
    {
        public string Username { get; set; }
        public DateTime Time { get; set; }
        public string ServerName { get; set; }
        public string ServerVersion { get; set; }
    }
}

using System;

namespace Chatter.AdminPanel.Transfer
{
    public struct TrInfo
    {
        public string Username { get; set; }
        public DateTime Time { get; set; }
        public string ServerName { get; set; }
        public string ServerVersion { get; set; }
    }
}

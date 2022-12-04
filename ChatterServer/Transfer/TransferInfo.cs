using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.Transfer
{
    public class TransferInfo
    {
        public string Username { get; set; }
        public DateTime Time { get; set; }
        public string ServerName { get; set; }
        public string ServerVersion { get; set; }
        public int Port { get; set; }
    }
}

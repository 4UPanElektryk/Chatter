using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server
{
    public class TConfig
    {
        public string ServerName { get; set; }
        public string ServerVersion { get; set; }
        public string UserBaseFile { get; set; }
        public string Messagebase { get; set; }
        public TConfig() { }
    }
}

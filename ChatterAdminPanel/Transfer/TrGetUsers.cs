using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.Transfer
{
    public class TrGetUsers
    {
        public List<TrUser> users { get; set; }
    }
    public class TrUser
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
        public bool _IsAdmin { get; set; }
        public Color _TextColor { get; set; }
    }
}

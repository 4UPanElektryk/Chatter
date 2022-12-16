using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.AdminPanel.Transfer
{
    public class TrAddUser
    {
        public string _Name { get; set; }
        public string _Password { get; set; }
        public bool _IsAdmin { get; set; }
        public Color _TextColor { get; set; }
    }
}

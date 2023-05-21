using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.AdminPanel.Transfer
{
    public struct TrAddUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public Color TextColor { get; set; }
    }
}

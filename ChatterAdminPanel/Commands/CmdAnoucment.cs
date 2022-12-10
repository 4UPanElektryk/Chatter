using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.AdminPanel.Commands
{
    public class CmdAnoucment : Command
    {
        public CmdAnoucment(string name) : base(name) { }
        public override bool Execute(string text)
        {
            string flag = text.Split(' ')[0];
            if (text)
            {

            }
        }
    }
}

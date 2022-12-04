using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.AdminPanel.Commands
{
    public class CommandHandeler
    {
        public static List<Command> commands;
        public CommandHandeler()
        {
            commands = new List<Command>
            {
                new CmdInfo("info"),
            };
        }
        public static bool Run(string input)
        {
            foreach (Command item in commands)
            {
                if (input.ToLower().StartsWith(item.Name))
                {
                    return item.Execute(input);
                }
            }
            return false;
        }
    }
}

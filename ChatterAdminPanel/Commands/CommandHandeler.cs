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
                new CmdAnoucment("anoucment"),
                new CmdAddUser("adduser"),
                new CmdGetUsers("getusers")
            };
        }
        public static bool Run(string input)
        {
            foreach (Command item in commands)
            {
                if (input.ToLower().StartsWith(item.Name))
                {
                    string data = input.ToArray().Skip(item.Name.Length + 1).ToString();
                    return item.Execute(data);
                }
            }
            return false;
        }
    }
}

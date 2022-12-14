using Chatter.Server.UserService;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Chatter.Server.CommandService
{
    public class CommandHandeler
    {
        public static List<Command> commands;
        public CommandHandeler()
        {
            commands = new List<Command>
            {
                new CmdAdminLogin("adminlogin"),
                new CmdLogin("login"),
                new CmdInfo("info"),
                new CmdCheckToken("checktoken"),
                new CmdAddMsg("addmsg"),
                new CmdGetMsgs("getmsgs"),
                new CmdAddUser("adduser"),
                new CmdGetUsers("getusers"),
                new CmdSetColor("setcolor"),
                new CmdSetPswd("setpswd"),
                new CmdRefresh("refresh")
            };
        }
        public static bool MakesImpact(string input)
        {
            List<string> strings = new List<string>
            {
                "addmsg",
                "setcolor",
            };
            return strings.Contains(input);
        }
        public static string Run(string input)
        {
            string[] s = input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            string name = s[0];
            User user = TokenHandeler.GetUser(s[1]);
            string data = s[2];
            foreach (Command item in commands)
            {
                if (name.ToLower().StartsWith(item.Name))
                {
                    return item.Execute(data, user);
                }
            }
            return "";
        }
    }
}

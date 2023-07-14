using Chatter.Server.UserService;
using IMTP.Server;
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
				new CmdAdminLogin("/adminlogin"),
				new CmdLogin("/login"),
				new CmdInfo("/info"),
				new CmdCheckToken("/checktoken"),
				new CmdAddMsg("/addmsg"),
				new CmdGetMsgs("/getmsgs"),
				new CmdAddUser("/adduser"),
				new CmdGetUsers("/getusers"),
				new CmdSetColor("/setcolor"),
				new CmdSetPswd("/setpswd"),
				new CmdRefresh("/refresh")
			};
		}
		public static bool MakesImpact(string input)
		{
			List<string> strings = new List<string>
			{
				"/addmsg",
				"/setcolor",
			};
			return strings.Contains(input);
		}
		public static IMTPResponse Run(IMTPRequest request)
		{
			string name = request.Path;
			if (request.Data == null)
			{
				request.Data = new Dictionary<string, object>();
			}
			User user = request.Data.ContainsKey("Auth") ? TokenHandeler.GetUser((string)request.Data["Auth"]) : null;
			foreach (Command item in commands)
			{
				if (name.ToLower().StartsWith(item.Name))
				{
					return item.Execute(request, user);
				}
			}
			return new IMTPResponse()
			{
				StatusCode = 1,
				Data = new Dictionary<string, object>
				{
					{ "Path", request.Path }
				},
			};
		}
	}
}

using Chatter.Server.UserService;
using IMTP.Server;
using System.Collections.Generic;
using System;

namespace Chatter.Server.CommandService
{
	public class CmdInfo : Command
	{
		public CmdInfo(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (user == null)
			{
				return new IMTPResponse(IMTPStatusCode.AuthenticationNeeded);
			}
			return new IMTPResponse(IMTPStatusCode.OK)
			{
				Data = new Dictionary<string, object>()
				{
					{ "ServerName", Config.Data.ServerName },
					{ "ServerVersion", Config.Data.ServerVersion },
					{ "Time", DateTime.UtcNow },
					{ "Username", user.Name }
				}
			};
		}
	}
}

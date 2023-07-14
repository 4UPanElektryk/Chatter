using Chatter.Server.UserService;
using IMTP.Server;
using System.Collections.Generic;
using System;

namespace Chatter.Server.CommandService
{
	public class CmdRefresh : Command
	{
		public CmdRefresh(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (!request.Data.ContainsKey("Time"))
			{
				return new IMTPResponse(IMTPStatusCode.IncorrectData);
			}
			return new IMTPResponse(IMTPStatusCode.OK)
			{
				Data = new Dictionary<string, object>()
				{
					{ "Changes", (DateTime)request.Data["Time"] < Program.LastChange }
				}
			};
		}
	}
}

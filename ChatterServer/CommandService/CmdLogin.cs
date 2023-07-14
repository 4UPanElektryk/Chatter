using Chatter.Server.UserService;
using IMTP.Server;
using System.Collections.Generic;

namespace Chatter.Server.CommandService
{
	public class CmdLogin : Command
	{
		public CmdLogin(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (!request.Data.ContainsKey("Login") || !request.Data.ContainsKey("Password"))
			{
				return new IMTPResponse(IMTPStatusCode.IncorrectData);
			}

			return new IMTPResponse(IMTPStatusCode.OK)
			{
				Data = new Dictionary<string, object>()
				{
					{ "Token", TokenHandeler.AddToken(UserHandeler.GetUser((string)request.Data["Login"], (string)request.Data["Password"])) }
				}
			};
		}
	}
}

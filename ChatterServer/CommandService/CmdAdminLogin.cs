using Chatter.Server.UserService;
using IMTP.Server;
using System.Collections.Generic;

namespace Chatter.Server.CommandService
{
	public class CmdAdminLogin : Command
	{
		public CmdAdminLogin(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (UserHandeler.GetUser((string)request.Data["Login"], (string)request.Data["Password"]).IsAdmin)
			{
				return new IMTPResponse(IMTPStatusCode.OK)
				{
					Data = new Dictionary<string, object>()
					{
						{ "Token", TokenHandeler.AddToken(UserHandeler.GetUser((string)request.Data["Login"], (string)request.Data["Password"])) }
					}
				};
			}
			return new IMTPResponse(IMTPStatusCode.OK)
			{
				Data = new Dictionary<string, object>()
				{
					{ "Token", "0" }
				}
			};
		}
	}
}

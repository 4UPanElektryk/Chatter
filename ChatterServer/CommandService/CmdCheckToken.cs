using Chatter.Server.UserService;
using System.Collections.Generic;
using IMTP.Server;

namespace Chatter.Server.CommandService
{
	public class CmdCheckToken : Command
	{
		public CmdCheckToken(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			return new IMTPResponse(IMTPStatusCode.OK)
			{
				Data = new Dictionary<string, object>()
				{
					{ "Valid", TokenHandeler.GetUser((string)request.Data["Token"]) != null }
				}
			};
		}
	}
}

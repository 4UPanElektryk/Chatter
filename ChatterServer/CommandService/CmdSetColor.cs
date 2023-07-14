using Chatter.Server.UserService;
using System.Drawing;
using IMTP.Server;

namespace Chatter.Server.CommandService
{
	public class CmdSetColor : Command
	{
		public CmdSetColor(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (user == null)
			{
				return new IMTPResponse(IMTPStatusCode.AuthenticationNeeded);
			}
			if (!request.Data.ContainsKey("Color"))
			{
				return new IMTPResponse(IMTPStatusCode.IncorrectData);
			}
			UserHandeler.SetUserColor((Color)request.Data["Color"], user.Id);
			return new IMTPResponse(IMTPStatusCode.OK);
		}
	}
}

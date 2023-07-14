using Chatter.Server.UserService;
using IMTP.Server;

namespace Chatter.Server.CommandService
{
	public class CmdSetPswd : Command
	{
		public CmdSetPswd(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (user == null)
			{
				return new IMTPResponse(IMTPStatusCode.AuthenticationNeeded);
			}
			if (!request.Data.ContainsKey("Password"))
			{
				return new IMTPResponse(IMTPStatusCode.IncorrectData);
			}
			UserHandeler.ChangePassword((string)request.Data["Password"], user.Id);
			return new IMTPResponse(IMTPStatusCode.OK);
		}
	}
}

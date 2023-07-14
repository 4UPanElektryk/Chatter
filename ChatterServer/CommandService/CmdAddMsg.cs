using Chatter.Server.MessageService;
using Chatter.Server.UserService;
using IMTP.Server;

namespace Chatter.Server.CommandService
{
	public class CmdAddMsg : Command
	{
		public CmdAddMsg(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (user == null)
			{
				return new IMTPResponse(IMTPStatusCode.AuthenticationNeeded);
			}
			if (!request.Data.ContainsKey("Message"))
			{
				return new IMTPResponse(IMTPStatusCode.IncorrectData);
			}
			Msg msg = new Msg
			{
				_Message = (string[])request.Data["Message"],
				_UserID = user.Id,
			};
			MsgHandeler.AddMsg(msg);
			return new IMTPResponse(IMTPStatusCode.OK);
		}
	}
}

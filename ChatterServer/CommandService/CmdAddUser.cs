using Chatter.Server.UserService;
using IMTP.Server;
using System.Collections.Generic;
using System.Drawing;

namespace Chatter.Server.CommandService
{
	public class CmdAddUser : Command
	{
		public CmdAddUser(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (user == null)
			{
				return new IMTPResponse(IMTPStatusCode.AuthenticationNeeded);
			}
			if (!request.Data.ContainsKey("Username") || !request.Data.ContainsKey("Password") || !request.Data.ContainsKey("TextColor") || !request.Data.ContainsKey("IsAdmin"))
			{
				return new IMTPResponse(IMTPStatusCode.IncorrectData);
			}
			if (UserHandeler.LoginInUse((string)request.Data["Username"]))
			{
				return new IMTPResponse(IMTPStatusCode.AuthenticationError)
				{
					Data = new Dictionary<string, object>()
					{
						{ "ErrorMessage", "Login in Use" }
					}
				};
			}
			User user1 = new User
			{
				Id = UserHandeler.GetNewId(),
				IsAdmin = (bool)request.Data["IsAdmin"],
				Name = (string)request.Data["Username"],
				Password = (string)request.Data["Password"],
				TextColor = (Color)request.Data["TextColor"],
			};
			UserHandeler.AddUser(user1);
			UserHandeler.Save();
			return new IMTPResponse(IMTPStatusCode.OK);
		}
	}
}

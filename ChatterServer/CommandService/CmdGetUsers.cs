using Chatter.Server.Transfer;
using Chatter.Server.UserService;
using IMTP.Server;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chatter.Server.CommandService
{
	public class CmdGetUsers : Command
	{
		public CmdGetUsers(string name) : base(name) { }
		public override IMTPResponse Execute(IMTPRequest request, User user)
		{
			if (user == null)
			{
				return new IMTPResponse(IMTPStatusCode.AuthenticationNeeded);
			}
			List<TrUser> tr = new List<TrUser>();
			foreach (User item in UserHandeler.users)
			{
				TrUser tr1 = new TrUser
				{
					Id = item.Id,
					IsAdmin = item.IsAdmin,
					Name = item.Name,
					TextColor = item.TextColor
				};
				tr.Add(tr1);
			}
			return new IMTPResponse(IMTPStatusCode.OK)
			{
				Data = new Dictionary<string, object>()
				{
					{ "Users", JsonConvert.SerializeObject(tr.ToArray()) }
				}
			};
		}
	}
}

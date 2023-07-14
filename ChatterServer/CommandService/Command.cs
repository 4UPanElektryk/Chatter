using Chatter.Server.UserService;
using IMTP.Server;
using System.Collections.Generic;

namespace Chatter.Server.CommandService
{
	public class Command
	{
		public string Name;
		public Command(string name)
		{
			Name = name;
		}
		public virtual IMTPResponse Execute(IMTPRequest data, User user)
		{
			return new IMTPResponse()
			{
				StatusCode = 1,
				Data = new Dictionary<string, object>
				{
					{ "Path", data.Path }
				},
			};
		}
	}
}

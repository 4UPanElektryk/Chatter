using Chatter.Server.Transfer;
using Chatter.Server.UserService;
using Newtonsoft.Json;

namespace Chatter.Server.CommandService
{
	public class CmdSetPswd : Command
	{
		public CmdSetPswd(string name) : base(name) { }
		public override string Execute(string text, User user)
		{
			if (user == null)
			{
				return "E-TKN";
			}
			string tr = JsonConvert.DeserializeObject<string>(text);
			if (tr == null)
			{
				return "E-DAT";
			}
			UserHandeler.ChangePassword(tr, user._Id);
			return "OK";
		}
	}
}

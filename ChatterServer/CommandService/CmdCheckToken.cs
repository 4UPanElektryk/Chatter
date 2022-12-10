using Chatter.Server.UserService;

namespace Chatter.Server.CommandService
{
    public class CmdCheckToken : Command
    {
        public CmdCheckToken(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            return (TokenHandeler.GetUser(text) != null).ToString();
        }
    }
}

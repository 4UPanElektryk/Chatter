using Chatter.Server.UserService;
namespace Chatter.Server.CommandService
{
    public class Command
    {
        public string Name;
        public Command(string name)
        {
            Name = name;
        }
        public virtual string Execute(string text,User user)
        {
            return "";
        }
    }
}

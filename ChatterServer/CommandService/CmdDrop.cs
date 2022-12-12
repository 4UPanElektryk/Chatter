using Chatter.Server.UserService;
using SimpleLogs4Net;
using System.Net.Sockets;

namespace Chatter.Server.CommandService
{
    public class CmdDrop : Command
    {
        public CmdDrop(string name) : base(name) { }
        public override string Execute(string text, User user)
        {
            foreach (TcpClient item in Program.Clients)
            {
                item.Close();
            }
            Program.Clients.Clear();
            Log.Write("All Connections Dropped", EType.Informtion);
            return "";
        }
    }
}

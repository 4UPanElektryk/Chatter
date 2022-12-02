using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolConsole.MenuItems;
using CoolConsole;
using System.Net;
using Chatter.Server.UserService;
using Chatter.Server.MessageService;
using System.Runtime.CompilerServices;
using SimpleLogs4Net;
using Chatter.Server.CommandService;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Chatter.Server
{
	public class Program
	{
		static SimpleTcpServer server;
        public static bool LinuxBased;
		static void Main(string[] args)
		{
            LinuxBased = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            server = new SimpleTcpServer();
			server.Delimiter = (byte)'\n';//enter
            server.StringEncoder = Encoding.UTF8;
			server.DataReceived += Server_DataReceived;
			server.ClientConnected += Server_ClientConnected;
			server.ClientDisconnected += Server_ClientDisconnected;
			Init();
			StartCommmandLisener();
		}

        private static void StartCommmandLisener()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "test")
                {
                    Msg msg = new Msg
                    {
                        _UserID = 0,
                        _Message = new string[] { "test", "test" },
                    };
                    MsgHandeler.AddMsg(msg);
                }
                if (input.ToLower() == "stop")
                {
                    MsgHandeler.Save();
                    UserHandeler.Save();
                    return;
                }
            }
        }

        private static void Init()
		{
            List<MenuItem> list = new List<MenuItem>
            {
                new NumboxMenuItem("Port",87),
                new TextboxMenuItem("IP","127.0.0.1"),
                new MenuItem("Start Server"),
            };
            ReturnCode output = Menu.Show(list);
            new CommandHandeler();
            if (LinuxBased)
            {
                new Log("Logs/", OutputStream.Both, "Log");
                new UserHandeler("./Data/UserDataBase.json");
                new MsgHandeler("./Data/MessageBase.json");
            }
            else
            {
                new Log("Logs\\", OutputStream.Both, "Log");
                new UserHandeler(".\\Data\\UserDataBase.json");
                new MsgHandeler(".\\Data\\MessageBase.json");
            }
			
			new TokenHandeler();
			server.Start(IPAddress.Parse(output.Textboxes[0]._Value), output.Numboxes[0]._Value);
			if (server.IsStarted)
			{
				Log.Write("Server Started on " + output.Textboxes[0]._Value + ":" + output.Numboxes[0]._Value, EType.Informtion);
			}
		}
        #region Clinet
        private static void Server_ClientDisconnected(object sender, TcpClient e)
        {
            Log.Write("Client Has Disconnected", EType.Informtion);
        }
        private static void Server_ClientConnected(object sender, TcpClient e)
        {
			Log.Write("New Client Has Connected", EType.Informtion);
        }
        #endregion
		private static void Server_DataReceived(object sender, Message e)
		{
			Log.AddEvent(new Event(e.MessageString.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None),EType.Normal));
			string reply = CommandHandeler.Run(e.MessageString);
			Log.Write(reply);
			e.ReplyLine(reply);
		}
	}
}

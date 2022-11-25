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
using System.Runtime.CompilerServices;

namespace Chatter.Server
{
	internal class Program
	{
		static SimpleTcpServer server;
		static void Main(string[] args)
		{
			server = new SimpleTcpServer();
			server.Delimiter = 0x13;//enter
            server.StringEncoder = Encoding.UTF8;
			server.DataReceived += Server_DataReceived;
			List<MenuItem> list = new List<MenuItem>
			{
				new NumboxMenuItem("Port",87),
				new TextboxMenuItem("IP","127.0.0.1"),
				new MenuItem("Start Server"),
			};
			ReturnCode output = Menu.Show(list);
            int port = output.Numboxes[0]._Value;
            IPAddress Ip = IPAddress.Parse(output.Textboxes[0]._Value);
			Init(port, Ip);
            while (true) 
			{
                string input = Console.ReadLine();
			}
		}
		public static void Init(int port, IPAddress address)
		{
            new UserHandeler("UserDataBase.json");
            new TokenHandeler();
            server.Start(address, port);
        }

		private static void Server_DataReceived(object sender, Message e)
		{
			
		}
		public static void CWrite(string text, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ResetColor();
		}
	}
}

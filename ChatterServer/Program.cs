using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolConsole.MenuItems;
using CoolConsole;
using System.Net;

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
			server.Start(Ip,port);
		}

		private static void Server_DataReceived(object sender, Message e)
		{
			
		}
	}
}

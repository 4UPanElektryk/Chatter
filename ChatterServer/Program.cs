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
		public static SimpleTcpServer server;
		public static List<TcpClient> Clients;
		public static bool Working = true;
		static void Main(string[] args)
		{
			server = new SimpleTcpServer
			{
				Delimiter = (byte)'\n',
				StringEncoder = Encoding.UTF8,
			};
			server.DataReceived += Server_DataReceived;
			server.ClientConnected += Server_ClientConnected;
			server.ClientDisconnected += Server_ClientDisconnected;
			Init();
			StartCommmandLisener();
		}
		private static void StartCommmandLisener()
		{
			while (Working)
			{
				string input = Console.ReadLine();
				if (input.ToLower() == "stop")
				{
					MsgHandeler.Save();
					UserHandeler.Save();
					server.Stop();
					return;
				}
				//CommandHandeler.Run(input);
			}
		}
		public static string ConvertPlatform(string input) 
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? input.Replace('\\','/') : input;
		}
		private static void Init()
		{
			new Config("config.json");
			Config.Load();
			new CommandHandeler();
			new Log(ConvertPlatform(Config.Data.LogsDirectory), OutputStream.Both, Config.Data.LogsPrefix);
			new UserHandeler(ConvertPlatform(Config.Data.UserBaseFile));
			new MsgHandeler(ConvertPlatform(Config.Data.MessagebaseFile));
			new TokenHandeler();
			server.Start(IPAddress.Parse(Config.Data.ServerIPAddress), Config.Data.ServerPort);
			if (server.IsStarted)
			{
				Log.Write("Server Started on " + Config.Data.ServerIPAddress + ":" + Config.Data.ServerPort, EType.Informtion);
			}
		}
		#region Client
		private static void Server_ClientDisconnected(object sender, TcpClient e)
		{
			Log.Write("Client Has Disconnected", EType.Informtion);
			Clients.Remove(e);
		}
		private static void Server_ClientConnected(object sender, TcpClient e)
		{
			Log.Write("New Client Has Connected", EType.Informtion);
			Clients.Add(e);
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

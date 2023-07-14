using Chatter.Server.CommandService;
using Chatter.Server.MessageService;
using Chatter.Server.UserService;
using SimpleLogs4Net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using IMTP.Server;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;

namespace Chatter.Server
{
	public class Program
	{
		public static DateTime LastChange;
		public static IMTPServer server;
		static void Main(string[] args)
		{
			server = new IMTPServer();
			server.HandleRequest += Server_HandleRequest;
			server.Log += LogMsg;
			Init();
			StartCommmandLisener();
		}
		public static void LogMsg(object sender, LogData e)
		{
			Console.WriteLine($"[{e.Time:HH:mm:ss.fff}]{e.Sender}>\n{e.Message}");
		}
		public static IMTPResponse Server_HandleRequest(IMTPRequest e)
		{
			if (CommandHandeler.MakesImpact(e.Path))
			{
				LastChange = DateTime.UtcNow;
			}
			return CommandHandeler.Run(e);
		}
		private static void StartCommmandLisener()
		{
			while (true)
			{
				string input = Console.ReadLine();
				if (input.ToLower() == "stop")
				{
					MsgHandeler.Save();
					UserHandeler.Save();
					return;
				}
				//CommandHandeler.Run(input);
			}
		}
		public static string ConvertPlatform(string input)
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? input.Replace('\\', '/') : input;
		}
		private static void Init()
		{
			new Config("config.json");
			Config.Load();
			new CommandHandeler();
			new LogConfiguration(ConvertPlatform(Config.Data.LogsDirectory), OutputStream.Both, Config.Data.LogsPrefix);
			new UserHandeler(ConvertPlatform(Config.Data.UserBaseFile));
			new MsgHandeler(ConvertPlatform(Config.Data.MessagebaseFile));
			new TokenHandeler();
			server.port = Config.Data.ServerPort;
			Task.Run(() => server.Start(IPAddress.Parse(Config.Data.ServerIPAddress)));
			Log.Write($"Server Started on {Config.Data.ServerIPAddress}:{Config.Data.ServerPort}", EType.Informtion);
			LastChange = DateTime.UtcNow;
		}
	}
}

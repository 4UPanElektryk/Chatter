using Chatter.AdminPanel.Commands;
using Newtonsoft.Json;
using IMTP.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatter.AdminPanel
{
	public class Program
	{
		public static IMTPClient _Client;
		public static string Token = string.Empty;
		public static string Address = string.Empty;
		public static int Port = 0;
		static void Main(string[] args)
		{
			Init();
			Connect();
			Login();
			Console.WriteLine("You're Logged In");
			MainLoop();
		}
		static void Init()
		{
			new CommandHandeler();
		}
		public static void ShowErr(IMTPResponse error)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"Error Encoutered: {error.Message}");
			Console.WriteLine($"Error Code: {error.StatusCode}");
			Console.ResetColor();
			Console.WriteLine("Aditional Data: ");
			foreach (KeyValuePair<string,object> item in error.Data)
			{
				Console.WriteLine(item.Key + " - " + JsonConvert.SerializeObject(item.Value));
			}
		}
		static void Connect()
		{
			while (true)
			{
				Console.Write("Address >");
				string address = Console.ReadLine();
				Console.Write("Port >");
				try
				{
					int port;
					if (!address.Contains(":"))
					{
						port = int.Parse(Console.ReadLine());
					}
					else
					{
						port = int.Parse(address.Split(':')[1]);
						address = address.Split(':')[0];
						Console.WriteLine(port);
					}
					_Client = new IMTPClient(address, port);
					Address = address;
					Port = port;
					break;
				}
				catch
				{
					Console.WriteLine("Incorrect Data");
					Console.ReadKey(true);
					Console.Clear();
				}
			}
			Console.WriteLine("Connected Succesfully");
		}
		static void Login()
		{
			while (true)
			{
				Console.Write("Login >");
				string login = Console.ReadLine();
				Console.CursorVisible = false;
				Console.Write("Password >");
				Console.ForegroundColor = Console.BackgroundColor;
				string password = Console.ReadLine();
				Console.ResetColor();
				Console.CursorVisible = true;
				Dictionary<string, object> data = new Dictionary<string, object>()
				{
					{ "Login", login },
					{ "Password", password }
				};
				Task<IMTPResponse> task = _Client.SendRequest("/adminlogin", data);
				task.Wait();
				IMTPResponse response = task.Result;
				if (response.StatusCode != (int)IMTPStatusCode.OK)
				{
					ShowErr(response);
					continue;
				}
				if ((string)response.Data["Token"] != "0")
				{
					Token = (string)response.Data["Token"];
					break;
				}
				Console.WriteLine("Login Failed!");
			}
		}
		static void MainLoop()
		{
			while (true)
			{
				Console.Write($"{Address}:{Port}>");
				if (!CommandHandeler.Run(Console.ReadLine()))
				{
					Console.WriteLine("Command Not Found!");
				}
			}
		}
	}
}

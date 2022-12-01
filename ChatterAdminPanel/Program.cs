using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Chatter.AdminPanel.Transfer;

namespace Chatter.AdminPanel
{
	public class Program
	{
		public static SimpleTcpClient _Client;
		static void Main(string[] args)
		{
			bool correct = true;
			_Client = new SimpleTcpClient
			{
				StringEncoder = Encoding.UTF8,
				AutoTrimStrings = true,
				Delimiter = (byte)'\n',
			};
            #region Connectiong
            do
            {
				Console.Write("IP >");
				string address = Console.ReadLine();
				Console.Write("Port >");
				try
				{
					int port = int.Parse(Console.ReadLine());
					_Client.Connect(address, port);
					correct = true;
				}
				catch
				{
					Console.WriteLine("Incorrect Data");
					Console.ReadKey(true);
					Console.Clear();
					correct = false;
				}
			} while (!correct);
			Console.WriteLine("Connected Succesfully");
            #endregion
            #region Login
            do
            {
				Console.Write("Login >");
				string login = Console.ReadLine();
				Console.CursorVisible = false;
				Console.Write("Password >");
				Console.ForegroundColor = Console.BackgroundColor;
				string password = Console.ReadLine();
				Console.ResetColor();
				Console.CursorVisible = true;
				if (true)
				{
					LoginTransfer transfer = new LoginTransfer
					{
						Login = login,
						Password = password,
					};
					string data = JsonConvert.SerializeObject(transfer);
					Message reply = _Client.WriteLineAndGetReply("login " + data,TimeSpan.FromSeconds(300));
					string token = reply.MessageString;
					if (token == "0")
					{
						correct = false;
					}
					else
					{
						correct = true;
					}
				}
			} while (!correct);
            #endregion
        }
    }
}

using SimpleTCP;
using System;
using System.Text;
using Newtonsoft.Json;
using Chatter.AdminPanel.Transfer;

namespace Chatter.AdminPanel
{
	public class Program
	{
		public static SimpleTcpClient _Client;
        public static string Token = string.Empty;
		static void Main(string[] args)
		{
            Init();
            Connect();
            Login();
			Console.WriteLine("Your logged");
			Console.ReadKey(true);
        }
		static void Init()
		{
            _Client = new SimpleTcpClient
            {
                StringEncoder = Encoding.UTF8,
                AutoTrimStrings = true,
                Delimiter = (byte)'\n',
            };
        }
		static void Connect()
		{
            while (true)
            {
                Console.Write("IP >");
                string address = Console.ReadLine();
                Console.Write("Port >");
                try
                {
                    int port = int.Parse(Console.ReadLine());
                    _Client.Connect(address, port);
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
                    Message reply = _Client.WriteLineAndGetReply("login\n0\n" + data, TimeSpan.FromSeconds(300));
                    if (reply.MessageString != "0")
                    {
                        Token = reply.MessageString;
                        break;
                    }
                }
            } while (true);
        }
    }
}

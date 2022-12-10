using SimpleTCP;
using System;
using System.Text;
using Newtonsoft.Json;
using Chatter.AdminPanel.Transfer;
using Chatter.AdminPanel.Commands;

namespace Chatter.AdminPanel
{
	public class Program
	{
		public static SimpleTcpClient _Client;
        public static string Token = string.Empty;
        public static string Address = string.Empty;
        public static int Port = 0;
		static void Main(string[] args)
		{
            Init();
            Connect();
            Login();
			Console.WriteLine("Your Logged In");
            MainLoop();
        }
		static void Init()
		{
            new CommandHandeler();
            _Client = new SimpleTcpClient
            {
                StringEncoder = Encoding.UTF8,
                AutoTrimStrings = true,
                Delimiter = (byte)'\n',
            };
        }
        static void ShowErr(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            switch (error)
            {
                case "E-TKN":
                    Console.WriteLine("Token Error");
                    break;
                case "E-DAT":
                    Console.WriteLine("Sent Data Error");
                    break;
                default:
                    Console.WriteLine("Un Expected Error");
                    break;
            }
            Console.WriteLine("Error Code: " + error);
            Console.ResetColor();
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
                    _Client.Connect(address, port);
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
        static void MainLoop()
        {
            while (true)
            {
                CommandHandeler.Run(Console.ReadLine());
            }
        }
    }
}

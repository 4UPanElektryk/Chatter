using Chatter.Server.CommandService;
using Chatter.Server.MessageService;
using Chatter.Server.UserService;
using SimpleLogs4Net;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Chatter.Server
{
    public class Program
    {
        public static SimpleTcpServer server;
        public static List<TcpClient> Clients;
        public static DateTime LastChange;
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
            server.Start(IPAddress.Parse(Config.Data.ServerIPAddress), Config.Data.ServerPort);
            if (server.IsStarted)
            {
                Log.Write("Server Started on " + Config.Data.ServerIPAddress + ":" + Config.Data.ServerPort, EType.Informtion);
            }
            LastChange = DateTime.UtcNow;
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
            Log.AddEvent(new Event(e.MessageString.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None), EType.Normal));
            if (CommandHandeler.MakesImpact(e.MessageString.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)[0]))
            {
                LastChange = DateTime.UtcNow;
            }
            string reply = CommandHandeler.Run(e.MessageString);
            Log.Write(reply);
            e.ReplyLine(reply);
        }
    }
}

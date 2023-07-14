using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using SimpleTCP;
using System.Threading;

namespace IMTP.Server
{
	public struct LogData
	{
		public string Sender;
		public DateTime Time;
		public string Message;
	}
	public class IMTPServer
	{
		private readonly SimpleTcpServer server;
		public delegate IMTPResponse HandleRequestdel(IMTPRequest requestInfo);
		public HandleRequestdel HandleRequest;
		public EventHandler<LogData> Log;
		public int port = 87;
		public IMTPServer()
		{
			server = new SimpleTcpServer()
			{
				Delimiter = (byte)'\n',
				AutoTrimStrings = true,
				StringEncoder = Encoding.UTF8
			};
			server.DataReceived += ProcessRequest;
		}

		public void Start(IPAddress address)
		{
			server.Start(address,port);
			Console.WriteLine("Server started. Listening for requests...");
			Thread.Sleep(-1);
		}
		private async void ProcessRequest(object sender, Message context)
		{
			#region Request Data
			string[] lines = context.MessageString.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
			IMTPVersion protocol = String2Version(lines[0].Split(' ')[0]);
			string path = lines[0].Split(' ')[1];
			string jsonData = context.MessageString.Substring(context.MessageString.IndexOf("{"));
			#endregion
			Log.Invoke(this, new LogData() { Time = DateTime.UtcNow, Message = context.MessageString, Sender = "Client" });
			//Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}]Client>\n{context.MessageString}");
			#region Request Processing
			Dictionary<string, object> requestData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
			IMTPResponse response = HandleRequest.Invoke(new IMTPRequest() {Version = protocol, Path = path, Data = requestData });
			response.Message = Code2Message(response.StatusCode,response.Message);
			#endregion
			#region Serializing Response
			string responseContent = $"{response.StatusCode}/{response.Message}\r\n";
			if (response.Data != null)
			{
				responseContent += JsonConvert.SerializeObject(response.Data);
			}
			#endregion
			// Reply
			Log.Invoke(this, new LogData() { Time = DateTime.UtcNow, Message = responseContent, Sender = "Server" });
			//Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}]Server>\n{responseContent}");
			context.ReplyLine(responseContent);
		}
		internal IMTPVersion String2Version(string s)
		{
			if (s == "IMTP/1.0")
			{
				return IMTPVersion.IMTP1_0;
			}
			return IMTPVersion.IMTP1_0;
		}
		public string Code2Message(int code, string message = null)
		{
			if (message != null)
			{
				return message;
			}
			switch (code)
			{
				case 0: return "OK";
				case 1: return "Not Found";
				case 2: return "Not Supported";
				case 3: return "Incorrect Data";
				case 4: return "Authentication Needed";
				case 5: return "Authentication Error";
				case 6: return "Forbidden";
				default: return "Internal Server Error";
			}
		}
	}
}

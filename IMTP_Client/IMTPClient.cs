using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleTCP;

namespace ConsoleApp1
{

	public class IMTPClient
	{
		private readonly SimpleTcpClient Client;

		public IMTPClient(string serverUrl, int port = 87)
		{
			Client = new SimpleTcpClient()
			{
				Delimiter = (byte)'\n',
				AutoTrimStrings = true,
				StringEncoder = Encoding.UTF8,
			};
			Client.Connect(serverUrl, port);
		}

		public async Task<ProtocolResponse> SendRequest(string path, Dictionary<string, object> requestData)
		{
			string jsonRequestData = JsonConvert.SerializeObject(requestData);
			string requestContent = $"IMTP/1.0 {path}\r\n{jsonRequestData}";
			
			var response = Client.WriteLineAndGetReply(requestContent,TimeSpan.FromSeconds(3));
			return ParseResponse(response.MessageString);
		}

		private ProtocolResponse ParseResponse(string responseContent)
		{
			string[] lines = responseContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
			int statusCode = int.Parse(lines[0].Split('/')[0]);
			string message = lines[0].Split('/')[1];
			string jsonContent = responseContent.Substring(lines[0].Length).Trim();

			var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);

			return new ProtocolResponse
			{
				StatusCode = statusCode,
				Message = message,
				Data = responseData
			};
		}
	}

	public struct ProtocolResponse
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public Dictionary<string, object> Data { get; set; }
	}
}

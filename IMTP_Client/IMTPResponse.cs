using System.Collections.Generic;

namespace IMTP.Client
{
	public struct IMTPResponse
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public Dictionary<string, object> Data { get; set; }
	}
}

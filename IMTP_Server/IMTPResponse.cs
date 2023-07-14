using System.Collections.Generic;

namespace IMTP.Server
{
	public struct IMTPResponse
	{
		public IMTPResponse(int StatusCode, string Message = null)
		{
			this.StatusCode = StatusCode;
			this.Message = Message;
			Data = null;
		}
		public IMTPResponse(IMTPStatusCode StatusCode, string Message = null)
		{
			this.StatusCode = (int)StatusCode;
			this.Message = Message;
			this.Data = null;
		}
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public Dictionary<string, object> Data { get; set; }
	}
}

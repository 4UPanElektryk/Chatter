using System.Collections.Generic;
namespace IMTP.Server
{
	public struct IMTPRequest
	{
		public string Path;
		public IMTPVersion Version;
		public Dictionary<string, object> Data;
	}
}

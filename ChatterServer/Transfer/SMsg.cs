using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chatter.Server.Transfer
{
	public struct SMsg
	{
		public Color Color { get; set; }
		public int MessageID { get; set; }
		public string UserName { get; set; }
		public DateTime Sent { get; set; }
		public string[] Message { get; set; }
	}
}

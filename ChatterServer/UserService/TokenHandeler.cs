using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.Server.UserService
{
	public class TokenHandeler
	{
		private static Dictionary<string, int> ActiveTokens;
		public TokenHandeler()
		{
			ActiveTokens = new Dictionary<string, int>();
		}
		public static string AddToken(User user)
		{
			if (user == null)
			{
				return "0";
			}
			Random random = new Random();
			int num = random.Next();
			string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(num.ToString()));
			while (ActiveTokens.ContainsKey(token))
			{
				token = Convert.ToBase64String(Encoding.UTF8.GetBytes(num.ToString()));
			}
			ActiveTokens.Add(token, user.Id);
			return token;
		}
		public static User GetUser(string token)
		{
			if (token == "0")
			{
				return null;
			}
			foreach (var item in ActiveTokens)
			{
				if (item.Key == token)
				{
					return UserHandeler.GetUser(item.Value);
				}
			}
			return null;
		}
	}
}

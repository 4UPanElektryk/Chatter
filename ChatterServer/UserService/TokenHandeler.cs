using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.UserService
{
	public class TokenHandeler
	{
		private static Dictionary<string, int> ActiveTokens;
		public TokenHandeler() 
		{
			ActiveTokens= new Dictionary<string, int>();
		}
		public static string AddToken(User user)
		{
			Random random = new Random();
			int num = random.Next();
			string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(num.ToString()));
            ActiveTokens.Add(token, user.Id);
			return token;
		}

		public static User GetUser(string token) 
		{
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

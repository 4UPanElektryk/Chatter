using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chatter.Server.UserService
{
	public class UserHandeler
	{
		public static List<User> users;
		public static string Path;
		public UserHandeler(string path) 
		{
			users = new List<User>();
			Path = path;
		}
		public static void Load()
		{
			if (!File.Exists(Path))
			{
				User admin = new User
				{
					Id = 0,
					Name = "ADMIN",
					IsAdmin = true,
					TextColor = Color.Red,
					Password = "01234567"
				};
				users.Add(admin);
				return;
			}
			users = (List<User>)JsonConvert.DeserializeObject(File.ReadAllText(Path));
		}
		public static void Save() 
		{
			File.WriteAllText(Path,JsonConvert.SerializeObject(users,Formatting.Indented));
		}
		public static User GetUser(int id)
		{
			foreach (User user in users)
			{
				if (user.Id == id)
				{
					return user;
				}
			}
			return null;
		}
		public static void AddUser(User user)
		{
			users.Add(user);
		}
		public static int GetNewId() 
		{
			int id = 0;
			users.ForEach(user => { if (user.Id >= id) { id = user.Id + 1; } });
			return id;
		}
	}
}

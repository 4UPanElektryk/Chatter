using Newtonsoft.Json;
using SimpleLogs4Net;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
			Load();
		}
		public static void Load()
		{
			if (!File.Exists(Path))
			{
				Log.Write("User Database file missing: " + Path, EType.Warning);
				User admin = new User
				{
					Id = 0,
					Name = "ADMIN",
					IsAdmin = true,
					TextColor = Color.Red,
					Password = "01234567"
				};
				users.Add(admin);
				Save();
				return;
			}
			Log.Write("Loading User database from: " + Path, EType.Informtion);
			users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(Path));
		}
		public static void Save()
		{
			Log.Write("Saving User database to: " + Path, EType.Informtion);
			File.WriteAllText(Path, JsonConvert.SerializeObject(users, Formatting.Indented));
		}
		public static void SetUserColor(Color color, int id)
		{
			User user = GetUser(id);
			users.Remove(user);
			user.TextColor = color;
			users.Add(user);
			Save();
		}
		public static void ChangePassword(string password, int id)
		{
			User user = GetUser(id);
			users.Remove(user);
			user.Password = password;
			users.Add(user);
			Save();
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
		public static User GetUser(string Login, string Password)
		{
			foreach (User user in users)
			{
				if (user.Name == Login && user.Password == Password)
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
		public static bool LoginInUse(string login)
		{
			foreach (User item in users)
			{
				if (item.Name == login)
				{
					return true;
				}
			}
			return false;
		}
	}
}

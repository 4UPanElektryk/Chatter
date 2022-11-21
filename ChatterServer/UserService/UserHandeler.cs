using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.UserService
{
    public class UserHandeler
    {
        public static List<User> users = new List<User>();
        public UserHandeler() 
        {
            users = new List<User>();
            new TokenHandeler();
        }
        public static User GetUser(int id)
        {
            return users[id];
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

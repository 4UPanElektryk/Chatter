using System.Drawing;

namespace Chatter.Server.UserService
{
    public class User
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
        public string _Password { get; set; }
        public bool _IsAdmin { get; set; }
        public Color _TextColor { get; set; }
        public User() { }
    }
}

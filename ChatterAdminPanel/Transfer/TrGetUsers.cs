using System.Drawing;

namespace Chatter.Server.Transfer
{
    public struct TrUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public Color TextColor { get; set; }
    }
}

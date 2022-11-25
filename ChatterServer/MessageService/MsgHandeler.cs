using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server.MessageService
{
    public class MsgHandeler
    {
        public static List<Msg> msgs;
        public static string Path;
        public MsgHandeler(string path)
        {
            msgs = new List<Msg>();
            Path = path;
        }
        public static void Load()
        {
            if (!File.Exists(Path))
            {
                return;
            }
            msgs = (List<Msg>)JsonConvert.DeserializeObject(File.ReadAllText(Path));
        }
        public static void AddMsg(Msg msg)
        {
            msg.Sent = DateTime.UtcNow;
            msg.MessageID = GetNewID();
            msgs.Add(msg);
        }
        public static int GetNewID()
        {
            int ID = 0;
            msgs.ForEach(msg => { if (msg.MessageID >= ID) { ID = msg.MessageID; } });
            return ID++;
        }
    }
}

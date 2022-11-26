using Newtonsoft.Json;
using SimpleLogs4Net;
using System;
using System.Collections.Generic;
using System.IO;

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
            Load();
        }
        public static void Load()
        {
            if (!File.Exists(Path))
            {
                Log.Write("Message Database file missing: " + Path, EType.Warning);
                return;
            }
            Log.Write("Loading Message database from: " + Path, EType.Informtion);
            msgs = JsonConvert.DeserializeObject<List<Msg>>(File.ReadAllText(Path));
        }
        public static void Save()
        {
            Log.Write("Saving Message database to: " + Path, EType.Informtion);
            File.WriteAllText(Path,JsonConvert.SerializeObject(msgs,Formatting.Indented));
        }
        public static void AddMsg(Msg msg)
        {
            msg._Sent = DateTime.UtcNow;
            int id = GetNewID();
            Log.Write("Message Added to Database: " + id);
            msg._MessageID = id;
            msgs.Add(msg);
            Save();
        }
        public static int GetNewID()
        {
            int ID = 0;
            msgs.ForEach(msg => { if (msg._MessageID >= ID) { ID = msg._MessageID; } });
            return ID++;
        }
    }
}

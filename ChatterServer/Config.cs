using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Server
{
    public class Config
    {
        public static string Path;
        public static TConfig Data;
        public Config(string path) 
        {
            Path = path;
        }
        public static void Load()
        {
            if (!File.Exists(Path))
            {
                Data = new TConfig
                {
                    ServerName = "StandardChatterServer",
                    ServerVersion = "1.0.0",
                    ServerIPAddress = "127.0.0.1",
                    ServerPort = 87,
                    UserBaseFile = "UserDataBase.json",
                    MessagebaseFile = "MessageDataBase.json",
                    LogsDirectory = "Logs\\",
                    LogsPrefix = "Log"
                };
                Save();
                return;
            }
            Data = JsonConvert.DeserializeObject<TConfig>(File.ReadAllText(Path));
        }
        public static void Save()
        {
            File.WriteAllText(Path,JsonConvert.SerializeObject(Data,Formatting.Indented));
        }
    }
}

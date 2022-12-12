namespace Chatter.Server
{
    public class TConfig
    {
        public string ServerName { get; set; }
        public string ServerVersion { get; set; }
        public string ServerIPAddress { get; set; }
        public int ServerPort { get; set; }
        public string UserBaseFile { get; set; }
        public string MessagebaseFile { get; set; }
        public string LogsDirectory { get; set; }
        public string LogsPrefix { get; set; }
        public TConfig() { }
    }
}

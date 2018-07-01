namespace OctoBot.Configs.Server
{
    public class ServerSettings 
    {
        public string ServerName { get; set; }
        public ulong ServerId { get; set; }
        public string Prefix { get; set; }
        public string Language { get; set; }
        public int ServerActivityLog { get; set; }
        public ulong LogChannelId { get; set; }
        public string RoleOnJoin { get; set; }

    }
}

using System.Collections.Generic;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.IO;

namespace OctoBot.Configs
{

    internal class Global
    {
        internal static ulong MessageIdToTrack  { get; set; }
     
        internal static DiscordSocketClient Client { get; set; }
        internal static ulong PlayingUserId { get; set; }

        internal static ulong YellowTurlteChannelId { get; set; }
        internal static RestUserMessage YellowTurlteMessageTorack { get; set; }
        internal static int CommandEnabled { get; set; }
      


        internal static int OctoGamePlaying { get; set; }
        public static List<OctoGameMessAndUserTrack> OctopusGameMessIdList { get; internal set; } = new List<OctoGameMessAndUserTrack>();

        public struct OctoGameMessAndUserTrack
        {
            public ulong OctoGameMessIdToTrack;
            public ulong OctoGameUserIdToTrack;
            public RestUserMessage SocketMsg;

            public OctoGameMessAndUserTrack(ulong octoGameMessIdToTrack, ulong octoGameUserIdToTrack, RestUserMessage socketMsg)
            {
                OctoGameMessIdToTrack = octoGameMessIdToTrack;
                OctoGameUserIdToTrack = octoGameUserIdToTrack;
                SocketMsg = socketMsg;
            }
        }



    }

    internal class Config
    {
       

        public static BotConfig Bot;

                                    
                     
        static Config()
        {
 

                var json = File.ReadAllText(@"OctoDataBase/config.json");
                Bot =  JsonConvert.DeserializeObject<BotConfig>(json);

            

        }
    }
    public struct BotConfig
    {
        public string Token;
        public string Prefix;
     
    }
}

using System.Collections.Generic;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.IO;
using Discord;

namespace OctoBot.Configs
{

    internal class Global
    {
       
     
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
            public IUser Iuser;
            

            public OctoGameMessAndUserTrack(ulong octoGameMessIdToTrack, ulong octoGameUserIdToTrack, RestUserMessage socketMsg, IUser iuser)
            {
                OctoGameMessIdToTrack = octoGameMessIdToTrack;
                OctoGameUserIdToTrack = octoGameUserIdToTrack;
                SocketMsg = socketMsg;
                Iuser = iuser;
               
            }
        }

        public static List<OctoGameMessAndUserTrack2048> OctopusGameMessIdList2048 { get; internal set; } = new List<OctoGameMessAndUserTrack2048>();

        public struct OctoGameMessAndUserTrack2048
        {
            public ulong OctoGameMessIdToTrack2048;
            public ulong OctoGameUserIdToTrack2048;
            public RestUserMessage SocketMsg;
            public IUser Iuser;

            public OctoGameMessAndUserTrack2048(ulong octoGameMessIdToTrack2048, ulong octoGameUserIdToTrack2048, RestUserMessage socketMsg, IUser iuser)
            {
                OctoGameMessIdToTrack2048 = octoGameMessIdToTrack2048;
                OctoGameUserIdToTrack2048 = octoGameUserIdToTrack2048;
                SocketMsg = socketMsg;
                Iuser = iuser;
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

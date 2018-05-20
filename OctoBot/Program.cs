using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Automated;
using OctoBot.Commands;
using OctoBot.Commands.PersonalCommands;
using OctoBot.Commands.ShadowCItyCOmmand;
using OctoBot.Configs;
using OctoBot.Games.Game2048;
using OctoBot.Games.OctoGame;
using OctoBot.Handeling;

namespace OctoBot
{

    internal class ProgramOctoBot
    {

        DiscordSocketClient _client;
        CommandHandeling _handler;


        private static void Main() => new ProgramOctoBot().RunBotAsync().GetAwaiter().GetResult();

        public async Task RunBotAsync()
        {
            if (string.IsNullOrEmpty(Config.Bot.Token)) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            var discordSocketConfig = new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 10000
            };

            _client = new DiscordSocketClient(discordSocketConfig);
            var botToken = Config.Bot.Token;

            //event subsciption
            _client.Log += Logger.Log;

           _client.ReactionAdded += Reaction.ReactionAddedFor2048;                                                        
           _client.ReactionAdded += OctoGameReaction.ReactionAddedForOctoGameAsync;
           _client.ReactionAdded += ColorRoleReaction.ReactionAddedForRole;
            _client.ReactionAdded += RoomRoleReaction.ReactionAddedForRole;
            _client.Ready += GreenBuuTimerClass.StartTimer;             ////////////// Timer1 Green Boo starts
            _client.Ready += DailyPull.CheckTimerForPull;                 ////////////// Timer3 For Pulls   
            _client.Ready += Reminder.CheckTimer;                       ////////////// Timer4 For For Reminders
            _client.Ready += ForBot.TimerForBotAvatar;   
            _client.UserJoined += Announcer.AnnounceUserJoin;
            _client.Ready += EveryLogHandeling._client_Ready;
           
         
            //  _client.Ready += YellowTurtle.StartTimer; //// Timer3


            await _client.SetGameAsync("Осьминожек! | *help");
            _handler = new CommandHandeling();
            await _handler.InitializeAsync(_client);
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();
            Global.Client = _client;

            ConsoleHandeling.ConsoleInput(_client);
            await Task.Delay(-1);
        }
    }
}


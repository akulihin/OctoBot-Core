using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Automated;
using OctoBot.Commands;
using OctoBot.Commands.PersonalCommands;
using OctoBot.Configs;
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
       
    
            var botToken = Config.Bot.Token;

            //event subsciption
            _client.Log += Log;
            _client.ReactionAdded += Reaction.ReactionAddedFor2048;   ////////////// УДАЛИ ЭТО ГОВНО
                                                                     
           _client.ReactionAdded += OctoGameReaction.ReactionAddedForOctoGameAsync;

            _client.Ready += GreenBuuTimerClass.StartTimer;             ////////////// Timer1 Green Boo starts
           
            _client.Ready += DailyPull.CheckTimerForPull;                 ////////////// Timer3 For Pulls   
            _client.Ready += Reminder.CheckTimer;                       ////////////// Timer4 For For Reminders
            _client.UserJoined += Announcer.AnnounceUserJoin;
            //  _client.Ready += YellowTurtle.StartTimer; //// Timer3


            await _client.SetGameAsync("Осьминожек!");
            _handler = new CommandHandeling();
            await _handler.InitializeAsync(_client);
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();
            Global.Client = _client;

            ConsoleHandeling.ConsoleInput(_client);
            await Task.Delay(-1);
        }
        private static Task Log(LogMessage arg)
        {

            
            Console.WriteLine(arg.Message);

            return Task.CompletedTask;
        }
    }
}


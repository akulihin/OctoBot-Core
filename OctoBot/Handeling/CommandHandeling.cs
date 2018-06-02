using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.LvLingSystem;

namespace OctoBot.Handeling
{
   public class CommandHandeling
    {
      
        DiscordSocketClient _client;
        CommandService _service;                   
        private static string LogFile = @"OctoDataBase/Log.json";
       
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Await for user input in chat
 
        public static async Task<SocketMessage> AwaitMessage(ulong userId, ulong channelId, int delayInMs)
        {
            SocketMessage response = null;
            var cancler = new CancellationTokenSource();
            var waiter = Task.Delay(delayInMs, cancler.Token);

            Global.Client.MessageReceived += OnMessageReceived;
            try { await waiter; }
            catch (TaskCanceledException) { }
            Global.Client.MessageReceived -= OnMessageReceived;
           
            return response;

            async Task OnMessageReceived(SocketMessage message)
            {

                if (message.Author.Id != userId || message.Channel.Id != channelId)
                    return;
                response = message;
                cancler.Cancel();
                await Task.CompletedTask;
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync;
           
        }
        private async Task HandleCommandAsync(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;

        

            if (message == null) return;
            var context = new SocketCommandContext(_client, message);
            var argPos = 0;


            if (message.Channel is SocketDMChannel)
            {
                if (context.User.IsBot) return;
                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)
                {
                    Console.ForegroundColor = LogColor("red");
                    Console.WriteLine(
                        $"{DateTime.Now.ToLongTimeString()} - DM: ERROR '{context.Channel}' {context.User}: {message} || {result.ErrorReason}");
                    Console.ResetColor();

                    File.AppendAllText(LogFile,
                        $"{DateTime.Now.ToLongTimeString()} - DM: ERROR '{context.Channel}' {context.User}: {message} || {result.ErrorReason} \n");
                }

                else
                {
                    Console.ForegroundColor = LogColor("white");
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - DM: '{context.Channel}' {context.User}: {message}");
                    Console.ResetColor();

                    File.AppendAllText(LogFile,
                        $"{DateTime.Now.ToLongTimeString()} - DM: '{context.Channel}' {context.User}: {message} \n");
                }

                return;
            }


            // Leveling up
                LvLing.UserSentMess((SocketGuildUser)context.User, (SocketTextChannel)context.Channel, message);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
          
            if (message.HasStringPrefix(Config.Bot.Prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {

                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)
                {
                    Console.ForegroundColor = LogColor("red");
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - ERROR '{context.Channel}' { context.User}: {message} || {result.ErrorReason}");
                    Console.ResetColor();

                    File.AppendAllText(LogFile, $"{DateTime.Now.ToLongTimeString()} - ERROR '{context.Channel}' { context.User}: {message} || {result.ErrorReason} \n");
                    var wordsCount = message.ToString().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                   if(wordsCount.Length <= 4)
                   await WrongCommand.ErrorCommandReply(msg);
                }
                else
                {
                    Console.ForegroundColor = LogColor("white");
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - '{context.Channel}' {context.User}: {message}");
                    Console.ResetColor();

                    File.AppendAllText(LogFile, $"{DateTime.Now.ToLongTimeString()} - '{context.Channel}' {context.User}: {message} \n");
                  
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static ConsoleColor LogColor(string color)
        {
            switch (color)
            {
                case "red":  //Critical or Error
                    return ConsoleColor.Red;
                case "green":    //Debug
                    return ConsoleColor.Green;
                case "cyan":     //Info
                    return ConsoleColor.Cyan;
                case "white":   //Regular
                    return ConsoleColor.White;
                case "yellow":  // Warning
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.White;
            }
        }


    }
}

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.LvLingSystem;

namespace OctoBot.Handeling
{
    class CommandHandeling
    {
      
        DiscordSocketClient _client;
        CommandService _service;                   
        private static string LogFile = @"OctoDataBase/Log.json";
       
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    Console.WriteLine(
                        $"ERROR :DM: {message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message} || {result.ErrorReason}");

                    File.AppendAllText(LogFile,
                        $"ERROR :DM: {message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message} || {result.ErrorReason} \n");
                }

                else
                {
                    Console.WriteLine($":DM: {message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message}");

                    File.AppendAllText(LogFile,
                        $":DM: {message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message} \n");
                }

                return;
            }


            // Leveling up
                LvLing.UserSentMess((SocketGuildUser)context.User, (SocketTextChannel)context.Channel, message);
         
            /////////////////////////////////////////CAPS Check////////////////////////////////////////////////////////////
           var upper = 0;
            for (int i = 0; i < message.ToString().Length; i++)
            {
                if (Char.IsLetter(message.ToString()[i]) && Char.IsUpper(message.ToString()[i]) || Char.IsNumber(message.ToString()[i])
                    || Char.IsPunctuation(message.ToString()[i]) || Char.IsSurrogate(message.ToString()[i]) || Char.IsDigit(message.ToString()[i]) || Char.IsControl(message.ToString()[i])
                    || Char.IsSeparator(message.ToString()[i]) || Char.IsHighSurrogate(message.ToString()[i]) )
                {
                    upper++;
                }

                if (upper == message.ToString().Length && upper > 50)
                {
                    
                    var embed = new EmbedBuilder();
                    embed.WithAuthor(message.Author);
                    embed.WithDescription("Буль-БУУУУЛЬ! А ну перестань капсить!");
                    embed.WithImageUrl("https://i.imgur.com/5MRKrbr.jpg");
                    await message.Channel.SendMessageAsync("", embed: embed);
                    Console.WriteLine($"CAPS: {message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message}");
                    File.AppendAllText(LogFile, $"CAPS: {message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message} \n");
                   
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////LOL_Commands////////////////////////////////////////////////////////
            if (message.HasStringPrefix("Я ", ref argPos) || message.HasStringPrefix("Кто ", ref argPos)
                || message.HasStringPrefix("я ", ref argPos) || message.HasStringPrefix("кто ", ref argPos) || message.HasStringPrefix("КТО ", ref argPos) || message.HasStringPrefix("КТО", ref argPos)
                || message.HasStringPrefix("кто", ref argPos) || message.HasStringPrefix("Кто", ref argPos))
            {

                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)
                {
                   // Console.WriteLine($"ERROR { message.CreatedAt.UtcDateTime} '{context.Channel}' { context.User}: {message} || {result.ErrorReason}");

                    File.AppendAllText(LogFile, $"ERROR { message.CreatedAt.UtcDateTime} '{context.Channel}' { context.User}: {message} || {result.ErrorReason} \n");
                }

                else
                {
                    Console.WriteLine($"LOL: {message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message}");

                    File.AppendAllText(LogFile, $"LOL: {message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message} \n");
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
          
            if (message.HasStringPrefix(Config.Bot.Prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {

                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)
                {
                    Console.WriteLine($"ERROR { message.CreatedAt.UtcDateTime} '{context.Channel}' { context.User}: {message} || {result.ErrorReason}");

                    File.AppendAllText(LogFile, $"ERROR { message.CreatedAt.UtcDateTime} '{context.Channel}' { context.User}: {message} || {result.ErrorReason} \n");
                }

                else
                {
                    Console.WriteLine($"{message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message}");

                    File.AppendAllText(LogFile, $"{message.CreatedAt.UtcDateTime} '{context.Channel}' {context.User}: {message} \n");
                }
            }
        }


    }
}

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using Color = Discord.Color;


namespace OctoBot.Commands.PersonalCommands
{
    public class ForBot : ModuleBase<SocketCommandContext>
    {
        private static string LogFile = @"OctoDataBase/AI.json";
        

        private static Timer _loopingTimerForOctoAva;
      
        internal static Task TimerForBotAvatar()
        {


            _loopingTimerForOctoAva = new Timer
            {
                AutoReset = true,
                Interval = 3600000  ,
                Enabled = true
            };
            _loopingTimerForOctoAva.Elapsed += SetBotAva;


            return Task.CompletedTask;
        }

        public static async void SetBotAva(object sender, ElapsedEventArgs e)
        {
            try
            {
                var rand = new Random();
                var randomIndex = rand.Next(OctoPull.OctoPics.Length);
                var octoToPost = OctoPull.OctoPics[randomIndex];

                var webClient = new WebClient();
                var imageBytes = webClient.DownloadData(octoToPost);

                var stream = new MemoryStream(imageBytes);

                var image = new Image(stream);
                await Global.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);

            }
            catch 
            {

              //
             
            }
        }

        [Command("text")]
        public async Task YoKErateMate([Remainder] string mess)
        {
            File.AppendAllText(LogFile, $"{mess}\n");
          await  Task.CompletedTask;

        }


        [Command("setAvatar")]
        [Alias("ava")]
        public async Task SetAvatar(string link)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {   
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(link);

                var stream = new MemoryStream(imageBytes);

                var image = new Image(stream);
                await Context.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);
                await Context.Message.DeleteAsync();
                await ReplyAsync($"Avatar have been set to `{link}`");
            }
            catch (Exception e)
            {
                var embed = new EmbedBuilder();
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.AddField("Ошибка", $"Не можем поставить аватарку: {e.Message}");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
        }

        [Command("Game"), Alias("ChangeGame", "SetGame")]
        public async Task SetGame([Remainder] string gamename)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                await Context.Client.SetGameAsync(gamename);
                await ReplyAsync($"Changed game to `{gamename}`");
                await Context.Message.DeleteAsync();
            }
            catch (Exception e)
            {
                var embed = new EmbedBuilder();
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.AddField("Ошибка", $"Не можем изменить игру: {e.Message}");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }

        }

        [Command("username")]
        public async Task ChangeUsername([Remainder] string name)
        {
            await Context.Client.CurrentUser.ModifyAsync(usr => usr.Username = name);
            await ReplyAsync($":ok_hand: Changed my username to {name}");
        }

        [Command("nickname")]
        [RequireContext(ContextType.Guild)]
        public async Task ChangeNickname([Remainder] string name)
        {
            await Context.Guild.GetUser(Context.Client.CurrentUser.Id).ModifyAsync(usr => usr.Nickname = name);
            await ReplyAsync($":ok_hand: Changed my Nickname to {name}");
        }

        [Command("nick")]
        public async Task Nickname(SocketGuildUser username, [Remainder]string name)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try{
            await Context.Guild.GetUser(username.Id).ModifyAsync(x => x.Nickname = name);
            }
            catch (Exception e)
            {
                var embed = new EmbedBuilder();
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.AddField("Ошибка", $"Не можем изменить ник: {e.Message}");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
        }

                
        [Command("ApdMessEm")]
        public async Task ApdMessEmbed(ulong channeld, ulong messId, [Remainder] string messa)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithColor(Color.Blue);
                // embed.WithFooter("Записная книжечка Осьминожек");
                embed.WithFooter("lil octo notebook");
                embed.AddInlineField("Сообщение:",
                    $"{messa}");

                var textChannel = await Context.Guild.GetTextChannel(channeld)
                    .GetMessageAsync(messId) as IUserMessage;

                await textChannel.ModifyAsync(mess =>
                {
                    mess.Embed = embed.Build();
                    // This somehow can't be empty or it won't update the 
                    // embed propperly sometimes... I don't know why
                    // message.Content =  Constants.InvisibleString;
                });
                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot update this message!");
            }
        }

        [Command("ApdMess")]
        public async Task ApdMessString(ulong channeld, ulong messId, [Remainder] string messa)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                var message = await Context.Guild.GetTextChannel(channeld)
                    .GetMessageAsync(messId) as IUserMessage;

                var builder = new StringBuilder();
                builder.Append($"{messa}");

                if (message != null) await message.ModifyAsync(m => m.Content = builder.ToString());
                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot update this message!");
            }
        }

        [Command("SendMess")]
        public async Task SendMessString(ulong channeld, [Remainder] string messa)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                var textChannel = Context.Guild.GetTextChannel(channeld);
                await textChannel.SendMessageAsync($"{messa}");

                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot send this message!");
            }
        }

        [Command("emo")]
        public async Task EmoteToMEss(ulong channeld, ulong messId, [Remainder] string messa)
        {

            try
            {
                Console.WriteLine(messa);
                if (Context.User.Id != 181514288278536193)
                    return;
                var emote = Emote.Parse($"{messa}");

                var check = messa.ToCharArray();
                if (check[0] == '<')
                    if (await Context.Guild.GetTextChannel(channeld).GetMessageAsync(messId) is IUserMessage message) await message.AddReactionAsync(emote);  
              else
                if (await Context.Guild.GetTextChannel(channeld).GetMessageAsync(messId) is IUserMessage mess) await mess.AddReactionAsync((new Emoji($"{messa}")));

                await Task.CompletedTask;
            }
            catch
            {
                //
            }
        }

    }
}
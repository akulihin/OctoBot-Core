using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using Discord.WebSocket;
using System.IO;
using System.Net;

namespace OctoBot.Commands.PersonalCommands
{
    public class ForBot : ModuleBase<SocketCommandContext>
    {

        
        private static Timer _loopingTimerForOctoAva;
      
        internal static Task TimerForBotAvatar()
        {


            _loopingTimerForOctoAva = new Timer()
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

    }
}
using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;

namespace OctoBot.Commands
{
    public class Managing : ModuleBase<SocketCommandContext>
    {

        static readonly SocketTextChannel LogTextChannel =
            Global.Client.GetGuild(375104801018609665).GetTextChannel(446868049589698561);

        [Command("purge")]
        [Alias("clean", "убрать", "clear")]

        //[RequireUserPermission(GuildPermission.Administrator)]
        public async Task Delete(int number)
        {
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 100)
            {

                var items = await Context.Channel.GetMessagesAsync(number + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(items);
                var embed = new EmbedBuilder()
                    .WithColor(Color.DarkRed)
                    .AddField($"**PURGE** {number} used", $"By {Context.User.Mention} in {Context.Channel}");
                await LogTextChannel.SendMessageAsync("", embed: embed);
            }
            else
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
        }

        [Command("warn")]
        [Alias("варн","предупреждение", "warning")]
       // [RequireUserPermission(GuildPermission.Administrator)]
        public async Task WarnUser(IGuildUser user, [Remainder]string message)
        {
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 100)
            {
                var time = DateTime.Now.ToString("");
            var account = UserAccounts.GetAccount((SocketUser)user);
            account.Warnings += $"{time} {Context.User}: " + message + "|";
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync(user.Mention + " Was Forewarned");

                var embed = new EmbedBuilder()
                    .WithColor(Color.DarkRed)
                    .AddField("**WARN** used", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                               $"**Content:**\n" +
                                               $"{user.Mention} - {message}");
                await LogTextChannel.SendMessageAsync("", embed: embed);
            }
            else
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");

        }

        [Command("kick")]
        [Alias("кик")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, string reason)
        {
            await user.KickAsync(reason);

            var embed = new EmbedBuilder()
                .WithColor(Color.DarkRed)
                .AddField("**kick** used", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                           $"**Content:**\n" +
                                           $"{user.Mention} - {reason}");
            await LogTextChannel.SendMessageAsync("", embed: embed);
        }

        [Command("ban")]
        [Alias("бан")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, string reason)
        {
            await user.Guild.AddBanAsync(user, 0, reason);

            var embed = new EmbedBuilder()
                .WithColor(Color.DarkRed)
                .AddField("**ban** used", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                           $"**Content:**\n" +
                                           $"{user.Mention} - {reason}");
            await LogTextChannel.SendMessageAsync("", embed: embed);
        }

        [Command("sleep")]
        public async Task SleepMode(double time)
        {
            
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 10000)
            {
           time = TimeSpan.FromMinutes(time).TotalMilliseconds;
            await Context.Channel.SendMessageAsync("Бууууль, спааатки");
            await Task.Delay((int)time);
            }
            else
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");

        }



    }
}

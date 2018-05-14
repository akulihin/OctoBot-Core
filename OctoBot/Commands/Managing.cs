using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;

namespace OctoBot.Commands
{
    public class Managing : ModuleBase<SocketCommandContext>
    {
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
            }
            else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска такого уровня!");
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
            await Context.Channel.SendMessageAsync(user.Mention + " Был Предупреждён");
            }
            else
                await Context.Channel.SendMessageAsync("буль-буууль, у тебя нет допуска такого уровня!");

        }

        [Command("kick")]
        [Alias("кик")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, string reason)
        {
            await user.KickAsync(reason);
        }

        [Command("ban")]
        [Alias("бан")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, string reason)
        {
            await user.Guild.AddBanAsync(user, 0, reason);
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
                await Context.Channel.SendMessageAsync("буль-буууль, у тебя нет допуска такого уровня!");

        }


    }
}

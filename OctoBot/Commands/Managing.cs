using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Server;
using OctoBot.Configs.Users;
using OctoBot.Handeling;
using OctoBot.Services;

namespace OctoBot.Commands
{
    public class Managing : ModuleBase<SocketCommandContextCustom>
    {
        private static readonly SocketTextChannel LogTextChannel =
            Global.Client.GetGuild(375104801018609665).GetTextChannel(454435962089373696);

        [Command("purge")]
        [Alias("clean", "убрать", "clear")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        public async Task Delete(int number)
        {
            try {
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 100)
            {

                var items = await Context.Channel.GetMessagesAsync(number + 1).FlattenAsync();
                if (Context.Channel is ITextChannel channel) await channel.DeleteMessagesAsync(items);
                var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed);
                embed.AddField($"**PURGE** {number}", $"Used By {Context.User.Mention} in {Context.Channel}");
                await LogTextChannel.SendMessageAsync("", false, embed.Build());

                    var guild = ServerAccounts.GetServerAccount(Context.Guild);
                await Context.Guild.GetTextChannel(guild.LogChannelId).SendMessageAsync("", false, embed.Build());
            }
            else
              
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, "Boole! You do not have a tolerance of this level!");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", "Boole! You do not have a tolerance of this level!");
                }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **clear [number]**\n" +
                                 "Alias: purge, clean, убрать");
            }
        }

        [Command("warn")]
        [Alias("варн","предупреждение", "warning")]
       // [RequireUserPermission(GuildPermission.Administrator)]
        public async Task WarnUser(IGuildUser user, [Remainder]string message)
        {
            try {
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 100)
            {
            var time = DateTime.Now.ToString("");
            var account = UserAccounts.GetAccount((SocketUser)user);
            account.Warnings += $"{time} {Context.User}: [warn]" + message + "|";
            UserAccounts.SaveAccounts();
           
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, user.Mention + " Was Forewarned");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", user.Mention + " Was Forewarned");
                }

                var embed = new EmbedBuilder()
                    .WithColor(Color.DarkRed)
                    .AddField("**WARN** used", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                               $"**Content:**\n" +
                                               $"{user.Mention} - {message}");
                await LogTextChannel.SendMessageAsync("", false, embed.Build());
            }
            else
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, "Boole! You do not have a tolerance of this level!");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", "Boole! You do not have a tolerance of this level!");
            }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **warn [user_ping(or user ID)] [reason_mesasge]**\n" +
                                 "Alias: варн, warning, предупреждение");
            }
        }

        [Command("kick")]
        [Alias("кик")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, string reason)
        {
            try{
            await user.KickAsync(reason);
                var time = DateTime.Now.ToString("");
                var account = UserAccounts.GetAccount((SocketUser)user);
                account.Warnings += $"{time} {Context.User}: [kick]" + reason + "|";
                UserAccounts.SaveAccounts();
            var embed = new EmbedBuilder()
                .WithColor(Color.DarkRed)
                .AddField("**kick** used", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                           $"**Content:**\n" +
                                           $"{user.Mention} - {reason}");
            await LogTextChannel.SendMessageAsync("", false, embed.Build());
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **kick [user_ping(or user ID)] [reason_mesasge]**\n" +
                                 "Alias: кик");
            }
        }

        [Command("ban")]
        [Alias("бан")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, string reason)
        {
            try {
            await user.Guild.AddBanAsync(user, 0, reason);
            var time = DateTime.Now.ToString("");
            var account = UserAccounts.GetAccount((SocketUser)user);
            account.Warnings += $"{time} {Context.User}: [ban]" + reason + "|";
            UserAccounts.SaveAccounts();
            var embed = new EmbedBuilder()
                .WithColor(Color.DarkRed)
                .AddField("**ban** used", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                           $"**Content:**\n" +
                                           $"{user.Mention} - {reason}");
            await LogTextChannel.SendMessageAsync("", false, embed.Build());
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **ban [user_ping(or user ID)] [reason_mesasge]**\n" +
                                 "Alias: бан");
            }
        }


        [Command("mute")]
        public async Task MuteCommand(SocketGuildUser user, uint minute, [Remainder]string warningMess)
        {
              try
              {
                  var commandre = UserAccounts.GetAccount(Context.User);
                  if(commandre.OctoPass < 100)
                      return;


            var hour = 0;
            var timeFormat = $"{minute}m";

            if (minute >= 60)
            {

                // ReSharper disable once NotAccessedVariable
                for (var i = 0; minute >= 59; i++)
                {
                    minute = minute - 59;
                    hour++;

                    timeFormat = $"{hour}h {minute}m";
                }

            }

            var timeString = timeFormat; //// MAde t ominutes

            var timeDateTime = DateTime.UtcNow + TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);

                  var roleToGive = Global.Client.GetGuild(Context.Guild.Id).Roles
                      .SingleOrDefault(x => x.Name.ToString() == "Muted");
              await user.AddRoleAsync(roleToGive);
                    
            var account = UserAccounts.GetAccount(user);
            account.MuteTimer = timeDateTime;
                  var time = DateTime.Now.ToString("");
            account.Warnings += $"{time} {Context.User}: [mute]" + warningMess + "|";
            UserAccounts.SaveAccounts();

              
                  if (Context.MessegeContent228 != "edit")
                  {
                      await CommandHandeling.SendingMess(Context, null, null, $"{user.Mention} бу!");
  
                  }
                  else if(Context.MessegeContent228 == "edit")
                  {
                      await CommandHandeling.SendingMess(Context, null, "edit", $"{user.Mention} бу!");
                  }
              }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **mute [user] [time_in_minutes] [Any_text]**\n");
            }
      
        }

        [Command("unmute")]
        [Alias("umute")]
        public async Task UnMuteCommand(SocketGuildUser user)
        {
            var commandre = UserAccounts.GetAccount(Context.User);
            if(commandre.OctoPass < 100)
                return;
            var roleToGive = Global.Client.GetGuild(Context.Guild.Id).Roles
                .SingleOrDefault(x => x.Name.ToString() == "Muted");
            await user.RemoveRoleAsync(roleToGive);
            var account = UserAccounts.GetAccount(user);
            account.MuteTimer = Convert.ToDateTime("0001-01-01T00:00:00");
            UserAccounts.SaveAccounts();

            
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, "как хочешь, буль...");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", "как хочешь, буль...");
            }
        }
    }
}

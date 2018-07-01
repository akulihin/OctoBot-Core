using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

using OctoBot.Configs.Server;
using OctoBot.Configs.Users;
using static OctoBot.Configs.Global;

namespace OctoBot.Handeling
{
    public class EveryLogHandeling
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        static readonly SocketTextChannel LogOwnerTextChannel =
            Client.GetGuild(375104801018609665).GetTextChannel(454435962089373696);
            
        public async Task NonStaticMethod(Cacheable<IUserMessage, ulong> arg1, SocketReaction arg3)
        {
            try
            {
                var artVoteMess = new ArtVotes(arg1.Value.Author, arg1.Value, arg1.Value.Author, arg3.Emote.Name);
                ArtVotesList.Add(artVoteMess);
            }
            catch (Exception e)
            {
                Console.WriteLine("NonStaticMethod");
                Console.WriteLine(e.Message);
            }

            await Task.CompletedTask;
        }

        public static async Task ReactionAddedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {


            if (arg3.User.Value.IsBot)
                return;


            var artMessagesList = ArtVotesList;

            if (arg3.Emote.Name == "📊" || arg3.Emote.Name == "🎨" || arg3.Emote.Name == "🏆")
            {


                foreach (var v in artMessagesList)
                {
                    if (arg1.Value.Id == v.SocketMsg.Id)
                        return;
                }

                try
                {
                    var everyLogHandeling = new EveryLogHandeling();
                    await everyLogHandeling.NonStaticMethod(arg1, arg3);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                await arg1.GetOrDownloadAsync().Result
                    .RemoveReactionAsync(arg3.Emote, arg3.User.Value, RequestOptions.Default);
                await arg1.Value.AddReactionAsync(new Emoji("1⃣"));
                await arg1.Value.AddReactionAsync(new Emoji("2⃣"));
                await arg1.Value.AddReactionAsync(new Emoji("3⃣"));
                await arg1.Value.AddReactionAsync(new Emoji("4⃣"));
                await arg1.Value.AddReactionAsync(new Emoji("5⃣"));
            }


            if (arg3.User.Value.Id == arg1.Value.Author.Id)
                return;
            foreach (var v1 in artMessagesList)
            {
                if (!v1.UserVoted.Contains(arg3.User.Value) && v1.SocketMsg == arg1.Value)
                {


                    // Console.WriteLine($"working2");
                    if (arg3.Channel is IGuildChannel chanGuild)
                    {
                        var account = UserAccounts.GetAccount(v1.BlogAuthor, chanGuild.Guild.Id);
                        switch (arg3.Emote.Name)
                        {
                            case "1⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 1;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "2⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 2;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "3⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 3;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "4⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 4;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "5⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 5;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                        }
                    }

                    v1.UserVoted.Add(arg3.User.Value);
                    v1.Emotename.Add(arg3.Emote.Name);
                }

                //return;
            }
        }

        public static async Task Client_ReactionAddedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            ReactionAddedForArtVotes(arg1, arg2, arg3);
            await Task.CompletedTask;
        }

        public static async Task ReactionRemovedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (arg3.User.Value.IsBot)
                return;
            if (arg3.User.Value.Id == arg1.Value.Author.Id)
                return;

            var artMessagesList = ArtVotesList;
            foreach (var v in artMessagesList)
            {
                if (v.UserVoted.Contains(arg3.User.Value) && v.SocketMsg == arg1.Value)
                {
                    for (var j = 0; j < v.UserVoted.Count; j++)
                    {
                        //  Console.WriteLine($"working remove voted123 emote: {artMessagesList[i].Emotename[j]}  entered: {arg3.Emote.Name}");
                        if (arg3.Emote.Name == v.Emotename[j] && arg3.User.Value.Id == v.UserVoted[j].Id)
                        {
                            // Console.WriteLine($"working remove voted = {artMessagesList[i].UserVoted.Count}");
                            if (arg3.Channel is IGuildChannel chanGuild)
                            {
                                var account = UserAccounts.GetAccount(v.BlogAuthor, chanGuild.Guild.Id);
                                switch (arg3.Emote.Name)
                                {
                                    case "1⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 1;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                    case "2⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 2;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                    case "3⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 3;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                    case "4⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 4;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                    case "5⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 5;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                }
                            }

                            v.UserVoted.Remove(arg3.User.Value);
                            v.Emotename.Remove(arg3.Emote.Name);
                            //  Console.WriteLine($"removed from voted = {artMessagesList[i].UserVoted.Count}");
                        }
                    }
                }
            }

            await Task.CompletedTask;
        }


        public static async Task Client_ReactionRemovedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {

             ReactionRemovedForArtVotes(arg1, arg2, arg3);
            await Task.CompletedTask;
        }

        public static async Task ReactionAddedAsyncForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (arg3.User.Value.IsBot)
                return;

            var blogList = BlogVotesMessIdList;
            foreach (var v in blogList)
            {


                if (v.SocketMsg.Id == arg1.Id && v.ReactionUser.Id == arg3.User.Value.Id)
                {

                    if (arg3.User.Value.Id == v.BlogAuthor.Id)
                    {

                        await arg3.Channel.SendMessageAsync("Ты не можешь ставить оценку самому себе!");
                        return;
                    }

                    /*
                    if (blogList[i].Available == 0)
                    {
                       await arg3.Channel.SendMessageAsync($"Ты уже голосовал! Сними прошлую оценку, чтобы поставить новую.");
                        continue;
                    }*/
                    if (arg3.Channel is IGuildChannel chanGuild)
                    {
                        var account = UserAccounts.GetAccount(v.BlogAuthor, chanGuild.Guild.Id);

                        switch (arg3.Emote.Name)
                        {
                            case "1⃣":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 1;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "2⃣":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 2;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "3⃣":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 3;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "4⃣":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 4;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "zazz":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 5;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                        }
                    }
                }
            }

            await Task.CompletedTask;
        }

        public static async Task Client_ReactionAddedAsyncForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
             ReactionAddedAsyncForBlog(arg1, arg2, arg3);
            await Task.CompletedTask;
        }

        public static async Task ReactionRemovedForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            var blogList = BlogVotesMessIdList;
            foreach (var v in blogList)
            {


                if (v.SocketMsg.Id == arg1.Id && v.ReactionUser.Id == arg3.User.Value.Id)
                {
                    if (arg3.User.Value.Id == v.BlogAuthor.Id)
                    {
                        return;
                    }

                    if (arg3.Channel is IGuildChannel chanGuild)
                    {
                        var account = UserAccounts.GetAccount(v.BlogAuthor, chanGuild.Guild.Id);
                        switch (arg3.Emote.Name)
                        {

                            case "1⃣":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 1;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "2⃣":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 2;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "3⃣":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 3;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "4⃣":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 4;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "zazz":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 5;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                        }
                    }

                    v.Available = 1;
                }
            }

            await Task.CompletedTask;
        }

        public static async Task Client_ReactionRemovedForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
             ReactionRemovedForBlog(arg1, arg2, arg3);
            await Task.CompletedTask;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Write Log to Channel
        ///
        /// 

        public static async Task Client_UserJoined_ForRoleOnJoin(SocketGuildUser arg)
        {
            var guid = ServerAccounts.GetServerAccount(arg.Guild);
            Console.WriteLine($"{guid.RoleOnJoin}");

            if (guid.RoleOnJoin == null)
                return;

            var roleToGive = arg.Guild.Roles
                .SingleOrDefault(x => x.Name.ToString() == $"{guid.RoleOnJoin}");

            await arg.AddRoleAsync(roleToGive);
            
           
         
        }

        public static async Task ChannelDestroyed(IChannel arg)
        {
            try
            {
                var embed = new EmbedBuilder();
                embed.WithColor(14, 243, 247);

                if (arg is ITextChannel channel)
                {
                    var log = await channel.Guild.GetAuditLogAsync(1);
                    var audit = log.ToList();

                    var name = audit[0].Action == ActionType.ChannelDeleted ? audit[0].User.Mention : "error";
                    var auditLogData = audit[0].Data as ChannelDeleteAuditLogData;
                    embed.AddField("🚫 Channel Destroyed", $"Name: {arg.Name}\n" +
                                                           $"WHO: {name}\n" +
                                                           $"Type { auditLogData?.ChannelType}" +
                                                           $"NSFW: {channel.IsNsfw}\n" +
                                                           $"Category: {channel.GetCategoryAsync().Result.Name}\n" +
                                                           $"ID: {arg.Id}\n");

                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.WithThumbnailUrl($"{audit[0].User.GetAvatarUrl()}");
                }

                 

                var currentIguildChannel = arg as IGuildChannel;
                var guild = ServerAccounts.GetServerAccount(currentIguildChannel);
                if (guild.ServerActivityLog == 1)
                {
                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                        .SendMessageAsync("", false, embed.Build());
                }
            }
            catch
            {
            //
            }
        }

        public static async Task Client_ChannelDestroyed(IChannel arg)
        {
             ChannelDestroyed(arg);
            await Task.CompletedTask;
        }

        public static async Task ChannelCreated(IChannel arg)
        {
            try
            {
                if (!(arg is ITextChannel channel))
                    return;

                var log = await channel.Guild.GetAuditLogAsync(1);
                var audit = log.ToList();
                var name = audit[0].Action == ActionType.ChannelCreated ? audit[0].User.Mention : "error";
                var auditLogData = audit[0].Data as ChannelCreateAuditLogData;

                var embed = new EmbedBuilder();
                embed.WithColor(14, 243, 247);
                embed.AddField("📖 Channel Created", $"Name: {arg.Name}\n" +
                                                     $"WHO: {name}\n" +
                                                     $"Type: {auditLogData?.ChannelType.ToString()}\n" +
                                                     $"NSFWL {channel.IsNsfw}\n" +
                                                     $"Category: {channel.GetCategoryAsync().Result.Name}\n" +
                                                     $"ID: {arg.Id}\n");
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithThumbnailUrl($"{audit[0].User.GetAvatarUrl()}");
                 

                var currentIGuildChannel = (IGuildChannel) arg;
                var guild = ServerAccounts.GetServerAccount(currentIGuildChannel);
                if (guild.ServerActivityLog == 1)
                {
                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                        .SendMessageAsync("", false, embed.Build());
                }
            }
            catch
            {
//
            }

        }

        public static async Task Client_ChannelCreated(IChannel arg)
        {
             ChannelCreated(arg);
            await Task.CompletedTask;

        }

        public static async Task GuildMemberUpdated(SocketGuildUser before, SocketGuildUser after)
        {
            try
            {
                if (after == null || before == after || before.IsBot)
                    return;

                var guild = ServerAccounts.GetServerAccount(before.Guild);

                var embed = new EmbedBuilder();
                if (before.Nickname != after.Nickname)
                {
                    var log = await before.Guild.GetAuditLogsAsync(1).FlattenAsync();
                    var audit = log.ToList();
                    var beforeName = before.Nickname ?? before.Username;

                    var afterName = after.Nickname ?? after.Username;

                    embed.WithColor(255,255,0);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.AddField("💢 Nickname Changed:",
                        $"User: **{before.Username} {before.Id}**\n" +
                        $"Server: **{before.Guild.Name}**\n" +
                        $"Before:\n" +
                        $"**{beforeName}**\n" +
                        $"After:\n" +
                        $"**{afterName}**");
                    if (audit[0].Action == ActionType.MemberUpdated)
                        embed.AddField("WHO:", $"{audit[0].User.Mention}\n");
                    embed.WithThumbnailUrl($"{after.GetAvatarUrl()}");

                     

                    if (guild.ServerActivityLog == 1)
                    {
                        await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                            .SendMessageAsync("", false, embed.Build());
                    }

                    var userAccount = UserAccounts.GetAccount(after, guild.ServerId);
                    var user = after;
                    if (userAccount.ExtraUserName != null)
                    {

                        var dublicate = 0;
                        var extra = userAccount.ExtraUserName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                        for (var i = 0; i < extra.Length; i++)
                        {
                            if (extra[i] == user.Nickname && extra[i] != null)
                                dublicate = 1;
                        }

                        if (dublicate != 1 && user.Nickname != null)
                            userAccount.ExtraUserName += (user.Nickname + "|");

                    }
                    else if (user.Nickname != null)
                        userAccount.ExtraUserName = (user.Nickname + "|");

                    UserAccounts.SaveAccounts(guild.ServerId);
                }

                if (before.GetAvatarUrl() != after.GetAvatarUrl())
                {
                    embed.WithColor(255,255,0);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.AddField("💢 Avatar Changed:",
                        $"User: **{before.Username} {before.Id}**\n" +
                        $"Server: **{before.Guild.Name}**\n" +
                        $"Before:\n" +
                        $"**{before.GetAvatarUrl()}**\n" +
                        $"After:\n" +
                        $"**{after.GetAvatarUrl()}**");
                    embed.WithThumbnailUrl($"{after.GetAvatarUrl()}");

                     

                    if (guild.ServerActivityLog == 1)
                    {
                        await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                            .SendMessageAsync("", false, embed.Build());
                    }
                }

                if (before.Username != after.Username || before.Id != after.Id)
                {
                    embed.WithColor(255,255,0);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.AddField("💢 USERNAME Changed:",
                        $"Server: **{before.Guild.Name}**\n" +
                        $"Before:\n" +
                        $"**{before.Username} {before.Id}**\n" +
                        $"After:\n" +
                        $"**{after.Username} {after.Id}**\n");
                    embed.WithThumbnailUrl($"{after.GetAvatarUrl()}");

                    if (guild.ServerActivityLog == 1)
                    {
                        await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                            .SendMessageAsync("", false, embed.Build());
                    }
                }

                if (before.Roles.Count != after.Roles.Count)
                {

                    string roleString;
                    var list1 = before.Roles.ToList();
                    var list2 = after.Roles.ToList();
                    var role = "";
                    if (before.Roles.Count > after.Roles.Count)
                    {
                        roleString = "Removed";
                        var differenceQuery = list1.Except(list2);
                        var socketRoles = differenceQuery as SocketRole[] ?? differenceQuery.ToArray();
                        for (var i = 0; i < socketRoles.Count(); i++)
                            role += socketRoles[i];
                    }
                    else
                    {
                        roleString = "Added";
                        var differenceQuery = list2.Except(list1);
                        var socketRoles = differenceQuery as SocketRole[] ?? differenceQuery.ToArray();
                        for (var i = 0; i < socketRoles.Count(); i++)
                            role += socketRoles[i];
                        if (role == "LoL")
                        {
                            await Client.GetGuild(338355570669256705).GetTextChannel(429345059486564352)
                                .SendMessageAsync(
                                    $"Буль тебе, {after.Mention}! Если ты новенький в этом мире, то ты можешь попросить у нас реферальную ссылку, чтобы получить **сразу 50 персов на аккаунт**\n" +
                                    $"А если ты профи, то можешь попробовать спросить mylorik аккаунт с персонажами, на время, разумеется.");

                        }
                    }

                    var log = await before.Guild.GetAuditLogsAsync(1).FlattenAsync();
                    var audit = log.ToList();

                    embed.WithColor(255,255,0);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.AddField($"👑 Role Update (Role {roleString}):",

                        $"User: **{before.Username} {before.Id}**\n" +
                        $"Server: **{before.Guild.Name}**\n" +
                        $"Role ({roleString}): **{role}**");
                    if (audit[0].Action == ActionType.MemberRoleUpdated)
                        embed.AddField("WHO:", $"{audit[0].User.Mention}\n");
                    embed.WithThumbnailUrl($"{after.GetAvatarUrl()}");
                     

                    if (guild.ServerActivityLog == 1)
                    {
                        await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                            .SendMessageAsync("", false, embed.Build());
                    }
                }

            }
            catch
            {
                // ignored
            }

        }

        public static async Task Client_GuildMemberUpdated(SocketGuildUser before, SocketGuildUser after)
        {
             GuildMemberUpdated(before, after);
            await Task.CompletedTask;
        }

        public static async Task JoinedGuild(SocketGuild arg)
        {
            //<:octo_hi:371424193008369664>
            //<:octo_ye:365703699601031170>
            //<:octo_shrug_v0_2:434100874889920522>
            try
            {
                var emoji = Emote.Parse("<:warframe:445467639242948618>");
                await LogOwnerTextChannel.SendMessageAsync(
                    $"<@181514288278536193> OctoBot have been connected to {arg.Name}");
                var text =
                    $"Boooole! {new Emoji("<:octo_hi:371424193008369664>")} I am an **Octopus** and I do many thing, you may check it via `Help` commands\n" +
                    $"Set Prefix: `{Client.CurrentUser.Mention} setPrefix whatever_you_want`\n" +
                    $"See prefix: `prefix`\n" +
                    $"Set Channel for logs: `SetLog` OR `SetLog Channel_ID`(I can logg ANY files and even 2000 lenght messages), `offLog` to turn it off\n" +
                    $"Set Role On Join: `RoleOnJoin role` will give the role every user who joined the server\n" +
                    $"**You can edit your previous commands, and OctoBot will edit previous response to that command**, so you don't have to spam Channels with messages\n" +
                    $"I need an admin role (see channel, manage emojis, messages, roles, channels, **Audit log** access, etc...) to logg all info to `SetLog`, otherwise, I will not log anything.\n" +
                    $"Please note: all Help commands are Russian, I will translate them as soon as possible! (c) mylorik#2828\n" +
                    $"Also, `boole` is bot's language, pronunciation is same to `boolean` without that `an` sound {new Emoji("<:octo_shrug_v0_2:434100874889920522>")}";
                var mess = await arg.DefaultChannel.SendMessageAsync(text);

                await mess.AddReactionAsync(emoji);
            }
            catch
            {
                //
            }

        }

        public static async Task Client_JoinedGuild(SocketGuild arg)
        {
             JoinedGuild(arg);
            await Task.CompletedTask;
        }

        public static async Task Client_Connected()
        {
            await LogOwnerTextChannel.SendMessageAsync($"OctoBot on Duty!");
        }

        public static async Task Client_Disconnected(Exception arg)
        {
            /*
            Client.Ready -= GreenBuuTimerClass.StartTimer; ////////////// Timer1 Green Boo starts
            Client.Ready -= DailyPull.CheckTimerForPull; ////////////// Timer3 For Pulls   
            Client.Ready -= Reminder.CheckTimer; ////////////// Timer4 For For Reminders
            Client.Ready -= ForBot.TimerForChangeBotAvatar;
            Client.Ready -= _client_Ready;
            */
            await LogOwnerTextChannel.SendMessageAsync($"OctoBot Disconnect: {arg.Message}");
            // await LogOwnerTextChannel.SendMessageAsync($"<@181514288278536193> Disconnect!");
        }

        public static readonly IServiceProvider Services;

        public static async Task ReplyOnEdit(SocketMessage messageAfter)
        {
            var commands = new CommandService();
            await commands.AddModulesAsync(
                Assembly.GetEntryAssembly(), 
                Services);
            var tempTask = new CommandHandelingSendingAndUpdatingMessages(Services, commands, Client );
            await tempTask.HandleCommandAsync(messageAfter);
          //  CommandHandelingSendingAndUpdatingMessages.SendingMess()
        }


        public static async Task MessageUpdated(Cacheable<IMessage, ulong> messageBefore,
            SocketMessage messageAfter, ISocketMessageChannel arg3)
        {
            try
            {
                var currentIGuildChannel = arg3 as IGuildChannel;
                var guild = ServerAccounts.GetServerAccount(currentIGuildChannel);

                var ss = 0;
                foreach (var t in CommandList)
                {
                    if (t.UserSocketMsg.Id == messageAfter.Id)
                    {
                         ss = 1;
                    }
                }
                var message = messageAfter as SocketUserMessage;
                var argPos = 0;
                if (ss !=1 && (message.HasStringPrefix(guild.Prefix, ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos)))
                {
                 
                  ReplyOnEdit(messageAfter);
                 
                 //   CommandHandelingSendingAndUpdatingMessages.HandleCommandAsync(messageAfter);
                }

                if (messageAfter.Author.IsBot)
                    return;

                var after = messageAfter as IUserMessage;

                if (messageAfter.Content == null)
                {
                    return;
                }

                var before = (messageBefore.HasValue ? messageBefore.Value : null) as IUserMessage;
                if (before == null)
                    return;


                if (arg3 == null)
                    return;

                if (before.Content == after?.Content)
                    return;


                var embed = new EmbedBuilder();
                embed.WithColor(Color.Green);
                embed.WithFooter($"MessId: {messageBefore.Id}");
                embed.WithThumbnailUrl($"{messageBefore.Value.Author.GetAvatarUrl()}");
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithTitle($"📝 Updated Message");
                embed.WithDescription($"Where: <#{before.Channel.Id}>" +
                                      $"\nMess Author: **{after?.Author}**\n");




                if (messageBefore.Value.Content.Length > 1000)
                {
                    var string1 = messageBefore.Value.Content.Substring(0, 1000);

                    embed.AddField("Before:", $"{string1}");

                    if (messageBefore.Value.Content.Length <= 2000)
                    {

                        var string2 =
                            messageBefore.Value.Content.Substring(1000, messageBefore.Value.Content.Length - 1000);
                        embed.AddField("Before: Continued", $"...{string2}");

                    }
                }
                else if (messageBefore.Value.Content.Length != 0)
                {
                    embed.AddField("Before:", $"{messageBefore.Value.Content}");
                }


                if (messageAfter.Content.Length > 1000)
                {
                    var string1 = messageAfter.Content.Substring(0, 1000);

                    embed.AddField("After:", $"{string1}");

                    if (messageAfter.Content.Length <= 2000)
                    {

                        var string2 =
                            messageAfter.Content.Substring(1000, messageAfter.Content.Length - 1000);
                        embed.AddField("After: Continued", $"...{string2}");

                    }
                }
                else if (messageAfter.Content.Length != 0)
                {
                    embed.AddField("After:", $"{messageAfter.Content}");
                }



                if (messageBefore.Value.Attachments.Any())
                {

                    var temp = messageBefore.Value.Attachments.FirstOrDefault()?.Url;
                    var check2 = $"{temp?.Substring(temp.Length - 8, 8)}";
                    var output = check2.Substring(check2.IndexOf('.') + 1);
                    //OctoAttachments/{ll?.GuildId}
                    var ll = arg3 as IGuildChannel;


                    if (messageBefore.Value.Attachments.Count == 1)
                    {
                        if (output == "png" || output == "jpg" || output == "gif")
                        {
                            embed.WithImageUrl(
                                $"attachment://{Path.GetFileName($"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}.{output}")}");                
                            if (guild.ServerActivityLog == 1)
                            {

                                await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                    .SendFileAsync(
                                        $"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}.{output}",
                                        "",
                                        embed: embed.Build());
                            }
                        }
                        else
                        {
                           
                            if (guild.ServerActivityLog == 1)
                            {

                                await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                    .SendMessageAsync("", false, embed.Build());
                                await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                    .SendFileAsync(
                                        $@"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}.{output}",
                                        $"");
                            }
                        }
                    }
                    else
                    {
                        var sent = 0;
                        for (var i = 0; i < messageBefore.Value.Attachments.Count; i++)
                        {
                            var tempMulty = messageBefore.Value.Attachments.ToList();
                            var checkMulty = $"{tempMulty[i].Url.Substring(tempMulty[i].Url.Length - 8, 8)}";
                            var outputMylty = checkMulty.Substring(checkMulty.IndexOf('.') + 1);

                            if (i == 0 && (outputMylty == "png" || outputMylty == "jpg" || outputMylty == "gif"))
                            {
                                sent = 1;
                                embed.WithImageUrl(
                                    $"attachment://{Path.GetFileName($"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}-{i + 1}.{outputMylty}")}");

                                if (guild.ServerActivityLog == 1)
                                {

                                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                        .SendFileAsync(
                                            $"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}-{i + 1}.{outputMylty}",
                                            "",
                                            embed: embed.Build());
                                }
                            }
                            else
                            {
                                if (guild.ServerActivityLog == 1)
                                {
                                    if (sent != 1)
                                        await Client.GetGuild(guild.ServerId)
                                            .GetTextChannel(guild.LogChannelId)
                                            .SendMessageAsync("", false, embed.Build());
                                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                        .SendFileAsync(
                                            $@"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}-{i + 1}.{
                                                    outputMylty
                                                }",
                                            $"");
                                }

                                sent = 1;
                            }
                        }
                    }
                }
                else
                {

                     

                 
                    if (guild.ServerActivityLog == 1)
                    {

                        await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                            .SendMessageAsync("", false, embed.Build());
                    }
                }

            }
            catch
            {
              //  Console.WriteLine("Cath messupd");
            }

        }


        public static async Task Client_MessageUpdated(Cacheable<IMessage, ulong> messageBefore,
            SocketMessage messageAfter, ISocketMessageChannel arg3)
        {
             MessageUpdated(messageBefore, messageAfter, arg3);
            await Task.CompletedTask;

        }

        public static async Task MessageReceivedDownloadAttachment(SocketMessage arg)
        {
            try
            {
                if (arg.Attachments.Count == 1)
                {

                    var ll = arg.Channel as IGuildChannel;

                    Directory.CreateDirectory($@"OctoDataBase/OctoAttachments");
                    Directory.CreateDirectory($@"OctoDataBase/OctoAttachments/{ll?.GuildId}");

                    var temp = arg.Attachments.FirstOrDefault()?.Url;
                    if (!arg.Attachments.Any())
                        return;
                    var check = $"{temp?.Substring(temp.Length - 8, 8)}";
                    var output = check.Substring(check.IndexOf('.') + 1);

                    if (output == "png" || output == "jpg" || output == "gif")
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFileAsync(new Uri(arg.Attachments.FirstOrDefault()?.Url),
                                $@"OctoDataBase/OctoAttachments/{ll?.GuildId}/{arg.Id}.{output}");
                        }
                    }
                    else
                    {
                        // Console.WriteLine(output);
                        using (var client = new WebClient())
                        {
                            client.DownloadFileAsync(new Uri(arg.Attachments.FirstOrDefault()?.Url),
                                $@"OctoDataBase/OctoAttachments/{ll?.GuildId}/{arg.Id}.{output}");
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < arg.Attachments.Count; i++)
                    {
                        var ll = arg.Channel as IGuildChannel;

                        Directory.CreateDirectory($@"OctoDataBase/OctoAttachments");
                        Directory.CreateDirectory($@"OctoDataBase/OctoAttachments/{ll?.GuildId}");

                        var temp = arg.Attachments.ToList();



                        var check = $"{temp[i].Url.Substring(temp[i].Url.Length - 8, 8)}";
                        var output = check.Substring(check.IndexOf('.') + 1);

                        if (output == "png" || output == "jpg" || output == "gif")
                        {
                            using (var client = new WebClient())
                            {
                                client.DownloadFileAsync(new Uri(temp[i].Url),
                                    $@"OctoDataBase/OctoAttachments/{ll?.GuildId}/{arg.Id}-{i + 1}.{output}");
                            }
                        }
                        else
                        {
                            // Console.WriteLine(output);
                            using (var client = new WebClient())
                            {
                                client.DownloadFileAsync(new Uri(temp[i].Url),
                                    $@"OctoDataBase/OctoAttachments/{ll?.GuildId}/{arg.Id}-{i + 1}.{output}");
                            }
                        }
                    }
                }

                await Task.CompletedTask;
            }
            catch
            {
                //
            }
        }


        public static async Task Client_MessageReceived(SocketMessage arg)
        {
            if (arg.Author.Id == Client.CurrentUser.Id)
                return;

             MessageReceivedDownloadAttachment(arg);
            await Task.CompletedTask;
        }


        public static async Task DeleteLogg(Cacheable<IMessage, ulong> messageBefore,
            ISocketMessageChannel arg3)
        {
            try
            {

                foreach (var t in CommandList)
                {
                    if (t.UserSocketMsg.Id == messageBefore.Id)
                    {
                       await t.BotSocketMsg.DeleteAsync();
                    }
                }


                if (messageBefore.Value.Author.IsBot)
                    return;
                if (messageBefore.Value.Channel is ITextChannel kek)
                {


                    var log = await kek.Guild.GetAuditLogAsync(1);
                    var audit = log.ToList();

                    var name = $"{messageBefore.Value.Author.Mention}";
                    var check = audit[0].Data as MessageDeleteAuditLogData;

                    if (check?.ChannelId == messageBefore.Value.Channel.Id &&
                        audit[0].Action == ActionType.MessageDeleted)
                        name = $"{audit[0].User.Mention}";

                    var embedDel = new EmbedBuilder();

                    embedDel.WithFooter($"MessId: {messageBefore.Id}");
                    embedDel.WithTimestamp(DateTimeOffset.UtcNow);
                    embedDel.WithThumbnailUrl($"{messageBefore.Value.Author.GetAvatarUrl()}");

                    embedDel.WithColor(Color.Red);
                    embedDel.WithTitle($"🗑 Deleted Message");
                    embedDel.WithDescription($"Where: <#{messageBefore.Value.Channel.Id}>\n" +
                                             $"WHO: **{name}** (not always correct)\n" +
                                             $"Mess Author: **{messageBefore.Value.Author}**\n");


                    if (messageBefore.Value.Content.Length > 1000)
                    {
                        var string1 = messageBefore.Value.Content.Substring(0, 1000);

                        embedDel.AddField("Content1", $"{string1}");

                        if (messageBefore.Value.Content.Length <= 2000)
                        {

                            var string2 =
                                messageBefore.Value.Content.Substring(1000, messageBefore.Value.Content.Length - 1000);
                            embedDel.AddField("Continued", $"...{string2}");

                        }
                    }
                    else if (messageBefore.Value.Content.Length != 0)
                    {
                        embedDel.AddField("Content", $"{messageBefore.Value.Content}");
                    }

                    if (messageBefore.Value.Attachments.Any())
                    {

                        var temp = messageBefore.Value.Attachments.FirstOrDefault()?.Url;
                        var check2 = $"{temp?.Substring(temp.Length - 8, 8)}";
                        var output = check2.Substring(check2.IndexOf('.') + 1);
                        //OctoAttachments/{ll?.GuildId}
                        var ll = arg3 as IGuildChannel;


                        if (messageBefore.Value.Attachments.Count == 1)
                        {
                            if (output == "png" || output == "jpg" || output == "gif")
                            {
                                embedDel.WithImageUrl(
                                    $"attachment://{Path.GetFileName($"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}.{output}")}");
                             

                                var currentIGuildChannel = arg3 as IGuildChannel;
                                var guild = ServerAccounts.GetServerAccount(currentIGuildChannel);
                                if (guild.ServerActivityLog == 1)
                                {

                                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                        .SendFileAsync(
                                            $"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}.{output}",
                                            "",
                                            embed: embedDel.Build());
                                }
                            }
                            else
                            {
                   

                                var currentIGuildChannel = arg3 as IGuildChannel;
                                var guild = ServerAccounts.GetServerAccount(currentIGuildChannel);
                                if (guild.ServerActivityLog == 1)
                                {

                                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                        .SendMessageAsync("", false, embedDel.Build());
                                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                        .SendFileAsync(
                                            $@"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}.{output}",
                                            $"");
                                }
                            }
                        }
                        else
                        {
                            var sent = 0;
                            for (var i = 0; i < messageBefore.Value.Attachments.Count; i++)
                            {
                                var tempMulty = messageBefore.Value.Attachments.ToList();
                                var checkMulty = $"{tempMulty[i].Url.Substring(tempMulty[i].Url.Length - 8, 8)}";
                                var outputMylty = checkMulty.Substring(checkMulty.IndexOf('.') + 1);

                                if (i == 0 && (outputMylty == "png" || outputMylty == "jpg" || outputMylty == "gif"))
                                {
                                    sent = 1;
                                    embedDel.WithImageUrl(
                                        $"attachment://{Path.GetFileName($"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}-{i + 1}.{outputMylty}")}");
                                   

                                    var currentIGuildChannel = arg3 as IGuildChannel;
                                    var guild = ServerAccounts.GetServerAccount(currentIGuildChannel);
                                    if (guild.ServerActivityLog == 1)
                                    {

                                        await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                            .SendFileAsync(
                                                $"OctoDataBase/OctoAttachments/{ll?.GuildId}/{messageBefore.Id}-{i + 1}.{outputMylty}",
                                                "",
                                                embed: embedDel.Build());
                                    }
                                }
                                else
                                {
                                    var currentIGuildChannel = arg3 as IGuildChannel;
                                    var guild = ServerAccounts.GetServerAccount(currentIGuildChannel);
                                    if (guild.ServerActivityLog == 1)
                                    {
                                        if (sent != 1)
                                            await Client.GetGuild(guild.ServerId)
                                                .GetTextChannel(guild.LogChannelId)
                                                .SendMessageAsync("", false, embedDel.Build());
                                        await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                            .SendFileAsync(
                                                $@"OctoDataBase/OctoAttachments/{ll?.GuildId}/{
                                                        messageBefore.Id
                                                    }-{i + 1}.{outputMylty}",
                                                $"");
                                    }

                                    sent = 1;

                                }
                            }
                        }
                    }
                    else
                    {

                       

                        var currentIGuildChannel = arg3 as IGuildChannel;
                        var guild = ServerAccounts.GetServerAccount(currentIGuildChannel);
                        if (guild.ServerActivityLog == 1)
                        {

                            await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                                .SendMessageAsync("", false, embedDel.Build());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

        }

        public static async Task Client_MessageDeleted(Cacheable<IMessage, ulong> messageBefore,
            ISocketMessageChannel arg3)
        {
             DeleteLogg(messageBefore, arg3);
            await Task.CompletedTask;
        }

        public static async Task RoleUpdated(SocketRole arg1, SocketRole arg2)
        {
            try
            {
                var before = arg1;
                var after = arg2;
                if (after == null)
                    return;
                if (before == after)
                    return;


                var roleString = "nothing";
                var list1 = before.Permissions.ToList();
                var list2 = after.Permissions.ToList();
                var role = "\n";

                if (list1.Count > list2.Count)
                {
                    roleString = "Removed";
                    var differenceQuery = list1.Except(list2);
                    var socketRoles = differenceQuery as GuildPermission[] ?? differenceQuery.ToArray();
                    for (var i = 0; i < socketRoles.Count(); i++)
                        role += $"{socketRoles[i]}\n";
                }
                else if (list1.Count < list2.Count)
                {
                    roleString = "Added";
                    var differenceQuery = list2.Except(list1);
                    var socketRoles = differenceQuery as GuildPermission[] ?? differenceQuery.ToArray();
                    for (var i = 0; i < socketRoles.Count(); i++)
                        role += $"{socketRoles[i]}\n";
                }

                var extra = "";
                if (before.Name != after.Name)
                {
                    extra += $"__**Before:**__\n" +
                             $"Name: **{before}**\n";
                    if (before.Color.ToString() != after.Color.ToString())
                    {
                        extra += $"Color: {before.Color}\n";
                    }

                    extra += $"__**After:**__\n" +
                             $"Name: **{after}**\n";
                    if (before.Color.ToString() != after.Color.ToString())
                    {
                        extra += $"Color: {after.Color}\n";
                    }

                }
                else if (before.Color.ToString() != after.Color.ToString())
                {
                    extra += $"__**Before:**__\n";
                    extra += $"Color: {before.Color}\n";
                    extra += $"__**After:**__\n";
                    extra += $"Color: {after.Color}\n";

                }

                var log = await before.Guild.GetAuditLogsAsync(1).FlattenAsync();
                var audit = log.ToList();
                var check = audit[0].Data as RoleUpdateAuditLogData;
                var name = "error";
                if (check?.After.Name == arg2.Name)
                {
                    name = audit[0].User.Mention;
                }

                var guild = ServerAccounts.GetServerAccount(before.Guild);
                var embed = new EmbedBuilder();
                embed.WithColor(57, 51, 255 );
                embed.AddField($"🛠️ Role Updated({roleString})", $"Role: {after.Mention}\n" +
                                                                  $"WHO: {name}\n" +
                                                                  $"ID: {before.Id}\n" +
                                                                  $"Guild: {before.Guild.Name}\n" +
                                                                  $"{extra}" +
                                                                  $"Permission ({roleString}): **{role}**");
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithThumbnailUrl($"{audit[0].User.GetAvatarUrl()}");


                var s = role.Replace("\n", "");
                if (s.Length < 1 && extra.Length < 1)
                    return;


                 


                if (guild.ServerActivityLog == 1)
                {
                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                        .SendMessageAsync("", false, embed.Build());
                }
            }
            catch
            {
                //
            }
        }

        public static async Task Client_RoleUpdated(SocketRole arg1, SocketRole arg2)
        {
             RoleUpdated(arg1, arg2);
            await Task.CompletedTask;

        }

        //Fix it
        public static async Task Client_ChannelUpdated(SocketChannel arg1, SocketChannel arg2)
        {
            await Task.CompletedTask;

        }

        public static async Task RoleDeleted(SocketRole arg)
        {
            try
            {

                var log = await arg.Guild.GetAuditLogsAsync(1).FlattenAsync();
                var audit = log.ToList();
                var check = audit[0].Data as RoleDeleteAuditLogData;
                var name = "erorr";

                if (check?.RoleId == arg.Id)
                {
                    name = audit[0].User.Mention;
                }

                var embed = new EmbedBuilder();
                embed.WithColor(240, 51, 255);
                embed.AddField("⚰️ Role Deleted", $"WHO: {name}\n" +

                                                  $"Name: {arg.Name} ({arg.Guild})\n" +
                                                  $"Color: {arg.Color}\n" +
                                                  $"ID: {arg.Id}\n");
                embed.WithTimestamp(DateTimeOffset.UtcNow);

                embed.WithThumbnailUrl($"{audit[0].User.GetAvatarUrl()}");
                 

                var guild = ServerAccounts.GetServerAccount(arg.Guild);

                if (guild.ServerActivityLog == 1)
                {
                    await Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogChannelId)
                        .SendMessageAsync("", false, embed.Build());
                }
            }
            catch
            {
                //
            }

        }

        public static async Task Client_RoleDeleted(SocketRole arg)
        {
             RoleDeleted(arg);
            await Task.CompletedTask;
        }
    }
}

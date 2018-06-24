using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using OctoBot.Commands;
using OctoBot.Commands.PersonalCommands;
using OctoBot.Configs;
using OctoBot.Configs.Server;
using OctoBot.Configs.Users;
using static OctoBot.Configs.Global;

namespace OctoBot.Handeling
{
    public class EveryLogHandeling
    {
        private static readonly DiscordSocketClient Client = Global.Client;

        static readonly SocketTextChannel LogOwnerTextChannel =
            Global.Client.GetGuild(375104801018609665).GetTextChannel(454435962089373696);

        public static Task _client_Ready()
        {
            Client.JoinedGuild += Client_JoinedGuild; // Please Check more options!
            Client.ReactionAdded += Client_ReactionAddedAsyncForBlog;
            Client.ReactionRemoved += Client_ReactionRemovedForBlog;
            Client.ReactionAdded += Client_ReactionAddedForArtVotes;
            Client.ReactionRemoved += Client_ReactionRemovedForArtVotes;
            Client.Disconnected += Client_Disconnected;
            Client.Connected += Client_Connected;
            Client.MessageUpdated += Client_MessageUpdated;
            Client.MessageDeleted += Client_MessageDeleted;
            Client.ChannelCreated += Client_ChannelCreated;
            Client.ChannelDestroyed += Client_ChannelDestroyed;
            Client.RoleDeleted += Client_RoleDeleted;
            Client.RoleUpdated += Client_RoleUpdated;
           



            Client.ChannelUpdated += Client_ChannelUpdated;
            Client.GuildMemberUpdated += Client_GuildMemberUpdated;

            return Task.CompletedTask;

        }

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

        private static async Task Client_ReactionAddedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
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

                await arg1.DownloadAsync().Result
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
                    var account = UserAccounts.GetAccount(v1.BlogAuthor);
                    switch (arg3.Emote.Name)
                    {
                        case "1⃣":
                            account.ArtVotesQty += 1;
                            account.ArtVotesSum += 1;
                            UserAccounts.SaveAccounts();
                            break;
                        case "2⃣":
                            account.ArtVotesQty += 1;
                            account.ArtVotesSum += 2;
                            UserAccounts.SaveAccounts();
                            break;
                        case "3⃣":
                            account.ArtVotesQty += 1;
                            account.ArtVotesSum += 3;
                            UserAccounts.SaveAccounts();
                            break;
                        case "4⃣":
                            account.ArtVotesQty += 1;
                            account.ArtVotesSum += 4;
                            UserAccounts.SaveAccounts();
                            break;
                        case "5⃣":
                            account.ArtVotesQty += 1;
                            account.ArtVotesSum += 5;
                            UserAccounts.SaveAccounts();
                            break;
                    }

                    v1.UserVoted.Add(arg3.User.Value);
                    v1.Emotename.Add(arg3.Emote.Name);
                }

                //return;
            }

            await Task.CompletedTask;
        }

        private static async Task Client_ReactionRemovedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
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
                            var account = UserAccounts.GetAccount(v.BlogAuthor);
                            switch (arg3.Emote.Name)
                            {
                                case "1⃣":
                                    account.ArtVotesQty -= 1;
                                    account.ArtVotesSum -= 1;
                                    UserAccounts.SaveAccounts();
                                    break;
                                case "2⃣":
                                    account.ArtVotesQty -= 1;
                                    account.ArtVotesSum -= 2;
                                    UserAccounts.SaveAccounts();
                                    break;
                                case "3⃣":
                                    account.ArtVotesQty -= 1;
                                    account.ArtVotesSum -= 3;
                                    UserAccounts.SaveAccounts();
                                    break;
                                case "4⃣":
                                    account.ArtVotesQty -= 1;
                                    account.ArtVotesSum -= 4;
                                    UserAccounts.SaveAccounts();
                                    break;
                                case "5⃣":
                                    account.ArtVotesQty -= 1;
                                    account.ArtVotesSum -= 5;
                                    UserAccounts.SaveAccounts();
                                    break;
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

        public static async Task Client_ReactionAddedAsyncForBlog(Cacheable<IUserMessage, ulong> arg1,
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
                    var account = UserAccounts.GetAccount(v.BlogAuthor);

                    switch (arg3.Emote.Name)
                    {
                        case "1⃣":
                            account.BlogVotesQty += 1;
                            account.BlogVotesSum += 1;
                            UserAccounts.SaveAccounts();
                            break;
                        case "2⃣":
                            account.BlogVotesQty += 1;
                            account.BlogVotesSum += 2;
                            UserAccounts.SaveAccounts();
                            break;
                        case "3⃣":
                            account.BlogVotesQty += 1;
                            account.BlogVotesSum += 3;
                            UserAccounts.SaveAccounts();
                            break;
                        case "4⃣":
                            account.BlogVotesQty += 1;
                            account.BlogVotesSum += 4;
                            UserAccounts.SaveAccounts();
                            break;
                        case "zazz":
                            account.BlogVotesQty += 1;
                            account.BlogVotesSum += 5;
                            UserAccounts.SaveAccounts();
                            break;
                    }
                }
            }

            await Task.CompletedTask;
        }

        public static async Task Client_ReactionRemovedForBlog(Cacheable<IUserMessage, ulong> arg1,
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

                    var account = UserAccounts.GetAccount(v.BlogAuthor);
                    switch (arg3.Emote.Name)
                    {

                        case "1⃣":
                            account.BlogVotesQty--;
                            account.BlogVotesSum -= 1;
                            UserAccounts.SaveAccounts();
                            break;
                        case "2⃣":
                            account.BlogVotesQty--;
                            account.BlogVotesSum -= 2;
                            UserAccounts.SaveAccounts();
                            break;
                        case "3⃣":
                            account.BlogVotesQty--;
                            account.BlogVotesSum -= 3;
                            UserAccounts.SaveAccounts();
                            break;
                        case "4⃣":
                            account.BlogVotesQty--;
                            account.BlogVotesSum -= 4;
                            UserAccounts.SaveAccounts();
                            break;
                        case "zazz":
                            account.BlogVotesQty--;
                            account.BlogVotesSum -= 5;
                            UserAccounts.SaveAccounts();
                            break;
                    }

                    v.Available = 1;
                }
            }

            await Task.CompletedTask;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Write Log to Channel
        private static async Task Client_ChannelDestroyed(IChannel arg)
        {
            try
            {
        

                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkGreen);

                if (arg is ITextChannel channel)
                {
                    var log = await channel.Guild.GetAuditLogAsync(1);
                    var audit = log.ToList();
                    var name = audit[0].Action == ActionType.ChannelDeleted ? audit[0].User.Mention : "error";
                    var a = audit[0].Data as ChannelDeleteAuditLogData;
                    embed.AddField("Channel Destroyed", $"Name: {arg.Name}\n" +
                                                        $"WHO: {name}\n" +
                                                       
                                                        
                                                        $"Type {a?.ChannelType}" +
                                                        $"NSFW: {channel.IsNsfw}\n" +
                                                        $"Category: {channel.GetCategoryAsync().Result.Name}\n" +
                                                        $"ID: {arg.Id}\n");
                    
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.WithThumbnailUrl($"{audit[0].User.GetAvatarUrl()}"); 
                }

                await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());

                var argument = arg as IGuildChannel;
                var guild = ServerAccounts.GetServerAccount(argument);
                if (guild.ServerActivityLog == 1)
                {
                    await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                        .SendMessageAsync("", false, embed.Build());
                }
            }
            catch
            {
//
            }
        }

        private static async Task Client_ChannelCreated(IChannel arg)
        {

            try
            {
                if (!(arg is ITextChannel channel))
                    return;

                var log = await channel.Guild.GetAuditLogAsync(1);
                var audit = log.ToList();
                var name = audit[0].Action == ActionType.ChannelCreated ? audit[0].User.Mention : "error";
                var a = audit[0].Data as ChannelCreateAuditLogData;
                
                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkBlue);
                embed.AddField("Channel Created", $"Name: {arg.Name}\n" +
                                                  $"WHO: {name}\n" +
                                                  $"Type: {a?.ChannelType.ToString()}\n" +
                                                  $"NSFWL {channel.IsNsfw}\n" +
                                                  $"Category: {channel.GetCategoryAsync().Result.Name}\n" +
                                                  $"ID: {arg.Id}\n");
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithThumbnailUrl($"{audit[0].User.GetAvatarUrl()}");
                await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());

                var argument = (IGuildChannel) arg;
                var guild = ServerAccounts.GetServerAccount(argument);
                if (guild.ServerActivityLog == 1)
                {
                    await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                        .SendMessageAsync("", false, embed.Build());
                }
            }
            catch
            {
//
            }
        }

        private static async Task Client_GuildMemberUpdated(SocketGuildUser before, SocketGuildUser after)
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

                    embed.WithColor(Color.DarkGreen);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.AddField("Nickname Changed:", 
                                                        $"User: **{before.Username} {before.Id}**\n" +
                                                        $"Server: **{before.Guild.Name}**\n" +
                                                        $"Before:\n" +
                                                        $"**{beforeName}**\n" +
                                                        $"After:\n" +
                                                        $"**{afterName}**");
                    if (audit[0].Action == ActionType.MemberUpdated)
                        embed.AddField("WHO:", $"{audit[0].User.Mention}\n");
                    embed.WithThumbnailUrl($"{after.GetAvatarUrl()}");

                    await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());

                    if (guild.ServerActivityLog == 1)
                    {
                        await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                            .SendMessageAsync("", false, embed.Build());
                    }

                    var userAccount = UserAccounts.GetAccount(after);
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

                    UserAccounts.SaveAccounts();
                }

                if (before.GetAvatarUrl() != after.GetAvatarUrl())
                {
                    embed.WithColor(Color.DarkGreen);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.AddField("Avatar Changed:", 
                                                      $"User: **{before.Username} {before.Id}**\n" +
                                                      $"Server: **{before.Guild.Name}**\n" +
                                                      $"Before:\n" +
                                                      $"**{before.GetAvatarUrl()}**\n" +
                                                      $"After:\n" +
                                                      $"**{after.GetAvatarUrl()}**");
                    embed.WithThumbnailUrl($"{after.GetAvatarUrl()}");

                    await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());

                    if (guild.ServerActivityLog == 1)
                    {
                        await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                            .SendMessageAsync("", false, embed.Build());
                    }
                }

                if (before.Username != after.Username || before.Id != after.Id)
                {
                    embed.WithColor(Color.DarkRed);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.AddField("USERNAME Changed:", 
                                                        $"Server: **{before.Guild.Name}**\n" +
                                                        $"Before:\n" +
                                                        $"**{before.Username} {before.Id}**\n" +
                                                        $"After:\n" +
                                                        $"**{after.Username} {after.Id}**\n");
                    embed.WithThumbnailUrl($"{after.GetAvatarUrl()}");
                    await LogOwnerTextChannel.SendMessageAsync($"<@181514288278536193> here is a gay:");
                    await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());


                    if (guild.ServerActivityLog == 1)
                    {
                        await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
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
                            await Global.Client.GetGuild(338355570669256705).GetTextChannel(429345059486564352)
                                .SendMessageAsync(
                                    $"Буль тебе, {after.Mention}! Если ты новенький в этом мире, то ты можешь попросить у нас реферальную ссылку, чтобы получить **сразу 50 персов на аккаунт**\n" +
                                    $"А если ты профи, то можешь попробовать спросить mylorik аккаунт с персонажами, на время, разумеется.");

                        }
                    }
                    var log = await before.Guild.GetAuditLogsAsync(1).FlattenAsync();
                    var audit = log.ToList();

                    embed.WithColor(Color.DarkGreen);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                    embed.AddField($"Role Update (Role {roleString}):",
                        
                        $"User: **{before.Username} {before.Id}**\n" +
                        $"Server: **{before.Guild.Name}**\n" +
                        $"Role ({roleString}): **{role}**");
                    if (audit[0].Action == ActionType.MemberRoleUpdated)
                        embed.AddField("WHO:", $"{audit[0].User.Mention}\n");
                    embed.WithThumbnailUrl($"{after.GetAvatarUrl()}");
                    await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());

                    if (guild.ServerActivityLog == 1)
                    {
                        await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                            .SendMessageAsync("", false, embed.Build());
                    }
                }

            }
            catch
            {
                // ignored
            }
        } 

        public static async Task Client_JoinedGuild(SocketGuild arg)
        {
            //<:octo_hi:371424193008369664>
            //<:octo_ye:365703699601031170>
            //<:octo_shrug_v0_2:434100874889920522>

            var emoji = Emote.Parse("<:warframe:445467639242948618>");
            await LogOwnerTextChannel.SendMessageAsync($"<@181514288278536193> OctoBot have been connected to {arg.Name}");
            var text = $"Boooole! {new Emoji("<:octo_hi:371424193008369664>")} I am an **Octopus** and I do many thing, you may check it via `Help` commands\n" +
                       $"Set Prefix: `{Global.Client.CurrentUser.Mention} setPrefix !`\n" +
                       $"See prefix: `prefix`\n" +
                       $"Set Channel for logs: `SetLog` OR `SetLog Channel_ID`\n" +
                       $"**You can edit your previous commands, and OctoBot will edit previous response to that command**, so you don't have to spam Channels with messages\n" +
                       $"I need an admin role (see channel, manage emojis, messages, roles, channels, Audit log access, etc...)\n" +
                       $"Please note: all Help commands are Russian, I will translate them as soon as possible! (c) mylorik#2828\n" +
                       $"Also, `boole` is bot's language, if you want to hear *close enough* pronunciation of it, please use Google Translate and listen to Ukrainian pronunciation of `Буль` {new Emoji("<:octo_shrug_v0_2:434100874889920522>")}";
           var mess =  await arg.DefaultChannel.SendMessageAsync(text);

            await mess.AddReactionAsync(emoji);
          
        }

        public static async Task Client_Connected()
        {
            await LogOwnerTextChannel.SendMessageAsync($"OctoBot on Duty!");
        }

        public static async Task Client_Disconnected(Exception arg)
        {
            Global.Client.Ready -= GreenBuuTimerClass.StartTimer; ////////////// Timer1 Green Boo starts
            Global.Client.Ready -= DailyPull.CheckTimerForPull; ////////////// Timer3 For Pulls   
            Global.Client.Ready -= Reminder.CheckTimer; ////////////// Timer4 For For Reminders
            Global.Client.Ready -= ForBot.TimerForBotAvatar;
            Global.Client.Ready -= _client_Ready;
            await LogOwnerTextChannel.SendMessageAsync($"OctoBot Disconnect: {arg.Message}");
            // await LogOwnerTextChannel.SendMessageAsync($"<@181514288278536193> Disconnect!");
        }

        public static async Task Client_MessageUpdated(Cacheable<IMessage, ulong> messageBefore,
            SocketMessage messageAfter, ISocketMessageChannel arg3)
        {
 

            try
            {
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
                embed.AddField("Before:", $"{messageBefore.Value.Content}\n**______**");
                if (messageBefore.Value.Attachments.Any())
                    embed.AddField("attachments", $"{messageBefore.Value.Attachments.FirstOrDefault()?.Url}");
                embed.AddField("After:", $"{after}");
                if (messageAfter.Attachments.Any())
                    embed.AddField("attachments", $"{messageAfter.Attachments.FirstOrDefault()?.Url}");

                await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());

                var argument  = arg3 as IGuildChannel;
                var guild = ServerAccounts.GetServerAccount(argument);
                if (guild.ServerActivityLog == 1)
                {
                    await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                        .SendMessageAsync("", false, embed.Build());
                }
            }
            catch
            {
                Console.WriteLine("Cath messupd");
            }
        }

        private static async Task Client_MessageDeleted(Cacheable<IMessage, ulong> messageBefore,
            ISocketMessageChannel arg3)
        {
            try
            {
                if (messageBefore.Value.Channel is ITextChannel kek)
                {
                    var log = await kek.Guild.GetAuditLogAsync(1);
                    var audit = log.ToList();
                    var name = audit[0].Action == ActionType.MessageDeleted
                        ? audit[0].User.Mention
                        : messageBefore.Value.Author.Mention;
                    
                    var embedDel = new EmbedBuilder();

                    embedDel.WithFooter($"MessId: {messageBefore.Id}");
                    embedDel.WithTimestamp(DateTimeOffset.UtcNow);
                    embedDel.WithThumbnailUrl($"{messageBefore.Value.Author.GetAvatarUrl()}");
                    
                    embedDel.WithColor(Color.Red);
                    embedDel.WithTitle($"🗑 Deleted Message");
                    embedDel.WithDescription($"Where: <#{messageBefore.Value.Channel.Id}>\n" +
                                             $"WHO: **{name}**\n" +
                                             $"Mess Author: **{messageBefore.Value.Author}**\n");
                    embedDel.AddField("Content", $"{messageBefore.Value.Content}");

                    if (messageBefore.Value.Attachments.Any())
                        embedDel.AddField("attachments",
                            $"URL: {messageBefore.Value.Attachments.FirstOrDefault()?.Url}\n" +
                            $"Proxy URL: {messageBefore.Value.Attachments.FirstOrDefault()?.ProxyUrl}");


                    await LogOwnerTextChannel.SendMessageAsync("", false, embedDel.Build());

                    var argument  = arg3 as IGuildChannel;
                    var guild = ServerAccounts.GetServerAccount(argument);
                    if (guild.ServerActivityLog == 1)
                    {
                        await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                            .SendMessageAsync("", false, embedDel.Build());
                    }
                }
            }
            catch
            {

                //
            }
        }

        private static async Task Client_RoleUpdated(SocketRole arg1, SocketRole arg2)
        {
            var before = arg1;
            var after = arg2;
            if (after == null)
                return;
            if (before == after)
                return;


            string roleString;
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
            else
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
                extra +=   $"__**After:**__\n" +
                       $"Name: **{after}**\n";
                if (before.Color.ToString() != after.Color.ToString())
                {
                    extra += $"Color: {after.Color}\n";
                }
            }
           

            var log = await before.Guild.GetAuditLogsAsync(1).FlattenAsync();
            var audit = log.ToList();
            var guild = ServerAccounts.GetServerAccount(before.Guild);
            var embed = new EmbedBuilder();
            embed.WithColor(Color.Purple);
           
            embed.AddField($"Role Updatet({roleString})", $"Role: {after.Mention}\n" +
                                                          $"WHO: {audit[0].User.Mention}\n" +
                                                          $"ID: {before.Id}\n" +
                                                          $"Guild: {before.Guild.Name}\n" +                 
                                                          $"{extra}" +
                                                          $"Permission ({roleString}): **{role}**");
            embed.WithTimestamp(DateTimeOffset.UtcNow);
            embed.WithThumbnailUrl($"{audit[0].User.GetAvatarUrl()}");
            await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());

           
            if (guild.ServerActivityLog == 1)
            {
                await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                    .SendMessageAsync("", false, embed.Build());
            }
        }

        //Fix it
        private static async Task Client_ChannelUpdated(SocketChannel arg1, SocketChannel arg2)
        {
            await Task.CompletedTask;

        }

        private static async Task Client_RoleDeleted(SocketRole arg)
        {
            try
            {

                var log = await arg.Guild.GetAuditLogsAsync(1).FlattenAsync();
                var audit = log.ToList();

                var name = audit[0].Action == ActionType.RoleDeleted ? audit[0].User.Mention : "error";

                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkRed);
                embed.AddField("Role Deleted", $"WHO: {name}\n" +
                                               
                                               $"Name: {arg.Name} ({arg.Guild})\n" +
                                               $"Color: {arg.Color}\n" +
                                               $"ID: {arg.Id}\n");
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithThumbnailUrl($"{audit[0].User.GetAvatarUrl()}");
                await LogOwnerTextChannel.SendMessageAsync("", false, embed.Build());

               
                var guild = ServerAccounts.GetServerAccount(arg.Guild);

                if (guild.ServerActivityLog == 1)
                {
                    await Global.Client.GetGuild(guild.ServerId).GetTextChannel(guild.LogsId)
                        .SendMessageAsync("", false, embed.Build());
                }
            }
            catch
            {
                //
            }
        }
    }
}

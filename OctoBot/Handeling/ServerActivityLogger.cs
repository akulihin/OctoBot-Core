using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using static OctoBot.Configs.Global;

namespace OctoBot.Handeling
{
    public class EveryLogHandeling 
    {
        private static readonly DiscordSocketClient Client = Global.Client;

        static readonly SocketTextChannel LogTextChannel =
            Global.Client.GetGuild(375104801018609665).GetTextChannel(446868049589698561);


        public static Task _client_Ready()
        {

            Client.ReactionAdded += Client_ReactionAddedAsyncForBlog;
            Client.ReactionRemoved += Client_ReactionRemovedForBlog;

            Client.ReactionAdded += Client_ReactionAddedForArtVotes;
            Client.ReactionRemoved += Client_ReactionRemovedForArtVotes;

            Client.Disconnected += Client_Disconnected;
            Client.Connected += Client_Connected;
            Client.MessageUpdated += Client_MessageUpdated;
            Client.MessageDeleted += Client_MessageDeleted;
            Client.JoinedGuild += Client_JoinedGuild; // Please Check more options!
            Client.ChannelCreated += Client_ChannelCreated;
            Client.ChannelDestroyed += Client_ChannelDestroyed; 
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


                foreach (ArtVotes v in artMessagesList)
                {
                    if (arg1.Value.Id == v.SocketMsg.Id)
                        return;
                }

                try
                {
                    EveryLogHandeling everyLogHandeling = new EveryLogHandeling();
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


            if(arg3.User.Value.Id == arg1.Value.Author.Id)
                return;
            foreach (ArtVotes v1 in artMessagesList)
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

           private static async Task Client_ReactionRemovedForArtVotes(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (arg3.User.Value.IsBot)
                return;
            if(arg3.User.Value.Id == arg1.Value.Author.Id)
                return;

            var artMessagesList = ArtVotesList;
            foreach (ArtVotes v in artMessagesList)
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


        private static async Task Client_ChannelDestroyed(IChannel  arg)
        {
            try
            {


                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkGreen);
                embed.AddField("Channel Destroyed", $"Name:{arg.Name}\n" +
                                                    $"NSFW: {arg.IsNsfw}\n" +
                                                    $"ID: {arg.Id}\n");

                await LogTextChannel.SendMessageAsync("", embed: embed);
                await Task.CompletedTask;
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
                var ch = arg as IGuildChannel;
                if (ch == null)
                    return;

                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkGreen);
                embed.AddField("Channel Created", $"Name: {arg.Name}\n" +
                                                  $"NSFWL {arg.IsNsfw}\n" +
                                                  $"ID: {arg.Id}\n" +
                                                  $"{arg.CreatedAt}");

                await LogTextChannel.SendMessageAsync("", embed: embed);
                await Task.CompletedTask;
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

                  await Task.CompletedTask;
              }
                catch
                {
                    // ignored
                }
        }

        public static async Task Client_JoinedGuild(SocketGuild arg)
        {
            await LogTextChannel.SendMessageAsync($"<@181514288278536193> OctoBot have been connected to {arg.Name}");
        }


        public static async Task Client_Connected()
        {
            await LogTextChannel.SendMessageAsync($"OctoBot on Duty!");
        }

        public static async Task Client_Disconnected(Exception arg)
        {
            await LogTextChannel.SendMessageAsync($"OctoBot Disconnect: {arg.Message}");
        }

        public static async Task Client_MessageUpdated(Cacheable<IMessage, ulong> messageBefore,
            SocketMessage messageAfter, ISocketMessageChannel arg3)
        {
            try
            {


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
                embed.WithColor(Color.DarkPurple);
                embed.WithTitle($"📝 Updated Message: {after?.Channel.Name}");
                embed.WithDescription($"**Mess Author: {after?.Author}**");
                embed.AddField("Before:", $"{messageBefore.Value.Content}\n**______**");
                if (messageBefore.Value.Attachments.Any())
                    embed.AddField("attachments", $"{messageBefore.Value.Attachments.FirstOrDefault()?.Url}");
                embed.AddField("After:", $"{after}");
                if (messageAfter.Attachments.Any())
                    embed.AddField("attachments", $"{messageAfter.Attachments.FirstOrDefault()?.Url}");

                await LogTextChannel.SendMessageAsync("", embed: embed);
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
              

                var embedDel = new EmbedBuilder();
                embedDel.WithColor(Color.DarkPurple);
                embedDel.WithTitle($"🗑 Deleted Message in {messageBefore.Value.Channel.Name}");
                embedDel.WithDescription($"Mess Author: **{messageBefore.Value.Author}**\n");
                embedDel.AddField("Content", $"{messageBefore.Value.Content}");
                embedDel.AddField("Mess ID", $"{messageBefore.Id}");
                if (messageBefore.Value.Attachments.Any())
                    embedDel.AddField("attachments", $"URL: {messageBefore.Value.Attachments.FirstOrDefault()?.Url}\n" +
                                                     $"Proxy URL: {messageBefore.Value.Attachments.FirstOrDefault()?.ProxyUrl}"); 
   

                await LogTextChannel.SendMessageAsync("", embed: embedDel);
            }
            catch
            {
                //
            }
        }



        public static async Task Client_ReactionAddedAsyncForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (arg3.User.Value.IsBot)
                return;

            var blogList = BlogVotesMessIdList;
            foreach (BlogVotes v in blogList)
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

        public static async Task Client_ReactionRemovedForBlog(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            var blogList = BlogVotesMessIdList;
            foreach (BlogVotes v in blogList)
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
    }
}

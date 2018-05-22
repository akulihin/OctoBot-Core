using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;
using static OctoBot.Configs.Global;

namespace OctoBot.Commands
{
    public class Blog : ModuleBase<SocketCommandContext>
    {


        [Command("подписчики")]
        [Alias("MySubc", "subscribers", "Subc")]
        public async Task CheckMySu()
        {
            var account = UserAccounts.GetAccount(Context.User);
            if (account.SubedToYou == null)
            {
                await ReplyAsync("На тебя никто не подписан, буль!");
                return;
            }

            var accountSubs = account.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            string mess = "";
            
            for (var i = 0; i < accountSubs.Length; i++)
            {
                var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                mess += ($"{i + 1}. {globalAccount.Username}\n");
            }

            var embed = new EmbedBuilder();
            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithTitle("Твои подписчики:");
            embed.WithDescription($"{mess}");
            await Context.Channel.SendMessageAsync("", embed: embed);


        }



        [Command("подписки")]
        [Alias("MySubs", "Subscriptions", "subs")]
        public async Task CheckMySubscriptions()
        {
            var account = UserAccounts.GetAccount(Context.User);
            if (account.SubToPeople == null)
            {
                await ReplyAsync("Ты и так ни на кого не подписан, буль!");
                return;
            }

            var accountSubs = account.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            
            string mess = "";
            for (var i = 0; i < accountSubs.Length; i++)
            {
                var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                mess += ($"{i + 1}. {globalAccount.Username}\n");
            }

        var embed = new EmbedBuilder();
            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithTitle("Твои подписки:");
            embed.WithDescription($"{mess}");
            await Context.Channel.SendMessageAsync("", embed: embed);

        }


        [Command("Sub")]
        public async Task Sub(SocketUser user)
        {
            if (user.Id == 423593006436712458)
            {
                await ReplyAsync("Нельзя подписываться на осьминожку!");
                return;
            }

            if (user.IsBot)
            {
                await ReplyAsync("Нельзя подписываться на бота.");
                return;
            }

            var account = UserAccounts.GetAccount(Context.User);
            var el = UserAccounts.GetAccount(user);


            if (account.SubToPeople != null)
            {
                var accountSubs = account.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < accountSubs.Length; i++)
                {
                    if (accountSubs[i] == user.Id.ToString())
                    {
                        await ReplyAsync($"Ты уже подписан на {user.Username}.");
                        return;
                    }
                }
            }



            account.SubToPeople += (user.Id + "|");
            el.SubedToYou += (Context.User.Id + "|");
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync(
                $"Ты подписался на {user.Username}!\nЕсли хочешь отписаться введи команду ***unsub [user]**");

        }


        [Command("unsub")]
        public async Task Unsub(SocketUser user)
        {
            var account = UserAccounts.GetAccount(Context.User);
            if (account.SubToPeople == null)
            {
                await ReplyAsync("Ты и так ни на кого не подписан, буль!");
                return;
            }

            var el = UserAccounts.GetAccount(user);
            var accountSubs = account.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            
            var check = 0;
            for (var i = 0; i < accountSubs.Length; i++)
            {
                if (Convert.ToUInt64(accountSubs[i]) != user.Id)
                {
                    check++;
                }

                if (check >= accountSubs.Length && Convert.ToUInt64(accountSubs[i]) != user.Id )
                {
                    await ReplyAsync($"Ты **не был** подписан на {user.Username}.");
                    return;
                }
          

            }

            account.SubToPeople = null;
            for (var i = 0; i < accountSubs.Length; i++)
            {
                if (Convert.ToUInt64(accountSubs[i]) != user.Id)
                    account.SubToPeople += ($"{accountSubs[i]}|");

            }

            UserAccounts.SaveAccounts();

            var elSubs = el.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            el.SubedToYou = null;
            for (var i = 0; i < elSubs.Length; i++)
            {
                if (Convert.ToUInt64(elSubs[i]) != Context.User.Id)
                    el.SubedToYou += ($"{elSubs[i]}|");

            }

            UserAccounts.SaveAccounts();

            await ReplyAsync($"Ты был успешно ансабнут от {user.Username}");
        }




        [Command("Blog")]
        [Alias("блог", "пост", "пинг", "BlogPost", "Blog Post")]
        public async Task BlogPost([Remainder] string mess)
        {


            var account = UserAccounts.GetAccount(Context.User);
            if (account.SubedToYou == null)
            {
                await ReplyAsync("На тебя никто не подписан ещё буль... Попробуй чем то завлечь людей сначала!");
                return;
            }

            var accountSubs = account.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            var messList = new List<IUserMessage>();


            for (var i = 0; i < accountSubs.Length; i++)
            {
                try
                {

                    var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                    var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();


                    var embed = new EmbedBuilder();
                    embed.WithFooter("Записная книжечка Осьминожек");
                    embed.WithTitle("OctoNotification");
                    embed.WithDescription($"**{Context.User.Mention}:** {mess}");
                    // embed.WithUrl("https://puu.sh/AqC2d/715e9eb16e.mp3");
                    embed.AddField("Оценить творчество",
                        "Если тебе нравится блоги этой/этого няши, почему бы не оценить?\n" +
                        "Можешь оценить от 1 до zazz, оценка анонимная, но общую оценку можно посомтрет чрезе команду **topr**\n" +
                        "https://puu.sh/AqC2d/715e9eb16e.mp3");
                    var message = await dmChannel.SendMessageAsync("", embed: embed);
                    //await dmChannel.SendFileAsync("https://puu.sh/AqC2d/715e9eb16e.mp3");


                    messList.Add(message);

                    var voteMessToTrack = new BlogVotes(Context.User, message, globalAccount, 1);

                    BlogVotesMessIdList.Add(voteMessToTrack);


                }
                catch
                {
                    account.SubedToYou = null;
                    for (var j = 0; j < accountSubs.Length; j++)
                    {
                        if (j != i)
                            account.SubedToYou += ($"{accountSubs[j]}|");

                    }

                    UserAccounts.SaveAccounts();

                    var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                    var errorGuyAcc = UserAccounts.GetAccount(globalAccount);
                    var errorGuy = errorGuyAcc.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                    errorGuyAcc.SubToPeople = null;
                    for (var k = 0; k < errorGuy.Length; k++)
                    {
                        if (Convert.ToUInt64(errorGuy[k]) != Context.User.Id)
                            errorGuyAcc.SubToPeople += ($"{errorGuy[k]}|");
                    }

                    UserAccounts.SaveAccounts();
                    var dmCommander = await Context.User.GetOrCreateDMChannelAsync();

                    await dmCommander.SendMessageAsync(
                        $"Возникла проблема с {globalAccount.Username}, мы его удалили из списка.");

                }
            }

            await AddReactionForBlogMessages(messList, Context.Channel);
            await ReplyAsync($"{Context.User.Mention}, твой блог был отправлен!");
        }


        [Command("amaron")]
        public static async Task AmaronLel()
        {
            var list = BlogVotesMessIdList.ToList();
            for(var i = 0; i < list.Count; i++)
                Console.WriteLine($"{list[i].BlogUser.Username} blog {i}");
            await Task.CompletedTask;
        }


        [Command("IBlog")]     
        public async Task BlogPostWithUrl(string url, [Remainder] string mess)
        {
            var account = UserAccounts.GetAccount(Context.User);
            if (account.SubedToYou == null)
            {
                await ReplyAsync("На тебя никто не подписан ещё буль... Попробуй чем то завлечь людей сначала!");
                return;
            }

            string httpsCheck;
            if (url.Length < 5)
            {
                await ReplyAsync("буууу! Ссылка не в правильном формате!");
                return;
            }
            httpsCheck = ($"{url[0]}{url[1]}{url[2]}{url[3]}{url[4]}");
            if (httpsCheck != "https")
            {
                await ReplyAsync("буууу! Ссылка не в правильном формате!");
                return;
            }

          

            var accountSubs = account.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            var messList = new List<IUserMessage>();
            
                

            for (var i = 0; i < accountSubs.Length; i++)
            {
                try
                {
                    var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                    var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                    
                    var embed = new EmbedBuilder();
                    embed.WithFooter("Записная книжечка Осьминожек");
                    embed.WithTitle("OctoNotification");
                    embed.WithImageUrl($"{url}");
                    embed.WithDescription($"**{Context.User.Mention}:** {mess}\n");
                    //https://puu.sh/AqCnq/8ac50e7232.mp3
                    //https://puu.sh/AqCse/1e0dbed1d5.mp3
                    //embed.WithUrl("https://puu.sh/AqC2d/715e9eb16e.mp3");
                    embed.AddField("Оценить творчество",
                        "Если тебе нравится блоги этой/этого няши, почему бы не оценить?\n" +
                        "Можешь оценить от 1 до zazz, оценка анонимная, но общую оценку можно посомтрет чрезе команду **topr**\n" +
                        "https://puu.sh/AqC2d/715e9eb16e.mp3");
                    
                    var message =  await dmChannel.SendMessageAsync("", embed: embed);
                  //  await dmChannel.SendFileAsync("https://puu.sh/AqC2d/715e9eb16e.mp3");
                    messList.Add(message);

                    var voteMessToTrack = new BlogVotes(Context.User, message, Context.User, 1);
                    BlogVotesMessIdList.Add(voteMessToTrack);
                   
                }
                catch {  account.SubedToYou = null;
                    for (var j = 0; j < accountSubs.Length; j++)
                    {
                        if (j != i)
                            account.SubedToYou += ($"{accountSubs[j]}|");

                    }
                    UserAccounts.SaveAccounts();

                    var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                    var errorGuyAcc = UserAccounts.GetAccount(globalAccount);
                    var errorGuy = errorGuyAcc.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                    errorGuyAcc.SubToPeople = null;
                    for (var k = 0; k < errorGuy.Length; k++)
                    {
                        if (Convert.ToUInt64(errorGuy[k]) != Context.User.Id)
                            errorGuyAcc.SubToPeople += ($"{errorGuy[k]}|");
                    }
                    UserAccounts.SaveAccounts();
                    var dmCommander = await Context.User.GetOrCreateDMChannelAsync();
                    await dmCommander.SendMessageAsync($"Возникла проблема с {globalAccount.Username}, мы его удалили из списка.");
                } 

            }

            await AddReactionForBlogMessages(messList, Context.Channel);
            await ReplyAsync($"{Context.User.Mention}, твой блог был отправлен!");
        }




        public static async Task AddReactionForBlogMessages(List<IUserMessage> messageList, ISocketMessageChannel contextChannel)
        {
            var zaaz = Emote.Parse("<:zazz:448323858492162049>");

            Parallel.For(0, messageList.Count, async i =>
            {
                try
                {
                    await messageList[i].AddReactionAsync(new Emoji("1⃣"));
                    await messageList[i].AddReactionAsync(new Emoji("2⃣"));
                    await messageList[i].AddReactionAsync(new Emoji("3⃣"));
                    await messageList[i].AddReactionAsync(new Emoji("4⃣"));
                    await messageList[i].AddReactionAsync(zaaz);
                }
                catch
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("AddReactionForBlogMessages CATCH");
                        Console.ResetColor();
                        for (var j = 0; j < messageList.Count; j++)
                        {
                            await messageList[j].DeleteAsync();

                        }

                        await contextChannel.SendMessageAsync(
                            $"Произошла какая-то ошибка, попробуй отправить снова");
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("AddReactionForBlogMessages CATCH-CATCH");
                        Console.ResetColor();
                    }

                }

            });

            await Task.CompletedTask;
        }
    }
}

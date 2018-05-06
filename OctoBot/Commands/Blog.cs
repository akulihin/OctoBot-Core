using Discord.Commands;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using OctoBot.Configs.Users;
using OctoBot.Configs;
using Discord;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                var globalAccount = Global.Client.GetUser(Convert.ToUInt64(accountSubs[i]));
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
                var globalAccount = Global.Client.GetUser(Convert.ToUInt64(accountSubs[i]));
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
            else if (user.IsBot)
            {
                await ReplyAsync("Нельзя подписываться на бота.");
                return;
            }

            var account = UserAccounts.GetAccount(Context.User);
            var EL = UserAccounts.GetAccount(user);


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



            account.SubToPeople += (user.Id.ToString() + "|");
            EL.SubedToYou += (Context.User.Id.ToString() + "|");
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

            var EL = UserAccounts.GetAccount(user);
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

            var ELSubs = EL.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            EL.SubedToYou = null;
            for (var i = 0; i < ELSubs.Length; i++)
            {
                if (Convert.ToUInt64(ELSubs[i]) != Context.User.Id)
                    EL.SubedToYou += ($"{ELSubs[i]}|");

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
            
            for (var i = 0; i < accountSubs.Length; i++)
            {
                try
                {

                    var globalAccount = Global.Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                    var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();


                    var embed = new EmbedBuilder();
                    embed.WithFooter("Записная книжечка Осьминожек");
                    embed.WithTitle("OctoNotification");
                    embed.WithDescription($"**{Context.User.Mention}:** {mess}");
                    await dmChannel.SendMessageAsync("", embed: embed);

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
                    
                    var globalAccount = Global.Client.GetUser(Convert.ToUInt64(accountSubs[i]));
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

            await ReplyAsync($"{Context.User.Mention}, твой блог был отправлен!");
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

            for (var i = 0; i < accountSubs.Length; i++)
            {
                try
                {
                    var globalAccount = Global.Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                    var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                    
                    var embed = new EmbedBuilder();
                    embed.WithFooter("Записная книжечка Осьминожек");
                    embed.WithTitle("OctoNotification");
                    embed.WithImageUrl($"{url}");
                    embed.WithDescription($"**{Context.User.Mention}:** {mess}");
                    await dmChannel.SendMessageAsync("", embed: embed);
                }
                catch {  account.SubedToYou = null;
                    for (var j = 0; j < accountSubs.Length; j++)
                    {
                        if (j != i)
                            account.SubedToYou += ($"{accountSubs[j]}|");

                    }
                    UserAccounts.SaveAccounts();

                    var globalAccount = Global.Client.GetUser(Convert.ToUInt64(accountSubs[i]));
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

            await ReplyAsync($"{Context.User.Mention}, твой блог был отправлен!");
        }



        [Command("topSub")]
        [Alias("TopSubs")]
        public async Task TopSubs()
        {

            
                string result;
                try
                {
                    result = new System.Net.WebClient().DownloadString(@"OctoDataBase/accounts.json");
                }
                catch
                {
                    Console.WriteLine("Failed To ReadFile(TopSubs).");
                    return;
                }

              var data = JsonConvert.DeserializeObject<List<AccountSettings>>(result);


            string[]topu = new string[data.Count];
            var j = 0;
                for (var i = 0; i < data.Count; i++)
                {
                    if (data[i].SubedToYou !=null && data[i].Points != 0 )
                    {
                    
                         var globalAccount = Global.Client.GetUser(data[i].Id);
                         var account = UserAccounts.GetAccount(globalAccount);
                         var accountSubs = account.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                        topu[j] = $"{accountSubs.Length} {account.UserName}";
                        j++;
                    }
                }

            Array.Sort(topu);
                var embed = new EmbedBuilder();
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.WithTitle("OctoNotification");
                embed.WithDescription($"{topu[topu.Length-1]}\n{topu[topu.Length - 2]}\n{topu[topu.Length - 3]}\n{topu[topu.Length - 4 ]}\n{topu[topu.Length - 5]}");
                await ReplyAsync("", embed : embed);
            

        }
    }
}

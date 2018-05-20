using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;

namespace OctoBot.Commands
{

    public class Fact : ModuleBase<SocketCommandContext>
    {


        [Command("записать")]
        [Alias("факт", "write", "write down", "fact")]
        public async Task WriteFuckt(IGuildUser user, [Remainder] string message)
        {

            var account = UserAccounts.GetAccount((SocketUser) user);
            if (account == null)
                return;
         
            account.Fuckt += message + "|";
            UserAccounts.SaveAccounts();
            var id = Context.Message.Id;


            var msg = await Context.Channel.GetMessageAsync(id);
            await msg.DeleteAsync();
            
            await Context.Channel.SendMessageAsync($"We wrote down this fact about {user.Mention}!");

        }

        [Command("факт")]
        [Alias("fact")]
        public async Task ReadFuckt(SocketUser user)
        {
            var account = UserAccounts.GetAccount(user);

            if (account.Fuckt == null)
            {
                await Context.Channel.SendMessageAsync("boole. :c\nWe could not find the facts about this user");
                return;
            }


            var randomFuktArr = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);




            var rand = new Random();
            var randomIndex = rand.Next(randomFuktArr.Length);
            var randomFukt = ($"{randomFuktArr[randomIndex]}");


            string httpsCheck = null;
            if (randomFukt.Length >= 5)
            {
                httpsCheck = ($"{randomFukt[0]}{randomFukt[1]}{randomFukt[2]}{randomFukt[3]}{randomFukt[4]}");
            }


            //onsole.WriteLine($"Длина: {RandomFuktArr.Length} | Индекс: {randomIndex} | HTTP Check: {httpsCheck}");

            string randomNick = null;
            if (account.ExtraUserName != null)
            {
                var extra = account.ExtraUserName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                var randomIndexTwo = rand.Next(extra.Length);
                randomNick = ($"{extra[randomIndexTwo]}");
            }


            var embed = new EmbedBuilder();
            embed.WithColor(Color.Purple);
            embed.WithAuthor(user);
            embed.WithFooter("lil octo notebook");
            if (randomNick != null)
            {
                embed.AddField("Was seen under the nickname: ", " " + randomNick);
            }

            if (httpsCheck == "https")
            {
                embed.WithImageUrl($"{randomFukt}");
            }
            else
                embed.AddField("Random fact: ", " " + randomFukt);


            await Context.Channel.SendMessageAsync("", embed: embed);

        }

        [Command("факт")]
        [Alias("fact")]
        public async Task ReadFucktIndex(SocketUser user, int index)
        {
            var account = UserAccounts.GetAccount(user);
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 10)
            {



                if (account.Fuckt == null)
                {
                    await Context.Channel.SendMessageAsync("boole. :c\nWe could not find the facts about this user");
                    return;
                }


                var randomFuktArr = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);




                var rand = new Random();

                var randomFukt = ($"{randomFuktArr[index]}");


                string httpsCheck = null;
                if (randomFukt.Length >= 5)
                {
                    httpsCheck = ($"{randomFukt[0]}{randomFukt[1]}{randomFukt[2]}{randomFukt[3]}{randomFukt[4]}");
                }


                //onsole.WriteLine($"Длина: {RandomFuktArr.Length} | Индекс: {randomIndex} | HTTP Check: {httpsCheck}");

                string randomNick = null;
                if (account.ExtraUserName != null)
                {
                    var extra = account.ExtraUserName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                    var randomIndexTwo = rand.Next(extra.Length);
                    randomNick = ($"{extra[randomIndexTwo]}");
                }


                var embed = new EmbedBuilder();
                embed.WithColor(Color.Purple);
                embed.WithAuthor(user);
                embed.WithFooter("lil octo notebook");
                if (randomNick != null)
                {
                    embed.AddField("Was seen under the nickname: ", " " + randomNick);
                }

                if (httpsCheck == "https")
                {
                    embed.WithImageUrl($"{randomFukt}");
                }
                else
                    embed.AddField("Random fact: ", " " + randomFukt);


                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            

        }


        [Command("ВсеФакты")]
        [Alias("Все Факты", "allfact", "allfacts", "all fact", "all facts")]
        public async Task DeleteTheFucktUser( )
        {
         
            var account = UserAccounts.GetAccount(Context.User);
            if (account.OctoPass >= 3)
            {
                var fuckts = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                string mess = "";
                for (var i = 0; i < fuckts.Length; i++)
                {
                    
                    mess += ($"index: {i} | {fuckts[i]}\n");
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("All the facts about you:");
                embed.WithDescription($"{mess}\n**del [index]** to delete the fact");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            else
                await Context.Channel.SendMessageAsync("Boole :< You do not have 3rd level tolerance");

        }


        [Command("ВсеФакты")]
        [Alias("Все Факты", "allfact", "allfacts", "all fact", "all facts")]
        public async Task DeleteTheFuckt(IGuildUser user)
        {
            var account = UserAccounts.GetAccount((SocketUser) user);
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 4)
            {


                var fuckts = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                string mess = "";
                for (var i = 0; i < fuckts.Length; i++)
                {
                    
                    mess += ($"index: {i} | {fuckts[i]}\n");
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("All the facts about you:");
                embed.WithDescription($"{mess}\n**del [index]** to delete the fact");
                await Context.Channel.SendMessageAsync("", embed: embed);
          
            }
            else
                await Context.Channel.SendMessageAsync("Boole :< You do not have 4rd level tolerance");
        }


        [Command("УдалитьФакт")]
        [Alias("Удалить Факт", "del")]
        public async Task DeleteTheFucktUser(int index)
        {
            
            var account = UserAccounts.GetAccount(Context.User);
            if (account.OctoPass >= 2)
            {
                var fuckts = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                account.Fuckt = null;

                for (var i = 0; i < fuckts.Length; i++)
                {
                    if (i != index)
                        account.Fuckt += ($"{fuckts[i]}|");

                }

                UserAccounts.SaveAccounts();

                await Context.Channel.SendMessageAsync(
                    $"fact under index {index} was removed from the lil octo notebook ;c");

            }
            else
                await Context.Channel.SendMessageAsync("Boole :< You do not have 3rd level tolerance");
        }



        [Command("УдалитьФакт")]
        [Alias("Удалить Факт", "del")]
        public async Task DeleteTheFuckt(IGuildUser user, int index)
        {
            var account = UserAccounts.GetAccount((SocketUser) user);
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 100)
            {
                var fuckts = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                account.Fuckt = null;

                for (var i = 0; i < fuckts.Length; i++)
                {
                    if (i != index)
                        account.Fuckt += ($"{fuckts[i]}|");

                }

                UserAccounts.SaveAccounts();

                await Context.Channel.SendMessageAsync(
                    $"fact under index {index} was removed from the lil octo notebook ;c");

            }
            else
                await Context.Channel.SendMessageAsync("Boole :< You do not have 10th level tolerance");
        }

    }
}



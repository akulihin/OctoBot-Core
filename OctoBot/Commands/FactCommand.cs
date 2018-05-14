using System;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using OctoBot.Configs.Users;

namespace OctoBot.Commands
{

    public class Fact : ModuleBase<SocketCommandContext>
    {
        [Command("рандом")]
        public async Task GetRandomPerson()
        {
            string json;
            using (var client = new WebClient())
            {
                json = client.DownloadString("https://randomuser.me/api/");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            string firstName = dataObject.results[0].name.first.ToString();
            string lastName = dataObject.results[0].name.last.ToString();
            string avatarUrl = dataObject.results[0].picture.large.ToString();

            var embed = new EmbedBuilder();
            embed.WithThumbnailUrl(avatarUrl);
            embed.WithTitle("Странный API");
            embed.AddInlineField("First name", firstName);
            embed.AddInlineField("Last name", lastName);

            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("записать")]
        [Alias("факт")]
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
            
            await Context.Channel.SendMessageAsync($"Мы записали этот факт о {user.Mention}!");

        }

        [Command("факт")]
        public async Task ReadFuckt(SocketUser user)
        {
            var account = UserAccounts.GetAccount(user);

            if (account.Fuckt == null)
            {
                await Context.Channel.SendMessageAsync("бууууль :c\nмы не смогли найти фактов об этом юзере");
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
            embed.WithFooter("Записная книжечка Осьминожек");
            if (randomNick != null)
            {
                embed.AddField("Был замечен под ником: ", " " + randomNick);
            }

            if (httpsCheck == "https")
            {
                embed.WithImageUrl($"{randomFukt}");
            }
            else
                embed.AddField("Рандомный факт: ", " " + randomFukt);


            await Context.Channel.SendMessageAsync("", embed: embed);

        }

        [Command("факт")]
        public async Task ReadFucktIndex(SocketUser user, int index)
        {
            var account = UserAccounts.GetAccount(user);
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 10)
            {



                if (account.Fuckt == null)
                {
                    await Context.Channel.SendMessageAsync("бууууль :c\nмы не смогли найти фактов об этом юзере");
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
                embed.WithFooter("Записная книжечка Осьминожек");
                if (randomNick != null)
                {
                    embed.AddField("Был замечен под ником: ", " " + randomNick);
                }

                if (httpsCheck == "https")
                {
                    embed.WithImageUrl($"{randomFukt}");
                }
                else
                    embed.AddField("Рандомный факт: ", " " + randomFukt);


                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            

        }


        [Command("ВсеФакты")]
        [Alias("Все Факты")]
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
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.WithTitle("Твои подписки:");
                embed.WithDescription($"{mess}\n**УдалитьФакт [index]** Чтобы удалить");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска 3го уровня!");

        }


        [Command("ВсеФакты")]
        [Alias("Все Факты")]
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
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.WithTitle("Твои подписки:");
                embed.WithDescription($"{mess}\n**УдалитьФакт [user] [index]** Чтобы удалить");
                await Context.Channel.SendMessageAsync("", embed: embed);
          
            }
            else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска 4го уровня!");
        }


        [Command("УдалитьФакт")]
        [Alias("Удалить Факт")]
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
                    $"факт под номером {index} был удален из записной книжечки Осьминожек ;c");

            }
            else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска 3го уровня!");
        }



        [Command("УдалитьФакт")]
        [Alias("Удалить Факт")]
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
                    $"факт под номером {index} был удален из записной книжечки Осьминожек ;c");

            }
            else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска 10го уровня!");
        }

    }
}



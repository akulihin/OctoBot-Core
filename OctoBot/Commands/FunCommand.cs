using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Handeling;
using OctoBot.Services;
namespace OctoBot.Commands
{
    public class Fun : ModuleBase<SocketCommandContextCustom>
    {


        [Command("pick")]
        public async Task Pick([Remainder] string message)
        {
            try{
            var option = message.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            var rand = new Random();
            var selection = option[rand.Next(0, option.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("I chose instead " + Context.User.Username);
            embed.WithDescription(selection);

            embed.WithColor(new Color(255, 0, 94));
            embed.WithThumbnailUrl("https://i.imgur.com/I3o0bm4.jpg");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **pick option1|option2|option3**(output random option (can be as many as you want))\n");
            }
        }

        [Command("roll")]
        [Alias("Роллл", "Ролл")]
        public async Task Roll(int times, ulong number, [Remainder] string rem  = null)
        {
            try
            {
                string mess = "";
                if (times >= 100)
                {
                    await ReplyAsync("Boole! We are not going to roll that many times!");
                    return;
                }

                if (number > 999999999)
                {
                    await ReplyAsync("Boole! This numbers is way too big for us :c");
                    return;
                }

                for (var i = 0; i < times; i++)
                {
                    var randRoll = new Random();
                    var randomIndexRoll = randRoll.Next((int) number + 1);
                    if (randomIndexRoll == 0)
                        randomIndexRoll = 1;
                    mess += ($"It's a {randomIndexRoll}!\n");
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle($"Roll {times} times:");
                embed.WithDescription($"{mess}");


                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
                }

              
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **roll [times] [max_value_of_roll]**\n" +
                                 "Alias: Роллл, Ролл");
            }
        }


        [Command("roll")]
        [Alias("Роллл", "Ролл")]
        public async Task Roll(ulong number)
        {


            try
            {
                
                
                var randRoll = new Random();
                var randomIndexRoll = randRoll.Next((int) number + 1);
                if (randomIndexRoll == 0)
                    randomIndexRoll = 1;
                await Context.Channel.SendMessageAsync($"It's a {randomIndexRoll}!");
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **roll [max_value_of_roll]**\n" +
                                 "Alias: Роллл, Ролл");
            }
        }


        [Command("ping")]
        [Alias("пинг")]
        public async Task DefaultPing()
        {
            try
            {
            await ReplyAsync($"{Context.User.Mention} pong!");
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **ping**\n");
            }
        }



        [Command("DM")]
        public async Task DmMess()
        {
            try {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync("Boole.");
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **DM**(sends you a DM)\n");
            }
        }

        [Command("guess", RunMode = RunMode.Async)]
        [Alias("Рулетка", "угадайка")]
        public async Task GuessGame(ulong enter)
        {
            try {
            int amount = (int) enter;

            var userAccount = UserAccounts.GetAccount(Context.User);
            var octoAcccount = UserAccounts.GetAccount(Context.Guild.CurrentUser);

            if (amount > userAccount.Points || amount <= 0)
            {
                await Context.Channel.SendMessageAsync("You do not have enough OktoPoints! Or you just entered something wrong.");
                return;
            }


            var randSlot = new Random();
            var slots = randSlot.Next(72);


            await Context.Channel.SendMessageAsync($"Number of slots **{slots}**. What is your choice?");
            var response = await CommandHandeling.AwaitMessage(Context.User.Id, Context.Channel.Id, 10000);

            bool result = int.TryParse(response.Content, out _);
            if (result)
            {
                var choise = Convert.ToInt32(response.Content);
                var bank = Math.Abs((amount * slots) / 5);



                var rand = new Random();
                var random = rand.Next(slots);

                if (choise == random)
                {
                    userAccount.Points += bank;
                    UserAccounts.SaveAccounts();
                    await Context.Channel.SendMessageAsync(
                        $"You won **{bank}** OctoPoints!\nNow you have **{userAccount.Points}** OctoPoints!");
                    userAccount.Points += bank;
                    UserAccounts.SaveAccounts();

                }
                else
                {

                    await Context.Channel.SendMessageAsync(
                        $"booole. Yuor **{amount}** OctoPoints stayed with us. Btw, number was **{random}**");
                    userAccount.Points -= amount;
                    octoAcccount.Points += amount;
                    UserAccounts.SaveAccounts();

                }
            }
            else
                await Context.Channel.SendMessageAsync(
                    $"The choice should be between 0 and {slots}, answer only with a number.");
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **guess [rate_num]**(like cassino, ty it!)\n" +
                                 "Alias: Рулетка, угадайка");
            }
        }
    }
}

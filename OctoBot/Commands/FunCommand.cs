using System;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs.Users;
using OctoBot.Handeling;

namespace OctoBot.Commands
{
    public class Fun : ModuleBase<SocketCommandContext>
    {


        [Command("pick")]
        public async Task Pick([Remainder] string message)
        {

            var option = message.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            var rand = new Random();
            var selection = option[rand.Next(0, option.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("I chose instead " + Context.User.Username);
            embed.WithDescription(selection);

            embed.WithColor(new Color(255, 0, 94));
            embed.WithThumbnailUrl("https://i.imgur.com/I3o0bm4.jpg");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("roll")]
        [Alias("Роллл", "Ролл")]
        public async Task Roll(int times, ulong number)
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
                    mess += ($"{randomIndexRoll} dropped out\n");
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle($"Roll {times} times:");
                embed.WithDescription($"{mess}");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            catch
            {
                await ReplyAsync("Boole! This numbers is way too big for us :c");
                return;
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
                await Context.Channel.SendMessageAsync($"{randomIndexRoll} dropped out");
            }
            catch
            {
                await ReplyAsync("Boole! This numbers is way too big for us :c");
                return;
            }
        }


        [Command("ping")]
        [Alias("пинг")]
        public async Task DefaultPing()
        {
            await ReplyAsync($"{Context.User.Mention} pong!");
         
        }



        [Command("DM")]
        public async Task DmMess()
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync("Boole.");

        }

        [Command("guess", RunMode = RunMode.Async)]
        [Alias("Рулетка", "угадайка")]
        public async Task GuessGame(ulong enter)
        {

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
        
        [Command("ApdMessEm")]
        public async Task ApdMessEmbed(ulong channeld, ulong messId, [Remainder] string messa)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithColor(Color.Blue);
                // embed.WithFooter("Записная книжечка Осьминожек");
                embed.WithFooter("lil octo notebook");
                embed.AddInlineField("Сообщение:",
                    $"{messa}");

                var textChannel = await Context.Guild.GetTextChannel(channeld)
                    .GetMessageAsync(messId) as IUserMessage;

                await textChannel.ModifyAsync(mess =>
                {
                    mess.Embed = embed.Build();
                    // This somehow can't be empty or it won't update the 
                    // embed propperly sometimes... I don't know why
                    // message.Content =  Constants.InvisibleString;
                });
                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot update this message!");
            }
        }

        [Command("ApdMess")]
        public async Task ApdMessString(ulong channeld, ulong messId, [Remainder] string messa)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                var message = await Context.Guild.GetTextChannel(channeld)
                    .GetMessageAsync(messId) as IUserMessage;

                var builder = new StringBuilder();
                builder.Append($"{messa}");

                if (message != null) await message.ModifyAsync(m => m.Content = builder.ToString());
                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot update this message!");
            }
        }

        [Command("SendMess")]
        public async Task SendMessString(ulong channeld, [Remainder] string messa)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                var textChannel = Context.Guild.GetTextChannel(channeld);
                await textChannel.SendMessageAsync($"{messa}");

                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot send this message!");
            }
        }

        [Command("emo")]
        public async Task EmoteToMEss(ulong channeld, ulong messId, [Remainder] string messa)
        {

            try
            {
                Console.WriteLine(messa);
                if (Context.User.Id != 181514288278536193)
                    return;
                var emote = Emote.Parse($"{messa}");

                var check = messa.ToCharArray();
                if (check[0] == '<')
                    if (await Context.Guild.GetTextChannel(channeld).GetMessageAsync(messId) is IUserMessage message) await message.AddReactionAsync(emote);  
              else
                if (await Context.Guild.GetTextChannel(channeld).GetMessageAsync(messId) is IUserMessage mess) await mess.AddReactionAsync((new Emoji($"{messa}")));

                await Task.CompletedTask;
            }
            catch
            {
                //
            }
        }


    }
}

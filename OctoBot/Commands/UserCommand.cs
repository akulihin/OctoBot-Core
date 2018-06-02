using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;
using OctoBot.Handeling;

namespace OctoBot.Commands
{
    public class UserCommand : ModuleBase<SocketCommandContext>
    {
        [Command("stats")]
        [Alias("статы")]
        public async Task Xp()
        {
            try
            {
            var account = UserAccounts.GetAccount(Context.User);

            var avatar =
                ("https://cdn.discordapp.com/avatars/" + Context.User.Id + "/" + Context.User.AvatarId + ".png");

            var usedNicks = "";
            var usedNicks2 = "";
            if (account.ExtraUserName != null)
            {

                var extra = account.ExtraUserName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < extra.Length; i++)
                {

                    if (i == extra.Length - 1)
                    {
                        usedNicks += (extra[i]);

                    }
                    else if (usedNicks.Length <= 1000)
                    {
                        usedNicks += (extra[i] + ", ");
                    }
                    else
                    {
                        usedNicks2 += (extra[i] + ", ");
                    }
                }

            }
            else
                usedNicks = "No one :c";

            var octopuses = "";
            if (account.Octopuses != null)
            {
                string[] octo = account.Octopuses.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);



                for (int i = 0; i < octo.Length; i++)
                {

                    if (i == octo.Length - 1)
                    {
                        octopuses += (octo[i]);

                    }
                    else
                    {

                        octopuses += (octo[i] + ", ");
                    }

                }

            }
            else
            {
                octopuses = "No one :c";
            }

            string[] warns = null;
            if (account.Warnings != null)
            {
                warns = account.Warnings.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            }

            var embed = new EmbedBuilder();

            embed.WithColor(Color.Blue);
            embed.WithAuthor(Context.User);
            embed.WithFooter("lil octo notebook");
            embed.AddInlineField("ID", "" + Context.User.Id);
            embed.AddInlineField("Status", "" + Context.User.Status);

            embed.AddInlineField("UserName", "" + Context.User);

            embed.AddInlineField("NickName", "" + Context.User.Mention);
            embed.AddInlineField("Octo Points", "" + account.Points);
            embed.AddInlineField("Octo Reputation", "" + account.Rep);
            embed.AddInlineField("Access LVL", "" + account.OctoPass);
            embed.AddInlineField("User LVL", "" + account.Lvl);
            embed.AddInlineField("Pull Points", "" + account.DailyPullPoints);
            if (warns != null)
                embed.AddInlineField("Warnings", "" + warns.Length);
            else
                embed.AddInlineField("Warnings", "Clear.");
            embed.AddField("Best 2048 Game Score", $"{account.Best2048Score}");
            embed.AddField("OctoCollection ", "" + octopuses);
            embed.AddField("Used Nicknames", "" + usedNicks);
            if (usedNicks2.Length >= 5)
                embed.AddField("Extra Nicknames", "" + usedNicks2);
            embed.WithThumbnailUrl($"{avatar}");
            //embed.AddField("Роли", ""+avatar);

            await Context.Channel.SendMessageAsync("", embed: embed);
        }
        catch
        {
              //  await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Stats**");
            }
        }

        [Command("OctoRep")]
        [Alias("Octo Rep", "Rep", "октоРепа", "Окто Репа", "Репа")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddPoints(IGuildUser user, long rep)
        {
            try{
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 100)
            {
                var account = UserAccounts.GetAccount((SocketUser) user);
                account.Rep += rep;
                UserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync(
                    $"{rep} Octo Reputation were credited, altogether {user.Mention} have {account.Rep} Octo Reputation!");
            }
            else
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
            }
            catch
            {
             //   await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **OctoRep [ping_user(or user ID)] [number_of_points]**\n" +
             //                    "Alias: Rep, октоРепа, Репа, Окто Репа");
            }
        }

        [Command("OctoPoint")]
        [Alias("Octo Point", "OctoPoints", "Octo Points", "ОктоПоинты", "Окто Поинты", "Поинты", "points", "point")]
       // [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GivePoints(IGuildUser user, long points)
        {
            try {
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 100)
            {


                var account = UserAccounts.GetAccount((SocketUser) user);
                account.Points += points;
                UserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync(
                    $"{points} Octo Points were credited, altogether {user.Mention} have {account.Points} Octo Points!");
            }
            else
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
            }
            catch
            {
              //  await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **OctoPoint [ping_user(or user ID)] [number_of_points]**\n" +
               //                  "Alias: OctoPoints, ОктоПоинты, Поинты, points, point");
            }
        }


        [Command("stats")]
        [Alias("Статы")]
        public async Task CheckUser(IGuildUser user)
        {
            try {
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 4)
            {


                var account = UserAccounts.GetAccount((SocketUser) user);

                var avatar = ("https://cdn.discordapp.com/avatars/" + user.Id + "/" + user.AvatarId + ".png");

                var usedNicks = "";
                var usedNicks2 = "";
                if (account.ExtraUserName != null)
                {

                    var extra = account.ExtraUserName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
         
                    for (var i = 0; i < extra.Length; i++)
                    {
                    
                        if (i == extra.Length - 1)
                        {
                            usedNicks += (extra[i]);

                        }
                        else if(usedNicks.Length <= 1000)
                        {
                            usedNicks += (extra[i] + ", ");
                        }
                        else
                        {
                            usedNicks2 += (extra[i] + ", ");
                        }
                    }

                }
                else
                    usedNicks = "No one :c";



                var octopuses = "";
                if (account.Octopuses != null)
                {
                    var octo = account.Octopuses.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);



                    for (int i = 0; i < octo.Length; i++)
                    {

                        if (i == octo.Length - 1)
                        {
                            octopuses += (octo[i]);

                        }
                        else
                        {

                            octopuses += (octo[i] + ", ");
                        }

                    }

                }
                else
                {
                    octopuses = "No one :c";
                }

                var warnings = "No one :c";
                if (account.Warnings != null)
                {
                    var warns = account.Warnings.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    warnings = "";
                    for (var index = 0; index < warns.Length; index++)
                    {
                        var t = warns[index];
                        warnings += t + "\n";
                    }
                }
                var embed = new EmbedBuilder();
               
                embed.WithColor(Color.Purple);
                embed.WithAuthor(user);
                embed.WithFooter("lil octo notebook");
                embed.AddInlineField("ID", "" + user.Id);
                embed.AddInlineField("Status", "" + user.Status);
                embed.AddInlineField("Registered", "" + user.CreatedAt);
                embed.AddInlineField("UserName", "" + user);
                embed.AddInlineField("Joined", "" +  user.JoinedAt);
                embed.AddInlineField("NickName", "" + user.Mention);
                embed.AddInlineField("Octo Points", "" + account.Points);
                embed.AddInlineField("Octo Reputation", "" + account.Rep);
                embed.AddInlineField("Access LVL", "" + account.OctoPass);
                embed.AddInlineField("User LVL", "" + account.Lvl);
                embed.AddInlineField("Pull Points", "" + account.DailyPullPoints);
                embed.AddField("Warnings", "" + warnings);
                embed.AddField("Best 2048 Game Score", $"{account.Best2048Score}");
                embed.AddField("OctoCollection ", "" + octopuses);
                embed.AddField("Used Nicknames", "" + usedNicks);
                if(usedNicks2.Length >=5)
                    embed.AddField("Extra Nicknames", "" + usedNicks2);
                embed.WithThumbnailUrl($"{avatar}");

                await Context.Channel.SendMessageAsync("", embed: embed);
            }else
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
            }
            catch
            {
            //    await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Stats [user_ping(or user ID)]**");
            }
        }

        [Command("pass", RunMode = RunMode.Async)]
        [Alias("Пасс", "Купить Пропуск", "Пропуск", "КупитьПропуск", "Доступ")]
        public async Task BuyPass()
        {
            try {
                var account = UserAccounts.GetAccount(Context.User);
                var cost = 4000 * (account.OctoPass + 1);

            if (account.Points >= cost)
            {
                await Context.Channel.SendMessageAsync(
                    $"Are you sure about buying pass #{account.OctoPass + 1} for {cost} Octo Points? Than write **yes**!");
                var response = await CommandHandeling.AwaitMessage(Context.User.Id, Context.Channel.Id, 6000);
               
                if (response.Content == "yes" || response.Content == "Yes")
                {
                    account.OctoPass += 1;
                    account.Points -= cost;
                    UserAccounts.SaveAccounts();
                    await Context.Channel.SendMessageAsync($"Booole! You've Got Access **#{account.OctoPass}**");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("You should say `yes` или `Yes` in 6s to get the pass.");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(
                    $"You did not earn enough Octo Points, current amount: **{account.Points}**\nFor pass #{account.OctoPass + 1} you will need **{cost}** Octo Points!");
            }
            }
            catch
            {
               // await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **pass**\n Alias: Пасс, Пропуск, Доступ, КупитьПропуск, Купить Пропуск");
            }
        }

        [Command("CheckLvlLOL")]
        public async Task Check(uint xp)
        {
            try {
            var level = (uint)Math.Sqrt((double)xp / 100);
            await Context.Channel.SendMessageAsync("Это " + level + "сможешь ли ты достичь высот?");
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Stats**");
            }
        }

        [Command("GiftPoints")]
        [Alias("Gift Points", "GiftPoint", "Gift Point")]
        public async Task GidftPoints(IGuildUser user, long points)
        {
            try {
            var passCheck = UserAccounts.GetAccount(Context.User);

            if (passCheck.OctoPass >= 1)
            {

                if (points <= 0)
                {
                    await Context.Channel.SendMessageAsync("You cannot send 0 or -number, boo!");
                    return;
                }


                if (passCheck.Points >= points)
                {
                    var account = UserAccounts.GetAccount((SocketUser) user);

                    var taxes = points * 0.9;
                    var bot = UserAccounts.GetAccount(Context.Client.CurrentUser);


                    account.Points += (int) taxes;
                    passCheck.Points -= points;


                    var toBank = (points * 1.1) - points;
                    bot.Points += (int) toBank;
                    UserAccounts.SaveAccounts();
                    await Context.Channel.SendMessageAsync(
                        $"Was transferred{points}\n {user.Mention} now have {account.Points} Octo Points!\nyou have left {passCheck.Points}\ntaxes: {taxes}");

                }
                else
                    await Context.Channel.SendMessageAsync($"You do not have enough Octo Points to pass them.");

            }
            else
            {
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
            }
            }
            catch
            {
           //     await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **GiftPoints [ping_user(or user ID)] [number_of_points]**\nAlias: GiftPoint ");
            }
        }

    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
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
            
            var account = UserAccounts.GetAccount(Context.User);

            var avatar = ("https://cdn.discordapp.com/avatars/" + Context.User.Id + "/" + Context.User.AvatarId + ".png");

            var usedNicks = "";

            if (account.ExtraUserName != null)
            {

                var extra = account.ExtraUserName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
         
                for (var i = 0; i < extra.Length; i++)
                {
                    
                    if (i == extra.Length - 1)
                    {
                        usedNicks += (extra[i]);

                    }
                    else
                    {
                        usedNicks += (extra[i] + ", ");
                    }
                }

            }
            else
                usedNicks = "No one :c";

            var octopuses = "";
            if (account.Octopuses != null)
            {
                string[] octo = account.Octopuses.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);



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
            if(warns != null)
            embed.AddInlineField("Warnings", "" + warns.Length);
            else
            embed.AddInlineField("Warnings", "Clear." );
            embed.AddField("Best 2048 Game Score", $"{account.Best2048Score}");
            embed.AddField("OctoCollection ", "" + octopuses);   
            embed.AddField("Used Nicknames", "" + usedNicks);
            embed.WithThumbnailUrl($"{avatar}");
            //embed.AddField("Роли", ""+avatar);

            await Context.Channel.SendMessageAsync("", embed: embed);



            //await Context.Channel.SendMessageAsync($"У тебя {account.Points} Octo Points и {account.Rep} Octo Reputation");
        }

        [Command("OctoRep")]
        [Alias("Octo Rep", "Rep", "октоРепа", "Окто Репа", "Репа")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddPoints(IGuildUser user, long rep)
        {
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

        [Command("OctoPoint")]
        [Alias("Octo Point", "OctoPoints", "Octo Points", "ОктоПоинты", "Окто Поинты", "Поинты", "points")]
       // [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GivePoints(IGuildUser user, long points)
        {
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



        [Command("stats")]
        [Alias("Статы")]

        public async Task CheckUser(IGuildUser user)
        {

           
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 4)
            {


                var account = UserAccounts.GetAccount((SocketUser) user);

                var avatar = ("https://cdn.discordapp.com/avatars/" + user.Id + "/" + user.AvatarId + ".png");

                var usedNicks = "";


                if (account.ExtraUserName != null)
                {
                    string[] extra = account.ExtraUserName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);



                    for (var i = 0; i < extra.Length; i++)
                    {
                        
                        if (i == extra.Length - 1)
                        {
                            usedNicks += (extra[i]);

                        }
                        else
                        {
                            usedNicks += (extra[i] + ", ");
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
                embed.WithThumbnailUrl($"{avatar}");

                await Context.Channel.SendMessageAsync("", embed: embed);
            }else
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
        }




        [Command("pass", RunMode = RunMode.Async)]
        [Alias("ПАсс", "Купить Пропуск", "Пропуск", "КупитьПропуск", "Доступ")]
        public async Task BuyPass()
        {
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


        [Command("CheckLvlLOL")]
        public async Task Check(uint xp)
        {

            var level = (uint)Math.Sqrt(xp / 100);
            await Context.Channel.SendMessageAsync("Это " + level + "сможешь ли ты достичь высот?");

        }

        [Command("GiftPoints")]
        [Alias("Gift Points", "GiftPoint", "Gift Point")]
        public async Task GidftPoints(IGuildUser user, long points)
        {

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

        [Command("topo")]
        [Alias("topp")]
        public async Task TopByOctoPoints(int page = 1)
        {
            if (page < 1)
            {
                await ReplyAsync("Are you fucking sure about that?");
                return;
            }

            var currentGuildUsersId = Context.Guild.Users.Select(user => user.Id);
            // Get only accounts of this server
            var accounts = UserAccounts.GetFilteredAccounts(acc => currentGuildUsersId.Contains(acc.Id));

            const int usersPerPage = 9;

            var lastPage = 1 + (accounts.Count / (usersPerPage+1));
            if (page > lastPage)
            {
                await ReplyAsync($"Boole. Last Page is {lastPage}");
                return;
            }
       
            var ordered = accounts.OrderByDescending(acc => acc.Points).ToList();

            var embB = new EmbedBuilder()
                .WithTitle("Top By Octo Points:")
                .WithFooter($"Page {page}/{lastPage}");


            page--;
            for (var i = 1; i <= usersPerPage && i + usersPerPage * page <= ordered.Count; i++)
            {
              
                var account = ordered[i - 1 + usersPerPage * page];
                var user = Global.Client.GetUser(account.Id);
                embB.AddField($"#{i + usersPerPage * page} {user.Username}", $"{account.Points} OctoPoints", true);
            }

            await ReplyAsync("", false, embB.Build());
        }

        [Command("tops")]
        public async Task TopBySubc(int page = 1)
        {
            if (page < 1)
            {
                await ReplyAsync("Are you fucking sure about that?");
                return;
            }

            // Get only accounts of this server
            var accounts = UserAccounts.GetFilteredAccounts(acc => Context.Guild.Users.Select(user => user.Id).Contains(acc.Id));

            const int usersPerPage = 9;

            var lastPage = 1 + (accounts.Count / (usersPerPage+1));
            if (page > lastPage)
            {
                await ReplyAsync($"Boole. Last Page is {lastPage}");
                return;
            }

            for (var j = 0; j < accounts.Count; j++)
            {
                if (accounts[j].SubedToYou == null)
                    accounts[j].SubedToYou = "0";
            }
            var ordered = accounts.OrderByDescending(acc => acc.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries).Length).ToList();

            var embB = new EmbedBuilder()
                .WithTitle("Top By Octo Points:")
                .WithFooter($"Page {page}/{lastPage}");
   
            page--;

        
            
            for (var i = 1; i <= usersPerPage && i + usersPerPage * page <= ordered.Count; i++)
            {
               
                var account = ordered[i - 1 + usersPerPage * page];
                var user = Global.Client.GetUser(account.Id);

                var size = account.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries).Length;
                if ( size == 1)
                    size = 0;
                embB.AddField($"#{i + usersPerPage * page} {user.Username}", $"{size} Subscribers", true);
            }
            
            await ReplyAsync("", false, embB.Build());
        }

        [Command("top")]
        [Alias("topl", "topa")]
        public async Task TopByLvL(int page = 1)
        {
            if (page < 1)
            {
                await ReplyAsync("Are you fucking sure about that?");
                return;
            }


            var currentGuildUsersId = Context.Guild.Users.Select(user => user.Id);
            // Get only accounts of this server
            var accounts = UserAccounts.GetFilteredAccounts(acc => currentGuildUsersId.Contains(acc.Id));


            for (var j = 0; j < accounts.Count; j++)
            {

                accounts[j].Lvl = (uint) Math.Sqrt(accounts[j].LvlPoinnts / 150);
                UserAccounts.SaveAccounts();
            }

            const int usersPerPage = 9;

            var lastPage = 1 + (accounts.Count / (usersPerPage+1));
            if (page > lastPage)
            {
                await ReplyAsync($"Boole. Last Page is {lastPage}");
                return;
            }
       
            var ordered = accounts.OrderByDescending(acc => acc.Lvl).ToList();

            var embB = new EmbedBuilder()
                .WithTitle("Top By Activity:")
                .WithFooter($"Page {page}/{lastPage}");


            page--;
            for (var i = 1; i <= usersPerPage && i + usersPerPage * page <= ordered.Count; i++)
            {
              
                var account = ordered[i - 1 + usersPerPage * page];
                var user = Global.Client.GetUser(account.Id);
                embB.AddField($"#{i + usersPerPage * page} {user.Username}", $"{account.Lvl} LVL", true);
            }

            await ReplyAsync("", false, embB.Build());
        }

        [Command("topr")]
        [Alias("topb")]
        public async Task TopByRating(int page = 1)
        {
            if (page < 1)
            {
                await ReplyAsync("Are you fucking sure about that?");
                return;
            }


            var currentGuildUsersId = Context.Guild.Users.Select(user => user.Id);
            // Get only accounts of this server
            var accounts = UserAccounts.GetFilteredAccounts(acc => currentGuildUsersId.Contains(acc.Id));


            for (var j = 0; j < accounts.Count; j++)
            {
                if (accounts[j].AvarageScoreVotes <= 0.0)
                {
                    accounts[j].AvarageScoreVotes = 0;
                }
                else
                {
                    accounts[j].AvarageScoreVotes = accounts[j].BlogVotesSum / accounts[j].BlogVotesQty;
                }

                UserAccounts.SaveAccounts();
            }

            const int usersPerPage = 9;

            var lastPage = 1 + (accounts.Count / (usersPerPage+1));
            if (page > lastPage)
            {
                await ReplyAsync($"Boole. Last Page is {lastPage}");
                return;
            }
       
            var ordered = accounts.OrderByDescending(acc => acc.AvarageScoreVotes).ToList();

            var embB = new EmbedBuilder()
                .WithTitle("Top By Avarage Rating in Blog:")
                .WithFooter($"Page {page}/{lastPage}");


            page--;
            for (var i = 1; i <= usersPerPage && i + usersPerPage * page <= ordered.Count; i++)
            {
              
                var account = ordered[i - 1 + usersPerPage * page];
                var user = Global.Client.GetUser(account.Id);
                embB.AddField($"#{i + usersPerPage * page} {user.Username}", $"{account.AvarageScoreVotes} {new Emoji("⭐")} out of {account.BlogVotesQty} votes", true);
            }

            await ReplyAsync("", false, embB.Build());
        }


    }
}

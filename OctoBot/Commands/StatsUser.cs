using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;

namespace OctoBot.Commands
{
    public class StatsUser : ModuleBase<SocketCommandContextCustom>
    {
        [Command("stats")]
        [Alias("статы")]
        public async Task Xp()
        {
            try
            {
            var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);

           
             //   ("https://cdn.discordapp.com/avatars/" + Context.User.Id + "/" + Context.User.AvatarId + ".png");

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
                usedNicks = "None";

            var octopuses = "";
            if (account.Octopuses != null)
            {
                var octo = account.Octopuses.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);



                for (var i = 0; i < octo.Length; i++)
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
                octopuses = "None";
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
            embed.AddField("ID", "" + Context.User.Id, true);
            embed.AddField("Status", "" + Context.User.Status, true);

            embed.AddField("UserName", "" + Context.User, true);

            embed.AddField("NickName", "" + Context.User.Mention, true);
            embed.AddField("Octo Points", "" + account.Points, true);
            embed.AddField("Octo Reputation", "" + account.Rep, true);
            embed.AddField("Access LVL", "" + account.OctoPass, true);
            embed.AddField("User LVL", "" + Math.Round(account.Lvl, 2), true );
            embed.AddField("Pull Points", "" + account.DailyPullPoints, true);
                embed.AddField("Best 2048 Game Score", $"{account.Best2048Score}", true);
            if (warns != null)
                embed.AddField("Warnings", "" + warns.Length, true);
            else
                embed.AddField("Warnings", "Clear.", true);
            
            embed.AddField("OctoCollection ", "" + octopuses);
            embed.AddField("Used Nicknames", "" + usedNicks);
            if (usedNicks2.Length >= 5)
                embed.AddField("Extra Nicknames", "" + usedNicks2);
            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
            //embed.AddField("Роли", ""+avatar);

                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);

        }
        catch
        {
              //  await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Stats**");
            }
        }

        [Command("stats")]
        [Alias("Статы")]
        public async Task CheckUser(IGuildUser user)
        {
            try {
            var comander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            if (comander.OctoPass >= 4)
            {


                var account = UserAccounts.GetAccount((SocketUser) user, Context.Guild.Id);

             //   var avatar = ("https://cdn.discordapp.com/avatars/" + user.Id + "/" + user.AvatarId + ".png");

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
                    usedNicks = "None";



                var octopuses = "";
                if (account.Octopuses != null)
                {
                    var octo = account.Octopuses.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);



                    for (var i = 0; i < octo.Length; i++)
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
                    octopuses = "None";
                }

                var warnings = "None";
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
                embed.AddField("ID", "" + user.Id, true);
                embed.AddField("Status", "" + user.Status, true);
                embed.AddField("Registered", "" + user.CreatedAt, true);
                embed.AddField("UserName", "" + user, true);
                embed.AddField("Joined", "" +  user.JoinedAt, true);
                embed.AddField("NickName", "" + user.Mention, true);
                embed.AddField("Octo Points", "" + account.Points, true);
                embed.AddField("Octo Reputation", "" + account.Rep, true);
                embed.AddField("Access LVL", "" + account.OctoPass, true);
                embed.AddField("User LVL", "" + Math.Round(account.Lvl, 2), true);
                embed.AddField("Pull Points", "" + account.DailyPullPoints, true);
                embed.AddField("Best 2048 Game Score", $"{account.Best2048Score}", true);
                embed.AddField("Warnings", "" + warnings);
                
                embed.AddField("OctoCollection ", "" + octopuses);
                embed.AddField("Used Nicknames", "" + usedNicks);
                if(usedNicks2.Length >=5)
                    embed.AddField("Extra Nicknames", "" + usedNicks2);
                embed.WithThumbnailUrl(user.GetAvatarUrl());


                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);

            }else
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,   "Boole! You do not have a tolerance of this level!");
  

            }
            catch
            {
            //    await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Stats [user_ping(or user ID)]**");
            }
        }
    }
}

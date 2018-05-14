using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;
using OctoBot.Handeling;
using System;
using System.Threading.Tasks;


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
                usedNicks = "Нету.";

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
                octopuses = "Нету.";
            }

            string[] warns = null;
            if (account.Warnings != null)
            {
                warns = account.Warnings.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            }

            var embed = new EmbedBuilder();

            embed.WithColor(Color.Blue);
            embed.WithAuthor(Context.User);
            embed.AddInlineField("ID", "" + Context.User.Id);
            embed.AddInlineField("Статус", "" + Context.User.Status);
           
            embed.AddInlineField("UserName", "" + Context.User);
           
            embed.AddInlineField("NickName", "" + Context.User.Mention);
            embed.AddInlineField("ОктоПоинтов", "" + account.Points);
            embed.AddInlineField("ОктоРепы", "" + account.Rep);
            embed.AddInlineField("Уровень Доступа", "" + account.OctoPass);
            embed.AddInlineField("Уровень", "" + account.Lvl);
            embed.AddInlineField("Pull поинты", "" + account.DailyPullPoints);
            if(warns != null)
            embed.AddInlineField("Предупреждений", "" + warns.Length);
            else
            embed.AddInlineField("Предупреждений", "Нету." );
            embed.AddField("Best 2048 Game Score", $"{account.Best2048Score}");
            embed.AddField("Коллекция Осьминогов", "" + octopuses);   
            embed.AddField("Использованные никнеймы", "" + usedNicks);
            embed.WithThumbnailUrl($"{avatar}");
            //embed.AddField("Роли", ""+avatar);

            await Context.Channel.SendMessageAsync("", embed: embed);



            //await Context.Channel.SendMessageAsync($"У тебя {account.Points} ОктоПоинтов и {account.Rep} ОктоРепы");
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
                    $"Было зачислено {rep}, всего у {user.Mention} {account.Rep} ОктоРепы!");
            }
            else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска такого уровня!");
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
                    $"Было зачислено {points}, всего у {user.Mention} {account.Points} ОктоПоинтов!");
            }
            else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска такого уровня!");
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
                    usedNicks = "Нету.";


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
                    octopuses = "Нету.";
                }

                var warnings = "Нету";
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
                embed.AddInlineField("ID", "" + user.Id);
                embed.AddInlineField("Статус", "" + user.Status);
                embed.AddInlineField("Зарегистрирован", "" + user.CreatedAt);
                embed.AddInlineField("UserName", "" + user);
                embed.AddInlineField("Присоединился", "" +  user.JoinedAt);
                embed.AddInlineField("NickName", "" + user.Mention);
                embed.AddInlineField("ОктоПоинтов", "" + account.Points);
                embed.AddInlineField("ОктоРепы", "" + account.Rep);
                embed.AddInlineField("Уровень Доступа", "" + account.OctoPass);
                embed.AddInlineField("Уровень", "" + account.Lvl);
                embed.AddInlineField("Pull поинты", "" + account.DailyPullPoints);
                embed.AddField("Предупреждения", "" + warnings);
                embed.AddField("Best 2048 Game Score", $"{account.Best2048Score}");
                embed.AddField("Коллекция Осьминогов", "" + octopuses);
                embed.AddField("Использованные никнеймы", "" + usedNicks);
                embed.WithThumbnailUrl($"{avatar}");

                await Context.Channel.SendMessageAsync("", embed: embed);
            }else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска такого уровня!");
        }




        [Command("Доступ", RunMode = RunMode.Async)]
        [Alias("ПАсс", "Купить Пропуск", "Пропуск", "КупитьПропуск", "pass")]
        public async Task BuyPass()
        {
                var account = UserAccounts.GetAccount(Context.User);
                var cost = 4000 * (account.OctoPass + 1);

            if (account.Points >= cost)
            {
                await Context.Channel.SendMessageAsync(
                    $"Ты точно хочешь купить пасс #{account.OctoPass + 1} за {cost} ОктоПоинтов? Тогда пиши **да**!");
                var response = await CommandHandeling.AwaitMessage(Context.User.Id, Context.Channel.Id, 6000);
               
                if (response.Content == "да" || response.Content == "Да")
                {
                    account.OctoPass += 1;
                    account.Points -= cost;
                    UserAccounts.SaveAccounts();
                    await Context.Channel.SendMessageAsync($"Буууль! Ты получил Доступ **#{account.OctoPass}**");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Нужно ответить `да` или `Да` в течении 6 секунд.");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(
                    $"Ты не заработал достаточное Количество ОктоПоинтов, текущее количество: **{account.Points}**\nДля доступа #{account.OctoPass + 1} нужно **{cost}** ОктоПоинтов!");
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
                    await Context.Channel.SendMessageAsync("Нельзя передавать минус или ноль, бу!");
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
                        $"Было передано {points}\nУ {user.Mention} теперь {account.Points} ОктоПоинтов!\nА у тебя осталось {passCheck.Points}");

                }
                else
                    await Context.Channel.SendMessageAsync($"У вас не достаточно ОктоПоинтов чтобы передать их");

            }
            else
            {
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска такого уровня!");
            }
        }

      

    }
}

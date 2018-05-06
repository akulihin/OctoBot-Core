using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;

namespace OctoBot.Commands
{
    public class OctoGift : ModuleBase<SocketCommandContext>
    {
        [Command("GiftCooki")]
        [Alias("Gift Cooki","подаритьКуки", "Подарить Куки")]
        public async Task GiftCooki(IGuildUser user)
        {

            var contextUser = UserAccounts.GetAccount(Context.User);

            var account = UserAccounts.GetAccount((SocketUser)user);
            if (account.Cooki >= 1)
            {
                await Context.Channel.SendMessageAsync($"У пользователя {user.Mention} уже есть Куки, выбери другого осьминога или черепашку!");
                return;
            }

            if (contextUser.Points >= 1488)
            {

                contextUser.Points -= 1488;
                UserAccounts.SaveAccounts();

                account.Cooki += 1;
                account.Octopuses += ("Куки|");
                UserAccounts.SaveAccounts();

                var embed = new EmbedBuilder();
                embed.WithColor(244, 66, 107);
                embed.WithTitle($"Вы подарили {user} Куки!!");
                embed.WithDescription("Плата составила 1488 ОткоПоинтов.");
                embed.AddField("Осталось ОктоПоинтов: ", " " + contextUser.Points);
                embed.WithImageUrl("https://i.imgur.com/dCJwloV.jpg");

                await Context.Channel.SendMessageAsync("", embed: embed);

            }
            else
            {
                await Context.Channel.SendMessageAsync($"У вас не достаточко ОктоПоинтов, чтобы подарить **Куки**!");
            }

        }

        [Command("GiftPinki")]
        [Alias("Gift Pinki", "Подарить Пинки", "ПодаритьПинки")]
        public async Task GiftPinki(IGuildUser user)
        {

            var contextUser = UserAccounts.GetAccount(Context.User);

            var account = UserAccounts.GetAccount((SocketUser)user);
            if (account.Pinki >= 1)
            {
                await Context.Channel.SendMessageAsync($"У пользователя {user.Mention} уже есть Пинки, выбери другого осьминога или черепашку!");
                return;
            }

            if (contextUser.Points >= 1488)
            {

                contextUser.Points -= 1488;
                UserAccounts.SaveAccounts();

                account.Pinki += 1;
                account.Octopuses += ("Пинки|");
                UserAccounts.SaveAccounts();

                var embed = new EmbedBuilder();
                embed.WithColor(244, 66, 107);
                embed.WithTitle($"Вы подарили {user} Пинки!!");
                embed.WithDescription("Плата составила 1488 ОктоПоинтов.");
                embed.AddField("Осталось ОктоПоинтов: ", " " + contextUser.Points);
                embed.WithImageUrl("https://i.imgur.com/xxE7EeX.jpg");

                await Context.Channel.SendMessageAsync("", embed: embed);

            }
            else
            {
                await Context.Channel.SendMessageAsync($"У вас не достаточко ОктоПоинтов, чтобы подарить **Пинки**!");
            }

        }




        [Command("GiftRainbow")]
        [Alias("Gift Rainbow", "Подарить рудужного", "Подарить радужный", "ПодаритьРадужный", "ПодаритьРадужного")]
        public async Task GiftRainbow(IGuildUser user)
        {

            var contextUser = UserAccounts.GetAccount(Context.User);

            var account = UserAccounts.GetAccount((SocketUser)user);
            if (account.Raqinbow >= 1)
            {
                await Context.Channel.SendMessageAsync($"У пользователя {user.Mention} уже есть Радужная Осьминожка, выбери другого осьминога или черепашку!");
                return;
            }

            if (contextUser.Points >= 1000)
            {

                contextUser.Points -= 1000;
                UserAccounts.SaveAccounts();

                account.Raqinbow += 1;
                account.Octopuses += ("Радужный|");
                UserAccounts.SaveAccounts();

                var embed = new EmbedBuilder();
                embed.WithColor(244, 66, 107);
                embed.WithTitle($"Вы подарили {user} Радужную Осьминожку!!");
                embed.WithDescription("Плата составила 1000 ОктоПоинтов.");
                embed.AddField("Осталось ОктоПоинтов: ", " " + contextUser.Points);
                embed.WithImageUrl("https://i.imgur.com/Ufky6UB.jpg");

                await Context.Channel.SendMessageAsync("", embed: embed);

            }
            else
            {
                await Context.Channel.SendMessageAsync($"У вас не достаточко ОктоПоинтов, чтобы подарить **Радужную Осьминожку**!");
            }

        }

        [Command("AllOcto")]
        [Alias("All Octo", "ВсеОкто", "Все Окто")]
        public async Task AllOcto()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(Color.Blue);
            embed.WithTitle("Доступные Осьминоги:");
            embed.AddField("**Куки!**", "`GiftCooki [user]` стоимость: **1488** ОктоПоинтов");
            embed.AddField("**Пинки~**", "`GiftPinki [user]` стоимость: **1488** ОктоПоинтов");
            embed.AddField("**Радужный Осьминога** :gay_pride_flag:", "`GiftRainbow [user]` стоимость: **1000** ОктоПоинтов");
            await Context.Channel.SendMessageAsync("", embed: embed);


        }

    }
}

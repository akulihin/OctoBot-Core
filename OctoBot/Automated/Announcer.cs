using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using OctoBot.Configs.Users;

namespace OctoBot.Automated
{
    internal static class Announcer
    {

        internal static async Task AnnounceUserJoin(SocketGuildUser user)
        {

            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            Console.WriteLine($"{user}, Присойденился к серверу ");
            var account = UserAccounts.GetAccount(user);
            var time = DateTime.Now.ToString("");
            UserAccounts.SaveAccounts();



            var kek = 1;  // DELETE
            if (kek != 1) // DELETE
                await channel.SendMessageAsync($" {user.Mention}, Приветвсвую тебя в подводный мир осьминожек! ");
        }

    }
}

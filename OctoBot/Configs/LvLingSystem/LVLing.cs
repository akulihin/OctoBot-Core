using System;
using Discord.WebSocket;

namespace OctoBot.Configs.LvLingSystem
{
    internal class LvLing
    {
        internal static void UserSentMess(SocketGuildUser user, SocketTextChannel channel, SocketMessage arg)
        {
            // if the user a timeout, ignore them

            if (user.IsBot) return;
            var mess = ($"{arg}");

            var option = mess.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var words = option.Length;

            var wordsPoints = 0.1 * words;
            var userAccount = Users.UserAccounts.GetAccount(user);
            userAccount.Points += 5 + (int)wordsPoints;
            userAccount.LvlPoinnts += 30;
            userAccount.UserName = user.Username;
            
           

            if (userAccount.LastJoinTime == null)
            {
                userAccount.LastJoinTime = "буль.";
                Users.UserAccounts.SaveAccounts();
            }


            if (userAccount.ExtraUserName != null)
            {

                var dublicate = 0;
                var extra = userAccount.ExtraUserName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < extra.Length; i++)
                {
                    if (extra[i] == user.Nickname && extra[i] != null)
                        dublicate = 1;
                }

                if (dublicate != 1 && user.Nickname != null)
                    userAccount.ExtraUserName += (user.Nickname + "|");

            }
            else if (user.Nickname != null)
                userAccount.ExtraUserName = (user.Nickname + "|");

            userAccount.Lvl = (uint)Math.Sqrt(userAccount.LvlPoinnts / 250);

            Users.UserAccounts.SaveAccounts();




        }
    }
}

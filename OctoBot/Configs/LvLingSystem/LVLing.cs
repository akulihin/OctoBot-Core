using System;
using Discord.WebSocket;
using OctoBot.Configs.Users;

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

            

            double wordsPoints = 0;
            double wordsPointsActivity = 0;

            for (var j = 0; j < option.Length; j++)
            {
                if (option[j].Length >= 4)
                {
                    wordsPoints = 0.1 * option[j].Length;
                    wordsPointsActivity = 2.5 * option[j].Length;
                }
                else
                {
                    wordsPoints = 0;
                    wordsPointsActivity = 0; 
                }
            }



            var userAccount = UserAccounts.GetAccount(user);
            userAccount.Points += 5 + (int)wordsPoints;
            userAccount.LvlPoinnts += 30 + (uint)wordsPointsActivity;
            userAccount.UserName = user.Username;
            
          

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

            userAccount.Lvl = Math.Sqrt(userAccount.LvlPoinnts / 150);

            UserAccounts.SaveAccounts();




        }
    }
}

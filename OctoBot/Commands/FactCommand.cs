using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;
using OctoBot.Handeling;
using OctoBot.Services;

namespace OctoBot.Commands
{

    public class Fact : ModuleBase<SocketCommandContextCustom>
    {


        [Command("записать")]
        [Alias("факт", "write", "write down", "fact")]
        public async Task WriteFuckt(IGuildUser user, [Remainder] string message)
        {
            try {
            var account = UserAccounts.GetAccount((SocketUser) user);
            if (account == null)
                return;
         
            account.Fuckt += message + "|";
            UserAccounts.SaveAccounts();
            var id = Context.Message.Id;


            var msg = await Context.Channel.GetMessageAsync(id);
            await msg.DeleteAsync();
            
           
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, $"We wrote down this fact about {user.Mention}!");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", $"We wrote down this fact about {user.Mention}!");
                }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **fact [user_ping(or user ID)] [message]**(write down a fact about user!)\n" +
                                 "Alias: факт, write, fact, write down");
            }
        }

        [Command("факт")]
        [Alias("fact")]
        public async Task ReadFuckt(SocketUser user)
        {
            try {
            var account = UserAccounts.GetAccount(user);

            if (account.Fuckt == null)
            {
               
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, "boole. :c\nWe could not find the facts about this user");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", "boole. :c\nWe could not find the facts about this user");
                }
                return;
            }

            var randomFuktArr = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            var rand = new Random();
            var randomIndex = rand.Next(randomFuktArr.Length);
            var randomFukt = ($"{randomFuktArr[randomIndex]}");


            string httpsCheck = null;
            if (randomFukt.Length >= 5)
            {
                httpsCheck = ($"{randomFukt[0]}{randomFukt[1]}{randomFukt[2]}{randomFukt[3]}{randomFukt[4]}");
            }


            //onsole.WriteLine($"Длина: {RandomFuktArr.Length} | Индекс: {randomIndex} | HTTP Check: {httpsCheck}");

            string randomNick = null;
            if (account.ExtraUserName != null)
            {
                var extra = account.ExtraUserName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                var randomIndexTwo = rand.Next(extra.Length);
                randomNick = ($"{extra[randomIndexTwo]}");
            }


            var embed = new EmbedBuilder();
            embed.WithColor(Color.Purple);
            embed.WithAuthor(user);
            embed.WithFooter("lil octo notebook");
            if (randomNick != null)
            {
                embed.AddField("Was seen under the nickname: ", " " + randomNick);
            }

            if (httpsCheck == "https")
            {
                embed.WithImageUrl($"{randomFukt}");
            }
            else
                embed.AddField("Random fact: ", " " + randomFukt);

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
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **fact [user_ping(or user ID)]**(show a random fact about user)");
            }
        }

        [Command("факт")]
        [Alias("fact")]
        public async Task ReadFucktIndex(SocketUser user, int index)
        {
            try {
            var account = UserAccounts.GetAccount(user);
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 10)
            {
                if (account.Fuckt == null)
                {
                    if (Context.MessegeContent228 != "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, null, "boole. :c\nWe could not find the facts about this user");
  
                    }
                    else if(Context.MessegeContent228 == "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, "edit", "boole. :c\nWe could not find the facts about this user");
                    }
                    return;
                }

                var randomFuktArr = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                var rand = new Random();

                var randomFukt = ($"{randomFuktArr[index]}");

                string httpsCheck = null;
                if (randomFukt.Length >= 5)
                {
                    httpsCheck = ($"{randomFukt[0]}{randomFukt[1]}{randomFukt[2]}{randomFukt[3]}{randomFukt[4]}");
                }


                string randomNick = null;
                if (account.ExtraUserName != null)
                {
                    var extra = account.ExtraUserName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                    var randomIndexTwo = rand.Next(extra.Length);
                    randomNick = ($"{extra[randomIndexTwo]}");
                }


                var embed = new EmbedBuilder();
                embed.WithColor(Color.Purple);
                embed.WithAuthor(user);
                embed.WithFooter("lil octo notebook");
                if (randomNick != null)
                {
                    embed.AddField("Was seen under the nickname: ", " " + randomNick);
                }

                if (httpsCheck == "https")
                {
                    embed.WithImageUrl($"{randomFukt}");
                }
                else
                    embed.AddField("Random fact: ", " " + randomFukt);


                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
                }
            }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **fact [user_ping(or user ID)] [index]**(show [index] fact about user)");
            }
        }


        [Command("ВсеФакты")]
        [Alias("Все Факты", "allfact", "allfacts", "all fact", "all facts")]
        public async Task DeleteTheFucktUser( )
        {
         try {
            var account = UserAccounts.GetAccount(Context.User);
            if (account.OctoPass >= 3)
            {
                var fuckts = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                var mess = "";
                for (var i = 0; i < fuckts.Length; i++)
                {
                    
                    mess += ($"index: {i} | {fuckts[i]}\n");
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("All the facts about you:");
                embed.WithDescription($"{mess}\n**del [index]** to delete the fact");
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
                }
            }
            else
            
             if (Context.MessegeContent228 != "edit")
             {
                 await CommandHandeling.SendingMess(Context, null, null, "Boole :< You do not have 3rd level tolerance");
  
             }
             else if(Context.MessegeContent228 == "edit")
             {
                 await CommandHandeling.SendingMess(Context, null, "edit", "Boole :< You do not have 3rd level tolerance");
             }
         }
         catch
         {
             await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **allfacts [user_ping(or user ID)]**(show all of your facts)\n");
         }
        }


        [Command("ВсеФакты")]
        [Alias("Все Факты", "allfact", "allfacts", "all fact", "all facts")]
        public async Task DeleteTheFuckt(IGuildUser user)
        {
            try {
            var account = UserAccounts.GetAccount((SocketUser) user);
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 4)
            {

                var fuckts = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                var mess = "";
                for (var i = 0; i < fuckts.Length; i++)
                {
                    
                    mess += ($"index: {i} | {fuckts[i]}\n");
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("All the facts about you:");
                embed.WithDescription($"{mess}\n**del [index]** to delete the fact");
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
                }
          
            }
            else
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, "Boole :< You do not have 4rd level tolerance");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", "Boole :< You do not have 4rd level tolerance");
            }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **allfacts [user_ping(or user ID)]**(show all facts about user)\n" +
                                 "Alias: allfact, all facts, ВсеФакты, Все Факты ");
            }
        }


        [Command("УдалитьФакт")]
        [Alias("Удалить Факт", "del")]
        public async Task DeleteTheFucktUser(int index)
        {
           try { 
            var account = UserAccounts.GetAccount(Context.User);
            if (account.OctoPass >= 2)
            {
                var fuckts = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                account.Fuckt = null;

                for (var i = 0; i < fuckts.Length; i++)
                {
                    if (i != index)
                        account.Fuckt += ($"{fuckts[i]}|");

                }

                UserAccounts.SaveAccounts();

      
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null,  $"fact under index {index} was removed from the lil octo notebook ;c");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit",  $"fact under index {index} was removed from the lil octo notebook ;c");
                }

            }
            else
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, "Boole :< You do not have 3rd level tolerance");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", "Boole :< You do not have 3rd level tolerance");
            }
           }
           catch
           {
               await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **del [index]**(delete [index] fact)\n" +
                                "Alias: УдалитьФакт");
           }
        }



        [Command("УдалитьФакт")]
        [Alias("Удалить Факт", "del")]
        public async Task DeleteTheFuckt(IGuildUser user, int index)
        {
            try {
            var account = UserAccounts.GetAccount((SocketUser) user);
            var comander = UserAccounts.GetAccount(Context.User);
            if (comander.OctoPass >= 100)
            {
                var fuckts = account.Fuckt.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                account.Fuckt = null;

                for (var i = 0; i < fuckts.Length; i++)
                {
                    if (i != index)
                        account.Fuckt += ($"{fuckts[i]}|");

                }

                UserAccounts.SaveAccounts();

                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null,  $"fact under index {index} was removed from the lil octo notebook ;c");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit",  $"fact under index {index} was removed from the lil octo notebook ;c");
                }

            }
            else
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, "Boole :< You do not have 10th level tolerance");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", "Boole :< You do not have 10th level tolerance");
            }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **del [user_ping(or user ID)] [index]**(delete [index] fact of the user)\n" +
                                 "Alias: УдалитьФакт");
            }
        }

    }
}



using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Handeling;
using OctoBot.Services;

namespace OctoBot.Commands
{
    public class DailyPull : ModuleBase<SocketCommandContextCustom>
    {
        
        public enum DailyPullResult { Success, AlreadyRecieved }

        public static DailyPullResult GetDailyPull(SocketUser user, ulong guilid)
        {
            var account = UserAccounts.GetAccount(user, guilid);
            var difference = DateTime.UtcNow - account.LastDailyPull;

            if (difference.TotalHours < 23) return DailyPullResult.AlreadyRecieved;

            account.DailyPullPoints += 1;
            account.LastDailyPull = DateTime.UtcNow;
            UserAccounts.SaveAccounts(guilid);
            return DailyPullResult.Success;
        }




        [Command("pull")]
        public async Task Pull()
        {
            try {
                if(Context.Guild.Id != 338355570669256705)
                    return;

            var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            var result = GetDailyPull(Context.User, Context.Guild.Id);
            var difference = DateTime.UtcNow - account.LastDailyPull;
               var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithColor(Color.Gold);
            switch (result)
            {

                case DailyPullResult.AlreadyRecieved:
                    embed.AddField("Pull Points",
                        $"Ты **уже** получал 1 поинт, {Context.User.Username}, у тебя сейчас {account.DailyPullPoints} поинтов. попробуй ещё раз через {23 - (int) difference.TotalHours} часов\n" +
                        $"Если тебе нравится осьминожка, пожалуйста, поставь свой осьминожий лайк ему сюда https://discordbots.org/bot/423593006436712458. Спасибо!");
                    
                    if (Context.MessageContentForEdit != "edit")
                    {
                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                    }
                    else if(Context.MessageContentForEdit == "edit")
                    {
                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                    }
                    break;
                case DailyPullResult.Success:
                    if (account.DailyPullPoints == 31)
                    {
                        embed.AddField("Pull Points",  $"**Поит записан!** У тебя все {account.DailyPullPoints} поинтов!! В течении минуты наши черепашки вышлют тебе в ЛС ключик!\n" +
                                                       $"Если тебе нравится осьминожка, пожалуйста, поставь свой осьминожий лайк ему сюда https://discordbots.org/bot/423593006436712458. Спасибо!");
                        if (Context.MessageContentForEdit != "edit")
                        {
                            await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                        }
                        else if(Context.MessageContentForEdit == "edit")
                        {
                            await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                        }
                    }
                    else
                    {
                        embed.AddField("Pull Points",
                            $"**Поит записан!** У тебя **теперь** есть {account.DailyPullPoints} поинтов. Приходи через 1 день за новым!");
                      

                        if (Context.MessageContentForEdit != "edit")
                        {
                            await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                        }
                        else if(Context.MessageContentForEdit == "edit")
                        {
                            await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                        }
                    }
                    break;
            }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **pull**\n");
            }
        }

        [Command("AddKey")]
        public async Task JsonTask([Remainder] string mess)
        {
            try
            {
                var mylorikGlobal = Global.Client.GetUser(181514288278536193);
                var mylorik = UserAccounts.GetAccount(mylorikGlobal, 338355570669256705);

                var gameAndKey = mess.Split(new[] {"&&"}, StringSplitOptions.RemoveEmptyEntries);
                if (gameAndKey.Length < 2 || gameAndKey.Length >= 3)
                    return;

                mylorik.KeyPullName += (gameAndKey[0] + "|");
                mylorik.KeyPullKey += (gameAndKey[1] + "|");
                UserAccounts.SaveAccounts(338355570669256705);

                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, null, "бууль-буль, записали!");

                }
                else if (Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, "edit", "бууль-буль, записали!");
                }
            }
            catch
            {
                await ReplyAsync(
                    "boo... An error just appear >_< \nTry to use this command properly: **AddKey Gamename && key (platform)**\n");
            }
        }

        [Command("KeyDel")]
        [RequireOwner]
        public async Task JsonDel(int index)
        {
            try {
            var account = UserAccounts.GetAccount(Context.User, 338355570669256705);
            if (account.OctoPass >= 100)
            {
            var mylorikGlobal = Global.Client.GetUser(181514288278536193);
            var mylorik = UserAccounts.GetAccount(mylorikGlobal, 338355570669256705);
            var keyName =
                mylorik.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            var keykey =
                    mylorik.KeyPullKey.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

  
            mylorik.KeyPullName = null;
                mylorik.KeyPullKey= null;
            for (var i = 0; i < keyName.Length; i++)
            {
                if (i != index)
                {
                            mylorik.KeyPullName += ($"{keyName[i]}|");
                            mylorik.KeyPullKey += ($"{keykey[i]}|");
                }
            }
           
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, null, $"ключ **{keyName[index]} {keykey[index]}** был удалён");
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, "edit", $"ключ **{keyName[index]} {keykey[index]}** был удалён");
                }
            UserAccounts.SaveAccounts(338355570669256705);
            }
            else
                
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, null, "буль-буль, у тебя нет допуска 100го уровня!");
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, "edit", "буль-буль, у тебя нет допуска 100го уровня!");
                }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **KeyDel [index]**\n");
            }
        }

        [Command("Ключи")]
        [Alias("Keys")]
        public async Task AllKeys( )
        {
         try {
            var account = UserAccounts.GetAccount(Context.User, 338355570669256705);
          
                var keyName = account.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var keyKey = account.KeyPullKey.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            var keys = "";
            var keysExtra = "";
            var keysExtra2 = "";

                for (var i = 0; i < keyName.Length; i++)
                {
                    if (keys.Length <= 800)
                    {
                        keys += $"index: {i} | {keyName[i]} {keyKey[i]}\n";
                       
                    }
                    else if(keys.Length <= 1600)
                    {
                        keysExtra += $"index: {i} | {keyName[i]} {keyKey[i]} \n";
                    }
                    else
                    {
                        keysExtra2 += $"index: {i} | {keyName[i]} {keyKey[i]} \n";
                    }
                }

            var embed = new EmbedBuilder();
            embed.WithAuthor(Context.User);
            embed.AddField("Ключи:", $"{keys}\n**KeyDel [index]** Чтобы удалить ");
            embed.WithFooter("Записная книжечка Осьминожек");
             if(keysExtra.Length > 10)
                 embed.AddField("Ключи(cont):", $"{keysExtra}\n**KeyDel [index]** Чтобы удалить ");
             if(keysExtra2.Length > 10)
                 embed.AddField("Ключи(cont):", $"{keysExtra2}\n**KeyDel [index]** Чтобы удалить ");

             if (Context.MessageContentForEdit != "edit")
             {
                 await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
             }
             else if(Context.MessageContentForEdit == "edit")
             {
                 await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
             }
         }
         catch
         {
             await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Keys** (show all **YOUR** keys)\n");
         }
        }

        [Command("cKey")]
        public async Task ChooseKeyFromPull(int choice)
        {
            try
            {
              

                if (choice < 0 || choice > 3)
                {
                    await ReplyAsync("бу!");
                    return;
                }
                var account = UserAccounts.GetAccount(Context.User, 338355570669256705);
                if (choice == 0)
                {
                    
                    if (Context.MessageContentForEdit != "edit")
                    {
                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, null, "Бульк~");
  
                    }
                    else if(Context.MessageContentForEdit == "edit")
                    {
                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, "edit", "Бульк~");
                    }
                    account.PullToChoose = null;
                    UserAccounts.SaveAccounts(338355570669256705);
                    return;
                }

                
                var keysToChooseList=
                    account.PullToChoose.Split(new[] {"%%"}, StringSplitOptions.RemoveEmptyEntries);


                int index;
                
                if (choice == 1)
                {
                  var something = keysToChooseList[0].Split(new[] {"%%"}, StringSplitOptions.RemoveEmptyEntries);
                   index = Convert.ToInt32(something[0]);
                 
                }
                else if(choice == 2)
                {
                  var something = keysToChooseList[1].Split(new[] {"%%"}, StringSplitOptions.RemoveEmptyEntries);
                    index = Convert.ToInt32(something[0]);
                  
                }
                else
                {
                  var something = keysToChooseList[2].Split(new[] {"%%"}, StringSplitOptions.RemoveEmptyEntries);
                    index = Convert.ToInt32(something[0]);
                 
                }
            
                var mylorikGlobal = Global.Client.GetUser(181514288278536193);
                var mylorik = UserAccounts.GetAccount(mylorikGlobal, 338355570669256705);
                var keyName =
                    mylorik.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var keykey =
                    mylorik.KeyPullKey.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("OctoNotification");
                embed.WithDescription($"А вот и ключ!\n\n**{keyName[index]} : {keykey[index]}**\n\nБуль!");
               
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                }

                
                mylorik.KeyPullName = null;
                mylorik.KeyPullKey= null;
                for (var i = 0; i < keyName.Length; i++)
                {
                    if (i != index)
                    {
                        mylorik.KeyPullName += ($"{keyName[i]}|");
                        mylorik.KeyPullKey += ($"{keykey[i]}|");
                    }
                }

                account.PullToChoose = null;
                UserAccounts.SaveAccounts(338355570669256705);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
               
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, null, "У тебя либо нет ключей на выбор, либо произошла какая-то ошибка.");
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, "edit", "У тебя либо нет ключей на выбор, либо произошла какая-то ошибка.");
                }
            }
        }

        [Command("pullp")]
        public async Task GivePullPoints(SocketGuildUser user, int pullPoints)
        {
            var commander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            if (commander.OctoPass < 100)
                return;
            var account = UserAccounts.GetAccount(user, Context.Guild.Id);

            account.DailyPullPoints += pullPoints;
            UserAccounts.SaveAccounts(Context.Guild.Id);

            var embed = new EmbedBuilder();
            embed.WithColor(Color.DarkMagenta);
            embed.AddField("буууль~", $"Мы добавили {pullPoints} пулл Поинтов {user.Mention}. Теперь у него {account.DailyPullPoints} поинтов, буль!");

            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }

        }

    }
}

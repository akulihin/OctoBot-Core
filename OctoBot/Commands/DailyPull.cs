using System;
using System.Threading.Tasks;
using System.Timers;
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
                        $"Ты **уже** получал 1 поинт, {Context.User.Username}, у тебя сейчас {account.DailyPullPoints} поинтов. попробуй ещё раз через {23 - (int) difference.TotalHours} часов");
                    
                    if (Context.MessageContentForEdit != "edit")
                    {
                        await CommandHandeling.SendingMess(Context, embed);
  
                    }
                    else if(Context.MessageContentForEdit == "edit")
                    {
                        await CommandHandeling.SendingMess(Context, embed, "edit");
                    }
                    break;
                case DailyPullResult.Success:
                    if (account.DailyPullPoints == 31)
                    {
                        embed.AddField("Pull Points",  $"**Поит записан!** У тебя все {account.DailyPullPoints} поинтов!! В течении минуты наши черепашки вышлют тебе в ЛС ключик!");
                        if (Context.MessageContentForEdit != "edit")
                        {
                            await CommandHandeling.SendingMess(Context, embed);
  
                        }
                        else if(Context.MessageContentForEdit == "edit")
                        {
                            await CommandHandeling.SendingMess(Context, embed, "edit");
                        }
                    }
                    else
                    {
                        embed.AddField("Pull Points",
                            $"**Поит записан!** У тебя **теперь** есть {account.DailyPullPoints} поинтов. Приходи через 1 день за новым!");
                      

                        if (Context.MessageContentForEdit != "edit")
                        {
                            await CommandHandeling.SendingMess(Context, embed);
  
                        }
                        else if(Context.MessageContentForEdit == "edit")
                        {
                            await CommandHandeling.SendingMess(Context, embed, "edit");
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

        private static Timer _loopingTimerForPull;
      
        internal static Task CheckTimerForPull()
        {


            _loopingTimerForPull = new Timer
            {
                AutoReset = true,
                Interval = 60000,
                Enabled = true
            };
          //  _loopingTimerForPull.Elapsed += CheckPulls;


            return Task.CompletedTask;
        }
        
        public static async void CheckPulls(object sender, ElapsedEventArgs e)
        {          
            try
            {
                ulong shadowFoxGuildId = 338355570669256705;
                var allUserAccounts = UserAccounts.GetOrAddUserAccountsForGuild(shadowFoxGuildId);

                foreach (var t in allUserAccounts)
                {
                    if (Global.Client.GetUser(t.Id) != null)
                    {

                        var globalAccount = Global.Client.GetUser(t.Id);
                        var account = UserAccounts.GetAccount(globalAccount, shadowFoxGuildId);
                        var difference = DateTime.UtcNow - account.LastDailyPull;


                        if (difference.TotalHours > 39 && account.DailyPullPoints >= 1)
                        {
                            try
                            {
                             

                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();
                                ////taci
                                if (account.Id == 236184944064331777)
                                {
                                    await dmChannel.SendMessageAsync("бу-бу-бу-бу-бу!!!");
                                    account.LastDailyPull = DateTime.UtcNow;
                                    UserAccounts.SaveAccounts(shadowFoxGuildId);
                                    return;
                                }
                                ////taci
                                embed.WithFooter("lil octo notebook");
                                embed.WithTitle("OctoNotification");
                                embed.WithDescription($"Ты потерял свои ежедневные поинты, буль ;c");
                                await dmChannel.SendMessageAsync("", false, embed.Build());
                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts(shadowFoxGuildId);
                            }
                            catch
                            {
                                Console.WriteLine($"ERROR DM SENING {account.UserName} Closed DM");
                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts(shadowFoxGuildId);
                                return;

                            }
                        }
                        
                        if (account.DailyPullPoints >= 25)
                        {
                            var mylorikGlobal = Global.Client.GetUser(181514288278536193);
                            var mylorik = UserAccounts.GetAccount(mylorikGlobal, shadowFoxGuildId);

                            if (mylorik.KeyPullName != null)
                            {
                                var fullKeysNameList =
                                    mylorik.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                                var fullKeysKeyList =
                                    mylorik.KeyPullKey.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                                if (fullKeysNameList.Length == 1)
                                {
                                    Array.Resize(ref fullKeysNameList, fullKeysNameList.Length + 2);
                                    Array.Resize(ref fullKeysKeyList, fullKeysKeyList.Length + 2);

                                    fullKeysNameList[fullKeysNameList.Length - 2] = "Куда подевалось?";
                                    fullKeysNameList[fullKeysNameList.Length - 1] = "Куда подевалось?";


                                    fullKeysKeyList[fullKeysKeyList.Length - 2] = "Доволен?";
                                    fullKeysKeyList[fullKeysKeyList.Length - 1] = "Доволен?";
                                }
                                else if (fullKeysNameList.Length == 2)
                                {
                                    Array.Resize(ref fullKeysNameList, fullKeysNameList.Length + 1);
                                    Array.Resize(ref fullKeysKeyList, fullKeysKeyList.Length + 1);

                                    fullKeysNameList[fullKeysNameList.Length - 1] = "Куда подевалось?";
                                    fullKeysKeyList[fullKeysKeyList.Length - 1] = "Доволен?";
                                }

                                int randonKey1;
                                int randonKey2;
                                int randonKey3;
                                var key = new Random();
                                do
                                {
                                     randonKey1 = SecureRandom.Random(0,fullKeysNameList.Length);
                                     randonKey2 = SecureRandom.Random(0,fullKeysNameList.Length);
                                     randonKey3 = SecureRandom.Random(0,fullKeysNameList.Length);

                                } while (randonKey1 == randonKey2 || randonKey2 == randonKey3 || randonKey1 == randonKey3);

                                
                                UserAccounts.SaveAccounts(shadowFoxGuildId);


                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();

                                embed.WithFooter("lil octo notebook");
                                embed.WithColor(Color.Green);
                                embed.WithTitle("OctoNotification");
                                embed.WithDescription($"Вот и ключик подъе... буууль. Выбери одну:\nЧерез команду __cKey номер__\n\n**1. {fullKeysNameList[randonKey1]}**\n**2. {fullKeysNameList[randonKey2]}**\n**3. {fullKeysNameList[randonKey3]}**\n\n**0. Ничего не брать.**\n\n"+
                                                      $"**ВАЖНО, прочти пожалуйста:**\nЕсли ты НЕ будешь играть в эту игру, а просто добавишь ее и забьешь, тогда **прошу** верни ее осьминожкам, а мы подарим ее другому! - \nПросто Проигнорь это сообщение, или выбери 0 В таком случае.\n__Мы не любим топить ресурсы__. Буль c:");
                                await dmChannel.SendMessageAsync("", false, embed.Build());

                                account.PullToChoose = null;
                                account.PullToChoose += $"{randonKey1}%%";
                                account.PullToChoose += $"{randonKey2}%%";
                                account.PullToChoose += $"{randonKey3}%%";
                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts(shadowFoxGuildId);
                            }
                            else
                            {
                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();
                                embed.WithFooter("Записная книжечка Осьминожек");
                                embed.WithColor(Color.Green);
                                embed.WithTitle("OctoNotification");
                                embed.WithDescription($"Буууль... ключики кончились ;c");
                                await dmChannel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                    }
                }
            } catch {
                Console.WriteLine("Failed To ReadFile(CheckPulls). Will ty in 5 sec.");
            }
        }

        [Command("AddKey")]
        public async Task JsonTask([Remainder] string mess)
        {
            try {
            var mylorikGlobal = Global.Client.GetUser(181514288278536193);
            var mylorik = UserAccounts.GetAccount(mylorikGlobal,338355570669256705);

           var gameAndKey = mess.Split(new[] {"&&"}, StringSplitOptions.RemoveEmptyEntries);
                if(gameAndKey.Length < 2 || gameAndKey.Length >= 3 )
                    return;

            mylorik.KeyPullName += (gameAndKey[0] + "|");
            mylorik.KeyPullKey += (gameAndKey[1] + "|");
            UserAccounts.SaveAccounts(Context.Guild.Id);
           
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, "бууль-буль, записали!");
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", "бууль-буль, записали!");
                }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **AddKey [Gamename: key (platform)]**\n");
            }
        }
        
        [Command("KeyDel")]
        [RequireOwner]
        public async Task JsonDel(int index)
        {
            try {
            var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
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
                    await CommandHandeling.SendingMess(Context, null, null, $"ключ **{keyName[index]} {keykey[index]}** был удалён");
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", $"ключ **{keyName[index]} {keykey[index]}** был удалён");
                }
            UserAccounts.SaveAccounts(Context.Guild.Id);
            }
            else
                
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, "буль-буль, у тебя нет допуска 100го уровня!");
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", "буль-буль, у тебя нет допуска 100го уровня!");
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
                 await CommandHandeling.SendingMess(Context, embed);
  
             }
             else if(Context.MessageContentForEdit == "edit")
             {
                 await CommandHandeling.SendingMess(Context, embed, "edit");
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
                        await CommandHandeling.SendingMess(Context, null, null, "Бульк~");
  
                    }
                    else if(Context.MessageContentForEdit == "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, "edit", "Бульк~");
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
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
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
                    await CommandHandeling.SendingMess(Context, null, null, "У тебя либо нет ключей на выбор, либо произошла какая-то ошибка.");
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", "У тебя либо нет ключей на выбор, либо произошла какая-то ошибка.");
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
                await CommandHandeling.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandeling.SendingMess(Context, embed, "edit");
            }

        }

    }
}

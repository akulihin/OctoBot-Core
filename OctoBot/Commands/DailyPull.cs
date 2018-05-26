using System;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;

namespace OctoBot.Commands
{
    public class DailyPull : ModuleBase<SocketCommandContext>
    {
        [Command("pull")]

        public async Task Pull()
        {
            try {
            var account = UserAccounts.GetAccount(Context.User);
            var result = GetDailyPull(Context.User);
            var difference = DateTime.Now - account.LastDailyPull;

            switch (result)
            {


                case DailyPullResult.AlreadyRecieved:
                    await ReplyAsync($"Ты **уже** получал 1 поинт, {Context.User.Username}, у тебя сейчас {account.DailyPullPoints} поинтов. попробуй ещё раз через {24 - (int)difference.TotalHours} часов");
                    break;
                case DailyPullResult.Success:
                    if (account.DailyPullPoints == 20)
                    {
                        await ReplyAsync(
                            $"**Поит записан!** У тебя все {account.DailyPullPoints} поинтов!! В течении минуты наши черепашки вышлют тебе в ЛС ключик!");
                    }
                    else
                    {
                        await ReplyAsync(
                            $"**Поит записан!** У тебя **теперь** есть {account.DailyPullPoints} поинтов. Приходи через 1 день за новым!");
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

        public static DailyPullResult GetDailyPull(SocketUser user)
        {
            var account = UserAccounts.GetAccount(user);
            var difference = DateTime.Now - account.LastDailyPull;

            if (difference.TotalHours < 24) return DailyPullResult.AlreadyRecieved;

            account.DailyPullPoints += 1;
            account.LastDailyPull = DateTime.UtcNow;
            UserAccounts.SaveAccounts();
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
            _loopingTimerForPull.Elapsed += CheckPulls;


            return Task.CompletedTask;
        }
        
        public static async void CheckPulls(object sender, ElapsedEventArgs e)
        {          
            try
            {
                var allUserAccounts = UserAccounts.GetAllAccounts();

                foreach (var t in allUserAccounts)
                {
                    if (Global.Client.GetUser(t.Id) != null)
                    {

                        var globalAccount = Global.Client.GetUser(t.Id);
                        var account = UserAccounts.GetAccount(globalAccount);
                        var difference = DateTime.Now - account.LastDailyPull;


                        if (difference.TotalHours > 40 && account.DailyPullPoints >= 1)
                        {
                            try
                            {
                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();
                                embed.WithFooter("lil octo notebook");
                                embed.WithTitle("OctoNotification");
                                embed.WithDescription($"Ты потерял свои ежедневные поинты, буль ;c");
                                await dmChannel.SendMessageAsync("", embed: embed);
                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts();
                            }
                            catch
                            {
                                Console.WriteLine($"ERROR DM SENING {account.UserName} Closed DM");
                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts();
                                return;

                            }
                        }
                        if (account.DailyPullPoints == 20)
                        {
                            var mylorikGlobal = Global.Client.GetUser(181514288278536193);
                            var mylorik = UserAccounts.GetAccount(mylorikGlobal);

                            if (mylorik.KeyPull != null)
                            {
                                var randomKeyList =
                                    mylorik.KeyPull.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                                var key = new Random();
                                var randomKey = key.Next(randomKeyList.Length);


                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();
                                embed.WithFooter("lil octo notebook");
                                embed.WithTitle("OctoNotification");
                                embed.WithDescription($"Вот и ключик подъехал!\n\n**{randomKeyList[randomKey]}**\n\n" +
                                                      $"Если ты  **НЕ будешь играть в эту игру**, а просто добавишь ее и забьешь, **прошу** верни ее осьминожкам, и мы подарим ее другому через команду **addkey [любой текст]**. Мы не любим расходовать ресурсы просто так. Спасибо!");
                                await dmChannel.SendMessageAsync("", embed: embed);

                                mylorik.KeyPull = null;
                                for (var i = 0; i < randomKeyList.Length; i++)
                                {
                                    if (i != randomKey)
                                        mylorik.KeyPull += ($"{randomKeyList[i]}|");
                                }

                                account.KeyPull += (randomKeyList[randomKey] + "|");
                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts();
                            }
                            else
                            {
                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();
                                embed.WithFooter("Записная книжечка Осьминожек");
                                embed.WithTitle("OctoNotification");
                                embed.WithDescription($"Буууль... ключики кончились ;c");
                                await dmChannel.SendMessageAsync("", embed: embed);
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
            var mylorik = UserAccounts.GetAccount(mylorikGlobal);
            mylorik.KeyPull += (mess + "|");
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync("бууль-буль, записали!");
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **AddKey [Gamename: key (platform)]**\n");
            }
        }
        

        [Command("KeyDel")]
        public async Task JsonDel(int index)
        {
            try {
            var account = UserAccounts.GetAccount(Context.User);
            if (account.OctoPass >= 100)
            {
            var mylorikGlobal = Global.Client.GetUser(181514288278536193);
            var mylorik = UserAccounts.GetAccount(mylorikGlobal);
            var keyList =
                mylorik.KeyPull.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
  
            mylorik.KeyPull = null;
            for (var i = 0; i < keyList.Length; i++)
            {
                if (i != index)
                    mylorik.KeyPull += ($"{keyList[i]}|");
            }
            await Context.Channel.SendMessageAsync($"ключ **{keyList[index]}** был удалён");
            UserAccounts.SaveAccounts();
            }
            else
                await Context.Channel.SendMessageAsync("буль-буль, у тебя нет допуска 10го уровня!");
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
            var account = UserAccounts.GetAccount(Context.User);
          
                var fuckts = account.KeyPull.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            var keys = "";

                for (var i = 0; i < fuckts.Length; i++)
                {
                    keys += $"index: {i} | {fuckts[i]}\n";
                   
                }

            var embed = new EmbedBuilder();
            embed.WithAuthor(Context.User);
            embed.AddField("Ключи:", $"{keys}\n**KeyDel [index]** Чтобы удалить ");
            embed.WithFooter("Записная книжечка Осьминожек");

            await Context.Channel.SendMessageAsync("", embed: embed);
         }
         catch
         {
             await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Keys** (show all **YOUR** keys)\n");
         }
        }



    }
}
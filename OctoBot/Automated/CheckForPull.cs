using System;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Services;

namespace OctoBot.Automated
{
    public class CheckForPull
    {
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
    }
}

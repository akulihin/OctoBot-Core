using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using OctoBot.Configs.Users;

namespace OctoBot.Automated
{
    public class CheckReminders
    {
        private static Timer _loopingTimer;

        internal static Task CheckTimer()
        {


            _loopingTimer = new Timer
            {
                AutoReset = true,
                Interval = 5000,
                Enabled = true
            };
            _loopingTimer.Elapsed += CheckAllReminders;
            return Task.CompletedTask;
        }



        //client.GetUser(userId);
        public static async void CheckAllReminders(object sender, ElapsedEventArgs e)
        {
            try
            {
                var allUserAccounts = UserAccounts.GetOrAddUserAccountsForGuild(0);
                var now = DateTime.UtcNow;

                foreach (var t in allUserAccounts)
                {
                    if (Global.Client.GetUser(t.Id) == null)
                        continue;


                    var globalAccount = Global.Client.GetUser(t.Id);
                    var account = UserAccounts.GetAccount(globalAccount, 0);

                    var removeLaterList = new List<AccountSettings.CreateReminder>();

                    for (var j = 0; j < account.ReminderList?.Count; j++)
                    {

                        if (account.ReminderList[j].DateToPost > now)
                            continue;

                        try
                        {
                            var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                            var embed = new EmbedBuilder();
                            embed.WithFooter("lil octo notebook");
                            embed.WithColor(Color.Teal);
                            embed.WithTitle("Розовенькая черепашка напоминает тебе:");
                            embed.WithDescription($"\n{account.ReminderList[j].ReminderMessage}");

                            await dmChannel.SendMessageAsync("", false, embed.Build());

                            removeLaterList.Add(account.ReminderList[j]);

                            //  account.ReminderList.RemoveAt(j);
                           //   UserAccounts.SaveAccounts(0);
                        }
                        catch (Exception closedDm)
                        {
                            try
                            {
                                if (!closedDm.Message.Contains("404") || !closedDm.Message.Contains("403")) continue;
                                Console.WriteLine($"ERROR DM SENING {account.UserName} Closed DM: '{0}'",
                                    closedDm);
                                account.ReminderList = null;
                                UserAccounts.SaveAccounts(0);
                                return;
                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ERROR REMINDER (Catch-catch) {account.UserName}");
                                Console.ResetColor();
                            }
                        }
                    }

                    if (!removeLaterList.Any()) continue;
                    removeLaterList.ForEach(item => account.ReminderList.Remove(item));
                    UserAccounts.SaveAccounts(0);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("ERROR!!! REMINDER(Big try) Does not work: '{0}'", error);

            }
        }
    }
}

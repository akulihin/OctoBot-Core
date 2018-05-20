using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Timers;
using OctoBot.Configs.Users;
using System.Globalization;
using Discord;
using OctoBot.Configs;
using Discord.WebSocket;
using static OctoBot.Configs.Users.AccountSettings;

namespace OctoBot.Commands
{
    public class Reminder : ModuleBase<SocketCommandContext>
    {


        [Command("Remind")]
        [Alias("Напомнить", "напомни мне", "напиши мне", "напомни", "алярм", " Напомнить", " напомни мне",
            " напиши мне", " напомни", " алярм", " Remind")]
        public async Task AddReminder([Remainder] string args)
        {
            string[] splittedArgs = null;

            if (args.Contains("  через ")) splittedArgs = args.Split(new[] {"  через "}, StringSplitOptions.None);
            else if (args.Contains(" через  ")) splittedArgs = args.Split(new[] {" через  "}, StringSplitOptions.None);
            else if (args.Contains("  через  ")) splittedArgs = args.Split(new[] {"  через  "}, StringSplitOptions.None);
            else if (args.Contains(" через ")) splittedArgs = args.Split(new[] {" через "}, StringSplitOptions.None);
            else if (args.Contains("  in ")) splittedArgs = args.Split(new[] { "  in " }, StringSplitOptions.None);
            else if (args.Contains(" in  ")) splittedArgs = args.Split(new[] { " in  " }, StringSplitOptions.None);
            else if (args.Contains("  in  ")) splittedArgs = args.Split(new[] { "  in  " }, StringSplitOptions.None);
            else if (args.Contains(" in ")) splittedArgs = args.Split(new[] { " in " }, StringSplitOptions.None);




            if (splittedArgs == null || splittedArgs.Length < 2)
            {
                await ReplyAsync("boole-boole... you are using this command incorrectly!!\n" +
                                 "Right way: `Remind [text] in [time]`\n" +
                                 "Between message and time **HAVE TO BE** written `in` part" +
                                 "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                 "I'm a loving order octopus!");
                return;
            }

            var timeString = splittedArgs[splittedArgs.Length - 1];
            splittedArgs[splittedArgs.Length - 1] = "";
            var reminderString = string.Join(" in ", splittedArgs, 0, splittedArgs.Length - 1);
            string[] formats =
            {
                // Used to parse stuff like 1d14h2m11s and 1d 14h 2m 11s could add/remove more if needed

                "d'd'",
                "d'd'm'm'", "d'd 'm'm'",
                "d'd'h'h'", "d'd 'h'h'",
                "d'd'h'h's's'", "d'd 'h'h 's's'",
                "d'd'm'm's's'", "d'd 'm'm 's's'",
                "d'd'h'h'm'm'", "d'd 'h'h 'm'm'",
                "d'd'h'h'm'm's's'", "d'd 'h'h 'm'm 's's'",

                "h'h'",
                "h'h'm'm'", "h'h m'm'",
                "h'h'm'm's's'", "h'h 'm'm 's's'",
                "h'h's's'", "h'h s's'",
                "h'h'm'm'", "h'h 'm'm'",
                "h'h's's'", "h'h 's's'",

                "m'm'",
                "m'm's's'", "m'm 's's'",

                "s's'"
            };
            var timeDateTime = DateTime.UtcNow + TimeSpan.ParseExact(timeString, formats, CultureInfo.CurrentCulture);

            await Context.Channel.SendMessageAsync($"An Octopus will remind you\n" +
                                                   $"*{reminderString}*\n\n" +
                                                   $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                                                   $"**Time Now:                  {DateTime.UtcNow}** `by UTC`");

            var account = UserAccounts.GetAccount(Context.User);
            var newReminder = new CreateReminder(timeDateTime, reminderString);

            account.ReminderList.Add(newReminder);
            UserAccounts.SaveAccounts();

        }

        ///REMINDER FOR MINUTES!
        [Command("Re")]
        public async Task AddReminderMinute(uint minute, [Remainder] string reminderString)
        {

            if (minute > 1439)
            {
                await Context.Channel.SendMessageAsync(
                    "Booole. [time] have to be in range 0-1439 (in minutes)");
                return;

            }

            var hour = 0;
            var timeFormat = $"{minute}m";

            if (minute >= 60)
            {

                for (var i = 0; minute >= 59; i++)
                {
                    minute = minute - 59;
                    hour++;

                    timeFormat = $"{hour}h {minute}m";
                }

            }

            var timeString = timeFormat; //// MAde t ominutes

            string[] formats =
            {
                // Used to parse stuff like 1d14h2m11s and 1d 14h 2m 11s could add/remove more if needed

                "d'd'",
                "d'd'm'm'", "d'd 'm'm'",
                "d'd'h'h'", "d'd 'h'h'",
                "d'd'h'h's's'", "d'd 'h'h 's's'",
                "d'd'm'm's's'", "d'd 'm'm 's's'",
                "d'd'h'h'm'm'", "d'd 'h'h 'm'm'",
                "d'd'h'h'm'm's's'", "d'd 'h'h 'm'm 's's'",

                "h'h'",
                "h'h'm'm'", "h'h m'm'",
                "h'h'm'm's's'", "h'h 'm'm 's's'",
                "h'h's's'", "h'h s's'",
                "h'h'm'm'", "h'h 'm'm'",
                "h'h's's'", "h'h 's's'",

                "m'm'",
                "m'm's's'", "m'm 's's'",

                "s's'"
            };
            var timeDateTime = DateTime.UtcNow + TimeSpan.ParseExact(timeString, formats, CultureInfo.CurrentCulture);

            await Context.Channel.SendMessageAsync($"An Octopus will remind you\n" +
                                                   $"*{reminderString}*\n\n" +
                                                   $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                                                   $"**Time Now:                                {DateTime.UtcNow}** `by UTC`");

            var account = UserAccounts.GetAccount(Context.User);
            //account.SocketUser = SocketGuildUser(Context.User);
            var newReminder = new CreateReminder(timeDateTime, reminderString);

            account.ReminderList.Add(newReminder);
            UserAccounts.SaveAccounts();

        }




        //REminder To A User
        [Command("Rem"), Priority(1)]
        [Alias("Напомнить", "напомни мне", "напиши мне", "напомни", "алярм", " Напомнить", " напомни мне",
            " напиши мне", " напомни", " алярм", " Remind", "Remind")]
        public async Task AddReminderToSomeOne(ulong userId, [Remainder] string args)
        {

            //       var commander = UserAccounts.GetAccount(Context.User);


            string[] splittedArgs = null;
            if (args.Contains("  через "))
                splittedArgs = args.Split(new[] {"  через "}, StringSplitOptions.None);
            else if (args.Contains(" через  "))
                splittedArgs = args.Split(new[] {" через  "}, StringSplitOptions.None);
            else if (args.Contains("  через  "))
                splittedArgs = args.Split(new[] {"  через  "}, StringSplitOptions.None);
            else if (args.Contains(" через "))
                splittedArgs = args.Split(new[] {" через "}, StringSplitOptions.None);


            if (splittedArgs == null || splittedArgs.Length < 2)
            {
                await ReplyAsync("boole-boole... you are using this command incorrectly!!\n" +
                                 "Right way: `Remind [text] in [time]`\n" +
                                 "Between message and time **HAVE TO BE** written `in` part" +
                                 "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                 "I'm a loving order octopus!");
                return;
            }

            var timeString = splittedArgs[splittedArgs.Length - 1];
            splittedArgs[splittedArgs.Length - 1] = "";
            var reminderString = string.Join(" in ", splittedArgs, 0, splittedArgs.Length - 1);
            string[] formats =
            {
                // Used to parse stuff like 1d14h2m11s and 1d 14h 2m 11s could add/remove more if needed

                "d'd'",
                "d'd'm'm'", "d'd 'm'm'",
                "d'd'h'h'", "d'd 'h'h'",
                "d'd'h'h's's'", "d'd 'h'h 's's'",
                "d'd'm'm's's'", "d'd 'm'm 's's'",
                "d'd'h'h'm'm'", "d'd 'h'h 'm'm'",
                "d'd'h'h'm'm's's'", "d'd 'h'h 'm'm 's's'",

                "h'h'",
                "h'h'm'm'", "h'h m'm'",
                "h'h'm'm's's'", "h'h 'm'm 's's'",
                "h'h's's'", "h'h s's'",
                "h'h'm'm'", "h'h 'm'm'",
                "h'h's's'", "h'h 's's'",

                "m'm'",
                "m'm's's'", "m'm 's's'",

                "s's'"

            };
            var timeDateTime =
                DateTime.UtcNow + TimeSpan.ParseExact(timeString, formats, CultureInfo.CurrentCulture);

            var user = Global.Client.GetUser(userId);

            await Context.Channel.SendMessageAsync($"An Octopus will remind {user.Mention}\n" +
                                                   $"*{reminderString}*\n\n" +
                                                   $"We will send him a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                                                   $"**Time Now:                                {DateTime.UtcNow}** `by UTC`");

            var account = UserAccounts.GetAccount(user);
            var newReminder = new CreateReminder(timeDateTime, $"From {Context.User.Username}: " + reminderString);

            account.ReminderList.Add(newReminder);
            UserAccounts.SaveAccounts();
        }

        [Command("List")]
        [Alias("Напоминания", "Мои Напоминания", "список")]
        public async Task ShowReminders()
        {
            var account = UserAccounts.GetAccount(Context.User);
            if (account.ReminderList.Count == 0)
            {
                await ReplyAsync(
                    "Booole... You have no reminders! You can create one by using the command `Remind [text] in [time]`\n" +
                    "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                    "I'm a loving order octopus!");
                return;

            }

            var reminders = account.ReminderList;
            var embed = new EmbedBuilder();
            embed.WithTitle("Your Reminders:");
            embed.WithDescription($"**Your current time by UTC: {DateTime.UtcNow}**\n" +
                                  "To delete one of them, type the command `*del [index]`");
            embed.WithFooter("lil octo notebook");

            for (var i = 0; i < reminders.Count; i++)
            {
                embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);
            }

            await ReplyAsync("", embed: embed);
        }


        [Command("List")]
        [Alias("Напоминания", "Мои Напоминания", "список")]
        public async Task ShowUserReminders(SocketUser user)
        {
            var commander = UserAccounts.GetAccount(Context.User);
            if (commander.OctoPass >= 10)
            {
                var account = UserAccounts.GetAccount(user);
                if (account.ReminderList.Count == 0)
                {
                    await ReplyAsync(
                    "Booole... You have no reminders! You can create one by using the command `Remind [text] in [time]`\n" +
                        "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                        "I'm a loving order octopus!");
                    return;

                }

                var reminders = account.ReminderList;
                var embed = new EmbedBuilder();
                embed.WithTitle("Your Reminders:");
                embed.WithDescription($"**Your current time by UTC: {DateTime.UtcNow}**\n" +
                                      "To delete one of them, type the command `*del [index]`");
                embed.WithFooter("lil octo notebook");

                for (var i = 0; i < reminders.Count; i++)
                {
                    embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);
                }

                await ReplyAsync("", embed: embed);

            }
            else
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
        }




        [Command("Delete")]
        [Alias("Удалить Напоминания", "Удалить", "Удалить Напоминание", "del")]
        public async Task DeleteReminder(int index)
        {
            var account = UserAccounts.GetAccount(Context.User);

            var reminders = account.ReminderList;

            if (index > 0 && index <= reminders.Count)
            {
                reminders.RemoveAt(index - 1);
                UserAccounts.SaveAccounts();
                var embed = new EmbedBuilder();
                // embed.WithImageUrl("");
                embed.WithTitle("Boole.");
                embed.WithDescription($"Message by index **{index}** was removed!");
                embed.WithFooter("lil octo notebook");
                await Context.Channel.SendMessageAsync("", embed: embed);
                return;
            }

            await Context.Channel.SendMessageAsync(
                $"Booole...We could not find this reminder, could there be an error?\n" +
                $"Try to see all of your reminders through the command `list`");
        }


        [Command("Время")]
        [Alias("time", "date")]
        public async Task CheckTime()
        {
            await ReplyAsync($"**UTC Current Tшme: {DateTime.UtcNow}**");
        }

        private static Timer _loopingTimer;

        internal static Task CheckTimer()
        {


            _loopingTimer = new Timer()
            {
                AutoReset = true,
                Interval = 5000,
                Enabled = true
            };
            _loopingTimer.Elapsed += CheckReminders;


            return Task.CompletedTask;
        }



        //client.GetUser(userId);
        public static async void CheckReminders(object sender, ElapsedEventArgs e)
        {
            try
            {
                var allUserAccounts = UserAccounts.GetAllAccounts();
                var now = DateTime.UtcNow;

                for (var index = 0; index < allUserAccounts.Count; index++)
                {
                    if (Global.Client.GetUser(allUserAccounts[index].Id) != null)
                    {

                        var globalAccount = Global.Client.GetUser(allUserAccounts[index].Id);
                        var account = UserAccounts.GetAccount(globalAccount);

                        for (var j = 0; j < account.ReminderList.Count; j++)
                        {

                            if (account.ReminderList[j].DateToPost <= now)
                            {
                                try
                                {
                                    var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                    var embed = new EmbedBuilder();
                                    embed.WithFooter("lil octo notebook");
                                    embed.WithTitle("Pink Octopus is reminding you:");
                                    embed.WithDescription($"{account.ReminderList[j].ReminderMessage}");

                                    await dmChannel.SendMessageAsync("", embed: embed);

                                    account.ReminderList.RemoveAt(j);
                                    UserAccounts.SaveAccounts();
                                }
                                catch (Exception closedDm)
                                {
                                    Console.WriteLine($"ERROR DM SENING {account.UserName} Closed DM: '{0}'",
                                        closedDm);
                                    account.ReminderList = null;
                                    UserAccounts.SaveAccounts();
                                    return;

                                }
                            }
                        }

                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("ERROR!!! (REMINDER(Big try) Does not work: '{0}'", error);
            }
        }

    }
}


   

    

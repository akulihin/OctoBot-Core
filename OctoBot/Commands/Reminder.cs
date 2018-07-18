﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Custom_Library.DiscordBotsList.Api;
using OctoBot.Handeling;
using OctoBot.Helper;
using static OctoBot.Configs.Users.AccountSettings;

namespace OctoBot.Commands
{
    public class ReminderFormat
    {

     
        public static string[] Formats =
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
    }


    public class Reminder : ModuleBase<SocketCommandContextCustom>
    {
        private readonly AuthDiscordBotListApi _dblApi = new AuthDiscordBotListApi(423593006436712458, Config.Bot.DbLtoken);
        public async Task<bool> HasVoted(ulong userId)
        {
            var url = "https://discordbots.org/api/bots/423593006436712458/check?userId=" + userId;
            var response = await _dblApi.RestClient.SetAuthorization(Config.Bot.DbLtoken).GetAsync(url);
            return response.Body.Contains('1');
        }


        [Command("Remind")]
        [Priority(1)]
        [Alias("Напомнить", "напомни мне", "напиши мне", "напомни", "алярм", " Напомнить", " напомни мне",
            " напиши мне", " напомни", " алярм", " Remind")]
        public async Task AddReminder([Remainder] string args)
        {
            try
            {
                
                if (!await HasVoted(Context.User.Id))
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,
                        "Boole-Boole. To use this command, you have to vote here: <https://discordbots.org/bot/423593006436712458>\n" +
                        "**Please vote every day**, but I will ask it only once a month though " +
                        $"{new Emoji("<:octo_hi:465374417644552192>")}");
                    return;
                }

                string[] splittedArgs = { };

                if (args.Contains("  через ")) splittedArgs = args.Split(new[] {"  через "}, StringSplitOptions.None);
                else if (args.Contains(" через  "))
                    splittedArgs = args.Split(new[] {" через  "}, StringSplitOptions.None);
                else if (args.Contains("  через  "))
                    splittedArgs = args.Split(new[] {"  через  "}, StringSplitOptions.None);
                else if (args.Contains(" через "))
                    splittedArgs = args.Split(new[] {" через "}, StringSplitOptions.None);
                else if (args.Contains("  in ")) splittedArgs = args.Split(new[] {"  in "}, StringSplitOptions.None);
                else if (args.Contains(" in  ")) splittedArgs = args.Split(new[] {" in  "}, StringSplitOptions.None);
                else if (args.Contains("  in  ")) splittedArgs = args.Split(new[] {"  in  "}, StringSplitOptions.None);
                else if (args.Contains(" in ")) splittedArgs = args.Split(new[] {" in "}, StringSplitOptions.None);


                if (splittedArgs == null || splittedArgs.Length < 2)
                {
                    const string bigmess = "boole-boole... you are using this command incorrectly!!\n" +
                                           "Right way: `Remind [text] in [time]`\n" +
                                           "Between message and time **HAVE TO BE** written `in` part" +
                                           "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                           "I'm a loving order octopus!";
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, bigmess);
                    return;
                }

                var timeString = splittedArgs[1];
                if (timeString == "24h")
                    timeString = "1d";
                splittedArgs[1] = "";

                var reminderString = string.Join(" in ", splittedArgs, 0, 1);


                var timeDateTime = DateTime.UtcNow +
                                   TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);
                var randomIndex = SecureRandom.Random(0, OctoNamePull.OctoNameRu.Length);
                var randomOcto = OctoNamePull.OctoNameRu[randomIndex];

                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0, 1);
                var url = extra[1].Remove(extra[1].Length - 1, 1);

                var bigmess2 =
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                    $"**Time Now:                               {DateTime.UtcNow}** `by UTC`";
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithColor(SecureRandom.Random(0, 254), SecureRandom.Random(0, 254),
                    SecureRandom.Random(0, 254));
                embed.AddField($"**____**", $"{bigmess2}");
                embed.WithTitle($"{name} напомнит тебе:");
                embed.WithUrl(url);

                var account = UserAccounts.GetAccount(Context.User, 0);
                var newReminder = new CreateReminder(timeDateTime, reminderString);

                account.ReminderList.Add(newReminder);
                UserAccounts.SaveAccounts(0);


                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
            }
            catch (Exception e)
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \nTry to use this command properly: **Remind [Any_text] [in] [time format]**\n" +
                    "Alias: Напомнить, напомни мне, напиши мне, напомни, алярм, ");
                var k = HelperFunctions.DeleteMessOverTime(botMess, 10);
                ConsoleLogger.Log($" [REMINDER][Exception] ({Context.User.Username}) - {e.Message}",
                    ConsoleColor.DarkBlue);
                Console.WriteLine(e.Message);
            }
        }

        ///REMINDER FOR MINUTES!
        [Command("Re")]
        public async Task AddReminderMinute(uint minute = 0, [Remainder] string reminderString = null)
        {
            try
            {
             
                if (!await HasVoted(Context.User.Id))
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,
                        "Boole-Boole. To use this command, you have to vote here: <https://discordbots.org/bot/423593006436712458>\n" +
                        "**Please vote every day**, but I will ask it only once a month though " +
                        $"{new Emoji("<:octo_hi:465374417644552192>")}");
                    return;
                }

                if (minute > 1439)
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,
                        "Booole. [time] have to be in range 0-1439 (in minutes)");


                    return;
                }

                var hour = 0;
                var timeFormat = $"{minute}m";

                if (minute >= 60)
                    for (var i = 0; minute >= 59; i++)
                    {
                        minute = minute - 59;
                        hour++;

                        timeFormat = $"{hour}h {minute}m";
                    }

                var timeString = timeFormat; //// MAde t ominutes

                var timeDateTime = DateTime.UtcNow +
                                   TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);

                var randomIndex = SecureRandom.Random(0, OctoNamePull.OctoNameRu.Length);
                var randomOcto = OctoNamePull.OctoNameRu[randomIndex];
                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0, 1);
                var url = extra[1].Remove(extra[1].Length - 1, 1);

                var bigmess =
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                    $"**Time Now:                               {DateTime.UtcNow}** `by UTC`";

                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithColor(SecureRandom.Random(0, 254), SecureRandom.Random(0, 254),
                    SecureRandom.Random(0, 254));
                embed.AddField($"**____**", $"{bigmess}");
                embed.WithTitle($"{name} напомнит тебе:");
                embed.WithUrl(url);

                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);


                var account = UserAccounts.GetAccount(Context.User, 0);
                //account.SocketUser = SocketGuildUser(Context.User);
                var newReminder = new CreateReminder(timeDateTime, reminderString);

                account.ReminderList.Add(newReminder);
                UserAccounts.SaveAccounts(0);
            }
            catch
            {
                var botMess =
                    await ReplyAsync(
                        "boo... An error just appear >_< \nTry to use this command properly: **Remind [time_in_minutes] [Any_text]**\n");
                var k = HelperFunctions.DeleteMessOverTime(botMess, 10);
            }
        }

        //REminder To A User
        [Command("RemTo")]
        [Alias("RemindTo", "RemindTo")]
        public async Task AddReminderToSomeOne(ulong userId, [Remainder] string args)
        {
            try
            {
                
                if (!await HasVoted(Context.User.Id))
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,
                        "Boole-Boole. To use this command, you have to vote here: <https://discordbots.org/bot/423593006436712458>\n" +
                        "**Please vote every day**, but I will ask it only once a month though " +
                        $"{new Emoji("<:octo_hi:465374417644552192>")}");
                    return;
                }
                //       var commander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);


                string[] splittedArgs = null;
                if (args.Contains("  через ")) splittedArgs = args.Split(new[] {"  через "}, StringSplitOptions.None);
                else if (args.Contains(" через  "))
                    splittedArgs = args.Split(new[] {" через  "}, StringSplitOptions.None);
                else if (args.Contains("  через  "))
                    splittedArgs = args.Split(new[] {"  через  "}, StringSplitOptions.None);
                else if (args.Contains(" через "))
                    splittedArgs = args.Split(new[] {" через "}, StringSplitOptions.None);
                else if (args.Contains("  in ")) splittedArgs = args.Split(new[] {"  in "}, StringSplitOptions.None);
                else if (args.Contains(" in  ")) splittedArgs = args.Split(new[] {" in  "}, StringSplitOptions.None);
                else if (args.Contains("  in  ")) splittedArgs = args.Split(new[] {"  in  "}, StringSplitOptions.None);
                else if (args.Contains(" in ")) splittedArgs = args.Split(new[] {" in "}, StringSplitOptions.None);


                if (splittedArgs == null || splittedArgs.Length < 2)
                {
                    var bigmess = "boole-boole... you are using this command incorrectly!!\n" +
                                  "Right way: `Remind [text] in [time]`\n" +
                                  "Between message and time **HAVE TO BE** written `in` part" +
                                  "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                  "I'm a loving order octopus!";

                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, bigmess);

                    return;
                }

                var timeString = splittedArgs[splittedArgs.Length - 1];
                splittedArgs[splittedArgs.Length - 1] = "";
                var reminderString = string.Join(" in ", splittedArgs, 0, splittedArgs.Length - 1);
                var timeDateTime =
                    DateTime.UtcNow +
                    TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);

                var user = Global.Client.GetUser(userId);


                var randomIndex = SecureRandom.Random(0, OctoNamePull.OctoNameRu.Length);
                var randomOcto = OctoNamePull.OctoNameRu[randomIndex];
                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0, 1);
                var url = extra[1].Remove(extra[1].Length - 1, 1);

                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithColor(SecureRandom.Random(0, 254), SecureRandom.Random(0, 254),
                    SecureRandom.Random(0, 254));

                var bigmess2 =
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                    $"**Time Now:                               {DateTime.UtcNow}** `by UTC`";


                embed.AddField($"**____**", $"{bigmess2}");
                embed.WithTitle($"{name} напомнит {user.Username}:");
                embed.WithUrl(url);


                var account = UserAccounts.GetAccount(user, 0);
                var newReminder = new CreateReminder(timeDateTime, $"From {Context.User.Username}: " + reminderString);

                account.ReminderList.Add(newReminder);
                UserAccounts.SaveAccounts(0);


                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \nTry to use this command properly: **Remind [user_id] [Any_text] [in] [time format]** (remind to another user in my DB)\n" +
                    "Alias: Напомнить, напомни мне, напиши мне, напомни, алярм, ");
                var k = HelperFunctions.DeleteMessOverTime(botMess, 10);
            }
        }

        [Command("List")]
        [Alias("Напоминания", "Мои Напоминания", "список")]
        public async Task ShowReminders()
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, 0);
                if (account.ReminderList.Count == 0)
                {
                    var bigmess =
                        "Booole... You have no reminders! You can create one by using the command `Remind [text] in [time]`\n" +
                        "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                        "I'm a loving order octopus!";

                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, bigmess);


                    return;
                }

                var reminders = account.ReminderList;
                var embed = new EmbedBuilder();
                embed.WithTitle("Your Reminders:");
                embed.WithDescription($"**Your current time by UTC: {DateTime.UtcNow}**\n" +
                                      "To delete one of them, type the command `*Delete [index]`");
                embed.WithFooter("lil octo notebook");

                for (var i = 0; i < reminders.Count; i++)
                    embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);

                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \nTry to use this command properly: **List**(list all of your reminders)\n" +
                    "Alias: Напоминания, список, Мои Напоминания");

                var k = HelperFunctions.DeleteMessOverTime(botMess, 10);
            }
        }


        [Command("List")]
        [Alias("Напоминания", "Мои Напоминания", "список")]
        public async Task ShowUserReminders(SocketUser user)
        {
            try
            {
                var commander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (commander.OctoPass >= 10)
                {
                    var account = UserAccounts.GetAccount(user, 0);
                    if (account.ReminderList.Count == 0)
                    {
                        var bigmess =
                            "Booole... You have no reminders! You can create one by using the command `Remind [text] in [time]`\n" +
                            "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                            "I'm a loving order octopus!";

                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, bigmess);

                        return;
                    }

                    var reminders = account.ReminderList;
                    var embed = new EmbedBuilder();
                    embed.WithTitle("Your Reminders:");
                    embed.WithDescription($"**Your current time by UTC: {DateTime.UtcNow}**\n" +
                                          "To delete one of them, type the command `*del [index]`");
                    embed.WithFooter("lil octo notebook");

                    for (var i = 0; i < reminders.Count; i++)
                        embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);


                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
                }
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \nTry to use this command properly: **List [user_ping(or user ID)]**(list all of user's reminders)\n" +
                    "Alias: Напоминания, список, Мои Напоминания");
                var k = HelperFunctions.DeleteMessOverTime(botMess, 10);
            }
        }

        [Command("Delete")]
        [Alias("Удалить Напоминания", "Удалить", "Удалить Напоминание", "del")]
        public async Task DeleteReminder(int index)
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, 0);

                var reminders = account.ReminderList;

                if (index > 0 && index <= reminders.Count)
                {
                    reminders.RemoveAt(index - 1);
                    UserAccounts.SaveAccounts(0);
                    var embed = new EmbedBuilder();
                    // embed.WithImageUrl("");
                    embed.WithTitle("Boole.");
                    embed.WithDescription($"Message by index **{index}** was removed!");
                    embed.WithFooter("lil octo notebook");


                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);

                    return;
                }

                var bigmess =
                    $"Booole...We could not find this reminder, could there be an error?\n" +
                    $"Try to see all of your reminders through the command `list`";

                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, bigmess);
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \nTry to use this command properly: **del [index_num]**(delete the reminder(see all of them though comm **list**))\n" +
                    "Alias: Удалить, Delete");
                var k = HelperFunctions.DeleteMessOverTime(botMess, 10);
            }
        }

        [Command("Время")]
        [Alias("time", "date")]
        public async Task CheckTime()
        {
            try
            {
                var bigmess = $"**UTC Current Time: {DateTime.UtcNow}**";

                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, bigmess);
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \nTry to use this command properly: **time**(see current time by UTC)\n" +
                    "Alias: Удалить, Delete");
                var k = HelperFunctions.DeleteMessOverTime(botMess, 10);
            }
        }
    }
}
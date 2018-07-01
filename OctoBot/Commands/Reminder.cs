using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Handeling;
using OctoBot.Services;
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
        [Command("Remind"), Priority(1)]
        [Alias("Напомнить", "напомни мне", "напиши мне", "напомни", "алярм", " Напомнить", " напомни мне",
            " напиши мне", " напомни", " алярм", " Remind")]
        public async Task AddReminder([Remainder] string args)
        {
            try {
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
                const string bigmess = "boole-boole... you are using this command incorrectly!!\n" +
                                       "Right way: `Remind [text] in [time]`\n" +
                                       "Between message and time **HAVE TO BE** written `in` part" +
                                       "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                       "I'm a loving order octopus!";

                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, bigmess);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", bigmess);
                }
                return;
            }

            var timeString = splittedArgs[splittedArgs.Length - 1];
            splittedArgs[splittedArgs.Length - 1] = "";
            var reminderString = string.Join(" in ", splittedArgs, 0, splittedArgs.Length - 1);
            
              

            var timeDateTime = DateTime.UtcNow + TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);
                var randomIndex = SecureRandom.Random(0, OctoNamePull.OctoNameRU.Length);
                var randomOcto = OctoNamePull.OctoNameRU[randomIndex];

                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0,1);
                var url = extra[1].Remove(extra[1].Length - 1,1);
              
           var bigmess2 = 
                                                   $"{reminderString}\n\n" +
                                                   $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                                                   $"**Time Now:                               {DateTime.UtcNow}** `by UTC`";
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithColor(SecureRandom.Random(0, 255), SecureRandom.Random(0, 255),
                    SecureRandom.Random(0, 255));
                embed.AddField($"**____**", $"{bigmess2}");
                embed.WithTitle($"{name} напомнит тебе:");
                embed.WithUrl(url);
               


                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
                }

            var account = UserAccounts.GetAccount(Context.User, 0);
            var newReminder = new CreateReminder(timeDateTime, reminderString);

            account.ReminderList.Add(newReminder);
            UserAccounts.SaveAccounts(0);
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Remind [Any_text] [in] [time format]**\n" +
                                 "Alias: Напомнить, напомни мне, напиши мне, напомни, алярм, ");
            }
        }

        ///REMINDER FOR MINUTES!
        [Command("Re")]
        public async Task AddReminderMinute(uint minute = 0, [Remainder] string reminderString = null)
        {
            try {
            if (minute > 1439)
            {

                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null,  "Booole. [time] have to be in range 0-1439 (in minutes)");
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit",  "Booole. [time] have to be in range 0-1439 (in minutes)");
                }

                return;

            }

            var hour = 0;
            var timeFormat = $"{minute}m";

            if (minute >= 60)
            {

                // ReSharper disable once NotAccessedVariable
                for (var i = 0; minute >= 59; i++)
                {
                    minute = minute - 59;
                    hour++;

                    timeFormat = $"{hour}h {minute}m";
                }

            }

            var timeString = timeFormat; //// MAde t ominutes

            var timeDateTime = DateTime.UtcNow + TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);

                var randomIndex = SecureRandom.Random(0, OctoNamePull.OctoNameRU.Length);
                var randomOcto = OctoNamePull.OctoNameRU[randomIndex];
                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0,1);
                var url = extra[1].Remove(extra[1].Length - 1,1);

                var bigmess = 
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                    $"**Time Now:                               {DateTime.UtcNow}** `by UTC`";

                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithColor(SecureRandom.Random(0, 255), SecureRandom.Random(0, 255),
                    SecureRandom.Random(0, 255));
                embed.AddField($"**____**", $"{bigmess}");
                embed.WithTitle($"{name} напомнит тебе:");
                embed.WithUrl(url);


              
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
                }

            var account = UserAccounts.GetAccount(Context.User, 0);
            //account.SocketUser = SocketGuildUser(Context.User);
            var newReminder = new CreateReminder(timeDateTime, reminderString);

            account.ReminderList.Add(newReminder);
            UserAccounts.SaveAccounts(0);

            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Remind [time_in_minutes] [Any_text]**\n");
            }
        }

        //REminder To A User
        [Command("RemTo")]
        [Alias("RemindTo", "RemindTo")]
        public async Task AddReminderToSomeOne(ulong userId, [Remainder] string args)
        {

            try{
            //       var commander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);


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
                var bigmess = "boole-boole... you are using this command incorrectly!!\n" +
                                 "Right way: `Remind [text] in [time]`\n" +
                                 "Between message and time **HAVE TO BE** written `in` part" +
                                 "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                 "I'm a loving order octopus!";
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null,  bigmess);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit",  bigmess);
                }
                return;
            }

            var timeString = splittedArgs[splittedArgs.Length - 1];
            splittedArgs[splittedArgs.Length - 1] = "";
            var reminderString = string.Join(" in ", splittedArgs, 0, splittedArgs.Length - 1);
            var timeDateTime =
                DateTime.UtcNow + TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);

            var user = Global.Client.GetUser(userId);


                var randomIndex = SecureRandom.Random(0, OctoNamePull.OctoNameRU.Length);
                var randomOcto = OctoNamePull.OctoNameRU[randomIndex];
                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0,1);
                var url = extra[1].Remove(extra[1].Length - 1,1);

                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithTimestamp(DateTimeOffset.UtcNow);
                embed.WithColor(SecureRandom.Random(0, 255), SecureRandom.Random(0, 255),
                    SecureRandom.Random(0, 255));

                var bigmess2 = 
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                    $"**Time Now:                               {DateTime.UtcNow}** `by UTC`";
              
              
                embed.AddField($"**____**", $"{bigmess2}");
                embed.WithTitle($"{name} напомнит {user.Mention}:");
                embed.WithUrl(url);
          
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
                }

            var account = UserAccounts.GetAccount(user, 0);
            var newReminder = new CreateReminder(timeDateTime, $"From {Context.User.Username}: " + reminderString);

            account.ReminderList.Add(newReminder);
            UserAccounts.SaveAccounts(0);
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Remind [user_id] [Any_text] [in] [time format]** (remind to another user in my DB)\n" +
                                 "Alias: Напомнить, напомни мне, напиши мне, напомни, алярм, ");
            }
        }

        [Command("List")]
        [Alias("Напоминания", "Мои Напоминания", "список")]
        public async Task ShowReminders()
        {
            try {
            var account = UserAccounts.GetAccount(Context.User, 0);
            if (account.ReminderList.Count == 0)
            {
                var bigmess =
                    "Booole... You have no reminders! You can create one by using the command `Remind [text] in [time]`\n" +
                    "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                    "I'm a loving order octopus!";
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null,  bigmess);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit",  bigmess);
                }
                return;

            }

            var reminders = account.ReminderList;
            var embed = new EmbedBuilder();
            embed.WithTitle("Your Reminders:");
            embed.WithDescription($"**Your current time by UTC: {DateTime.UtcNow}**\n" +
                                  "To delete one of them, type the command `*Delete [index]`");
            embed.WithFooter("lil octo notebook");

            for (var i = 0; i < reminders.Count; i++)
            {
                embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);
            }

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
                 var text = "boo... An error just appear >_< \nTry to use this command properly: **List**(list all of your reminders)\n" +
                                 "Alias: Напоминания, список, Мои Напоминания";

                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, text);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", text);
                }
            }
        }


        [Command("List")]
        [Alias("Напоминания", "Мои Напоминания", "список")]
        public async Task ShowUserReminders(SocketUser user)
        {
            try {
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
                    if (Context.MessageContentForEdit != "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, null,  bigmess);
  
                    }
                    else if(Context.MessageContentForEdit == "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, "edit",  bigmess);
                    }
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
                await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
            }
            catch
            {
                var text = "boo... An error just appear >_< \nTry to use this command properly: **List [user_ping(or user ID)]**(list all of user's reminders)\n" +
                                 "Alias: Напоминания, список, Мои Напоминания";
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, text);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", text);
                }
            }
        }

        [Command("Delete")]
        [Alias("Удалить Напоминания", "Удалить", "Удалить Напоминание", "del")]
        public async Task DeleteReminder(int index)
        {
            try {
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
              
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, embed, "edit");
                }
                return;
            }

           var bigmess =
                $"Booole...We could not find this reminder, could there be an error?\n" +
                $"Try to see all of your reminders through the command `list`";
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null,  bigmess);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit",  bigmess);
                }
            }
            catch
            {
                var text = "boo... An error just appear >_< \nTry to use this command properly: **del [index_num]**(delete the reminder(see all of them though comm **list**))\n" +
                                 "Alias: Удалить, Delete";
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, text);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", text);
                }
            }
        }

        [Command("Время")]
        [Alias("time", "date")]
        public async Task CheckTime()
        {
            try {
            var bigmess = $"**UTC Current Time: {DateTime.UtcNow}**";
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null,  bigmess);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit",  bigmess);
                }
            }
            catch
            {
                await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **time**(see current time by UTC)\n" +
                                 "Alias: Удалить, Delete");
            }
        }

        private static Timer _loopingTimer;

        internal static Task CheckTimer()
        {


            _loopingTimer = new Timer
            {
                AutoReset = true,
                Interval = 5000,
                Enabled = true
            };
            _loopingTimer.Elapsed += CheckReminders;
            _loopingTimer.Elapsed += CheckForMute;


            return Task.CompletedTask;
        }



        //client.GetUser(userId);
        public static async void CheckReminders(object sender, ElapsedEventArgs e)
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

                    var removeLaterList = new List<CreateReminder>();

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

        public static async void CheckForMute(object sender, ElapsedEventArgs e)
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
         
                            if (account.MuteTimer <= now && account.MuteTimer != Convert.ToDateTime("0001-01-01T00:00:00"))
                            {


                                var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                    .SingleOrDefault(x => x.Name.ToString() == "Muted");
                                var wtf = Global.Client.GetGuild(338355570669256705).GetUser(account.Id);
                                await wtf.RemoveRoleAsync(roleToGive);
                                await wtf.ModifyAsync(u => u.Mute = false);
                                account.MuteTimer = Convert.ToDateTime("0001-01-01T00:00:00");
                                UserAccounts.SaveAccounts(0);

                                try
                                {
                                    var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                    var embed = new EmbedBuilder();
                                    embed.WithFooter("lil octo notebook");
                                    embed.WithColor(Color.Red);
                                    embed.WithImageUrl("https://i.imgur.com/puNz7pu.jpg");
                                    embed.WithDescription($"бу-бу-бу!\nБольше так не делай, тебя размутили.");

                                    await dmChannel.SendMessageAsync("", false, embed.Build());
                                }
                                catch
                                {
                                    var embed = new EmbedBuilder();
                                    embed.WithFooter("lil octo notebook");
                                    embed.WithColor(Color.Red);
                                    embed.WithImageUrl("https://i.imgur.com/puNz7pu.jpg");
                                    embed.WithDescription($"бу-бу-бу!\nБольше так не делай, тебя размутили.");

                                   await Global.Client.GetGuild(338355570669256705).GetTextChannel(374914059679694848)
                                        .SendMessageAsync("", false, embed.Build());
                                }
                            }
                    
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("ERROR!!! CheckForMute(Big try) Does not work: '{0}'", error);
               
            }
        }

    }
}


   

    

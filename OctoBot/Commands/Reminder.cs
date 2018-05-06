using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Timers;
using OctoBot.Configs.Users;
using System.Globalization;
using Discord;
using OctoBot.Configs;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Collections.Generic;
using static OctoBot.Configs.Users.AccountSettings;

namespace OctoBot.Commands
{
    public class Reminder : ModuleBase<SocketCommandContext>
    {


        [Command("Remind")]
        [Alias("Напомнить", "напомни мне", "напиши мне", "напомни", "алярм", " Напомнить", " напомни мне", " напиши мне", " напомни", " алярм", " Remind")]
        public async Task AddReminder([Remainder] string args)
        {
            string[] splittedArgs = null;
            if (args.Contains("  через ")) splittedArgs = args.Split(new[] { "  через " }, StringSplitOptions.None);
            else if (args.Contains(" через  ")) splittedArgs = args.Split(new[] { " через  " }, StringSplitOptions.None);
            else if (args.Contains("  через  ")) splittedArgs = args.Split(new[] { "  через  " }, StringSplitOptions.None);
            else if (args.Contains(" через ")) splittedArgs = args.Split(new[] { " через " }, StringSplitOptions.None);




            if (splittedArgs == null || splittedArgs.Length < 2)
            {
                await ReplyAsync("буль-буль... ты не правильно использываешь напоминалку!\n" +
                                 "Нужно так: `Напомнить [любой текст] через [время]`\n" +
                                 "Между сообщением и временем должно быть написанно `через`" +
                                 "(Время может быть разное, но соблюдай правила! **день-час-минута-секунда**. Любую из частей можно не писать. Один пробел или без него между каждой из частей\n" +
                                 "Я любящая порядок осьминожка!");
                return;
            }

            var timeString = splittedArgs[splittedArgs.Length - 1];
            splittedArgs[splittedArgs.Length - 1] = "";
            var reminderString = string.Join(" in ", splittedArgs, 0, splittedArgs.Length - 1);
            string[] formats = {
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

            await Context.Channel.SendMessageAsync($"Осьминоги тебе напомнят\n" +
                                                   $"*{reminderString}*\n\n" +
                                                   $"Мы пришём тебе ЛС в __**{timeDateTime}**__ `по UTC`\n" +
                                                   $"**Время сейчас:                  {DateTime.UtcNow}** `по UTC`");

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
               await Context.Channel.SendMessageAsync("буль-бууууль. Значение **в минутах** должно быть в ренже 0-1439");
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

            string[] formats = {
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

            await Context.Channel.SendMessageAsync($"Осьминоги тебе напомнят\n" +
                                                   $"*{reminderString}*\n\n" +
                                                   $"Мы пришём тебе ЛС в __**{timeDateTime}**__ `по UTC`\n" +
                                                   $"**Время сейчас:                  {DateTime.UtcNow}** `по UTC`");

            var account = UserAccounts.GetAccount(Context.User);
                //account.SocketUser = SocketGuildUser(Context.User);
            var newReminder = new CreateReminder(timeDateTime, reminderString);

            account.ReminderList.Add(newReminder); 
            UserAccounts.SaveAccounts();
            
        }

    


        //REminder To A User
        [Command("Rem"), Priority(1)]
        [Alias("Напомнить", "напомни мне", "напиши мне", "напомни", "алярм", " Напомнить", " напомни мне", " напиши мне", " напомни", " алярм", " Remind", "Remind")]
        public async Task AddReminderToSomeOne(ulong userId, [Remainder] string args)
        {

     //       var commander = UserAccounts.GetAccount(Context.User);
          

                string[] splittedArgs = null;
                if (args.Contains("  через "))
                    splittedArgs = args.Split(new [] {"  через "}, StringSplitOptions.None);
                else if (args.Contains(" через  "))
                    splittedArgs = args.Split(new [] {" через  "}, StringSplitOptions.None);
                else if (args.Contains("  через  "))
                    splittedArgs = args.Split(new [] {"  через  "}, StringSplitOptions.None);
                else if (args.Contains(" через "))
                    splittedArgs = args.Split(new [] {" через "}, StringSplitOptions.None);


                if (splittedArgs == null || splittedArgs.Length < 2)
                {
                    await ReplyAsync("буль-буль... ты не правильно использываешь напоминалку!\n" +
                                     "Нужно так: `Напомнить [user ID] [любой текст] через [время]`\n" +
                                     "Между сообщением и временем должно быть написанно `через`" +
                                     "(Время может быть разное, но соблюдай правила! **день-час-минута-секунда**. Любую из частей можно не писать. Один пробел или без него между каждой из частей\n" +
                                     "Я любящая порядок осьминожка!");
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

                await Context.Channel.SendMessageAsync($"Осьминоги напомнят {user.Mention}\n" +
                                                       $"*{reminderString}*\n\n" +
                                                       $"Мы пришём ему  ЛС в __**{timeDateTime}**__ `по UTC`\n" +
                                                       $"**Время сейчас:                  {DateTime.UtcNow}** `по UTC`");

                var account = UserAccounts.GetAccount(user);
                var newReminder = new CreateReminder(timeDateTime, $"От {Context.User.Username}: " + reminderString);

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
                await ReplyAsync("бууууль. У тебя нет напоминаний! Ты можешь создать новое использую команду `Напомнить [любой текст] через [время]`\n" +
                                 "(Время может быть разное, но соблюдай правила! **день-час-минута-секунда**. Любую из частей можно не писать. Один пробел или без него между каждой из частей\n" +
                                 "Я любящая порядок осьминожка!");
                return;
                
            }
            var reminders = account.ReminderList;
            var embed = new EmbedBuilder();
            embed.WithTitle("Твои Напоминания");
            embed.WithDescription($"**Твое текщее время по UTC: {DateTime.UtcNow}**\n" +
                                  "Чтобы удалить одно из них пропиши команду `*удалить индекс`");
            embed.WithFooter("Записная книжечка Осьминожек");

            for (var i = 0; i < reminders.Count; i++)
            {
                embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);
            }

            await ReplyAsync("", embed : embed);
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
                        "бууууль. У тебя нет напоминаний! Ты можешь создать новое использую команду `Напомнить [любой текст] через [время]`\n" +
                        "(Время может быть разное, но соблюдай правила! **день-час-минута-секунда**. Любую из частей можно не писать. Один пробел или без него между каждой из частей\n" +
                        "Я любящая порядок осьминожка!");
                    return;

                }

                var reminders = account.ReminderList;
                var embed = new EmbedBuilder();
                embed.WithTitle("Твои Напоминания");
                embed.WithDescription($"**Твое текщее время по UTC: {DateTime.UtcNow}**\n" +
                                      "Чтобы удалить одно из них пропиши команду `*удалить индекс`");
                embed.WithFooter("Записная книжечка Осьминожек");

                for (var i = 0; i < reminders.Count; i++)
                {
                    embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);
                }

                await ReplyAsync("", embed: embed);

            }
            else 
               await Context.Channel.SendMessageAsync("Буль-буууль, у тебя нет допуска такого уровня!");
        }




        [Command("Delete")]
        [Alias("Удалить Напоминания", "Удалить", "Удалить Напоминание")]
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
                embed.WithTitle("буль.буль.");
                embed.WithDescription($"Сообщение под номером **{index}** было удаленно, Буль!");
                embed.WithFooter("Записная книжечка Осьминожек");
                await Context.Channel.SendMessageAsync("", embed : embed);
                return;
            }

            await Context.Channel.SendMessageAsync($"бууууль. Мы не смогли найти этого напоминания, может произошла ошибка?\n" +
                                                   $"Попробуй посмотреть свои напоминание через команду `*Мои Напоминания` или `*Напоминания`");
        }


        [Command("Время")]
        [Alias("time", "date")]
        public async Task CheckTime()
        {
            await ReplyAsync($"**UTC Время сейчас: {DateTime.UtcNow}**");
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
                string result;
                try
                {
                    result = new System.Net.WebClient().DownloadString(@"OctoDataBase/accounts.json");
                }
                catch
                {
                    Console.WriteLine("Failed To ReadFile(Reminder). Will ty in 5 sec.");
                    return;
                }

                var data = JsonConvert.DeserializeObject<List<AccountSettings>>(result);
                var now = DateTime.UtcNow;


                for (var index = 0; index < data.Count; index++)
                {
                    if (Global.Client.GetUser(data[index].Id) != null)
                    {

                        var globalAccount = Global.Client.GetUser(data[index].Id);
                        var account = UserAccounts.GetAccount(globalAccount);

                        for (var j = 0; j < account.ReminderList.Count; j++)
                        {

                            if (account.ReminderList[j].DateToPost <= now)
                            {
                                try
                                {
                                    var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                    var embed = new EmbedBuilder();
                                    embed.WithFooter("Записная книжечка Осьминожек");
                                    embed.WithTitle("Ты просил нас напомнить тебе в это время:");
                                    embed.WithDescription($"{account.ReminderList[j].ReminderMessage}");

                                    await dmChannel.SendMessageAsync("", embed: embed);

                                    account.ReminderList.RemoveAt(j);
                                    UserAccounts.SaveAccounts();
                                }
                                catch
                                {
                                    Console.WriteLine($"ERROR DM SENING {account.UserName} Closed DM");
                                    account.ReminderList = null;
                                    UserAccounts.SaveAccounts();
                                    return;

                                }
                            }
                        }

                    }
                }
            }
            catch
            {
                Console.WriteLine($"ERROR REMINDER, it does not work.");
            }
        }


        [Command("CheckActive")]
        public async Task CheckActive()
        {

            string result;
            try
            {
                result = new System.Net.WebClient().DownloadString(@"OctoDataBase/accounts.json");
            }
            catch
            {
                Console.WriteLine("Failed To ReadFile(Reminder). Will ty in 5 sec.");
                return;
            }

            var data = JsonConvert.DeserializeObject<List<AccountSettings>>(result);
            var embed = new EmbedBuilder();
            var embed2 = new EmbedBuilder();
            var embed3 = new EmbedBuilder();
            var embed4 = new EmbedBuilder();
            var active = "";
            var active2 = "";
            var active3 = "";
            var active4 = "";
            var active5 = "";
            var active6 = "";
            var active7 = "";
            

            int j = 1;
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].Points > 1000 && j < 20)
                {
                    active += $"{j} {data[i].UserName} {data[i].Points} \n";
                    j++;
                }
                else if (data[i].Points > 1000 && j < 40)
                {
                    active2 += $"{j} {data[i].UserName} {data[i].Points} \n";
                    j++;
                }
                else if (data[i].Points > 1000 && j < 60)
                {
                    active3 += $"{j} {data[i].UserName} {data[i].Points} \n";
                    j++;
                }
                else if (data[i].Points > 1000 && j < 80)
                {
                    active4 += $"{j} {data[i].UserName} {data[i].Points} \n";
                    j++;
                }
                else if (data[i].Points > 1000 && j < 100)
                {
                    active5 += $"{j} {data[i].UserName} {data[i].Points} \n";
                    j++;
                }
                else if (data[i].Points > 1000 && j < 120)
                {
                    active6 += $"{j} {data[i].UserName} {data[i].Points} \n";
                    j++;
                }
                else if (data[i].Points > 1000 && j < 140)
                {
                    active7 += $"{j} {data[i].UserName} {data[i].Points} \n";
                    j++;
                }
              


            }

            embed.AddField("Актив сервера:", $"{active} {active2}");
            embed2.AddField("Актив сервера:",
                $"{active3} {active4} ");
            embed3.AddField("Актив сервера:", $"{active5} **Это все люди у которых больше 1000 окто поинтов.**");
                // embed4.AddField("Актив сервера:", $"{active7} ");
            

            await Context.Channel.SendMessageAsync("", embed: embed);
            await Context.Channel.SendMessageAsync("", embed: embed2);
            await Context.Channel.SendMessageAsync("", embed: embed3);
           // await Context.Channel.SendMessageAsync("", embed: embed4);

        }
    }
    }

   

    

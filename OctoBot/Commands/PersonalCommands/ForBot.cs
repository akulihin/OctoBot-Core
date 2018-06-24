using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using Color = Discord.Color;
using System.Linq;
using OctoBot.Configs.Users;
using OctoBot.Handeling;
using OctoBot.Services;


namespace OctoBot.Commands.PersonalCommands
{
    public class ForBot : ModuleBase<SocketCommandContextCustom>
    {
        private static Timer _loopingTimerForOctoAva;

        internal static Task TimerForBotAvatar()
        {

            _loopingTimerForOctoAva = new Timer
            {
                AutoReset = true,
                Interval = 3600000,
                Enabled = true
            };
            _loopingTimerForOctoAva.Elapsed += SetBotAva;


            return Task.CompletedTask;
        }

        public static async void SetBotAva(object sender, ElapsedEventArgs e)
        {
            try
            {
                var rand = new Random();
                var randomIndex = rand.Next(OctoPull.OctoPics.Length);
                var octoToPost = OctoPull.OctoPics[randomIndex];

                var webClient = new WebClient();
                var imageBytes = webClient.DownloadData(octoToPost);

                var stream = new MemoryStream(imageBytes);

                var image = new Image(stream);
                await Global.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);

            }
            catch
            {
                //
            }
        }


        [Command("op")]
        public async Task ModerMode()
        {
            var account = UserAccounts.GetAccount(Context.User);
            if (account.OctoPass < 100)
                return;
            var guildUser = Global.Client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id);
            
           
            var roleToGive = Global.Client.GetGuild(Context.Guild.Id).Roles
                .SingleOrDefault(x => x.Name.ToString() == "Страж");

            var roleList = guildUser.Roles.ToArray();
            if (roleList.Any(t => t.Name == "Страж"))
            {
                await guildUser.RemoveRoleAsync(roleToGive);
                

                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, "Буль!");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", "Буль!");
                }

                return;
            }

            await guildUser.AddRoleAsync(roleToGive);
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, "Буль?");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", "Буль?");
            }
        }


        [Command("LG")]
        public async Task LeaveGuild(ulong role)
        {

            var guild = Global.Client.Guilds.ToList();
            var text = "";
            for (var i = 0; i < guild.Count; i++)
            {

                text += $"{i + 1}. {guild[i].Name} {guild[i].Id} (members: {guild[i].MemberCount})\n";
            }

            //await Context.Channel.SendMessageAsync(text);
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, text);
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", text);
            }


            try
            {
                await Global.Client.GetGuild(role).LeaveAsync();
            }
            catch
            {
                //
            }
        }


        [Command("role")]
        public async Task TeninzRole(SocketUser user, string role)
        {

        

            var account = UserAccounts.GetAccount(Context.User);
            if (account.OctoPass < 100)
            {
                var errorUser = Global.Client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id);
                var timeFormat = $"1m";
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

                  var mutedRole = Global.Client.GetGuild(Context.Guild.Id).Roles
                      .SingleOrDefault(x => x.Name.ToString() == "Muted");
              await errorUser.AddRoleAsync(mutedRole);
            var mutedAccount = UserAccounts.GetAccount(Context.User);
                mutedAccount.MuteTimer = timeDateTime;
                  var time = DateTime.Now.ToString("");
          
            UserAccounts.SaveAccounts();

                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, $"{user.Mention} бу!");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", $"{user.Mention} бу!");
                }

                return;
            }

            var guildUser = Global.Client.GetGuild(Context.Guild.Id).GetUser(user.Id);
            
           
            var roleToGive = Global.Client.GetGuild(Context.Guild.Id).Roles
                .SingleOrDefault(x => x.Name.ToString() == role);

            var roleList = guildUser.Roles.ToArray();
            if (roleList.Any(t => t.Name == role))
            {
                await guildUser.RemoveRoleAsync(roleToGive);
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, "Буль!");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", "Буль!");
                }
                return;
            }

            await guildUser.AddRoleAsync(roleToGive);
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, "Буль?");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", "Буль?");
            }
        }


        [Command("marry", RunMode = RunMode.Async)]
        public async Task MarryMe(SocketUser user, [Remainder] string rem)
        {
            await ReplyAsync($"{user.Mention} Ты согласна выйти замуж за этого осьминога?({Context.User}) буль.");
            var response = await CommandHandeling.AwaitMessage(user.Id, Context.Channel.Id, 600000);
            var re = response.Content.ToLower();
            if (re == "yes" || re == "yes." || re == "yes!" || re == "for sure!" || re == "ofc." || re == "да" ||
                re == "да!" || re == "да." || re == "конечно!" || re == "конечно." || re == "конечно")
            {
                var contextAcc = UserAccounts.GetAccount(Context.User);
                var userAcc = UserAccounts.GetAccount(user);
                contextAcc.MarryTo = user.Id;
                userAcc.MarryTo = Context.User.Id;
                UserAccounts.SaveAccounts();
           
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, $"{Context.User.Username} и {user.Username} теперь женаты, бууууууль!");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", $"{Context.User.Username} и {user.Username} теперь женаты, бууууууль!");
                }
            }
            else
            {
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, $"Буль");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", $"Буль");
                }
            }
        }

        [Command("marryed")]
        [Alias("marryd")]
        public async Task MarryMe()
        {
            var account = UserAccounts.GetAccount(Context.User);
            var marr = Context.Guild.GetUser(account.MarryTo);


            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, $"ты женат/а на {marr.Username}!");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit",$"ты женат/а на {marr.Username}!");
            }
        }

        [Command("setAvatar")]
        [Alias("ava")]
        public async Task SetAvatar(string link)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {
                var webClient = new WebClient();
                var imageBytes = webClient.DownloadData(link);

                var stream = new MemoryStream(imageBytes);

                var image = new Image(stream);
                await Context.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);
                await Context.Message.DeleteAsync();
                await ReplyAsync($"Avatar have been set to `{link}`");
            }
            catch (Exception e)
            {
                var embed = new EmbedBuilder();
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.AddField("Ошибка", $"Не можем поставить аватарку: {e.Message}");
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

        [Command("Game"), Alias("ChangeGame", "SetGame")]
        public async Task SetGame([Remainder] string gamename)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                await Context.Client.SetGameAsync(gamename);
                await ReplyAsync($"Changed game to `{gamename}`");
                await Context.Message.DeleteAsync();
            }
            catch (Exception e)
            {
                var embed = new EmbedBuilder();
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.AddField("Ошибка", $"Не можем изменить игру: {e.Message}");
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

        [Command("username")]
        public async Task ChangeUsername([Remainder] string name)
        {
            await Context.Client.CurrentUser.ModifyAsync(usr => usr.Username = name);
            await ReplyAsync($":ok_hand: Changed my username to {name}");
        }

        [Command("nickname")]
        [RequireContext(ContextType.Guild)]
        public async Task ChangeNickname([Remainder] string name)
        {
            await Context.Guild.GetUser(Context.Client.CurrentUser.Id).ModifyAsync(usr => usr.Nickname = name);
            await ReplyAsync($":ok_hand: Changed my Nickname to {name}");
        }

        [Command("nick")]
        public async Task Nickname(SocketGuildUser username, [Remainder] string name)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {
                await Context.Guild.GetUser(username.Id).ModifyAsync(x => x.Nickname = name);
            }
            catch (Exception e)
            {
                var embed = new EmbedBuilder();
                embed.WithFooter("Записная книжечка Осьминожек");
                embed.AddField("Ошибка", $"Не можем изменить ник: {e.Message}");
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


        [Command("ApdMessEm")]
        public async Task ApdMessEmbed(ulong channeld, ulong messId, [Remainder] string messa)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithColor(Color.Blue);
                // embed.WithFooter("Записная книжечка Осьминожек");
                embed.WithFooter("lil octo notebook");
                embed.AddField("Сообщение:",
                    $"{messa}");

                if (await Context.Guild.GetTextChannel(channeld)
                    .GetMessageAsync(messId) is IUserMessage textChannel)
                    await textChannel.ModifyAsync(mess =>
                    {
                        mess.Embed = embed.Build();
                        // This somehow can't be empty or it won't update the 
                        // embed propperly sometimes... I don't know why
                        // message.Content =  Constants.InvisibleString;
                    });
                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot update this message!");
            }
        }

        [Command("ApdMess")]
        public async Task ApdMessString(ulong channeld, ulong messId, [Remainder] string messa)
        {

            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                var message = await Context.Guild.GetTextChannel(channeld)
                    .GetMessageAsync(messId) as IUserMessage;

                var builder = new StringBuilder();
                builder.Append($"{messa}");

                if (message != null) await message.ModifyAsync(m => m.Content = builder.ToString());
                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot update this message!");
            }
        }

        [Command("SendMess")]
        public async Task SendMessString(ulong channeld, [Remainder] string messa)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                var textChannel = Context.Guild.GetTextChannel(channeld);
                await textChannel.SendMessageAsync($"{messa}");

                await Context.Message.DeleteAsync();
            }
            catch
            {
                await ReplyAsync("We cannot send this message!");
            }
        }

        [Command("emo")]
        public async Task EmoteToMEss(ulong channeld, ulong messId, [Remainder] string messa)
        {

            try
            {
                Console.WriteLine(messa);
                if (Context.User.Id != 181514288278536193)
                    return;
                var emote = Emote.Parse($"{messa}");

                var check = messa.ToCharArray();
                if (check[0] == '<')
                    if (await Context.Guild.GetTextChannel(channeld).GetMessageAsync(messId) is IUserMessage message)
                        await message.AddReactionAsync(emote);
                    else if (await Context.Guild.GetTextChannel(channeld).GetMessageAsync(messId) is IUserMessage mess)
                        await mess.AddReactionAsync((new Emoji($"{messa}")));

                await Task.CompletedTask;
            }
            catch
            {
                //
            }
        }

        [Command("stuff")]
        public async Task Stuffening(string number)
        {
            //011111101111110
            var array = number.Select(ch => ch - '0').ToArray();

            var check = 0;
            var afterStuff = "";
            var times = 0;
            try
            {
                for (var i = 0; i < array.Length; i++)
                {
                    if (array[i] == 1 && array[i] == array[i + 1])
                    {
                        check += 1;
                        afterStuff += $"{array[i].ToString()}";
                        if (check == 5)
                        {
                            afterStuff += "**__0__**";
                            times++;
                            check = 0;
                        }
                    }
                    else
                    {
                        afterStuff += $"{array[i].ToString()}";
                        check = 0;
                    }
                }
            }
            catch
            {
                afterStuff += $"{array[array.Length - 1].ToString()}";
            }



            var embed = new EmbedBuilder();
            embed.WithColor(Color.Green);
            embed.AddField("Хм... что-бы это значило...", $"Before Stuffing: {number} - {number.Length} characters\n" +
                                                          $"After Stuffing: {afterStuff} - {afterStuff.Length - (times * 8)} characters\n" +
                                                          $"After Framing: **01111110**{afterStuff}**01111110**");

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
}
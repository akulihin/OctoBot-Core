using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs;
using OctoBot.Configs.Server;
using OctoBot.Handeling;
using OctoBot.Services;

namespace OctoBot.Commands.PersonalCommands
{
    public class ServerSetup : ModuleBase<SocketCommandContextCustom>
    {
        [Command("build")]
        [RequireOwner]
        public async Task BuildExistingServer()
        {

            var guild = Global.Client.Guilds.ToList();

            foreach (var t in guild)
            {
                ServerAccounts.GetServerAccount(t);
            } 
                   
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null,  "Севера бобавлены, бууууль!");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit",  "Севера бобавлены, бууууль!");
            }
        }

        [Command("prefix")]
        public async Task CheckPrefix()
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, $"boole: `{guild.Prefix}`");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", $"boole: `{guild.Prefix}`");
            } 
        }

        [Command("setPrefix")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetPrefix([Remainder]string prefix)
        {
            try
            {
                if (prefix.Length >= 5)
                {
                    if (Context.MessegeContent228 != "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, null, $"boole!! Please choose prefix using up to 4 characters");

                    }
                    else if (Context.MessegeContent228 == "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, "edit", $"boole!! Please choose prefix using up to 4 characters");
                    }

                    return;
                }
                var guild = ServerAccounts.GetServerAccount(Context.Guild);
                guild.Prefix = prefix;
                ServerAccounts.SaveServerAccounts();            
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, $"boole is now: `{guild.Prefix}`");

                }
                else if (Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", $"boole is now: `{guild.Prefix}`");
                }
            }
            catch
            {
                //
            }
        }

        [Command("SetLog")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetServerActivivtyLog(ulong logChannel = 0)
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);

            if (logChannel != 0)
            {
                try
                {
                    var channel = Context.Guild.GetTextChannel(logChannel);
                    guild.LogsId = channel.Id;
                    guild.ServerActivityLog = 1;
                    ServerAccounts.SaveServerAccounts();

                }
                catch
                {
                    if (Context.MessegeContent228 != "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, null, $"Booole >_< **an error** Maybe I am not an Administrator of this server? I need this permission to access audit, manage channel, emojis and users.");

                    }
                    else if (Context.MessegeContent228 == "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, "edit",  $"Booole >_< **an error** Maybe I am not an Administrator of this server? I need this permission to access audit, manage channel, emojis and users.");
                    }
                }

                return;
            }

            switch (guild.ServerActivityLog)
            {
                case 1:
                    guild.ServerActivityLog = 0;
                    ServerAccounts.SaveServerAccounts();

                    if (Context.MessegeContent228 != "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, null, $"Octopuses are not logging any activity now **:c**\n");

                    }
                    else if (Context.MessegeContent228 == "edit")
                    {
                        await CommandHandeling.SendingMess(Context, null, "edit", $"Octopuses are not logging any activity now **:c**");
                    }
                    return;
                case 0:
                    try
                    {
                        var tryChannel = Context.Guild.GetTextChannel(guild.LogsId);
                        if (tryChannel.Name != null)
                        {
                            guild.LogsId = tryChannel.Id;
                            guild.ServerActivityLog = 1;
                            ServerAccounts.SaveServerAccounts();
                            
                            var text2 = $"Boole! Now we log everything to {tryChannel.Mention}, you may rename and move it.";
                            if (Context.MessegeContent228 != "edit")
                            {
                                await CommandHandeling.SendingMess(Context, null, null, text2);

                            }
                            else if (Context.MessegeContent228 == "edit")
                            {
                                await CommandHandeling.SendingMess(Context, null, "edit", text2);
                            }
                            return;
                        }

                        var channel = Context.Guild.CreateTextChannelAsync("OctoLogs");
                        guild.LogsId = channel.Result.Id;
                        guild.ServerActivityLog = 1;
                        ServerAccounts.SaveServerAccounts();

                        var text = $"Boole! Now we log everything to {channel.Result.Mention}, you may rename and move it.";
                        if (Context.MessegeContent228 != "edit")
                        {
                            await CommandHandeling.SendingMess(Context, null, null, text);

                        }
                        else if (Context.MessegeContent228 == "edit")
                        {
                            await CommandHandeling.SendingMess(Context, null, "edit", text);
                        }

                    }
                    catch
                    {
                        if (Context.MessegeContent228 != "edit")
                        {
                            await CommandHandeling.SendingMess(Context, null, null, $"Booole >_< **an error** Maybe I am not an Administrator of this server? I need this permission to access audit, manage channel, emojis and users.");

                        }
                        else if (Context.MessegeContent228 == "edit")
                        {
                            await CommandHandeling.SendingMess(Context, null, "edit",  $"Booole >_< **an error** Maybe I am not an Administrator of this server? I need this permission to access audit, manage channel, emojis and users.");
                        }
                    }

                    break;
            }

        }

        [Command("SetLanguage")]
        [Alias("setlang")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetLanguage(string lang)
        {
           
            if (lang.ToLower() != "en" && lang.ToLower() != "ru")
            {
                if (Context.MessegeContent228 != "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, null, $"boole! only available options for now: `en`(default) and `ru`");
  
                }
                else if(Context.MessegeContent228 == "edit")
                {
                    await CommandHandeling.SendingMess(Context, null, "edit", $"boole! only available options for now: `en`(default) and `ru`");
                } 
                return;
            }
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            guild.Language = lang.ToLower();
            ServerAccounts.SaveServerAccounts();
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, $"boole~ language is now: `{lang.ToLower()}`");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", $"boole~ language is now: `{lang.ToLower()}`");
            } 
          
        }

        [Command("inv")]
        [RequireOwner]
        public async Task GetInviteLink(ulong serverId)
        {
            var guild = Global.Client.GetGuild(serverId);
            var inv = guild.GetInvitesAsync();
            
            if (Context.MessegeContent228 != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, $"{inv.Result}");
  
            }
            else if(Context.MessegeContent228 == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", $"{inv.Result}");
            } 
        }
    }
}

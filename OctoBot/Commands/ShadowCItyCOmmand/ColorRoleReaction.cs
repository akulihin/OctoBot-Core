
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.Commands.ShadowCItyCOmmand
{
    internal static class ColorRoleReaction
    {

        public static async Task ReactionAddedForRole(Cacheable<IUserMessage, ulong> cash,
            ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (reaction.MessageId == 445501974608216064)
                {
                    if (reaction.User.Value.IsBot)
                        return;
                    var globalAccount = Global.Client.GetUser(reaction.UserId);
                    switch (reaction.Emote.Name)
                    {
                        case "rem":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Weeb");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Weeb")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🦊":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Fox");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Fox")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "PeaceKeepo":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Deus Vult");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Deus Vult")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "Steampunk" :
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Steampunk");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Steampunk")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "praise" :
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Слуги солнца");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Слуги солнца")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "monkaS" :
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Meme-boy");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Meme-boy")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🐲" :
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Dragon");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Dragon")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🐼" :
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Panda");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Panda")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🦎" :
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Lizards");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Lizards")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🌑" :
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Shadow");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Shadow")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "mumu":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Nazrin");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Nazrin")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🐱":
                        {
                           
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Cat");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Cat")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    await cash.DownloadAsync().Result
                                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            

                           
                            break;
                        } 
                        case "pekaohmy" when reaction.UserId == 181514288278536193:
                        {
                            
                            var peaceKeepo = Emote.Parse("<:PeaceKeepo:438257037667729408>");
                            var praise = Emote.Parse("<:praise:445274481917952001>");
                            var rem = Emote.Parse("<:rem:445275743719522304>");
                            var steampunk = Emote.Parse("<:Steampunk:445276776676196353>");
                            var mumu = Emote.Parse("<:mumu:445277916872310785>");
                            var monkaS = Emote.Parse("<:monkaS:398183436613058570>");
                            var pekaohmy = Emote.Parse("<:pekaohmy:374656330742497280>");

                             await cash.DownloadAsync().Result.RemoveAllReactionsAsync();
                            await cash.DownloadAsync().Result.AddReactionAsync(rem);
                            await cash.DownloadAsync().Result.AddReactionAsync(new Emoji("🦊"));
                            await cash.DownloadAsync().Result.AddReactionAsync(peaceKeepo);
                            await cash.DownloadAsync().Result.AddReactionAsync(steampunk);
                            await cash.DownloadAsync().Result.AddReactionAsync(praise);
                            await cash.DownloadAsync().Result.AddReactionAsync(monkaS);
                            await cash.DownloadAsync().Result.AddReactionAsync(new Emoji("🐲"));
                            await cash.DownloadAsync().Result.AddReactionAsync(new Emoji("🐼"));
                            await cash.DownloadAsync().Result.AddReactionAsync(new Emoji("🦎"));
                            await cash.DownloadAsync().Result.AddReactionAsync(new Emoji("🌑"));
                            await cash.DownloadAsync().Result.AddReactionAsync(mumu);      
                            await cash.DownloadAsync().Result.AddReactionAsync(new Emoji("🐱"));
                            await cash.DownloadAsync().Result.AddReactionAsync(pekaohmy);    
                            
                              
                            break;
                            
                        }
                        default:
                            return;

                    }
                   
                    await cash.DownloadAsync().Result
                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                    Console.WriteLine($"REACTION ROLE: {Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId).Username} : {reaction.Emote.Name}");
                }

               
            }
            catch(Exception error)
            {
                Console.WriteLine("Reaction for Roles not workind. '{0}'", error);
            }
            
        }

    }
}



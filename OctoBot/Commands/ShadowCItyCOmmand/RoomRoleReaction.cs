
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.Commands.ShadowCItyCOmmand
{
    internal static class RoomRoleReaction
    {

        public static async Task ReactionAddedForRole(Cacheable<IUserMessage, ulong> cash,
            ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (reaction.MessageId == 445502492088860672)
                {
                    if (reaction.User.Value.IsBot)
                        return;
                    var globalAccount = Global.Client.GetUser(reaction.UserId);
                    switch (reaction.Emote.Name)
                    {
                        case "RKN":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "блок");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "блок"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }   
                        case "realy":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "антисрач");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "антисрач"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "FeelsBadMan":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "event block");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "event block"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "WoahMorfin":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "voice-game");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "voice-game"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "thonk":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "riddler");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "riddler"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "AkaShrug":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "настолочник");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "настолочник"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "such":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Технарь");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Технарь"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "GWnanamiKannaNom":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Аниме");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Аниме"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "PogChamp":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "card player");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "card player"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "hanzo":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "hots");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "hots"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "yasuo":
                        {
                            
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "LoL");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "LoL"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                              break;
                        }
                        case "gacHIPride":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Recruit");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Recruit"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "pekaohmy" when reaction.UserId == 181514288278536193:
                        {
                            
                            var rkn = Emote.Parse("<:RKN:445325930022436874>");
                            var realy = Emote.Parse("<:realy:374655750657540106>");
                            var feelsBadMan = Emote.Parse("<:FeelsBadMan:374655964843868162>");
                            var woahMorfin = Emote.Parse("<:WoahMorfin:436787514813186050>");
                            var thonk = Emote.Parse("<:thonk:445324435403309087>");
                            var akaShrug = Emote.Parse("<:AkaShrug:374802737596071936>");
                            var such = Emote.Parse("<:such:445322074781908993>");
                            var kannNom = Emote.Parse("<:GWnanamiKannaNom:445321264169746434>");
                            var pogChamp = Emote.Parse("<:PogChamp:374656108117098517>");
                            var hanzo = Emote.Parse("<:hanzo:445324859690582018>");
                           
                            var yasuo = Emote.Parse("<:yasuo:445323301137547264>");
                            var gacHiPride = Emote.Parse("<:gacHIPride:394782921749430273>");
                            var pekaohmy = Emote.Parse("<:pekaohmy:374656330742497280>");
                            var warframe = Emote.Parse("<:warframe:445467639242948618>");
                            var nintendoswitch = Emote.Parse("<:nintendoswitch:447209808064413707>");

                            await cash.DownloadAsync().Result.RemoveAllReactionsAsync();
                            await cash.DownloadAsync().Result.AddReactionAsync(rkn);
                            await cash.DownloadAsync().Result.AddReactionAsync(realy);
                            await cash.DownloadAsync().Result.AddReactionAsync(feelsBadMan);
                            await cash.DownloadAsync().Result.AddReactionAsync(woahMorfin);
                            await cash.DownloadAsync().Result.AddReactionAsync(thonk);
                            await cash.DownloadAsync().Result.AddReactionAsync(akaShrug);
                            await cash.DownloadAsync().Result.AddReactionAsync(such);
                            await cash.DownloadAsync().Result.AddReactionAsync(kannNom);
                            await cash.DownloadAsync().Result.AddReactionAsync(pogChamp);
                            await cash.DownloadAsync().Result.AddReactionAsync(hanzo);
                            
                            await cash.DownloadAsync().Result.AddReactionAsync(yasuo);
                            await cash.DownloadAsync().Result.AddReactionAsync(gacHiPride);
                            await cash.DownloadAsync().Result.AddReactionAsync(warframe);
                            await cash.DownloadAsync().Result.AddReactionAsync(nintendoswitch);
                            await cash.DownloadAsync().Result.AddReactionAsync(pekaohmy);
                            
                              
                            break;
                            
                        }
                        case "warframe":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "warframe");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "warframe"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "nintendoswitch":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "switcher");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "switcher"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.DownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        default:

                            return;
                    }
                    
                    await cash.DownloadAsync().Result
                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                 
                }
                
            }
            catch(Exception error)
            {
                Console.WriteLine("Reaction for Roles not workind. '{0}'", error);
            }
            
        }

    }
}



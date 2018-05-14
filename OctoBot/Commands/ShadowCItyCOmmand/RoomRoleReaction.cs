
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
                if (reaction.MessageId == 445338508840796180)
                {
                    switch (reaction.Emote.Name)
                    {
                        case "RKN":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "блок");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "блок")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "антисрач")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "event block")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "voice-game")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "riddler")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "настолочник")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Технарь")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Аниме")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "card player")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "hots")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "Minecrafticonfilegzpvzfll":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Minecraft");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Minecraft")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "LoL")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
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
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Recruit")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "pekaohmy":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Muted");

                            var roleList = guildUser.Roles.ToArray();
                            for (var i = 0; i < roleList.Length; i++)
                            {
                                if (roleList[i].Name == "Muted")
                                {
                                    await guildUser.RemoveRoleAsync(roleToGive);
                                    return;
                                }
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        default:
                            return;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Reaction for Roles not workind.");
            }
            
        }

    }
}



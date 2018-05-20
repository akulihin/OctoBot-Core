using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.Handeling
{
    public class EveryLogHandeling
    {
        private static readonly DiscordSocketClient Client = Global.Client;

        static readonly SocketTextChannel LogTextChannel =
            Global.Client.GetGuild(375104801018609665).GetTextChannel(446868049589698561);


        public static Task _client_Ready()
        {

            Client.ReactionAdded += Client_ReactionAddedAsync;//
            Client.Disconnected += Client_Disconnected;
            Client.Connected += Client_Connected;
            Client.MessageUpdated += Client_MessageUpdated;
            Client.MessageDeleted += Client_MessageDeleted;
            Client.JoinedGuild += Client_JoinedGuild; // Please Check more options!
            Client.ChannelCreated += Client_ChannelCreated;
            Client.ChannelDestroyed += Client_ChannelDestroyed;
            
            Client.GuildMemberUpdated += Client_GuildMemberUpdated;




            

            return Task.CompletedTask;

        }

        private static async Task Client_ChannelDestroyed(IChannel  arg)
        {
            try
            {


                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkGreen);
                embed.AddField("Channel Destroyed", $"Name:{arg.Name}\n" +
                                                    $"NSFW: {arg.IsNsfw}\n" +
                                                    $"ID: {arg.Id}\n");

                await LogTextChannel.SendMessageAsync("", embed: embed);
                await Task.CompletedTask;
            }
            catch
            {
               //
            }
        }

        private static async Task Client_ChannelCreated(IChannel arg)
        {
            
            try
            {
                var ch = arg as IGuildChannel;
                if (ch == null)
                    return;

                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkGreen);
                embed.AddField("Channel Created", $"Name: {arg.Name}\n" +
                                                  $"NSFWL {arg.IsNsfw}\n" +
                                                  $"ID: {arg.Id}\n" +
                                                  $"{arg.CreatedAt}");

                await LogTextChannel.SendMessageAsync("", embed: embed);
                await Task.CompletedTask;
            }
            catch
            {
               //
            }
        }

        private static async Task Client_GuildMemberUpdated(SocketGuildUser before, SocketGuildUser after)
        {
              try
              {

                  await Task.CompletedTask;
              }
                catch
                {
                    // ignored
                }
        }

        public static async Task Client_JoinedGuild(SocketGuild arg)
        {
            await LogTextChannel.SendMessageAsync($"<@181514288278536193> OctoBot have been connected to {arg.Name}");
        }


        public static async Task Client_Connected()
        {
            await LogTextChannel.SendMessageAsync($"OctoBot on Duty!");
        }

        public static async Task Client_Disconnected(Exception arg)
        {
            await LogTextChannel.SendMessageAsync($"OctoBot Disconnect: {arg.Message}");
        }


        public static async Task Client_ReactionAddedAsync(Cacheable<IUserMessage, ulong> arg1,
            Discord.WebSocket.ISocketMessageChannel arg2, Discord.WebSocket.SocketReaction arg3)
        {
            //  await LogTextChannel.SendMessageAsync($"<@{arg3.UserId}> placed **{arg3.Emote.Name}** emoji under **{arg3.Channel}** Channel");
            await Task.CompletedTask;
        }


        public static async Task Client_MessageUpdated(Cacheable<IMessage, ulong> messageBefore,
            SocketMessage messageAfter, ISocketMessageChannel arg3)
        {
            try
            {


                var after = messageAfter as IUserMessage;
                if (messageAfter.Content == null)
                {
                    return;
                }

                var before = (messageBefore.HasValue ? messageBefore.Value : null) as IUserMessage;
                if (before == null)
                    return;


                if (arg3 == null)
                    return;

                if (before.Content == after.Content)
                    return;


                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkPurple);
                embed.WithTitle($"📝 Updated Message: {after.Channel.Name}");
                embed.WithDescription($"**Mess Author: {after.Author}**");
                embed.AddField("Before:", $"{messageBefore.Value.Content}\n**______**");
                if (messageBefore.Value.Attachments.Any())
                    embed.AddField("attachments", $"{messageBefore.Value.Attachments.FirstOrDefault()?.Url}");
                embed.AddField("After:", $"{after}");
                if (messageAfter.Attachments.Any())
                    embed.AddField("attachments", $"{messageAfter.Attachments.FirstOrDefault()?.Url}");

                await LogTextChannel.SendMessageAsync("", embed: embed);
            }
            catch
            {
                Console.WriteLine("Cath messupd");
            }
        }

        private static async Task Client_MessageDeleted(Cacheable<IMessage, ulong> messageBefore,
            ISocketMessageChannel arg3)
        {
            try
            {
              

                var embedDel = new EmbedBuilder();
                embedDel.WithColor(Color.DarkPurple);
                embedDel.WithTitle($"🗑 Deleted Message in {messageBefore.Value.Channel.Name}");
                embedDel.WithDescription($"Mess Author: **{messageBefore.Value.Author}**\n");
                embedDel.AddField("Content", $"{messageBefore.Value.Content}");
                embedDel.AddField("Mess ID", $"{messageBefore.Id}");
                if (messageBefore.Value.Attachments.Any())
                    embedDel.AddField("attachments", $"URL: {messageBefore.Value.Attachments.FirstOrDefault()?.Url}\n" +
                                                     $"Proxy URL: {messageBefore.Value.Attachments.FirstOrDefault()?.ProxyUrl}"); 
   

                await LogTextChannel.SendMessageAsync("", embed: embedDel);
            }
            catch
            {
                //
            }
        }

    }
}

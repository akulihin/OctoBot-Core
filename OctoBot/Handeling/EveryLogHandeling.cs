using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.Handeling
{
    public class EveryLogHandeling
    {
        private static readonly DiscordSocketClient Client = Global.Client;
        static readonly SocketTextChannel LogTextChannel = Global.Client.GetGuild(375104801018609665).GetTextChannel(446868049589698561);


        public static Task _client_Ready()
        {
 
            Client.ReactionAdded += _client_ReactionAddedAsync;
            Client.ChannelCreated += Client_ChannelCreated;
            Client.ChannelDestroyed += Client_ChannelDestroyed;
            Client.ChannelUpdated += Client_ChannelUpdated;
            Client.Disconnected += Client_Disconnected;
            Client.Connected += Client_Connected;
          //  Client.GuildMemberUpdated += Client_GuildMemberUpdated; // Please Check more options!
            Client.JoinedGuild += Client_JoinedGuild; // Please Check more options!
            Client.MessageDeleted += Client_MessageDeleted; // DOES NO WORK
            Client.RoleCreated += Client_RoleCreated;  
            
            

            return Task.CompletedTask;
            
        }

        private static async Task Client_RoleCreated(SocketRole arg)
        {
            await LogTextChannel.SendMessageAsync($"{arg.Name} role Have been Created, color {arg.Color}");
        }

        private static  async Task Client_MessageDeleted(Cacheable<IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {

            var embed = new EmbedBuilder();
            
            embed.WithColor(Color.DarkRed);
            embed.AddField($"Messages have been deleted  in {arg2.Name}", $"{arg1.DownloadAsync().Result.Author} 123 {arg1.DownloadAsync().Result.Content}");
            
            await LogTextChannel.SendMessageAsync("", embed: embed);
        }

        private static async Task Client_JoinedGuild(SocketGuild arg)
        {
            await LogTextChannel.SendMessageAsync($"<@181514288278536193> OctoBot have been connected to {arg.Name}");
        }
        /*
        private static async Task Client_GuildMemberUpdated(SocketGuildUser arg1, SocketGuildUser arg2)
        {
            await LogTextChannel.SendMessageAsync($"{arg1.Mention} have changed to {arg2.Mention}");
        }*/

        private static async Task Client_Connected()
        {
            await LogTextChannel.SendMessageAsync($"OctoBot on Duty!");
        }

        private static async Task Client_Disconnected(Exception arg)
        {
            await LogTextChannel.SendMessageAsync($"OctoBot Disconnect: {arg.Message}");
        }

        private static async Task Client_ChannelUpdated(SocketChannel arg1, SocketChannel arg2)
        {
            await LogTextChannel.SendMessageAsync($"{arg1.CreatedAt.DateTime} Channel named **{arg1.ToString()}** Have been **Updated** to {arg2.ToString()}");
        }

        private static async Task Client_ChannelDestroyed(SocketChannel arg)
        {
            await LogTextChannel.SendMessageAsync($"{arg.CreatedAt.DateTime} Channel named **{arg.ToString()}** Have been **Destroyed**");
        }

        private static async Task Client_ChannelCreated(SocketChannel arg)
        {
            await LogTextChannel.SendMessageAsync($"{arg.CreatedAt.DateTime} Channel named **{arg.ToString()}** Have been **Created**");
        }

        private static async Task _client_ReactionAddedAsync(Cacheable<IUserMessage, ulong> arg1, Discord.WebSocket.ISocketMessageChannel arg2, Discord.WebSocket.SocketReaction arg3)
        {
            await LogTextChannel.SendMessageAsync($"<@{arg3.UserId}> placed {arg3.Emote.Name} emoji under {arg3.Channel} Channel ({arg1.Value.Author.Mention} message)");
        }
    }
}

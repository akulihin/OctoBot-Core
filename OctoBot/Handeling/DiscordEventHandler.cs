using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Automated;
using OctoBot.Commands;
using OctoBot.Commands.PersonalCommands;
using OctoBot.CustomForServers;
using OctoBot.Games.Game2048;
using OctoBot.Games.OctoGame;

namespace OctoBot.Handeling
{
    public class DiscordEventHandler
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private readonly DiscordSocketClient _client;
        private readonly CommandHandelingSendingAndUpdatingMessages _commandHandler;



        public DiscordEventHandler(DiscordSocketClient client, CommandHandelingSendingAndUpdatingMessages commandHandler)
        {
            _client = client;
            _commandHandler = commandHandler;
        }

        public void InitDiscordEvents()
        {
            _client.ChannelCreated += ChannelCreated;
            _client.ChannelDestroyed += ChannelDestroyed;
            _client.ChannelUpdated += ChannelUpdated;
            _client.Connected += Connected;
            _client.CurrentUserUpdated += CurrentUserUpdated;
            _client.Disconnected += Disconnected;
            _client.GuildAvailable += GuildAvailable;
            _client.GuildMembersDownloaded += GuildMembersDownloaded;
            _client.GuildMemberUpdated += GuildMemberUpdated;
            _client.GuildUnavailable += GuildUnavailable;
            _client.GuildUpdated += GuildUpdated;
            _client.JoinedGuild += JoinedGuild;
            _client.LatencyUpdated += LatencyUpdated;
            _client.LeftGuild += LeftGuild;
            _client.Log += Log;
            _client.LoggedIn += LoggedIn;
            _client.LoggedOut += LoggedOut;
            _client.MessageDeleted += MessageDeleted;
            _client.MessageReceived += MessageReceived;
            _client.MessageUpdated += MessageUpdated;
            _client.ReactionAdded += ReactionAdded;
            _client.ReactionRemoved += ReactionRemoved;
            _client.ReactionsCleared += ReactionsCleared;
            _client.Ready += Ready;
            _client.RecipientAdded += RecipientAdded;
            _client.RecipientRemoved += RecipientRemoved;
            _client.RoleCreated += RoleCreated;
            _client.RoleDeleted += RoleDeleted;
            _client.RoleUpdated += RoleUpdated;
            _client.UserBanned += UserBanned;
            _client.UserIsTyping += UserIsTyping;
            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;
            _client.UserUnbanned += UserUnbanned;
            _client.UserUpdated += UserUpdated;
            _client.UserVoiceStateUpdated += UserVoiceStateUpdated;

        }




        private async Task ChannelCreated(SocketChannel channel)
        {
            EveryLogHandeling.Client_ChannelCreated(channel);
        }

        private async Task ChannelDestroyed(SocketChannel channel)
        {
            EveryLogHandeling.Client_ChannelDestroyed(channel);
        }

        private async Task ChannelUpdated(SocketChannel channelBefore, SocketChannel channelAfter)
        {
            EveryLogHandeling.Client_ChannelUpdated(channelBefore, channelAfter);
        }

        private async Task Connected()
        {
            EveryLogHandeling.Client_Connected();
        }

        private async Task CurrentUserUpdated(SocketSelfUser userBefore, SocketSelfUser userAfter)
        {
            
        }

        private async Task Disconnected(Exception exception)
        {
            EveryLogHandeling.Client_Disconnected(exception);

        }

        private async Task GuildAvailable(SocketGuild guild)
        {
            
        }

        private async Task GuildMembersDownloaded(SocketGuild guild)
        {
            
        }

        private async Task GuildMemberUpdated(SocketGuildUser userBefore, SocketGuildUser userAfter)
        {
            EveryLogHandeling.Client_GuildMemberUpdated(userBefore, userAfter);
        }

        private async Task GuildUnavailable(SocketGuild guild)
        {
            
        }

        private async Task GuildUpdated(SocketGuild guildBefore, SocketGuild guildAfter)
        {
            
        }

        private async Task JoinedGuild(SocketGuild guild)
        {
            EveryLogHandeling.Client_JoinedGuild(guild);
        }

        private async Task LatencyUpdated(int latencyBefore, int latencyAfter)
        {
            
        }

        private async Task LeftGuild(SocketGuild guild)
        {
            
        }

        private async Task Log(LogMessage logMessage)
        {
            ConsoleLogger.Log(logMessage); 
        }

        private async Task LoggedIn()
        {
            
        }

        private async Task LoggedOut()
        {
            
        }

        private async Task MessageDeleted(Cacheable<IMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
            EveryLogHandeling.Client_MessageDeleted(cacheMessage, channel);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            _commandHandler.HandleCommandAsync(message);
            EveryLogHandeling.Client_MessageReceived(message);
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> cacheMessageBefore, SocketMessage messageAfter, ISocketMessageChannel channel)
        {
            _commandHandler._client_MessageUpdated(cacheMessageBefore, messageAfter, channel);
            EveryLogHandeling.Client_MessageUpdated(cacheMessageBefore, messageAfter, channel);
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (reaction.User.Value.IsBot) return;
             Reaction.ReactionAddedFor2048(cacheMessage, channel, reaction);                                                        
             OctoGameReaction.ReactionAddedForOctoGameAsync(cacheMessage, channel, reaction);
              ColorRoleReaction.ReactionAddedForRole(cacheMessage, channel, reaction);
             RoomRoleReaction.ReactionAddedForRole(cacheMessage, channel, reaction);
             RoomRoleReactionHandeling.ReactionAddedForRole(cacheMessage, channel, reaction);
            EveryLogHandeling.Client_ReactionAddedForArtVotes(cacheMessage, channel, reaction);
            EveryLogHandeling.Client_ReactionAddedAsyncForBlog(cacheMessage, channel, reaction);


        }

        private async Task ReactionRemoved(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
            EveryLogHandeling.Client_ReactionRemovedForBlog(cacheMessage, channel, reaction);
            EveryLogHandeling.Client_ReactionRemovedForArtVotes(cacheMessage, channel, reaction);
        }

        private async Task ReactionsCleared(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
            
        }

        private async Task Ready()
        {
            GreenBuuTimerClass.StartTimer(); ////////////// Timer1 Green Boo starts
            CheckForPull.CheckTimerForPull();
            CheckForMute.CheckTimer();
            CheckReminders.CheckTimer();
            ForBot.TimerForChangeBotAvatar();
            //  _client.Ready += YellowTurtle.StartTimer; //// Timer3
        }

        private async Task RecipientAdded(SocketGroupUser user)
        {
            
        }

        private async Task RecipientRemoved(SocketGroupUser user)
        {
            
        }

        private async Task RoleCreated(SocketRole role)
        {
            
        }

        private async Task RoleDeleted(SocketRole role)
        {
            EveryLogHandeling.Client_RoleDeleted(role);
        }

        private async Task RoleUpdated(SocketRole roleBefore, SocketRole roleAfter)
        {
            EveryLogHandeling.Client_RoleUpdated(roleBefore, roleAfter);
        }

        private async Task UserBanned(SocketUser user, SocketGuild guild)
        {
            
        }

        private async Task UserIsTyping(SocketUser user, ISocketMessageChannel channel)
        {
            
        }

        private async Task UserJoined(SocketGuildUser user)
        {
             Announcer.AnnounceUserJoin(user);
            EveryLogHandeling.Client_UserJoined_ForRoleOnJoin(user);
        }

        private async Task UserLeft(SocketGuildUser user)
        {
            
        }

        private async Task UserUnbanned(SocketUser user, SocketGuild guild)
        {
            
        }

        private async Task UserUpdated(SocketUser user, SocketUser guild)
        {
            
        }

        private async Task UserVoiceStateUpdated(SocketUser user, SocketVoiceState voiceStateBefore, SocketVoiceState voiceStateAfter)
        {
            
        }
    }
}

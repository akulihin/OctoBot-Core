
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace OctoBot.Services
{
    public class SocketCommandContextCustom : ICommandContext
    {
        public DiscordSocketClient Client { get; }

        public SocketGuild Guild { get; }

        public ISocketMessageChannel Channel { get; }

        public SocketUser User { get; }

        public SocketUserMessage Message { get; }

        public string MessegeContent228 { get; }

        public bool IsPrivate
        {
            get
            {
                return this.Channel is IPrivateChannel;
            }
        }

        public SocketCommandContextCustom(DiscordSocketClient client, SocketUserMessage msg, string edit = null)
        {
            this.Client = client;
            this.Guild = (msg.Channel as SocketGuildChannel)?.Guild;
            this.Channel = msg.Channel;
            this.User = msg.Author;
            this.Message = msg;
            this.MessegeContent228 = edit;
        }
   

        IDiscordClient ICommandContext.Client
        {
            get
            {
                return (IDiscordClient) this.Client;
            }
        }

        IGuild ICommandContext.Guild
        {
            get
            {
                return (IGuild) this.Guild;
            }
        }

        IMessageChannel ICommandContext.Channel
        {
            get
            {
                return (IMessageChannel) this.Channel;
            }
        }

        IUser ICommandContext.User
        {
            get
            {
                return (IUser) this.User;
            }
        }

        IUserMessage ICommandContext.Message
        {
            get
            {
                return (IUserMessage) this.Message;
            }
        }
    }
}
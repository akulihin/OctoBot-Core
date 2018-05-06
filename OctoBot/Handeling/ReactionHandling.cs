using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Games.Game2048;

namespace OctoBot.Handeling
{
    internal static class Reaction
    {


        internal static Task ReactionAddedFor2048(Cacheable<IUserMessage, ulong> cash, ISocketMessageChannel channel,
            SocketReaction reaction)
        {
            if (reaction.MessageId != Global.MessageIdToTrack) return Task.CompletedTask;

            switch (reaction.Emote.Name)
            {
                case "⬆":
                    NewGame.MakeMove(reaction.UserId, GameWork.MoveDirection.Up);
                    break;
                case "⬇":
                    NewGame.MakeMove(reaction.UserId, GameWork.MoveDirection.Down);
                    break;
                case "⬅":
                    NewGame.MakeMove(reaction.UserId, GameWork.MoveDirection.Left);
                    break;
                case "➡":
                    NewGame.MakeMove(reaction.UserId, GameWork.MoveDirection.Right);
                    break;
                default:
                    return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    
        
    }
}

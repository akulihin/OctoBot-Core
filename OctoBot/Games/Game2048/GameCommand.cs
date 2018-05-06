using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs;

namespace OctoBot.Games.Game2048
{
    public class GameBridge : ModuleBase<SocketCommandContext>
    {



        [Command("2048")]
        public async Task Start1024Game()
        {
            if (NewGame.UserIsPlaying(Context.User.Id))
            {

                await ReplyAsync("Ты **уже** играешь\n нужно закончит начатое\n ну или позови админа");
                return;
            }

            var message = await Context.Channel.SendMessageAsync("**Пожалуйста подожди**");


            await message.AddReactionAsync(new Emoji("⬅"));
            await message.AddReactionAsync(new Emoji("➡"));
            await message.AddReactionAsync(new Emoji("⬆"));
            await message.AddReactionAsync(new Emoji("⬇"));



            NewGame.CreateNewGame(Context.User.Id, message);


            Global.MessageIdToTrack = message.Id;


        }

    }

}

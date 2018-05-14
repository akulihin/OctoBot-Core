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

                await ReplyAsync("Ты **уже** играешь, нужно закончить начатое. Ну или закончи ее командой `*e2`");
                return;
            }

            var message = await Context.Channel.SendMessageAsync("**Пожалуйста подожди**");


            await message.AddReactionAsync(new Emoji("⬅"));
            await message.AddReactionAsync(new Emoji("➡"));
            await message.AddReactionAsync(new Emoji("⬆"));
            await message.AddReactionAsync(new Emoji("⬇"));
            await message.AddReactionAsync(new Emoji("❌"));


            NewGame.CreateNewGame(Context.User.Id, message);
            
            var new2048Game = new Global.OctoGameMessAndUserTrack2048(message.Id, Context.User.Id, message, Context.User);

           Global.OctopusGameMessIdList2048.Add(new2048Game);
             
          


        }

        [Command("end2048")]
        [Alias("finish2048", "e2048", "e2")]
        public async Task End2048Gmae()
        {
            OctoBot.Games.Game2048.NewGame.EndGame(Context.User.Id);
            await Task.CompletedTask;
        }
    

    }

}

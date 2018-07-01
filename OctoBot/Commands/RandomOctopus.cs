using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs.Users;
using OctoBot.Handeling;
using OctoBot.Services;

namespace OctoBot.Commands
{ 
    public class OctopusPic : ModuleBase<SocketCommandContextCustom>
    {

        [Command("octo")]
        [Alias("окто", "octopus", "Осьминог", "Осьминожка", "Осьминога", "o", "oct", "о")]
        public async Task OctopusPicture()
        {
            try
            {
               
                var index = SecureRandom.Random(0, 255);
                if (index == 5 || index == 38 || index == 69)
                {

                   var lll = await Context.Channel.SendMessageAsync("boole");
                  var k =  HelperFunctions.DeleteMessOverTime(lll, 6);
                }
                else
                {
                    var octoIndex = SecureRandom.Random(0, OctoPicPull.OctoPics.Length);
                    var octoToPost =  OctoPicPull.OctoPics[octoIndex];
                  

                    var color1Index = SecureRandom.Random(0, 255);
                    var color2Index = SecureRandom.Random(0, 255);
                    var color3Index = SecureRandom.Random(0, 255);
               
                    var randomIndex = SecureRandom.Random(0, OctoNamePull.OctoNameRu.Length);
                    var randomOcto = OctoNamePull.OctoNameRu[randomIndex];

                    var embed = new EmbedBuilder();
                    embed.WithDescription($"{randomOcto} нашоль фото:");
                    embed.WithFooter("lil octo notebook");
                    embed.WithColor(color1Index, color2Index, color3Index);
                    embed.WithAuthor(Context.User);
                    embed.WithImageUrl("" + octoToPost);

                    if (Context.MessageContentForEdit != "edit")
                    {
                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                    }
                    else if(Context.MessageContentForEdit == "edit")
                    {
                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                    }



                    if (octoIndex == 19)
                    {
                      var lll=  await Context.Channel.SendMessageAsync("Oppa, it was I who passed Dark Souls!");
                        var k =  HelperFunctions.DeleteMessOverTime(lll, 6);
                    }

                    if (octoIndex == 9)
                    {
                        var lll=   await Context.Channel.SendMessageAsync("I'm drawing an octopus :3");
                        var k =  HelperFunctions.DeleteMessOverTime(lll, 6);
                    }

                    if (octoIndex == 26)
                    {
                        var lll=    await Context.Channel.SendMessageAsync("Oh, this is New Year! time to gift turtles!!");
                        var k =  HelperFunctions.DeleteMessOverTime(lll, 6);
                    }

                }
            }
            catch
            {
               // await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Octo**\n");
            }
        }

        [Command("octo")]
        [Alias("окто", "octopus", "Осьминог", "Осьминожка", "Осьминога", "o", "oct", "о")]
        public async Task OctopusPictureSelector(int selection)
        {
            try
            {
                var passCheck = UserAccounts.GetAccount(Context.User, Context.Guild.Id);


                if (passCheck.OctoPass >= 1)
                {
                    var boo = new Random();
                    var index = boo.Next(30);
                    if (index == 20 || index == 19 || index == 18)
                    {

                        await Context.Channel.SendMessageAsync("boole");
                    }
                    else
                    {

                        if ((OctoPicPull.OctoPics.Length - 1) < selection)
                        {
                            await Context.Channel.SendMessageAsync(
                                $"Boole. The maximum available index is {(OctoPicPull.OctoPics.Length - 1)}");
                            return;
                        }

                        var octoToPost = OctoPicPull.OctoPics[selection];

                        var color1Index = SecureRandom.Random(0, 255);
                        var color2Index = SecureRandom.Random(0, 255);
                        var color3Index = SecureRandom.Random(0, 255);

                        var embed = new EmbedBuilder();
                        embed.WithColor(color1Index, color2Index, color3Index);
                        embed.WithFooter("lil octo notebook");
                        embed.WithAuthor(Context.User);
                        embed.WithImageUrl("" + octoToPost);
                        

                        if (Context.MessageContentForEdit != "edit")
                        {
                            await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                        }
                        else if(Context.MessageContentForEdit == "edit")
                        {
                            await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                        }
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
                }
            }
            catch
            {
               // await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **Octo [Octo_index]**\n");
            }
        }


    }

}

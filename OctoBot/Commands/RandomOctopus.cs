using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs.Users;

namespace OctoBot.Commands
{


    public static class OctoPull
    {

        public static string[] OctoPics = 
                 {
                //индекс осьминога -1 к его номеру
            "https://i.imgur.com/NQlAjwN.png", //1 розовый книга
            "https://i.imgur.com/rwQb4Zz.jpg", //2 Сервый верхтормашками
            "https://i.imgur.com/PoMF5Pn.jpg", //3
            "https://i.imgur.com/JhKwGgq.jpg", //4
            "https://i.imgur.com/puNz7pu.jpg", //5 черный с ножом
            "https://i.imgur.com/C44tEp4.jpg", //6
            "https://i.imgur.com/I5SoZU5.jpg", //7
            "https://i.imgur.com/KjbRCLE.jpg", //8
            "https://i.imgur.com/dxJq2Ey.png", //9
            "https://i.imgur.com/0Q2qoFh.jpg", //10 Жёлтый рисует
            "https://i.imgur.com/U4BCc4q.jpg", //11
            "https://i.imgur.com/chSMjPQ.jpg", //12
            "https://i.imgur.com/aqKDTB0.jpg", //13
            "https://i.imgur.com/axBcQrv.jpg", //14
            "https://i.imgur.com/pjaroXG.jpg", //15
            "https://i.imgur.com/BlqRoJX.jpg", //16
            "https://i.imgur.com/cIO2Nm7.jpg", //17
            "https://i.imgur.com/DTqlQ7J.jpg", //18
            "https://i.imgur.com/VGHWFwj.jpg", //19
            "https://i.imgur.com/odNQZqA.jpg", //20 Жёлтый с геймпадом
            "https://i.imgur.com/Hrm3FoB.jpg", //21
            "https://i.imgur.com/dM1fylv.jpg", //22
            "https://i.imgur.com/h3oUUsL.jpg", //23
            "https://i.imgur.com/EyxWjOl.jpg", //24
            "https://i.imgur.com/N1yX1p9.jpg", //25
            "https://i.imgur.com/D3CDBxh.jpg", //26
            "https://i.imgur.com/Xkv4kjs.jpg", //27 Жёлты Нвоый год
            "https://i.imgur.com/UZGqdub.jpg", //28
            "https://i.imgur.com/9Zh2wlg.jpg", //29
            "https://i.imgur.com/fzCQxDp.jpg", //30
            "https://i.imgur.com/wBAuR1c.jpg", //31
            "https://i.imgur.com/42vQjK1.jpg", //32
            "https://i.imgur.com/RD6hXRA.jpg", //33
            "https://i.imgur.com/EL4tj6W.jpg", //34
            "https://i.imgur.com/2czyupr.jpg", //35
            "https://i.imgur.com/f3Jlmo5.jpg", //36
            "https://i.imgur.com/YfsWeVw.jpg", //37
            "https://i.imgur.com/H1VpUXU.jpg", //38
            "https://i.imgur.com/39F8TIt.jpg", //39
            "https://i.imgur.com/TDmVtkU.jpg", //40
            "https://i.imgur.com/B018LtR.jpg", //41
            "https://i.imgur.com/etRphwD.jpg", //42
            "https://i.imgur.com/rjaNOYB.jpg", //43
            "https://i.imgur.com/y0AeviU.jpg", //44 Davlas Octo
            "https://i.imgur.com/47dNg8Y.jpg", //45
            "https://i.imgur.com/HcsXFSr.jpg", //46 OCto-spider
            "https://i.imgur.com/KfmI8d3.jpg", //47
            "https://i.imgur.com/AlTg8Gi.jpg", //48 (-1)
            "https://i.imgur.com/k9y6RmI.jpg",  //49 ДРЯКОООНЬ!
            "https://i.imgur.com/h1lVuBH.jpg", //50 (-1 alsways!)
            "https://i.imgur.com/I9xLycY.jpg", //51 пипашка в шапке!
            "https://i.imgur.com/OJvPxlQ.jpg", //52 дряконы!
            "https://i.imgur.com/0PGcMv5.jpg", //53
            "https://i.imgur.com/Mpnj6ry.jpg", //54
            "https://i.imgur.com/3T66o9I.jpg", //55
            "https://i.imgur.com/VNphTYI.jpg", //56 Pachi with headphones
            "https://i.imgur.com/PSi8N2i.jpg", //57 2Rainbow and green Boo
            "https://i.imgur.com/5gh7Kgs.jpg", //58 OctoSpider + Purple-Standing
            "https://i.imgur.com/akfktDb.jpg", //59  Rainbow on YellowTurtle
            "https://i.imgur.com/Pn9Sgl0.jpg", //60 Rainbow LeCrisp
            "https://i.imgur.com/YiW43ad.jpg", //61 GreenBoo With a knife
            "https://i.imgur.com/yMSGQYu.jpg",  //62 Братишки
            "https://i.imgur.com/GCKywPO.jpg",  //63 братишка за компом
            "https://i.imgur.com/i7CsKuk.jpg" //64 братишка и много осьминогов
                 };


    }



    public class OctopusPic : ModuleBase<SocketCommandContext>
    {

    [Command("octo")]
    [Alias("окто", "octopus", "Осьминог", "Осьминожка", "Осьминога")]
        public async Task OctopusPicture()
        {
            var boo = new Random();
            var  index = boo.Next(21);
            if (index == 20 || index == 19 || index == 18)
            {

                await Context.Channel.SendMessageAsync("буль");
            }
            else
            {

                Random rand;
                rand = new Random();
                int randomIndex = rand.Next(OctoPull.OctoPics.Length);
                string octoToPost = OctoPull.OctoPics[randomIndex];


                Random color1;
                Random color2;
                Random color3;
                color1 = new Random();
                color2 = new Random();
                color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);


                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);
                embed.WithAuthor(Context.User);
                embed.WithImageUrl("" + octoToPost);

                await Context.Channel.SendMessageAsync("", embed: embed);



                if (randomIndex == 19)
                {
                    await Context.Channel.SendMessageAsync("Оппа, это же я прошел ДаркСоулс!");
                }
                if (randomIndex == 9)
                {
                    await Context.Channel.SendMessageAsync("Рисую осьминога :3");
                }
                if (randomIndex == 26)
                {
                    await Context.Channel.SendMessageAsync("Новый Год, Новый Год, время дарить черепашек!!");
                }

            }
        }

        [Command("octo")]
        [Alias("окто", "octopus", "Осьминог", "Осьминожка", "Осьминога")]
        public async Task OctopusPictureSelector(int selection)
        {
            var passCheck = UserAccounts.GetAccount(Context.User);
       
            
            if (passCheck.OctoPass >= 1)
            {
                var boo = new Random();
                var index = boo.Next(21);
                if (index == 20 || index == 19 || index == 18)
                {

                    await Context.Channel.SendMessageAsync("буль");
                }
                else
                {
                 
                    if ((OctoPull.OctoPics.Length - 1) < selection)
                    {
                        await Context.Channel.SendMessageAsync($"буль-буль. Максимально доступный индекс {(OctoPull.OctoPics.Length - 1)}");
                        return;
                    }
                    string octoToPost = OctoPull.OctoPics[selection];

                    Random color1;
                    Random color2;
                    Random color3;
                    color1 = new Random();
                    color2 = new Random();
                    color3 = new Random();
                    int color1Index = color1.Next(256);
                    int color2Index = color2.Next(256);
                    int color3Index = color3.Next(256);


                    var embed = new EmbedBuilder();
                    embed.WithColor(color1Index, color2Index, color3Index);
                    embed.WithAuthor(Context.User);
                    embed.WithImageUrl("" + octoToPost);

                    await Context.Channel.SendMessageAsync("", embed: embed);



                    if (selection == 19)
                    {
                        await Context.Channel.SendMessageAsync("Оппа, это же я прошел ДаркСоулс!");
                    }
                    if (selection == 9)
                    {
                        await Context.Channel.SendMessageAsync("Рисую осьминога :3");
                    }
                    if (selection == 26)
                    {
                        await Context.Channel.SendMessageAsync("Новый Год, Новый Год, время дарить черепашек!!");
                    }    

                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("бууууууууууль, у тебя нет пропуска!");
            }
        }




   
} 

}

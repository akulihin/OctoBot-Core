using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs;

namespace OctoBot.Commands.PersonalCommands
{
    public class VollGaz : ModuleBase<SocketCommandContext>
    {

        [Command("Miserum")]
        public async Task Miserum()
        {
            if (Context.User.Id == 129658526460149760)
            {


                var meme = new[]
                {
                    "https://i.imgur.com/qWb1Q0x.jpg",
                    "https://i.imgur.com/H8umFpy.png",
                    "https://i.imgur.com/kBwCId7.jpg",
                    "https://i.imgur.com/OXqvzAo.jpg",
                    "https://i.imgur.com/snUQeuU.jpg",
                    "https://i.imgur.com/mzpy9wJ.jpg",
                    "https://i.imgur.com/PrEpuqH.jpg",
                    "https://i.imgur.com/ZGmIg8c.jpg"
                };
                var randMeme = new Random();
                var randomIndex = randMeme.Next(meme.Length);
                var memeToPost = meme[randomIndex];



                var color1 = new Random();
                var color2 = new Random();
                var color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);


                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);
                embed.WithAuthor("MISERUM!");
                embed.WithImageUrl("" + memeToPost);
                await Context.Channel.SendMessageAsync("", embed: embed);

            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
        }

        [Command("буль")]
        public async Task Boole()
        {
            
            var peaceKeepo = Emote.Parse("<:PeaceKeepo:438257037667729408>");
            var praise = Emote.Parse("<:praise:445274481917952001>");
            var rem = Emote.Parse("<:rem:445275743719522304>");
            var steampunk = Emote.Parse("<:Steampunk:445276776676196353>");
            var mumu = Emote.Parse("<:mumu:445277916872310785>");
            var monkaS = Emote.Parse("<:monkaS:398183436613058570>");
                

            var embed = new EmbedBuilder();
            embed.WithColor(Color.DarkGreen);
            embed.AddField("Роль для цвета",
                $" Чтобы **получить** или **снять** роль просто нажмите определенную эмоцию\n" +
                $"{new Emoji("<:rem:445275743719522304>")} <@&374900834946908160>\n{new Emoji("🦊")} <@&375079829642412034>\n{new Emoji("<:PeaceKeepo:438257037667729408>")} <@&374900824880447489>\n{new Emoji("<:Steampunk:445276776676196353>")} <@&374900827632041986>\n" +
                $"{new Emoji("<:praise:445274481917952001>")} <@&440420047005941761>\n{new Emoji("<:monkaS:398183436613058570>")} <@&374900838096961546>\n{new Emoji("🐲")} <@&374900821382529025>\n{new Emoji("🐼")} <@&374980985281970206>\n" +
                $"{new Emoji("🦎")} <@&425736884870840322>\n{new Emoji("🌑")} <@&375197931793285130>\n{new Emoji("<:mumu:445277916872310785>")} <@&374981082707394569>\n{new Emoji("🐱")} <@&375376861594910720>\n");


            var socketMsg = await Context.Channel.SendMessageAsync("", embed: embed);


            await socketMsg.AddReactionAsync(rem);
            await socketMsg.AddReactionAsync(new Emoji("🦊"));
            await socketMsg.AddReactionAsync(peaceKeepo);
            await socketMsg.AddReactionAsync(steampunk);
            await socketMsg.AddReactionAsync(praise);
            await socketMsg.AddReactionAsync(monkaS);
            await socketMsg.AddReactionAsync(new Emoji("🐲"));
            await socketMsg.AddReactionAsync(new Emoji("🐼"));
            await socketMsg.AddReactionAsync(new Emoji("🦎"));
            await socketMsg.AddReactionAsync(new Emoji("🌑"));
            await socketMsg.AddReactionAsync(mumu);      
            await socketMsg.AddReactionAsync(new Emoji("🐱"));
            
        }


        [Command("бууль")]
        public async Task Boooole()
        {
            var roles = Context.Guild.Roles.ToArray();
            for (var i = 0; i < roles.Length; i++)
            {
                Console.WriteLine($"({i}){roles[i].Name} {roles[i].Id}");

            }
            await Task.CompletedTask;
        }

        [Command("бульк")]
        public async Task Boolek()
        {
           


            var rkn = Emote.Parse("<:RKN:445325930022436874>");
            var realy = Emote.Parse("<:realy:374655750657540106>");
            var feelsBadMan = Emote.Parse("<:FeelsBadMan:374655964843868162>");
            var woahMorfin = Emote.Parse("<:WoahMorfin:436787514813186050>");
            var thonk = Emote.Parse("<:thonk:445324435403309087>");
            var akaShrug = Emote.Parse("<:AkaShrug:374802737596071936>");
            var such = Emote.Parse("<:such:445322074781908993>");
            var kannNom = Emote.Parse("<:GWnanamiKannaNom:445321264169746434>");
            var pogChamp = Emote.Parse("<:PogChamp:374656108117098517>");
            var hanzo = Emote.Parse("<:hanzo:445324859690582018>");
            var mine = Emote.Parse("<:Minecrafticonfilegzpvzfll:445325226427940874>");
            var yasuo = Emote.Parse("<:yasuo:445323301137547264>");
            var gacHiPride = Emote.Parse("<:gacHIPride:394782921749430273>");
            var pekaohmy = Emote.Parse("<:pekaohmy:374656330742497280>");
            


            var embed = new EmbedBuilder();
            embed.WithAuthor(Global.Client.GetUser(326736083847086081));
            embed.WithColor(Color.Blue);
            embed.AddInlineField("Блокирующие каналы роли:", $"!блок - всю категорию **stream**, больше никаких бесполезных оповещений не будет {new Emoji("<:RKN:445325930022436874>")}\n" +
                                                             $"!антисрач - блокирует <#375600819011190787>, не хочешь видеть о чем срутся люди? Ну так и не надо {new Emoji("<:realy:374655750657540106>")}\n" +
                                                             $"!eventblock - блокирует категорию events, эх, а ведь ты мог получить халявную репку {new Emoji("<:FeelsBadMan:374655964843868162>")}\n" +
                                                             $"**______**");

            embed.AddInlineField ($"Добавляющие каналы роли:", 
                                                      $"!voice - <#421951522700787713> когда-нибудь тут проведут мафию, свояк, или еще что-нибудь {new Emoji("<:WoahMorfin:436787514813186050>")}\n" +
                                                      $"!riddler - <#421972081232838677> любишь загадки? Они тут есть, правда, ну честно, ну есть же... {new Emoji("<:thonk:445324435403309087>")}\n" +
                                                      $"!настолки - <#422674753552384001>, <#436706101027799061> <#436550169215893524> описание будет от Лемура, позже, ага {new Emoji("<:AkaShrug:374802737596071936>")}\n" +
                                                      $"!tech - <#374635063976919051>, <#421637061787779072> ты просто должен понимать, что там сидят шкальники и диванные аналитики, которые все знают лучше тебя {new Emoji("<:such:445322074781908993>")}\n" +
                                                      $"!anime - ну тут без коммента... SONO CHI NO SADAME! {new Emoji("<:GWnanamiKannaNom:445321264169746434>")}\n" +
                                                      $"!cards - ты тоже любишь поиграть ручками с... рандомом? <#438955526450315265> <#438955581965860864> {new Emoji("<:PogChamp:374656108117098517>")}\n" +
                                                         $"**______**\n");

            embed.AddInlineField($"Игровые Комнаты:", $"!hots - <#421637740137021450> и <#425354467022602258> думаю ты понимаешь, что Хандзо все-равно сильнейший персонаж? {new Emoji("<:hanzo:445324859690582018>")}\n" +
                                                      $"!minecraft - <#425654423642177536> это то место, где ты сможешь построить **САМУЮ КРАСИВУЮ КОРОБКУ**, лучше из грязи {new Emoji("<:Minecrafticonfilegzpvzfll:445325226427940874>")}\n"+
                                                      $"!lol - <#429345059486564352>, <#436522034231640064> пикай Ясуо вместе с Ривен и получай удовольствие {new Emoji("<:yasuo:445323301137547264>")}\n" +
                                                      $"!r6 - стань радужным воином в <#436938171692089344> {new Emoji("<:gacHIPride:394782921749430273>")}\n" +
                                                      $"**______**\n" +
                                                      $"Сделай сервер максимально удобным для себя! {new Emoji("<:pekaohmy:374656330742497280>")}");
            
           var mess = await Context.Channel.SendMessageAsync("", embed: embed);
            await mess.AddReactionAsync(rkn);
            await mess.AddReactionAsync(realy);
            await mess.AddReactionAsync(feelsBadMan);
            await mess.AddReactionAsync(woahMorfin);
            await mess.AddReactionAsync(thonk);
            await mess.AddReactionAsync(akaShrug);
            await mess.AddReactionAsync(such);
            await mess.AddReactionAsync(kannNom);
            await mess.AddReactionAsync(pogChamp);
            await mess.AddReactionAsync(hanzo);
            await mess.AddReactionAsync(mine);
            await mess.AddReactionAsync(yasuo);
            await mess.AddReactionAsync(gacHiPride);
            await mess.AddReactionAsync(pekaohmy);
        
            
        }


        [Command("Approves")]
        public async Task Approves()
        {
            if (Context.User.Id == 129658526460149760)
            {
                const string url = "https://i.imgur.com/wiEppGx.jpg";

                var color1 = new Random();
                var color2 = new Random();
                var color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);


                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);
                embed.WithAuthor("Фистурион Одобряет");
                embed.WithImageUrl("" + url);
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
        }

        [Command("Perfectus")]
        public async Task Perfectus()
        {
            if (Context.User.Id == 129658526460149760)
            {
                const string url = "https://i.imgur.com/mKxhNAY.jpg";
                var color1 = new Random();
                var color2 = new Random();
                var color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);

                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);

                embed.WithImageUrl("" + url);
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
        }


        [Command("Fist")]
        public async Task Fist()
        {
            if (Context.User.Id == 129658526460149760)
            {
                const string url = "https://i.imgur.com/uLfBWZ3.jpg";
                var color1 = new Random();
                var color2 = new Random();
                var color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);

                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);
                embed.WithAuthor("Oh yea");
                embed.WithImageUrl("" + url);
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
        }

        [Command("ETIAM")]
        public async Task Etiam()
        {
            const string url = "https://i.imgur.com/wt8EN8R.jpg";
            var color1 = new Random();
            var color2 = new Random();
            var color3 = new Random();
            var color1Index = color1.Next(256);
            var color2Index = color2.Next(256);
            var color3Index = color3.Next(256);

            var embed = new EmbedBuilder();
            embed.WithColor(color1Index, color2Index, color3Index);
            embed.WithAuthor("INCREDIBLIS");
            embed.WithImageUrl("" + url);
            await Context.Channel.SendMessageAsync("", embed: embed);

        }
    }

}

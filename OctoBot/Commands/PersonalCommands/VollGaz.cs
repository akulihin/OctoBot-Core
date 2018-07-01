using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Handeling;
using OctoBot.Services;

namespace OctoBot.Commands.PersonalCommands
{
    public class VollGaz : ModuleBase<SocketCommandContextCustom>
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
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                }

            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");
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
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                }
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");
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
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                }
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");
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
                if (Context.MessageContentForEdit != "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                }
                else if(Context.MessageContentForEdit == "edit")
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                }
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");
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
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }

        }

        [Command("Emotify")]
        [Alias("emoji", "emotion", "emo")]
        public async Task Emotify([Remainder] string args)
        {
            string[] convertorArray = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
            var pattern = new Regex("^[a-zA-Z]*$", RegexOptions.Compiled);
            args = args.ToLower();

            var convertedText = "";
            foreach (var c in args)
            {
                switch (c.ToString())
                {
                    case "\\":
                        convertedText += "\\";
                        break;
                    case "\n":
                        convertedText += "\n";
                        break;
                    default:
                        if (pattern.IsMatch(c.ToString()))
                        {
                            convertedText += $":regional_indicator_{c}:";
                        }
                        else if (char.IsDigit(c)) convertedText += $":{convertorArray[(int) char.GetNumericValue(c)]}:";
                        else convertedText += $"{c}";

                        break;
                }
            }

           // await ReplyAsync(convertedText);

            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, null, $"{convertedText}\nby `{Context.User.Username}`");
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
               
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, "edit", $"{convertedText}\nby `{Context.User.Username}`");
            }
        }

        //var mumu = Emote.Parse("<:mumu:445277916872310785>");
        //await socketMsg.AddReactionAsync(mumu); 
        [Command("EmoteSay")]
        [Alias("emojiM", "emotionM", "emoM")]
        public async Task EmoteSay(ulong chanelId, ulong messId, [Remainder] string args)
        {
            string[] numArray = {"0⃣", "1⃣", "2⃣", "3⃣", "4⃣", "5⃣", "6⃣", "7⃣", "8⃣", "9⃣"};
            string[] letterArray =
            {
                "🇦", "🇧", "🇨", "🇩", "🇪", "🇫", "🇬", "🇭", "🇮", "🇯", "🇰", "🇱", "🇲", "🇳", "🇴", "🇵", "🇶",
                "🇷", "🇸", "🇹", "🇺", "🇻", "🇼", "🇽", "🇾", "🇿"
            };

            var patternLett = new Regex("^[a-z]*$", RegexOptions.Compiled);
            var patternNum = new Regex("^[0-9]*$", RegexOptions.Compiled);
            args = args.ToLower();
            var charArray = args.ToCharArray();
            var socketMsg = Context.Guild.GetTextChannel(chanelId).GetCachedMessage(messId) as SocketUserMessage;

            for (var i = 0; i < charArray.Length; i++)
            {
                if (patternLett.IsMatch(charArray[i].ToString()))
                {
                    var letter = (Convert.ToInt32(charArray[i]) % 32) - 1;
                    var emo = new Emoji($"{letterArray[letter]}");
                    if (socketMsg != null) await socketMsg.AddReactionAsync(emo);

                }
                else if (patternNum.IsMatch(charArray[i].ToString()))
                {
                    var emo = new Emoji($"{numArray[(int) char.GetNumericValue(charArray[i])]}");
                    if (socketMsg != null) await socketMsg.AddReactionAsync(emo);
                }
            }
        }



        
        [Command("roll")]
        public async Task CalculateStuf([Remainder]string yyyy)
        {
            var low = yyyy.ToLower();
            low = low.Replace(" ", string.Empty);

            var embed = new EmbedBuilder();
            embed.WithColor(Color.Green);
            embed.WithFooter("Записная книжечка осьминожек");
            var results = "steps:\n";
            var answer = 0;
            var numberString = "";
            var numberString2 = "";
            var sign = 'k';
            var check = 0;
            var doi = 0;
            var count = 0;
            var boole = 0;
            var isSuccess = 0 ;
            
            for (var i = 0; i < low.Length; i++)
            {
                isSuccess++;
                if (isSuccess >= 200)
                {
                   await ReplyAsync("буль!!");
                    return;
                }

                string reminder;
                if (low[0] == '-')
                {
                    Console.WriteLine($"hello== {low}");
                    //-79+3d1+7d1+99
                    reminder = "";
                    var kek = "";

                    for (var m = 1; m < low.Length; m++)
                    {
                        if (boole == 0)
                        {
                            
                            if (!int.TryParse($"{low[m]}", out _))
                            {
                                boole = 1;
                                m++;
                            }
                            else
                            {
                                kek += $"{low[m]}";
                            }
                        }
                        if (boole == 1)
                        {

                            reminder += $"{low[m]}";
                        }
                    }

                    low = reminder + $"-{kek}";
                       // Console.WriteLine($"bye== {low}");
                    i = -1;
                     answer = 0;
                     numberString = "";
                     numberString2 = "";
                     sign = 'k';
                     check = 0;
                     doi = 0;
                     count = 0;
                     boole = 0;
                }
                else 
                {
                if(int.TryParse($"{low[i]}", out _) || i == low.Length - 1 || i == low.Length || low[0] == '-')
                {
                    switch (check)
                    {
                        case 0:
                            if(low[0] == '-')
                                numberString += "-";
                            numberString += low[i];                
                            break;
                        case 1:
                            numberString2 += low[i];
                            break;
                    }

                    if (check != 2 && i != low.Length - 1) continue;
                    
                    if (doi < 2)
                    {
                        Int32.TryParse(numberString, out var num1);
                        Int32.TryParse(numberString2, out var num2);

                        var result = Calculator(num1, num2, sign);
                         answer = result.Item1;

                        if (i == low.Length - 1 && check == 1)
                        {
                            if (result.Item2.Length > 0)
                            {
                                embed.AddField($"It's a {answer}!", $"{results} **{result.Item2}**");
                            }
                            else
                            {
                                embed.AddField($"It's a {answer}!", $"{results}");
                            }

                            if (Context.MessageContentForEdit != "edit")
                            {
                                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
                            }
                            else if(Context.MessageContentForEdit == "edit")
                            {
                                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
                            }
                            return;
                        }


                            reminder = $"{answer}{low[i - 1]}";

                            for (var k = i; k < low.Length; k++)
                            {
                                reminder += $"{low[k]}";

                            }

                            low = reminder;

                            count++;
                        if (result.Item2.Length > 0)
                        {
                            results += $"{count})**|({result.Item2})|** {reminder} \n";
                        }
                        else
                        {
                            results += $"{count}) {reminder} \n";
                        }


                        i = -1;
                        numberString = "";
                        numberString2 = "";
                        check = 0;
                        sign = 'k';
                    }
                    else
                    {
                        reminder = "";

                       
                        for (var k = (answer.ToString().Length + 1) ; k < low.Length; k++)
                            reminder += $"{low[k]}";

                        reminder += $"{low[answer.ToString().Length]}{answer}";                    
                        
                        low = reminder;
                       
                        i = -1;
                        numberString = "";
                        numberString2 = "";
                        check = 0;
                        sign = 'k';
                        doi = 0;
                    }
                }
                else if(low[i] == 'd')
                {
                    check++;
                    doi++;
                    if(check <= 1)
                    sign = 'd';
                    
                }
                else if(low[i] == '+')
                {
                    check++;
                    if(check <= 1)
                    sign = '+';
                    
                } 
                else if(low[i] == '-')
                {
                    check++;
                    if(check <= 1)
                    sign = '-';
                   
                }
                else if(low[i] == '*')
                {
                    check++;
                    if(check <= 1)
                    sign = '*';
                   
                }
                else if(low[i] == '/')
                {
                    check++;
                    if (check <= 1)
                        sign = '/';
                }
                else
                    check++;
            } 
            }
           
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, null, $"It's a {answer}!");
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, null, "edit", $"It's a {answer}!");
            }
        }
       

        public (int, string ) Calculator(int times, int num, char sign)
        {
            switch (sign)
            {    

                case 'm':
                    break;
                case 'l':
                    break;
                case '+':
                    return (times + num, "");
                case '-':
                    return (times - num, "");
                case '*':
                    return (times * num, "");
                case '/':
                    return (times / num, "");
                case 'd':
                    var result = new List<int>();
                   // var random = new Random();
                    var answer = 0;
                   
                    if (times <= 0 || num <= 0)
                        return (0,"error");
                    if (times == 1)
                        return (SecureRandom.Random(1, num), "");

                    var resultString = "";
                    if (times <= 1000 && num <= 120)
                    {
                        for (var i = 0; i < times; i++)
                        {
                            var lol = SecureRandom.Random(1, num);
                            result.Add(lol);
                           
                        }

                        for (var i = 0; i < result.Count; i++)
                        {
                           
                            answer += result[i];
                            resultString += $"{result[i]}";
                            if(i < result.Count - 1)
                            resultString += $" + ";
                        }
                    }
                    
                    return (answer, resultString);
            }
            return (0,"error");
        }



    }
}




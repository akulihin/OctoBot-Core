using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Handeling;
using OctoBot.Services;

namespace OctoBot.Commands.Translate
{

    public class Translate : ModuleBase<SocketCommandContextCustom>
    {
        [Command("d")]
        public async Task DetecTask([Remainder] string query)
        {
           
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandeling.SendingMess(Context, null, null, $"{TranslatorApi.DetectLanguageName(query)}");
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandeling.SendingMess(Context, null, "edit", $"{TranslatorApi.DetectLanguageName(query)}");
            }
        }

        [Command("translate")]
        [Alias("t")]
        [Summary("Use `translate to-language` or `translate from-to`")]
        public async Task TrandlateTask(string toLang, [Remainder] string query)
        {
            var dataStrings = TranslatorApi.Translate(toLang, query);
            var embed = new EmbedBuilder();
            var rand = new Random();
            if (dataStrings.Length == 1)
            {
                embed.WithDescription(dataStrings[0]);
            }
            else
            {
                embed.WithDescription(dataStrings[2]);
                embed.WithTitle($"Translated from {dataStrings[0]} to {dataStrings[1]}");
            }

            embed.WithColor(new Color(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256)));
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandeling.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandeling.SendingMess(Context, embed, "edit");
            }
        }
    }

}
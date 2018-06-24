﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace OctoBot.Commands.Translate
{

    public class Translate : ModuleBase<SocketCommandContext>
    {
        [Command("d")]
        public async Task DetecTask([Remainder] string query)
        {
            await ReplyAsync(TranslatorApi.DetectLanguageName(query));
        }

        [Command("translate")]
        [Alias("t")]
        [Summary("Use `translate to-language` or `translate from-to`")]
        public async Task TrandlateTask(string toLang, [Remainder] string query)
        {
            string[] dataStrings = TranslatorApi.Translate(toLang, query);
            var embed = new EmbedBuilder();
            Random rand = new Random();
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
            await ReplyAsync("", false, embed.Build());
        }
    }

}
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using OctoBot.Helper;



namespace OctoBot.Automated
{
    public class TimerForChangingAvatar
    {
        private static Timer _loopingTimerForOctoAva;

        internal static Task TimerForChangeBotAvatar()
        {

            _loopingTimerForOctoAva = new Timer
            {
                AutoReset = true,
                Interval = 3600000,
                Enabled = true
            };
            _loopingTimerForOctoAva.Elapsed += SetBotAva;

            return Task.CompletedTask;
        }

        public static async void SetBotAva(object sender, ElapsedEventArgs e)
        {
            var rand = new Random();
            var randomIndex = rand.Next(OctoPicPull.OctoPics.Length);
            var octoToPost = OctoPicPull.OctoPics[randomIndex];

            var webClient = new WebClient();
            var imageBytes = webClient.DownloadData(octoToPost);

            var stream = new MemoryStream(imageBytes);

            var image = new Image(stream);
            await Global.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);
        }
    }
}

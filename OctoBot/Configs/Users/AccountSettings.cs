using System;
using System.Collections.Generic;

namespace OctoBot.Configs.Users
{



    public class AccountSettings
    {

        public string UserName { get; set; }

        public ulong Id { get; set; }
        public string ExtraUserName { get; set; }

        public long Rep { get; set; }
        public string Warnings { get; set; }


        public long Points { get; set; }
        public uint Lvl { get; set; }
        public uint LvlPoinnts { get; set; }

        public string Fuckt { get; set; }
        public int OctoPass { get; set; }

        public string Octopuses { get; set; }
        public uint Pinki { get; set; } //розовый айсика
        public uint Cooki { get; set; } // Куки!

        public uint Raqinbow { get; set; }

        public int YellowTries { get; set; }

        public int Lost { get; set; }

        public List<CreateReminder> ReminderList { get; internal set; } = new List<CreateReminder>();

        ///////////DailuPull////////////////
        public DateTime LastDailyPull { get; set; } = DateTime.UtcNow.AddDays(-2);
        public int DailyPullPoints { get; set; }

        public string KeyPull { get; set; }

        ///////////Subscriptions////////////////
        public string SubToPeople { get; set; }

        public string SubedToYou { get; set; }
        
        public struct CreateReminder
        {
            public DateTime DateToPost;
            public string ReminderMessage;

            public CreateReminder(DateTime dateToPost, string reminderMessage)
            {
                DateToPost = dateToPost;
                ReminderMessage = reminderMessage;
            }
        }

        public int Best2048Score { get; set; }


    }
}

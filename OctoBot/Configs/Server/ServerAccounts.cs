using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.WebSocket;

namespace OctoBot.Configs.Server
{
    public static class ServerAccounts
    {

        private static readonly List<ServerSettings> _accounts;

        private static string _serverAccountsFile = @"OctoDataBase/ServerAccounts.json";

        static ServerAccounts()
        {
            if (ServerDataStorage.SaveExists(_serverAccountsFile))
                _accounts = ServerDataStorage.LoadServerSettings(_serverAccountsFile).ToList();
            else
            {
                _accounts = new List<ServerSettings>();
                SaveServerAccounts();
            }
        }

        public static void SaveServerAccounts()
        {
            ServerDataStorage.SaveServerSettings(_accounts, _serverAccountsFile);
        }

        public static ServerSettings GetServerAccount(SocketGuild guild)
        {
            return GetOrCreateServerAccount(guild.Id, guild.Name);
        }

        public static ServerSettings GetServerAccount(IGuildChannel guild)
        {
            return GetOrCreateServerAccount(guild.Guild.Id, guild.Guild.Name);
        }

        private static ServerSettings GetOrCreateServerAccount(ulong id, string name)
        {
            var result = from a in _accounts
                         where a.ServerId == id
                         select a;
            var account = result.FirstOrDefault() ?? CreateServerAccount(id, name);

            return account;
        }

       


        internal static List<ServerSettings> GetAllServerAccounts()
        {
            return _accounts.ToList();
        }

            internal static List<ServerSettings> GetFilteredServerAccounts(Func<ServerSettings, bool> filter)
            {
               
                return _accounts.Where(filter).ToList();
            }


        private static ServerSettings CreateServerAccount(ulong id, string name)
        {
            var newAccount = new ServerSettings
            {
                ServerName = name,
                ServerId = id,
                Prefix = "*",
                ServerActivityLog = 0,
                Language = "en"
            };

            _accounts.Add(newAccount);
            SaveServerAccounts();
            return newAccount;
        }

    }
}

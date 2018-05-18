using System;
using System.Collections.Generic;
using System.Linq;
using Discord;

namespace OctoBot.Configs.Users
{
    public static class UserAccounts
    {

        private static List<AccountSettings> _accounts;

        private static string _accountsFile = @"OctoDataBase/accounts.json";

        static UserAccounts()
        {
            if (DataStorage.SaveExists(_accountsFile))
                _accounts = DataStorage.LoadAccountSettings(_accountsFile).ToList();
            else
            {
                _accounts = new List<AccountSettings>();
                SaveAccounts();
            }
        }

        public static void SaveAccounts()
        {
            DataStorage.SaveAccountSettings(_accounts, _accountsFile);

        }

        public static AccountSettings GetAccount(IUser user)
        {
            return GetOrCreateAccount(user.Id);
        }

        private static AccountSettings GetOrCreateAccount(ulong id)
        {
            var result = from a in _accounts
                         where a.Id == id
                         select a;


            var account = result.FirstOrDefault();
            if (account == null)
                account = CreateUserAccount(id);


            return account;
        }

       


        internal static List<AccountSettings> GetAllAccounts()
        {
            return _accounts.ToList();
        }

            internal static List<AccountSettings> GetFilteredAccounts(Func<AccountSettings, bool> filter)
            {
               
                return _accounts.Where(filter).ToList();
            }


        private static AccountSettings CreateUserAccount(ulong id)
        {
            var newAccount = new AccountSettings()
            {
                // Username = "буль"б
                Id = id,
                Rep = 0,
                Points = 0,
            };

            _accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;

        }

    }
}

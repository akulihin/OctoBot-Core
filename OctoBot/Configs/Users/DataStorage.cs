using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OctoBot.Configs.Users
{
    public static class DataStorage
    {

        //Save all AccountSettings

        public static void SaveAccountSettings(IEnumerable<AccountSettings> accounts, string filePath)
        {
            try{
            var json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(filePath, json);
            } catch {
                Console.WriteLine("Failed To ReadFile(SaveAccountSettings). Will ty in 5 sec.");
                return;
                
            }
        }

        //Get AccountSettings

        public static IEnumerable<AccountSettings> LoadAccountSettings(string filePath)
        {

            if (!File.Exists(filePath)) return null;
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<AccountSettings>>(json);

        }

        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }

    }
}

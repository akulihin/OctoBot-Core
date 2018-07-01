using System;
using System.Security.Cryptography;

namespace OctoBot.Services
{
    
    public class SecureRandom
    {
        
        private static readonly RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();

        public static int Random(int min, int max)
        {
            var randomNumber = new byte[1];

            generator.GetBytes(randomNumber);

            var asc2ConvertBytes = Convert.ToDouble(randomNumber[0]);

            var multy = Math.Max(0, (asc2ConvertBytes) / 255d);
            var range = max - min + 1;
            var randomInRange = Math.Floor(multy * range);

            return (int)(min + randomInRange);
        }

      
    }
 
}


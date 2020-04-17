using System.Security.Cryptography;

namespace RogueSheep.RandomNumbers
{
    internal static class Seeder
    {
        internal static ulong GetSeed()
        {
            var cryptoRng = new RNGCryptoServiceProvider();
            var seedArray = new byte[8];
            cryptoRng.GetBytes(seedArray);
            ulong seed = 0;
            for (var i = 0; i < 8; i++)
            {
                seed |= (ulong)seedArray[i] << (8 * (8 - i));
            }

            return seed;
        }
    }
}

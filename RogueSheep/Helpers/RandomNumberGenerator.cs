using System.Collections.Generic;
using System.Security.Cryptography;

namespace RogueSheep.Helpers
{
    // TODO: wrapper around most used methods
    public class RandomNumberGenerator
    {
        private readonly ulong seed;
        private readonly Dictionary<uint, PCGRandomMinimal> generators;
        public PCGRandomMinimal this[uint key]
        {
            get
            {
                if (!generators.ContainsKey(key))
                {
                    generators[key] = new PCGRandomMinimal(seed, key << 1);
                }
                return generators[key];
            }
        }

        public RandomNumberGenerator()
        {
            var cryptoRng = new RNGCryptoServiceProvider();
            var seedArray = new byte[8];
            cryptoRng.GetBytes(seedArray);
            ulong seed = 0;
            for (var i = 0; i < 8; i++)
            {
                seed |= (ulong)seedArray[i] << (8 * (8 - i));
            }
            this.seed = seed;
            generators = new Dictionary<uint, PCGRandomMinimal>();
        }

        public RandomNumberGenerator(ulong seed)
        {
            this.seed = seed;
            generators = new Dictionary<uint, PCGRandomMinimal>();
        }
    }
}

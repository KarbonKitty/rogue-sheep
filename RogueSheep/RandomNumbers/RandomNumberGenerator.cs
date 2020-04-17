using System.Collections.Generic;

namespace RogueSheep.RandomNumbers
{
    // TODO: wrapper around most used methods?
    public class MultistreamPCGRandom
    {
        private readonly ulong seed;
        private readonly Dictionary<uint, IRandom> generators;

        public IRandom this[uint key]
        {
            get
            {
                if (!generators.ContainsKey(key))
                {
                    generators[key] = new PCGRandom(seed, key << 1);
                }
                return generators[key];
            }
        }

        public MultistreamPCGRandom()
        {
            seed = Seeder.GetSeed();
            generators = new Dictionary<uint, IRandom>();
        }

        public MultistreamPCGRandom(ulong seed)
        {
            this.seed = seed;
            generators = new Dictionary<uint, IRandom>();
        }
    }
}

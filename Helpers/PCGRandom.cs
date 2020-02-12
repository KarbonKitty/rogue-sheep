using System;

namespace RogueSheep.Helpers
{
    /*
    * PCG Random Number Generation for C#
    * 
    * Original algorithm for C/C++ (c) by Melissa O'Neill <oneill@pcg-random.org>
    * and PCG Project contributors.
    * 
    * Original licensed under Apache 2.0 or MIT license.
    * 
    * For additional information about the PCG random number generation scheme,
    * original source code and binary builds, as well as original license details
    * visit http://www.pcg-random.org/.
    * 
    */
    public class PCGRandom
    {
        private PCGRandomMinimal innerPCG;

        public PCGRandom(ulong initState, ulong initStream)
        {
            innerPCG = new PCGRandomMinimal(initState, initStream);
        }

        public uint Next() => innerPCG.Next();

        public uint Next(uint maxValue)
        {
            if (maxValue == 0)
            {
                return 0;
            }

            uint threshold = (uint.MaxValue - maxValue) % maxValue;
            while (true)
            {
                uint next = Next();
                if (next > threshold)
                {
                    return next % maxValue;
                }
            }
        }

        public uint Next(uint minValue, uint maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }

            if (minValue == maxValue)
            {
                return minValue;
            }

            return Next(maxValue - minValue) + minValue;
        }

        public int NextInt() => (int)innerPCG.Next();

        public int NextInt(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }
            
            return (int)Next((uint)maxValue);
        }

        public int NextInt(int minValue, int maxValue)
        {
            if (minValue < 0 || maxValue < 0)
            {
                throw new ArgumentOutOfRangeException(minValue < 0 ? nameof(minValue) : nameof(maxValue));
            }

            return (int)Next((uint)minValue, (uint)maxValue);
        }
    }
}

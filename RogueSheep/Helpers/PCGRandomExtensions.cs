using System;

namespace RogueSheep.Helpers
{
    public static class PCGRandomExtensions
    {
        public static int Next(this PCGRandomMinimal pcg) => (int)((pcg.Step() << 1) >> 1);

        public static int Next(this PCGRandomMinimal pcg, int upperLimit)
        {
            if (upperLimit < 0)
            {
                throw new ArgumentException("Limit must be at least 0.", nameof(upperLimit));
            }

            if (upperLimit == 0)
            {
                return 0;
            }

            uint threshold = (uint.MaxValue - (uint)upperLimit) % (uint)upperLimit;
            while (true)
            {
                uint next = pcg.Step();
                if (next > threshold)
                {
                    return (int)(next % (uint)upperLimit);
                }
            }
        }

        public static int Next(this PCGRandomMinimal pcg, int lowerLimit, int upperLimit)
        {
            if (lowerLimit > upperLimit)
            {
                throw new ArgumentException("Lower limit must be lower than upper limit", nameof(lowerLimit));
            }

            if (lowerLimit == upperLimit)
            {
                return lowerLimit;
            }

            return pcg.Next(upperLimit - lowerLimit) + lowerLimit;
        }

        public static double NextDouble(this PCGRandomMinimal pcg)
        {
            int exponent = -32;
            ulong significand;
            int shift;

            while ((significand = pcg.Step()) == 0)
            {
                exponent -= 32;
            }

            shift = CountLeadingZeroes(significand);
            if (shift != 0)
            {
                exponent -= shift;
                significand <<= shift;
                significand |= pcg.Step() >> (32 - shift);
            }

            significand |= 1;

            return significand * Math.Pow(2, exponent);
        }

        private static int CountLeadingZeroes(ulong x)
        {
            if (x == 0)
            {
                return 64;
            }
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            x |= (x >> 32);
            x++;
            for (int i = 0; x != 0; i++)
            {
                if ((x & 1) == 1)
                {
                    return i;
                }
                x >>= 1;
            }
            return 0;
        }
    }
}

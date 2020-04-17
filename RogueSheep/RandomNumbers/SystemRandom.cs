using System;

namespace RogueSheep.RandomNumbers
{
    public class SystemRandom : IRandom
    {
        private readonly Random inner;

        public SystemRandom()
        {
            inner = new Random();
        }

        public SystemRandom(int seed)
        {
            inner = new Random(seed);
        }

        public int Next() => inner.Next();

        public int Next(int maxValue) => inner.Next(maxValue);

        public int Next(int minValue, int maxValue) => inner.Next(minValue, maxValue);

        public double NextDouble() => inner.NextDouble();

        public void NextBytes(byte[] buffer) => inner.NextBytes(buffer);

        public void NextBytes(Span<byte> buffer) => inner.NextBytes(buffer);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueSheep.Helpers
{
    public static class RandomExtensions
    {
        public static T SelectRandom<T>(this IEnumerable<T> enumerable, PCGRandomMinimal rng)
        {
            if (!(enumerable?.Any() ?? false))
            {
                throw new ArgumentException(nameof(enumerable), "Collection must contain at least one element to select randomly from it.");
            }

            if (enumerable is ICollection<T> c)
            {
                return c.ElementAt(rng.Next(c.Count));
            }

            var count = 0;
            T selected = default;
            foreach (var element in enumerable)
            {
                count++;
                if (rng.Next(count) == 0)
                {
                    selected = element;
                }
            }

            // first element has 100% chance to be selected, but compiler doesn't know it
            return selected!;
        }
    }
}

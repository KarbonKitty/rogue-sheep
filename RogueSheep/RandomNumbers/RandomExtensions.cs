using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueSheep.RandomNumbers
{
    public static class RandomExtensions
    {
        public static T SelectRandomElement<T>(this IRandom rng, IEnumerable<T> enumerable)
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

        public static T SelectRandomElementWithWeight<T>(this IRandom rng, int count, Func<int, T> itemFunc, Func<int, int> weightFunc)
        {
            var totalWeight = 0;
            for (var i = 0; i < count; i++)
            {
                totalWeight += weightFunc(i);
            }

            var randomSelection = rng.Next(totalWeight);
            var runningTotal = 0;
            for (var i = 0; i < count; i++)
            {
                runningTotal += weightFunc(i);
                if (runningTotal >= randomSelection)
                {
                    return itemFunc(i);
                }
            }

            throw new Exception("Impossible!");
        }

        public static T SelectRandomElementWithWeight<T>(this IRandom rng, ICollection<T> collection, ICollection<int> weights)
        {
            if (collection.Count != weights.Count)
            {
                throw new ArgumentException(nameof(weights), "Collection of weights must be the same length as the collection to select from.");
            }

            var totalWeight = weights.Sum();
            var randomSelection = rng.Next(totalWeight);

            var runningTotal = 0;
            for (var i = 0; i < collection.Count; i++)
            {
                runningTotal += weights.ElementAt(i);
                if (runningTotal >= randomSelection)
                {
                    return collection.ElementAt(i);
                }
            }

            throw new InvalidOperationException("Impossible!");
        }

        public static T SelectRandomElementWithWeight<T>(this IRandom rng, IDictionary<T, int> dictionary)
        {
            var totalWeight = dictionary.Sum(kvp => kvp.Value);

            var randomSelection = rng.Next(totalWeight);
            var runningTotal = 0;
            foreach (var kvp in dictionary)
            {
                runningTotal += kvp.Value;
                if (runningTotal >= randomSelection)
                {
                    return kvp.Key;
                }
            }

            throw new Exception("Impossible!");
        }

        public static T SelectRandomElementWithWeight<T>(this IRandom rng, ICollection<(int weight, T item)> tupleCollection)
        {
            var totalWeight = tupleCollection.Sum(t => t.weight);

            var randomSelection = rng.Next(totalWeight);
            var runningTotal = 0;
            foreach (var (weight, item) in tupleCollection)
            {
                runningTotal += weight;
                if (runningTotal >= randomSelection)
                {
                    return item;
                }
            }

            throw new Exception("Impossible!");
        }
    }
}

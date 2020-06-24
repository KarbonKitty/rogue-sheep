using System;
using System.Collections.Generic;

namespace RogueSheep.Schedulers
{
    public class RoundRobinScheduler<T> : IScheduler<T>
    {
        private readonly List<T> list;
        private int lastIndex = -1;

        public RoundRobinScheduler()
        {
            // TODO: think about the default size here
            list = new List<T>(32);
        }

        public T Current()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Current can only be called on non-empty scheduler");
            }

            if (lastIndex == -1)
            {
                lastIndex = 0;
            }

            return list[lastIndex];
        }

        public T Next()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Next can only be called on non-empty scheduler");
            }

            lastIndex++;
            if (lastIndex >= list.Count)
            {
                lastIndex = 0;
            }
            return list[lastIndex];
        }

        public void Add(T item) => list.Add(item);

        public void Add(params T[] items) => list.AddRange(items);

        public void AddRange(IEnumerable<T> items) => list.AddRange(items);

        public bool Remove(T item)
        {
            var index = list.IndexOf(item);
            if (index < lastIndex)
            {
                lastIndex--;
            }
            return list.Remove(item);
        }

        public void Clear() => list.Clear();
    }
}

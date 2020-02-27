using System.Collections;
using System.Collections.Generic;

namespace RogueSheep
{
    public class GameGrid<T> : IEnumerable<T>
    {
        public Point2i Size { get; }
        public int Length => Array.Length;
        public T[] Array { get; }

        public T this[int i]
        {
            get => Array[i];
            set => Array[i] = value;
        }

        public T this[int x, int y] => Array[(y * Size.X) + x];
        public T this[(int x, int y) t] => Array[(t.y * Size.X) + t.x];

        public GameGrid(Point2i size)
        {
            Array = new T[size.X * size.Y];
            Size = size;
        }

        public IEnumerable<T> GetNeighborhood(int index, T defaultValue = default, bool inclusive = false)
        {
            var position = (index % Size.X, index / Size.Y);
            return GetNeighborhood(position, defaultValue, inclusive);
        }

        public IEnumerable<T> GetNeighborhood((int x, int y) position, T defaultValue = default, bool inclusive = false)
        {
            var (x, y) = position;
            if (inclusive)
            {
                yield return TryGetValue((x, y), defaultValue);
            }
            yield return TryGetValue((x - 1, y), defaultValue);
            yield return TryGetValue((x + 1, y), defaultValue);
            yield return TryGetValue((x, y - 1), defaultValue);
            yield return TryGetValue((x, y + 1), defaultValue);
            yield return TryGetValue((x + 1, y + 1), defaultValue);
            yield return TryGetValue((x + 1, y - 1), defaultValue);
            yield return TryGetValue((x - 1, y + 1), defaultValue);
            yield return TryGetValue((x - 1, y - 1), defaultValue);
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Array).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Array.GetEnumerator();

        // private T TryGetValue(int index, T defaultValue = default)
        //     => index < Length ? this[index] : defaultValue;

        private T TryGetValue((int x, int y) index, T defaultValue = default)
        {
            if (index.x < 0 || index.y < 0)
            {
                return defaultValue;
            }

            var i = (index.y * Size.X) + index.x;
            return (i < Length && i >= 0) ? this[index] : defaultValue;
        }
    }
}

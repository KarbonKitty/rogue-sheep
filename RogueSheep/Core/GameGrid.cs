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

        public T this[int x, int y]
        {
            get => this[(x, y)];
            set => this[(x, y)] = value;
        }

        public T this[(int x, int y) t]
        {
            get => Array[(t.y * Size.X) + t.x];
            set => Array[(t.y * Size.X) + t.x] = value;
        }

        public T this[Point2i p]
        {
            get => this[(p.X, p.Y)];
            set => this[(p.X, p.Y)] = value;
        }

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

        public IEnumerable<T> GetNeighborhood(Point2i position, T defaultValue = default, bool inclusive = false)
            => GetNeighborhood((position.X, position.Y), defaultValue, inclusive);

        public IEnumerable<T> GetNeighborhood((int x, int y) position, T defaultValue = default, bool inclusive = false)
        {
            var (x, y) = position;
            if (inclusive)
            {
                yield return GetValueOrDefault((x, y), defaultValue);
            }
            yield return GetValueOrDefault((x - 1, y), defaultValue);
            yield return GetValueOrDefault((x + 1, y), defaultValue);
            yield return GetValueOrDefault((x, y - 1), defaultValue);
            yield return GetValueOrDefault((x, y + 1), defaultValue);
            yield return GetValueOrDefault((x + 1, y + 1), defaultValue);
            yield return GetValueOrDefault((x + 1, y - 1), defaultValue);
            yield return GetValueOrDefault((x - 1, y + 1), defaultValue);
            yield return GetValueOrDefault((x - 1, y - 1), defaultValue);
        }

        public Point2i IndexToPosition(int index) => (index % Size.X, index / Size.X);

        public bool IsInBounds(int index) => index >= 0 && index < Length;
        public bool IsInBounds(int x, int y) => x >= 0 && y >= 0 && x < Size.X && y < Size.Y;
        public bool IsInBounds((int x, int y) t) => IsInBounds(t.x, t.y);
        public bool IsInBounds(Point2i p) => IsInBounds(p.X, p.Y);

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Array).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Array.GetEnumerator();

        private T GetValueOrDefault((int x, int y) index, T defaultValue = default)
        {
            if (index.x < 0 || index.y < 0 || index.x >= Size.X || index.y >= Size.Y)
            {
                return defaultValue;
            }

            return this[(index.y * Size.X) + index.x];
        }
    }
}

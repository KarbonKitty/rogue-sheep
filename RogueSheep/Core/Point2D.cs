using System.Collections.Generic;
using SFML.System;

namespace RogueSheep
{
    public struct Point2i
    {
        public int X { get; }
        public int Y { get; }

        private static readonly Dictionary<Direction, (int x, int y)> transformations = new Dictionary<Direction, (int x, int y)> {
            { Direction.None, (0, 0) },
            { Direction.North, (0, -1) },
            { Direction.South, (0, 1) },
            { Direction.West, (-1, 0) },
            { Direction.East, (1, 0) },
            { Direction.NorthEast, (1, -1) },
            { Direction.NorthWest, (-1, -1) },
            { Direction.SouthEast, (1, 1) },
            { Direction.SouthWest, (-1, 1) }
        };

        public Point2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point2i(Point2i original)
        {
            X = original.X;
            Y = original.Y;
        }

        public Point2i Transform(Direction dir) => new Point2i(this.X + transformations[dir].x, this.Y + transformations[dir].y);

        public static implicit operator Point2i((int x, int y) t) => new Point2i(t.x, t.y);
        public static implicit operator Point2i(Vector2i vector) => new Point2i(vector.X, vector.Y);
        public static implicit operator Vector2i(Point2i position) => new Vector2i(position.X, position.Y);
        public static bool operator ==(Point2i a, Point2i b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Point2i a, Point2i b) => !(a == b);

        public static Point2i operator +(Point2i a, Point2i b) => new Point2i(a.X + b.X, a.Y + b.Y);
        public static Point2i operator -(Point2i a, Point2i b) => new Point2i(a.X - b.X, a.Y - b.Y);

        public override bool Equals(object? obj) => obj is Point2i position && Equals(position);

        public bool Equals(Point2i gamePosition) => gamePosition.X == X && gamePosition.Y == Y;

        public override int GetHashCode() => (((17 * 23) + X) * 23) + Y;
    }

    public struct Point2f
    {
        public float X { get; }
        public float Y { get; }

        private static readonly Dictionary<Direction, (float x, float y)> transformations = new Dictionary<Direction, (float x, float y)> {
            { Direction.None, (0, 0) },
            { Direction.North, (0, -1) },
            { Direction.South, (0, 1) },
            { Direction.West, (-1, 0) },
            { Direction.East, (1, 0) },
            { Direction.NorthEast, (1, -1) },
            { Direction.NorthWest, (-1, -1) },
            { Direction.SouthEast, (1, 1) },
            { Direction.SouthWest, (-1, 1) }
        };

        public Point2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Point2f(Point2f original)
        {
            X = original.X;
            Y = original.Y;
        }

        public Point2f Transform(Direction dir)
        {
            return new Point2f(this.X + transformations[dir].x, this.Y + transformations[dir].y);
        }

        public static implicit operator Point2f((int x, int y) t) => new Point2f(t.x, t.y);
        public static implicit operator Point2f(Vector2i vector) => new Point2f(vector.X, vector.Y);
        public static implicit operator Vector2f(Point2f position) => new Vector2f(position.X, position.Y);
        public static bool operator ==(Point2f a, Point2f b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Point2f a, Point2f b) => !(a == b);

        public static Point2f operator +(Point2f a, Point2f b) => new Point2f(a.X + b.X, a.Y + b.Y);
        public static Point2f operator -(Point2f a, Point2f b) => new Point2f(a.X - b.X, a.Y - b.Y);

        public override bool Equals(object? obj) => obj is Point2f position && Equals(position);

        public bool Equals(Point2f gamePosition) => gamePosition.X == X && gamePosition.Y == Y;

        public override int GetHashCode() => (int)((((17 * 23) + X) * 23) + Y);
    }
}

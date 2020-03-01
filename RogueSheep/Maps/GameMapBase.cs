using System.Collections.Generic;
using System.Linq;
using RogueSheep.Display;

namespace RogueSheep.Maps
{
    public abstract class GameMapBase<T> : IGameMap<T> where T : IHasPosition, IPresentable
    {
        public IList<T> Actors { get; }

        public Point2i Size { get; }
        protected virtual IMapTile[] Tiles { get; }

        protected GameMapBase(IMapTile[] tiles, int width, IEnumerable<T> actors)
        {
            Tiles = tiles;
            Size = (width, tiles.Length / width);
            Actors = new List<T>().Concat(actors).ToList();
        }

        public Point2i ClosestFreePosition(Point2i origin)
        {
            if (IsAvailableForMove(origin))
            {
                return origin;
            }
            var currentPoint = origin;
            var ring = 0;
            while (true)
            {
                ring++;
                var steps = ring * 2;
                currentPoint = currentPoint.Transform(Direction.NorthWest);
                foreach (var dir in new[] { Direction.East, Direction.South, Direction.West, Direction.North })
                {
                    for (var i = 0; i < steps; i++)
                    {
                        if (IsAvailableForMove(currentPoint))
                        {
                            return currentPoint;
                        }
                        currentPoint = currentPoint.Transform(dir);
                    }
                }
            }
        }

        public IPresentable[] GetMap() => Tiles;

        public IPresentable[] GetViewport(Point2i size, Point2i center)
        {
            return GetViewportImpl(size, FindOffsetForViewport(size, center));
        }

        public bool IsAvailableForMove(Point2i position) => IsInBounds(position) && IsPassable(position) && !Actors.Any(b => b.Position == position);

        public bool IsTransparent(Point2i position) => Tiles[PositionToIndex(position)].Transparent;

        public Point2i PositionOf(T actor) => Actors.Single(a => a.Equals(actor)).Position;

        protected virtual bool IsPassable(Point2i position) => Tiles[PositionToIndex(position)].Passable;

        private Point2i FindOffsetForViewport(Point2i size, Point2i center)
        {
            var attemptedLeft = center.X - (size.X / 2);
            var realLeft = attemptedLeft < 0 ? 0 : attemptedLeft;

            var attemptedRight = realLeft + size.X;
            realLeft = attemptedRight > Size.X ? Size.X - size.X : realLeft;

            var attemptedTop = center.Y - (size.Y / 2);
            var realTop = attemptedTop < 0 ? 0 : attemptedTop;

            var attemptedBottom = attemptedTop + size.Y;
            realTop = attemptedBottom > Size.Y ? Size.Y - size.Y : realTop;

            return (realLeft, realTop);
        }

        private IPresentable[] GetViewportImpl(Point2i size, Point2i offset)
        {
            var viewport = new IPresentable[size.X * size.Y];
            for (var i = 0; i < size.X; i++)
            {
                for (var j = 0; j < size.Y; j++)
                {
                    viewport[(j * size.X) + i] = Tiles[((j + offset.Y) * Size.X) + i + offset.X];
                }
            }

            // TODO: create IDrawable interface or something similar?
            foreach (var a in Actors)
            {
                if (IsInBounds(a.Position, offset, size))
                {
                    var viewportPosition = a.Position - offset;
                    viewport[(viewportPosition.Y * size.X) + viewportPosition.X] = a;
                }
            }
            return viewport;
        }

        private bool IsInBounds(Point2i position) => position.X >= 0 && position.X < Size.X && position.Y >= 0 && position.Y < Size.Y;

        private int PositionToIndex(Point2i position) => (position.Y * Size.X) + position.X;

        private bool IsInBounds(Point2i position, Point2i offset, Point2i size)
        {
            var left = offset.X;
            var right = size.X + offset.X;
            var top = offset.Y;
            var bottom = size.Y + offset.Y;

            return position.X >= left && position.X < right && position.Y >= top && position.Y < bottom;
        }
    }
}

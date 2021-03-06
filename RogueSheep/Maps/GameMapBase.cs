using System.Collections.Generic;
using System.Linq;
using RogueSheep.Display;

namespace RogueSheep.Maps
{
    public abstract class GameMapBase<TObject, TActor> : IGameMap<TObject, TActor>
        where TObject : IMapTile
        where TActor : IHasPosition, IPresentable
    {
        public IList<TActor> Actors { get; }

        public Point2i Size { get; }
        protected virtual TObject[] Tiles { get; }

        public GameGrid<bool> TransparencyGrid { get; }

        protected GameGrid<TObject> MapMemory { get; }

        public IPresentable[] GetMap() => Tiles.Cast<IPresentable>().ToArray();

        protected GameMapBase(TObject[] tiles, int width, IEnumerable<TActor> actors, TObject invisibleTile)
        {
            Tiles = tiles;
            Size = (width, tiles.Length / width);
            Actors = new List<TActor>().Concat(actors).ToList();

            TransparencyGrid = new GameGrid<bool>(Size);
            var i = 0;
            foreach (var tile in Tiles)
            {
                TransparencyGrid[i++] = tile.Transparent;
            }

            MapMemory = new GameGrid<TObject>(Size);
            MapMemory.Fill(invisibleTile);
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

        public TObject GetActualObject(Point2i p) => Tiles[PositionToIndex(p)];
        public TObject GetObjectFromMemory(Point2i p) => MapMemory[PositionToIndex(p)];

        public GameTile[] GetViewport(Point2i size, Point2i center)
        {
            var fullVisibility = new GameGrid<bool>(Size);
            fullVisibility.Fill(true);
            return GetViewportImpl(size, FindOffsetForViewport(size, center), fullVisibility);
        }

        public GameTile[] GetMaskedViewport(Point2i size, Point2i center, GameGrid<bool> visibilityGrid)
            => GetViewportImpl(size, FindOffsetForViewport(size, center), visibilityGrid);

        public bool IsAvailableForMove(Point2i position) => IsInBounds(position) && IsPassable(position) && !Actors.Any(b => b.Position == position);

        public bool IsTransparent(Point2i position) => Tiles[PositionToIndex(position)].Transparent;

        public Point2i PositionOf(TActor actor) => Actors.Single(a => a.Equals(actor)).Position;

        protected virtual bool IsPassable(Point2i position) => Tiles[PositionToIndex(position)].Passable;

        protected Point2i FindOffsetForViewport(Point2i size, Point2i center)
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

        protected GameTile[] GetViewportImpl(Point2i size, Point2i offset, GameGrid<bool> visibilityGrid)
        {
            FillMapMemory(visibilityGrid);
            var viewport = new GameTile[size.X * size.Y];
            for (var i = 0; i < size.X; i++)
            {
                for (var j = 0; j < size.Y; j++)
                {
                    int viewportIndex = j * size.X + i;
                    int mapIndex = ((j + offset.Y) * Size.X) + i + offset.X;
                    viewport[viewportIndex] = visibilityGrid[mapIndex] ? Tiles[mapIndex].Presentation : MapMemory[mapIndex].Presentation.ToGrayscale();
                }
            }

            foreach (var a in Actors)
            {
                if (visibilityGrid[a.Position] && IsInBounds(a.Position, offset, size))
                {
                    var viewportPosition = a.Position - offset;
                    viewport[(viewportPosition.Y * size.X) + viewportPosition.X] = a.Presentation;
                }
            }
            return viewport;
        }

        protected void FillMapMemory(GameGrid<bool> visibilityGrid)
        {
            for (var i = 0; i < visibilityGrid.Length; i++)
            {
                if (visibilityGrid[i])
                {
                    MapMemory[i] = Tiles[i];
                }
            }
        }

        protected bool IsInBounds(Point2i position) => position.X >= 0 && position.X < Size.X && position.Y >= 0 && position.Y < Size.Y;

        protected int PositionToIndex(Point2i position) => (position.Y * Size.X) + position.X;

        protected Point2i IndexToPosition(int index) => (index % Size.X, index / Size.X);

        protected bool IsInBounds(Point2i position, Point2i offset, Point2i size)
        {
            var left = offset.X;
            var right = size.X + offset.X;
            var top = offset.Y;
            var bottom = size.Y + offset.Y;

            return position.X >= left && position.X < right && position.Y >= top && position.Y < bottom;
        }
    }
}

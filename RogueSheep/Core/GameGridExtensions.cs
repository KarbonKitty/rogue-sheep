using System;

namespace RogueSheep
{
    public static class GameGridExtensions
    {
        public static Point2i ClosestPosition<T>(this GameGrid<T> grid, Point2i origin, Func<GameGrid<T>, Point2i, bool> predicate)
        {
            if (predicate(grid, origin))
            {
                return origin;
            }

            var currentPoint = origin;
            var ring = 0;
            while (ring < Math.Max(grid.Size.X, grid.Size.Y))
            {
                ring++;
                var steps = ring * 2;
                currentPoint = currentPoint.Transform(Direction.NorthWest);
                foreach (var dir in new[] { Direction.East, Direction.South, Direction.West, Direction.North })
                {
                    for (var i = 0; i < steps; i++)
                    {
                        if (grid.IsInBounds(currentPoint) && predicate(grid, currentPoint))
                        {
                            return currentPoint;
                        }
                        currentPoint = currentPoint.Transform(dir);
                    }
                }
            }

            throw new InvalidOperationException("No suitable position found on the grid");
        }
    }
}

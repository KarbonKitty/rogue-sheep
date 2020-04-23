using System.Collections.Generic;
using System.Linq;
using RogueSheep.RandomNumbers;

namespace RogueSheep.Maps.Generation
{
    public class WoodsGenerator : IMapGenerator<WoodsTiles>
    {
        private readonly IRandom rng;

        public WoodsGenerator()
        {
            this.rng = new PCGRandom();
        }

        public WoodsGenerator(IRandom rng)
        {
            this.rng = rng;
        }

        public GameGrid<WoodsTiles> Generate(Point2i size)
        {
            // create info carrier
            var mapDto = new GameGrid<WoodsTiles>(size);

            // drop some trees
            PlaceRandomly(mapDto, WoodsTiles.Tree, 1, 8);

            // drop some rocks
            PlaceRandomly(mapDto, WoodsTiles.Rock, 1, 24);

            // drop some bushes
            PlaceRandomly(mapDto, WoodsTiles.Bush, 1, 18);

            // make sure every space is reachable
            var startingPosition = mapDto.ClosestPosition((0, 0), (gird, point) => gird[point] == WoodsTiles.Empty);
            FloodFill(mapDto, startingPosition);

            // replace all unreachable space with (also unreachable) trees
            // TODO: it might be a good idea to replace this step with something else
            // TODO: even better to allow user to mix-and-match steps
            if (mapDto.Any(p => p == WoodsTiles.Empty))
            {
                for (var i = 0; i < mapDto.Length; i++)
                {
                    if (mapDto[i] == WoodsTiles.Empty)
                    {
                        mapDto[i] = WoodsTiles.Tree;
                    }
                }
            }

            // TODO: add some overgrown ruins

            // TODO: add cave entrance

            // return result
            return mapDto;
        }

        private void PlaceRandomly(GameGrid<WoodsTiles> grid, WoodsTiles tile, int a, int b)
        {
            for (var i = 0; i < grid.Length; i++)
            {
                if (rng.Next(b) < a)
                {
                    grid[i] = tile;
                }
            }
        }

        private void FloodFill(GameGrid<WoodsTiles> grid, Point2i origin)
        {
            var directions = new[] { (0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (-1, -1), (1, -1), (-1, 1) };
            grid[origin] = WoodsTiles.Grass;
            var queue = new Queue<Point2i>();
            queue.Enqueue(origin);
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                foreach (var direction in directions)
                {
                    var newPoint = p + direction;
                    if (grid.IsInBounds(newPoint) && grid[newPoint] == WoodsTiles.Empty)
                    {
                        grid[newPoint] = WoodsTiles.Grass;
                        queue.Enqueue(newPoint);
                    }
                }
            }
        }
    }

    public enum WoodsTiles
    {
        Empty = 0,
        Grass,
        Bush,
        Tree,
        Rock
    }
}

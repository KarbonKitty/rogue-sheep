using System;
using System.Collections.Generic;
using System.Linq;
using RogueSheep.Helpers;

namespace RogueSheep.Maps.Generation
{
    public class CellularAutomaton : IMapGenerator<bool>
    {
        private readonly PCGRandom rng;
        private readonly CellularAutomatonOptions options;

        public CellularAutomaton() : this(new CellularAutomatonOptions()) {}

        public CellularAutomaton(PCGRandom rng) : this(rng, new CellularAutomatonOptions()) {}

        public CellularAutomaton(CellularAutomatonOptions options)
        {
            this.rng = new PCGRandom((ulong)DateTime.Now.Ticks, 1);
            this.options = options;
        }

        public CellularAutomaton(PCGRandom rng, CellularAutomatonOptions options)
        {
            this.rng = rng;
            this.options = options;
        }

        public GameGrid<bool> Generate(Point2i size)
        {
            var mapDto = new GameGrid<bool>(size);

            // fill 50-50 with alive (true) and dead (false)
            for (var i = 0; i < mapDto.Length; i++)
            {
                mapDto[i] = rng.Next(100) < options.AliveProbabilityAtStart;
            }

            // generate a few steps
            for (var s = 0; s < options.NumberOfSteps; s++)
            {
                mapDto = ProcessStep(mapDto);
            }

            if (options.EnsureWalled)
            {
                mapDto = EnsureWalled(mapDto);
            }

            if (options.EnsureConnected)
            {
                mapDto = EnsureConnected(mapDto);
            }

            return mapDto;
        }

        private GameGrid<bool> ProcessStep(GameGrid<bool> mapDto)
        {
            var newGeneration = new GameGrid<bool>(mapDto.Size);

            for (var i = 0; i < mapDto.Length; i++)
            {
                var neighborCount = mapDto.GetNeighborhood(i, true, false).Count(c => c);
                if (mapDto[i] && neighborCount >= options.NeighborCountToKeepAlive)
                {
                    newGeneration[i] = true;
                }
                else if (!mapDto[i] && neighborCount >= options.NeighborCountToMakeAlive)
                {
                    newGeneration[i] = true;
                }
            }

            return newGeneration;
        }

        private GameGrid<bool> EnsureWalled(GameGrid<bool> mapDto)
        {
            var newMap = new GameGrid<bool>(mapDto.Size);
            for (var i = 0; i < mapDto.Length; i++)
            {
                newMap[i] = mapDto[i];
            }

            var maxX = mapDto.Size.X - 1;
            var maxY = mapDto.Size.Y - 1;
            for (var x = 0; x < maxX; x++)
            {
                newMap[(x, 0)] = true;
                newMap[(x, maxY)] = true;
            }
            for (var y = 0; y < maxY; y++)
            {
                newMap[(0, y)] = true;
                newMap[(maxX, y)] = true;
            }

            return newMap;
        }

        private GameGrid<bool> EnsureConnected(GameGrid<bool> mapDto)
        {
            var regionList = new Dictionary<int, List<Point2i>>();
            var regionCount = 0;

            GameGrid<int> regionMap = new GameGrid<int>(mapDto.Size);
            for (var i = 0; i < mapDto.Length; i++)
            {
                regionMap[i] = mapDto[i] ? 0 : -1;
            }

            while (regionMap.Array.Contains(-1))
            {
                var newRegionOrigin = regionMap.ClosestPosition((0, 0), (g, p) => g[p] == -1);
                var newRegionNumber = ++regionCount;
                regionList.Add(newRegionNumber, FloodFill(regionMap, newRegionOrigin, newRegionNumber));
            }

            while (regionList.Count > 1)
            {
                // select two regions
                var startingRegion = regionList.First().Value;
                var endingRegion = regionList.Last().Value;

                // select random starting points in both
                var startingPoint = startingRegion[rng.NextInt(startingRegion.Count)];
                var regionNumber = regionMap[startingPoint];
                var endingPoint = endingRegion[rng.NextInt(endingRegion.Count)];
                var endingRegionNumber = regionMap[endingPoint];

                // join them by random walk, removing walls from mapDto
                var path = DrunkardWalk(regionMap, startingPoint, endingPoint).ToHashSet();
                var regionsTouched = new HashSet<int>();
                foreach (var point in path)
                {
                    regionMap[point] = regionNumber;
                    // check if random walk touches any other regions
                    var touchedRegions = regionMap.GetNeighborhood(point, -1, false);
                    regionsTouched.UnionWith(touchedRegions.Where(x => x > 0).ToHashSet());
                }

                // merge all regions touched
                for (var i = 0; i < regionMap.Count(); i++)
                {
                    if (regionsTouched.Contains(regionMap[i]))
                    {
                        var region = regionMap[i];
                        var position = regionMap.IndexToPosition(i);
                        regionList[region].Remove(position);
                        regionList[regionNumber].Add(position);
                        regionMap[i] = regionNumber;
                    }
                }

                regionsTouched.Remove(regionNumber);
                foreach (var regionTouched in regionsTouched)
                {
                    regionList.Remove(regionTouched);
                }
            }

            var retVal = new GameGrid<bool>(mapDto.Size);
            for (var i = 0; i < mapDto.Length; i++)
            {
                retVal[i] = regionMap[i] == 0;
            }
            return retVal;
        }

        private List<Point2i> FloodFill(GameGrid<int> grid, Point2i origin, int newRegionNumber)
        {
            var newRegion = new List<Point2i>();

            var directions = new[] { (0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (-1, -1), (1, -1), (-1, 1) };
            grid[origin] = newRegionNumber;
            var queue = new Queue<Point2i>();
            queue.Enqueue(origin);
            newRegion.Add(origin);
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                foreach (var direction in directions)
                {
                    var newPoint = p + direction;
                    if (grid.IsInBounds(newPoint) && grid[newPoint] == -1)
                    {
                        grid[newPoint] = newRegionNumber;
                        newRegion.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }
                }
            }

            return newRegion;
        }

        private List<Point2i> DrunkardWalk<T>(GameGrid<T> grid, Point2i origin, Point2i target)
        {
            var walk = new List<Point2i>();

            var currentPoint = origin;

            bool canBeVisited(Point2i x) => x.X > 0 && x.X < grid.Size.X - 1 && x.Y > 0 && x.Y < grid.Size.Y - 1;

            while (currentPoint != target)
            {
                walk.Add(currentPoint);

                var diffX = target.X - currentPoint.X;
                var diffY = target.Y - currentPoint.Y;

                var weightPlusX = diffX > 0 ? 4 : 1;
                var weightMinusX = diffX < 0 ? 4 : 1;
                var weightPlusY = diffY > 0 ? 4 : 1;
                var weightMinusY = diffY < 0 ? 4 : 1;

                var totalWeight = weightMinusX + weightMinusY + weightPlusX + weightPlusY;

                var roll = rng.NextInt(totalWeight) + 1;
                var runningTotal = weightPlusX;
                Point2i testPoint;
                if (roll <= runningTotal)
                {
                    testPoint = currentPoint + (1, 0);
                    if (canBeVisited(testPoint))
                    {
                        currentPoint = testPoint;
                    }
                    continue;
                }
                runningTotal += weightMinusX;
                if (roll <= runningTotal)
                {
                    testPoint = currentPoint + (-1, 0);
                    if (canBeVisited(testPoint))
                    {
                        currentPoint = testPoint;
                    }
                    continue;
                }
                runningTotal += weightPlusY;
                if (roll <= runningTotal)
                {
                    testPoint = currentPoint + (0, 1);
                    if (canBeVisited(testPoint))
                    {
                        currentPoint = testPoint;
                    }
                    continue;
                }
                testPoint = currentPoint + (0, -1);
                if (canBeVisited(testPoint))
                {
                    currentPoint = testPoint;
                }
            }

            return walk;
        }
    }
}

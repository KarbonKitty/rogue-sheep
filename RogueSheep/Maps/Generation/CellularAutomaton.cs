using System.Linq;
using RogueSheep.Helpers;

namespace RogueSheep.Maps.Generation
{
    public class CellularAutomaton : IMapGenerator<bool>
    {
        private readonly PCGRandom rng;

        public CellularAutomaton(PCGRandom rng)
        {
            this.rng = rng;
        }

        public GameGrid<bool> Generate(Point2i size)
        {
            var mapDto = new GameGrid<bool>(size);

            // fill 50-50 with alive (true) and dead (false)
            for (var i = 0; i < mapDto.Length; i++)
            {
                mapDto[i] = rng.Next(100) < 50;
            }

            // generate a few steps
            for (var s = 0; s < 4; s++)
            {
                mapDto = ProcessStep(mapDto);
            }

            return mapDto;
        }

        private GameGrid<bool> ProcessStep(GameGrid<bool> mapDto)
        {
            var newGeneration = new GameGrid<bool>(mapDto.Size);

            for (var i = 0; i < mapDto.Length; i++)
            {
                var neighborCount = mapDto.GetNeighborhood(i, true, false).Count(c => c);
                if (mapDto[i] && neighborCount >= 4)
                {
                    newGeneration[i] = true;
                }
                else if (!mapDto[i] && neighborCount >= 5)
                {
                    newGeneration[i] = true;
                }
            }

            return newGeneration;
        }
    }
}

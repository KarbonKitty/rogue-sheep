namespace RogueSheep.Maps.Generation
{
    public class CellularAutomatonOptions
    {
        public int NumberOfSteps { get; set; } = 4;
        public int AliveProbabilityAtStart { get; set; } = 50;
        public int NeighborCountToMakeAlive { get; set; } = 5;
        public int NeighborCountToKeepAlive { get; set; } = 4;
        public bool EnsureWalled { get; set; } = true;
        public bool EnsureConnected { get; set; } = true;
    }
}

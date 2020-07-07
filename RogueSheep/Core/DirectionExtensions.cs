namespace RogueSheep
{
    public static class DirectionExtensions
    {
        public static Point2i Vector(this Direction direction) => direction switch
        {
            Direction.None => (0, 0),
            Direction.North => (0, -1),
            Direction.South => (0, 1),
            Direction.West => (-1, 0),
            Direction.East => (1, 0),
            Direction.NorthEast => (1, -1),
            Direction.NorthWest => (-1, -1),
            Direction.SouthEast => (1, 1),
            Direction.SouthWest => (-1, 1),
            _ => (0, 0)
        };
    }
}

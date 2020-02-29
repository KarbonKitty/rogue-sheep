namespace RogueSheep.Maps.Generation
{
    public interface IMapGenerator<T>
    {
        GameGrid<T> Generate(Point2i size);
    }
}

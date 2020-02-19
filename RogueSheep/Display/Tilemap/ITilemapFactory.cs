namespace RogueSheep.Display
{
    public interface ITilemapFactory
    {
        ITilemap CreateTilemap(ITilemapConfiguration config);
    }
}

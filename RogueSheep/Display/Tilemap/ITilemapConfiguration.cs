namespace RogueSheep.Display
{
    public interface ITilemapConfiguration
    {
        string? FilePath { get; }
        int SpriteHeight { get; }
        int SpriteWidth { get; }
        int SpriteRows { get; }
        int SpriteColumns { get; }
    }
}

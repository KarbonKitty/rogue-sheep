namespace RogueSheep.Display
{
    public class TilemapConfiguration : ITilemapConfiguration
    {
        public int SpriteHeight { get; }
        public int SpriteWidth { get; }
        public int SpriteRows { get; }
        public int SpriteColumns { get; }
        public string? FilePath { get; }

        public TilemapConfiguration(int spriteHeight, int spriteWidth, int spriteRows, int spriteColumns, string? filepath = null)
        {
            FilePath = filepath;
            SpriteHeight = spriteHeight;
            SpriteWidth = spriteWidth;
            SpriteRows = spriteRows;
            SpriteColumns = spriteColumns;
        }
    }
}

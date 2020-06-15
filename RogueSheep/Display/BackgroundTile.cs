using SFML.Graphics;

namespace RogueSheep.Display
{
    internal static class BackgroundTile
    {
        private static readonly Texture whiteTexture;
        internal static Sprite Tile;

        static BackgroundTile()
        {
            var pixelArray = new byte[16*16*4];
            for (var i = 0; i < 16 * 16 * 4; i += 4)
            {
                pixelArray[i] = 255;
                pixelArray[i + 1] = 255;
                pixelArray[i + 2] = 255;
                pixelArray[i + 3] = 255;
            }
            whiteTexture = new Texture(16, 16);
            whiteTexture.Update(pixelArray);
            Tile = new Sprite(whiteTexture);
        }
    }
}

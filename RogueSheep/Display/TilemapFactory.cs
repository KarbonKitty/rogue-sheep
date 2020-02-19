using System;
using System.IO;

namespace RogueSheep
{
    public class TilemapFactory
    {
        public Tilemap CreateTilemapFromFile(string path)
        {
            throw new NotImplementedException();
        }

        public Tilemap CreateTilemapFromResource(int fontWidth, int fontHeight, int fontRows, int fontColumns)
        {
            var assembly = typeof(GameDisplay).Assembly;
            var resourceName = $"RogueSheep.data.font{fontWidth}x{fontHeight}.png";
            try
            {
                Stream font = assembly.GetManifestResourceStream(resourceName);
                return new Tilemap(font, fontWidth, fontHeight, fontRows, fontColumns);
            }
            catch
            {
                // add actual error logging?
                throw;
            }
        }
    }
}

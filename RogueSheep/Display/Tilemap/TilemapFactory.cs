using System.IO;
using SFML.Graphics;

namespace RogueSheep.Display
{
    public class TilemapFactory : ITilemapFactory
    {
        public ITilemap CreateTilemap(ITilemapConfiguration config)
        {
            return config.FilePath switch
            {
                string val => this.CreateTilemapFromFile(val, config),
                null => this.CreateTilemapFromResource(config)
            };
        }

        private ITilemap CreateTilemapFromFile(string path, ITilemapConfiguration config)
        {
            using Stream fontFile = new FileStream(path, FileMode.Open);
            return CreateTilemap(fontFile, config);
        }

        private ITilemap CreateTilemapFromResource(ITilemapConfiguration config)
        {
            var assembly = typeof(GameDisplay).Assembly;
            var resourceName = $"RogueSheep.data.font{config.SpriteWidth}x{config.SpriteHeight}.png";
            try
            {
                using Stream font = assembly.GetManifestResourceStream(resourceName);
                return CreateTilemap(font, config);
            }
            catch
            {
                // add actual error logging?
                throw;
            }
        }

        private ITilemap CreateTilemap(Stream stream, ITilemapConfiguration config)
        {
            var g = new Sprite[config.SpriteRows * config.SpriteColumns];
            var texture = new Texture(stream);

            for (var x = 0; x < config.SpriteRows; x++)
            {
                for (var y = 0; y < config.SpriteColumns; y++)
                {
                    var rect = new IntRect(x * config.SpriteWidth, y * config.SpriteHeight, config.SpriteWidth, config.SpriteHeight);
                    g[(y * config.SpriteColumns) + x] = new Sprite(texture, rect);
                }
            }

            return new Tilemap(g);
        }
    }
}

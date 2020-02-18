using System.IO;
using SFML.Graphics;

namespace RogueSheep
{
    public class Tilemap
    {
        public Sprite[] Glyphs { get; }

        public Sprite this[int index] => Glyphs[index];
        public Sprite this[SpriteEnum index] => Glyphs[(int)index];

        public Tilemap(Stream data, int spriteWidth, int spriteHeight, int spriteRows, int spriteColumns)
        {
            var g = new Sprite[spriteRows * spriteColumns];
            var t = new Texture(data);

            for (var x = 0; x < spriteRows; x++)
            {
                for (var y = 0; y < spriteColumns; y++)
                {
                    var rect = new IntRect(x * spriteWidth, y * spriteHeight, spriteWidth, spriteHeight);
                    g[(y * spriteColumns) + x] = new Sprite(t, rect);
                }
            }

            Glyphs = g;
        }
    }
}

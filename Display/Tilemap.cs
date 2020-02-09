using System.IO;
using SFML.Graphics;

namespace RogueSheep
{
    public class Tilemap
    {
        public Sprite[] Glyphs { get; }

        public Sprite this[int index] => Glyphs[index];
        public Sprite this[SpriteEnum index] => Glyphs[(int)index];

        public Tilemap(Stream data, int spriteSize, int spriteCount)
        {
            var g = new Sprite[spriteCount * spriteCount];
            var t = new Texture(data);

            for (var i = 0; i < spriteCount; i++)
            {
                for (var j = 0; j < spriteCount; j++)
                {
                    var rect = new IntRect(i * spriteSize, j * spriteSize, spriteSize, spriteSize);
                    g[j * spriteCount + i] = new Sprite(t, rect);
                }
            }

            Glyphs = g;
        }
    }
}

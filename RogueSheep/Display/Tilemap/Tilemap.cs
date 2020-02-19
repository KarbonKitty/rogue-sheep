using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;

namespace RogueSheep.Display
{
    public class Tilemap : ITilemap
    {
        public Sprite[] Glyphs { get; }

        public Sprite this[int index] => Glyphs[index];
        public Sprite this[CP437Glyph index] => Glyphs[(int)index];

        public Tilemap(IEnumerable<Sprite> glyphs)
        {
            Glyphs = glyphs.ToArray();
        }
    }
}

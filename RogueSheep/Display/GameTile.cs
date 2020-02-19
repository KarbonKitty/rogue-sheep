using System;
using SFML.Graphics;

namespace RogueSheep.Display
{
    public class GameTile
    {
        public CP437Glyph Glyph { get; }
        public Color Foreground { get; }
        public Color Background { get; }

        public GameTile(CP437Glyph s) : this(s, Color.White, Color.Black) { }

        public GameTile(CP437Glyph s, Color foreground) : this(s, foreground, Color.Black) { }

        public GameTile(CP437Glyph s, Color foreground, Color background)
        {
            Glyph = s;
            this.Foreground = foreground;
            this.Background = background;
        }
    }
}

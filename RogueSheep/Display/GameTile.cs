using SFML.Graphics;

namespace RogueSheep
{
    public class GameTile
    {
        public SpriteEnum Glyph { get; }
        public Color Foreground { get; }
        public Color Background { get; }

        public GameTile(SpriteEnum s) : this(s, Color.White, Color.Black) { }

        public GameTile(SpriteEnum s, Color foreground) : this(s, foreground, Color.Black) { }

        public GameTile(SpriteEnum s, Color foreground, Color background)
        {
            Glyph = s;
            this.Foreground = foreground;
            this.Background = background;
        }
    }
}

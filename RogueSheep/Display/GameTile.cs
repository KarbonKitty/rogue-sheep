namespace RogueSheep.Display
{
    public class GameTile
    {
        public CP437Glyph Glyph { get; }
        public RogueColor Foreground { get; }
        public RogueColor Background { get; }

        public GameTile(CP437Glyph s) : this(s, RogueColor.White, RogueColor.Transparent) { }

        public GameTile(CP437Glyph s, RogueColor foreground) : this(s, foreground, RogueColor.Transparent) { }

        public GameTile(CP437Glyph s, RogueColor foreground, RogueColor background)
        {
            Glyph = s;
            Foreground = foreground;
            Background = background;
        }

        public GameTile(GameTile toBeCloned)
        {
            Glyph = toBeCloned.Glyph;
            Foreground = toBeCloned.Foreground;
            Background = toBeCloned.Background;
        }
    }
}

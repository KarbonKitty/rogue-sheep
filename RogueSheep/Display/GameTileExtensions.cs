namespace RogueSheep.Display
{
    public static class GameTileExtensions
    {
        public static GameTile ToGrayscale(this GameTile gameTile)
            => new GameTile(gameTile.Glyph, gameTile.Foreground.ToGrayscale(), gameTile.Background);
    }
}

namespace RogueSheep
{
    public static class DisplayConsts
    {
        public const int WINDOW_HEIGHT = 50;
        public const int WINDOW_WIDTH = 80;
        // public const int MAP_HEIGHT = 61;
        // public const int MAP_WIDTH = 61;
        // public const int PANEL_WIDTH = 50;
        public const int FONT_SIZE = 16;
        public const int FONT_COUNT = 16;

        // public const int TOP_PANEL_SIZE = 7;
        // public const int BOTTOM_PANEL_SIZE = 25;
        // public const int PANEL_BORDERS_NO = 4;

        public static readonly uint WindowHeightPx = WINDOW_HEIGHT * FONT_SIZE;
        public static readonly uint WindowWidthPx = WINDOW_WIDTH * FONT_SIZE;
        public static readonly uint BitDepth = 32;
        public static readonly int FontCountTotal = FONT_COUNT * FONT_COUNT;
    }
}

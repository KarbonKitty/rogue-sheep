namespace RogueSheep
{
    public static class RogueColorExtensions
    {
        public static RogueColor ToGrayscale(this RogueColor color)
        {
            var luminosity = (byte)(0.21 * color.R + 0.72 * color.G + 0.07 * color.B);
            return new RogueColor(luminosity, luminosity, luminosity);
        }
    }
}

using SFML.Graphics;

namespace RogueSheep.Display
{
    public interface ITilemap
    {
        Sprite this[int index] { get; }
        Sprite this[CP437Glyph index] { get; }
    }
}

using static SFML.Window.Keyboard;

namespace RogueSheep
{
    public interface IInputConsumer
    {
        void ConsumeInput(Key key);
    }
}

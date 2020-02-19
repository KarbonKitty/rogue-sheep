namespace RogueSheep.Messaging
{
    public interface IMessage<T> where T : System.Enum
    {
        T MessageType { get; }
        string Text { get; }
    }
}

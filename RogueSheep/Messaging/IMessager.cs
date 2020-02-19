using System.Collections.Generic;

namespace RogueSheep.Messaging
{
    public interface IMessager<T> where T : System.Enum
    {
        void WriteMessage(IMessage<T> message);
        IList<string> GetTexts(int maxSize);
        IEnumerable<IMessage<T>> GetMessages();
        IEnumerable<IMessage<T>> GetMessages(T type);
    }
}

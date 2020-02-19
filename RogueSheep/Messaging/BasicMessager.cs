using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueSheep.Messaging
{
    public class BasicMessager<T> : IMessager<T> where T : System.Enum
    {
        private readonly IMessage<T>[] messages;
        private int index = 0;

        public BasicMessager(int size)
        {
            messages = new IMessage<T>[size];
        }

        public IList<string> GetTexts(int maxSize)
        {
            var returnList = new List<string>(messages.Length);

            for (var i = -1; i > -messages.Length; i--)
            {
                var lines = CutToSize(messages[index + i < 0 ? index + i + messages.Length : index + i], maxSize);
                returnList.AddRange(lines);
            }

            return returnList;
        }

        public IEnumerable<IMessage<T>> GetMessages() => messages;

        public IEnumerable<IMessage<T>> GetMessages(T type) => messages.Where(m => m.MessageType.Equals(type));

        public void WriteMessage(IMessage<T> message)
        {
            messages[index] = message;
            index++;
            if (index >= messages.Length)
            {
                index = 0;
            }
        }

        private IList<string> CutToSize(IMessage<T> message, int size)
        {
            if (message?.Text is null)
            {
                return new List<string> { string.Empty };
            }

            if (message.Text.Length <= size)
            {
                return new List<string> { message.Text };
            }

            var words = message.Text.Split(' ');
            var lines = new List<string>(message.Text.Length / size);

            var line = new StringBuilder();

            for (var wordIndex = 0; wordIndex < words.Length; wordIndex++)
            {
                if (line.Length + 1 + words[wordIndex].Length > size)
                {
                    lines.Add(line.ToString().Trim());
                    line.Clear();
                }

                line.Append($" {words[wordIndex]}");
            }

            lines.Add(line.ToString().Trim());

            return lines;
        }
    }
}

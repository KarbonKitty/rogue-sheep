using System.Collections.Generic;

namespace RogueSheep.Schedulers
{
    public interface IScheduler<T>
    {
        T Current();
        T Next();
        void Add(T item);
        void Add(params T[] items);
        void AddRange(IEnumerable<T> items);
        bool Remove(T item);
        void Clear();
    }
}

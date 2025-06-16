using System.Collections;
using System.Collections.Generic;

namespace _GardenOfDreams.Scripts.Utilities
{
    public class OrderedSet<T> : IEnumerable<T>
    {
        private readonly HashSet<T> _set = new HashSet<T>();
        private readonly List<T> _order = new List<T>();

        public int Count => _set.Count;
        public bool IsEmpty => _set.Count == 0;

        public T Last => _order.Count > 0 ? _order[_order.Count - 1] : default;

        public bool Add(T item)
        {
            if (_set.Add(item))
            {
                _order.Add(item);
                return true;
            }
            return false;
        }

        public bool Remove(T item)
        {
            if (_set.Remove(item))
            {
                _order.Remove(item);
                return true;
            }
            return false;
        }

        public bool Contains(T item) => _set.Contains(item);

        public void Clear()
        {
            _set.Clear();
            _order.Clear();
        }

        public IEnumerator<T> GetEnumerator() => _order.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class CircularArray<T> : IEnumerable<T>
    {
        public T[] ItemsSource;

        public int StartingIndex;

        public CircularArray(T[] itemsSource, int startingIndex = 0)
        {
            ItemsSource = itemsSource;
            StartingIndex = startingIndex;
        }

        public T[] Items => ItemsSource.GetEnumerable(StartingIndex).ToArray();

        public int Length => Items.Length;

        public T this[int index]
        {
            get
            {
                while (index >= Length) index -= Length;
                while (index < 0) index += Length;
                return Items[index];
            }
        }

        public int IndexOf(T item) => Items.IndexOf(item);

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }
}
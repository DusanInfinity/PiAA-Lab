using System;

namespace LAB3.Strukture_podataka
{
    public class SortList : System.Collections.IEnumerable
    {
        private readonly int _maxEl;
        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }
        }
        private ShannonFano[] list;
        public ShannonFano this[int i]
        {
            get
            {
                return list[i];
            }
        }

        public SortList(int maxElements)
        {
            _maxEl = maxElements;
            list = new ShannonFano[maxElements];
            _count = 0;
        }


        public bool Add(ShannonFano newElement)
        {
            if (_count == _maxEl) // prekoracenje niza
                throw new OverflowException($"Doslo je do prekoracenja! Maksimalan broj elemenata je: {_maxEl}");

            float newFreq = newElement.Frequency;
            int index = 0;
            while (index < _count && list[index].Frequency > newFreq) index++;

            for (int i = _count; i > index; i--)
            {
                list[i] = list[i - 1];
            }

            list[index] = newElement;
            _count++;
            return true;
        }


        public System.Collections.Generic.IEnumerator<ShannonFano> GetEnumerator()
        {
            for (int i = 0; i < _count; ++i)
                yield return list[i];
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

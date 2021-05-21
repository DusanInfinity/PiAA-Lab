namespace LAB2.Strukture_podataka
{
    public class MaxHeap
    {
        private int[] niz;
        private int _maxElemenata;

        public MaxHeap(int max)
        {
            _maxElemenata = max;
            max++; // 0 je prazna, 1 ROOT
            niz = new int[max];

            for (int i = 0; i < max; i++)
                niz[i] = -1;
        }

        private bool IsValidIndex(int i)
        {
            return i <= _maxElemenata;
        }


        public int Parent(int i)
        {
            if (IsValidIndex(i / 2)) return -1;

            return niz[i / 2];
        }

        public int Left(int i)
        {
            if (IsValidIndex(2 * i)) return -1;

            return niz[2 * i];
        }
        public int Right(int i)
        {
            if (IsValidIndex(2 * i + 1)) return -1;

            return niz[2 * i + 1];
        }

        public bool AddElement(int el)
        {
            return AddElement(el, 1);
        }

        private bool AddElement(int el, int index)
        {
            if (!IsValidIndex(index)) return false;

            if (niz[index] == -1)
            {
                niz[index] = el;
                return true;
            }

            if (niz[index] > el)
                return AddElement(el, 2 * index);
            else
                return AddElement(el, 2 * index + 1);
        }


        public void Heapify(int i)
        {
            int left = 2 * i;
            int right = 2 * i + 1;

            int largest = i;
            if (IsValidIndex(left) && niz[left] > niz[i])
                largest = left;

            if (IsValidIndex(right) && niz[right] > niz[largest])
                largest = right;

            if (largest != i)
            {
                int temp = niz[i];
                niz[i] = niz[largest];
                niz[largest] = temp;
                Heapify(largest);
            }
        }

        public static MaxHeap BuildHeap(int[] nizSort)
        {
            int brEl = nizSort.Length;
            MaxHeap heap = new MaxHeap(nizSort.Length);

            for (int i = 0; i < brEl; i++)
                heap.niz[i + 1] = nizSort[i];

            for (int i = nizSort.Length / 2; i >= 1; i--)
                heap.Heapify(i);

            return heap;
        }


        public int[] HeapSort()
        {
            int velicina = _maxElemenata;
            int[] noviNiz = new int[velicina];
            if (velicina < 1) return noviNiz;


            for (int i = velicina; i >= 2; i--)
            {
                noviNiz[i - 1] = niz[1];
                niz[1] = niz[_maxElemenata];
                _maxElemenata--;
                Heapify(1);
            }
            noviNiz[0] = niz[1]; // najmanji element
            return noviNiz;
        }
    }
}

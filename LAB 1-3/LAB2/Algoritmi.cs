using LAB2.Strukture_podataka;
using System.Collections.Generic;

namespace LAB2
{
    public class Algoritmi
    {
        #region BubbleSort
        public static void BubbleSort(ref int[] niz)
        {
            int n = niz.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0, max = n - i - 1; j < max; j++)
                {
                    if (niz[j] > niz[j + 1])
                    {
                        int temp = niz[j];
                        niz[j] = niz[j + 1];
                        niz[j + 1] = temp;
                    }
                }
            }
        }
        #endregion

        #region HeapSort
        public static void HeapSort(ref int[] niz)
        {
            MaxHeap heap = MaxHeap.BuildHeap(niz);

            niz = heap.HeapSort();
        }
        #endregion

        #region BucketSort
        public static void BucketSort(ref int[] niz)
        {
            int n = niz.Length;
            //int[][] buckets = new int[n][];
            List<int>[] buckets = new List<int>[n];
            int[] addedElements = new int[n];

            for (int i = 0; i < n; i++)
            {
                //buckets[i] = new int[n];
                buckets[i] = new List<int>();
                addedElements[i] = 0;
            }

            for (int i = 0; i < n; i++)
            {
                int ind = niz[i] / n;
                //buckets[ind][addedElements[ind]++] = niz[i];
                buckets[ind].Add(niz[i]);
                addedElements[ind]++;
            }

            for (int i = 0; i < n; i++)
            {
                /*int[] sortNiz = buckets[i].ToArray();
                HeapSort(ref sortNiz);
                buckets[i] = sortNiz.ToList();*/


                buckets[i].Sort(); //InsertionSort(ref buckets[i]);
            }

            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < addedElements[i]; j++)
                {
                    niz[index++] = buckets[i][j];
                }
            }
        }


        private static void InsertionSort(ref int[] niz)
        {
            int n = niz.Length;
            for (int i = 1; i < n; ++i)
            {
                int el = niz[i];
                int j = i - 1;

                while (j >= 0 && niz[j] > el)
                {
                    niz[j + 1] = niz[j];
                    j--;
                }
                niz[j + 1] = el;
            }
        }
        #endregion
    }
}

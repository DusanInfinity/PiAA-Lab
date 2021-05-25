using LAB4.FibonacciHeap;
using System;

namespace LAB4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region BinomHeap
                /*BHeap heap = new BHeap();

                heap = heap.Insert(10);
                heap = heap.Insert(20);
                heap = heap.Insert(5);
                heap = heap.Insert(3);
                heap = heap.Insert(25);
                heap = heap.Insert(1);
                heap = heap.Insert(8);
                heap = heap.Insert(15);
                heap = heap.Insert(2);
                heap = heap.Insert(30);

                Console.WriteLine("Elementi heap-a:\n");
                heap.PrintHeap();

                BNode temp = heap.FindMin();
                if (temp != null)
                    Console.WriteLine($"\nMinimum: {temp.Key}\nSledi brisanje minimuma...\n");

                heap = heap.ExtractMin();
                Console.WriteLine("\nHeap nakon brisanja minimalnog elementa\n");
                heap.PrintHeap(); */
                #endregion


                #region FibonacciHeap
                FHeap heap = new FHeap();

                heap.Insert(30);
                heap.Insert(20);
                heap.Insert(10);
                heap.Insert(5);
                heap.Insert(1);
                heap.Insert(45);

                Console.WriteLine("Elementi heap-a:\n");
                heap.PrintHeap();

                FNode temp = heap.FindMin();
                if (temp != null)
                    Console.WriteLine($"\nMinimum: {temp.Key}\nSledi brisanje minimuma...\n");

                heap.ExtractMin();
                Console.WriteLine("\nHeap nakon brisanja minimalnog elementa\n");
                heap.PrintHeap();

                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

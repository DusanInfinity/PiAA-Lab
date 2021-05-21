using LAB4.BinomniHeap;
using System;

namespace LAB4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BHeap heap = new BHeap();

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

                // Delete minimum element of heap
                heap = heap.ExtractMin();
                Console.WriteLine("\nHeap nakon brisanja minimalnog elementa\n");
                heap.PrintHeap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }


        }
    }
}

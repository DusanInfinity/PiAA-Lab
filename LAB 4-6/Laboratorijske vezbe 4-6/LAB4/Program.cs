using LAB4.BinomniHeap;
using LAB4.FibonacciHeap;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace LAB4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int[] brElemenata = { 10, 100, 1000, 10000, 100000 };
                Stopwatch stopWatch = new Stopwatch();
                Random rnd = new Random();

                #region Generisanje nizova
                /*Console.WriteLine("Sledi generisanje nizova...");
                for (int i = 0; i < 5; i++)
                {
                    GenerisiNizove(brElemenata[i]);
                } */
                #endregion


                #region BinomHeap
                Console.Write("Pokrecem kreiranje BinomHeap-a");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(1000);
                    Console.Write($".{(i == 2 ? "\n\n\n" : "")}");
                }
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"BinomHeap za {brElemenata[i]} elemenata.");
                    Console.WriteLine($"Sledi citanje fajlova i kreiranje objekta heap-a...");
                    BHeap heap = BHeap.ProcitajHeap($"niz({brElemenata[i]}).txt");
                    int desetPosto = brElemenata[i] / 10;
                    Console.WriteLine($"Heap procitan iz fajla, objekat kreiran.\n");

                    Console.WriteLine($"Sledi izvrsenje ExtractMin za 10% ({desetPosto})...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    for (int j = 0; j < desetPosto; j++)
                    {
                        heap = heap.ExtractMin();
                    }
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje ExctractMin-a za {brElemenata[i]} elemenata (10% = {desetPosto}) je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n");

                    Console.WriteLine($"Sledi izvrsenje Decrease-Key za 10% ({desetPosto})...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    for (int j = 0; j < desetPosto; j++)
                    {
                        BNode node = heap.FindNode(heap.FindMin(), rnd.Next(1, 10001)); // Od 1 do 10000 jer ako je 0, bice -1
                        if (node != null)
                        {
                            heap.DecreaseKey(node, node.Key - 1);
                        }

                    }
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje Decrease-Key-a za {brElemenata[i]} elemenata (10% = {desetPosto}) je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n");

                    Console.WriteLine($"Sledi izvrsenje Delete za 10% ({desetPosto})...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    for (int j = 0; j < desetPosto; j++)
                    {
                        BNode node = heap.FindNode(heap.FindMin(), rnd.Next(1, 10001)); // Od 1 do 10000 jer ako je 0, bice -1
                        if (node != null)
                        {
                            heap = heap.Delete(node);
                        }

                    }
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje Delete-a za {brElemenata[i]} elemenata (10% = {desetPosto}) je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n");

                    Console.WriteLine($"Sledi izvrsenje Insert za 10% ({desetPosto})...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    for (int j = 0; j < desetPosto; j++)
                    {
                        int newKey = rnd.Next(0, 10001);
                        heap = heap.Insert(newKey);
                    }
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje Insert-a za {brElemenata[i]} elemenata (10% = {desetPosto}) je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n\n");
                }



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
                Console.Write("Pokrecem kreiranje FibonacciHeap-a");
                for (int i = 1, max = 8; i <= max; i++)
                {
                    Thread.Sleep(1000);
                    Console.Write($".{(i == max ? "\n\n\n" : "")}");
                }
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"FibonacciHeap za {brElemenata[i]} elemenata.");
                    Console.WriteLine($"Sledi citanje fajlova i kreiranje objekta heap-a...");
                    FHeap heap = FHeap.ProcitajHeap($"niz({brElemenata[i]}).txt");
                    int desetPosto = brElemenata[i] / 10;
                    Console.WriteLine($"Heap procitan iz fajla, objekat kreiran.\n");

                    Console.WriteLine($"Sledi izvrsenje ExtractMin za 10% ({desetPosto})...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    for (int j = 0; j < desetPosto; j++)
                    {
                        heap.ExtractMin();
                    }
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje ExctractMin-a za {brElemenata[i]} elemenata (10% = {desetPosto}) je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n");

                    Console.WriteLine($"Sledi izvrsenje Decrease-Key za 10% ({desetPosto})...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    for (int j = 0; j < desetPosto; j++)
                    {
                        FNode node = heap.FindNode(rnd.Next(1, 10001)); // Od 1 do 10000 jer ako je 0, bice -1
                        if (node != null)
                        {
                            heap.DecreaseKey(node, node.Key - 1);
                        }
                    }
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje Decrease-Key-a za {brElemenata[i]} elemenata (10% = {desetPosto}) je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n");

                    Console.WriteLine($"Sledi izvrsenje Delete za 10% ({desetPosto})...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    for (int j = 0; j < desetPosto; j++)
                    {
                        FNode node = heap.FindNode(rnd.Next(1, 10001)); // Od 1 do 10000 jer ako je 0, bice -1
                        if (node != null)
                        {
                            heap.Delete(node);
                        }
                    }
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje Delete-a za {brElemenata[i]} elemenata (10% = {desetPosto}) je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n");

                    Console.WriteLine($"Sledi izvrsenje Insert za 10% ({desetPosto})...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    for (int j = 0; j < desetPosto; j++)
                    {
                        int newKey = rnd.Next(0, 10001);
                        heap.Insert(newKey);
                    }
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje Insert-a za {brElemenata[i]} elemenata (10% = {desetPosto}) je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n\n");
                }


                /*Console.WriteLine($"FibonacciHeap za {brElemenata[0]} elemenata.");
                Console.WriteLine($"Sledi citanje fajlova i kreiranje objekta heap-a...");
                FHeap heap = FHeap.ProcitajHeap($"niz({brElemenata[0]}).txt");
                int desetPosto = brElemenata[0] / 10;
                Console.WriteLine($"Heap procitan iz fajla, objekat kreiran.\n");

                Console.WriteLine("Elementi heap-a:\n");
                heap.PrintHeap();

                FNode temp = heap.FindMin();
                if (temp != null)
                    Console.WriteLine($"\nMinimum: {temp.Key}\nSledi brisanje minimuma...\n");

                heap.ExtractMin();
                Console.WriteLine("\nHeap nakon brisanja minimalnog elementa\n");
                heap.PrintHeap();

                heap.ExtractMin();
                Console.WriteLine("\nHeap nakon brisanja minimalnog elementa\n");
                heap.PrintHeap();

                heap.ExtractMin();
                Console.WriteLine("\nHeap nakon brisanja minimalnog elementa\n");
                heap.PrintHeap();

                heap.ExtractMin();
                Console.WriteLine("\nHeap nakon brisanja minimalnog elementa\n");
                heap.PrintHeap(); */

                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static void GenerisiNizove(int brElemenata)
        {
            try
            {
                Random rnd = new Random();
                using (StreamWriter sw = new StreamWriter($"niz({brElemenata}).txt"))
                {
                    int vrednost;
                    for (int i = 0; i < brElemenata; i++)
                    {
                        vrednost = rnd.Next(0, 10001);
                        sw.WriteLine($"{vrednost}");
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

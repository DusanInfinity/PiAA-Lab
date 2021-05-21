using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace LAB2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> wordsCount = new List<int>() { 100, 1000, 10000, 100000, 1000000, 10000000, 100000000 };
            Stopwatch stopWatch = new Stopwatch();

            #region Generisanje fajlova sa nizovima
            /*Console.WriteLine("Sledi generisanje nizova...");
            for (int i = 0; i < 7; i++)
                GenerisiNiz(wordsCount[i]);
            Console.WriteLine("Nizovi uspesno generisani!"); */
            #endregion

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberDecimalDigits = 0;

            #region Bubble-Sort
            /*for (int i = 0; i < 7; i++)
            {

                Console.WriteLine($"Pokrecem Bubble-Sort za {wordsCount[i].ToString("N", nfi)} elemenata...");
                int[] niz = new int[1];
                using (StreamReader sr = new StreamReader($"{wordsCount[i]}.txt"))
                {
                    string text = sr.ReadToEnd();
                    niz = JsonConvert.DeserializeObject<int[]>(text);
                }

                //Console.WriteLine("Niz pre sortiranja: ");
                //StampajNiz(niz);

                stopWatch.Reset();
                stopWatch.Start();
                Algoritmi.BubbleSort(ref niz);
                stopWatch.Stop();

                //Console.WriteLine("Niz nakon sortiranja: ");
                //StampajNiz(niz);

                Console.WriteLine($"Bubble-Sort za {wordsCount[i].ToString("N", nfi)} elemenata zavrsen! Vreme: {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n\n");
            }*/
            #endregion

            #region Heap-Sort
            /*for (int i = 0; i < 7; i++)
            {

                Console.WriteLine($"Pokrecem Heap-Sort za {wordsCount[i].ToString("N", nfi)} elemenata...");
                int[] niz = new int[1];
                using (StreamReader sr = new StreamReader($"{wordsCount[i]}.txt"))
                {
                    string text = sr.ReadToEnd();
                    niz = JsonConvert.DeserializeObject<int[]>(text);
                }

                //Console.WriteLine("Niz pre sortiranja: ");
                //StampajNiz(niz);

                stopWatch.Reset();
                stopWatch.Start();
                Algoritmi.HeapSort(ref niz);
                stopWatch.Stop();

                if (i == 6)
                {
                    Console.WriteLine("Niz nakon sortiranja: ");
                    StampajNiz(niz);
                }


                Console.WriteLine($"Heap-Sort za {wordsCount[i].ToString("N", nfi)} elemenata zavrsen! Vreme: {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n\n");
            } */
            #endregion

            #region Bucket-Sort
            /*for (int i = 0; i < 7; i++)
            {

                Console.WriteLine($"Pokrecem Bucket-Sort za {wordsCount[i].ToString("N", nfi)} elemenata...");
                int[] niz = new int[1];
                using (StreamReader sr = new StreamReader($"{wordsCount[i]}.txt"))
                {
                    string text = sr.ReadToEnd();
                    niz = JsonConvert.DeserializeObject<int[]>(text);
                }

                ///Console.WriteLine("Niz pre sortiranja: ");
                //StampajNiz(niz);

                stopWatch.Reset();
                stopWatch.Start();
                Algoritmi.BucketSort(ref niz);
                stopWatch.Stop();

                //Console.WriteLine("Niz nakon sortiranja: ");
                //StampajNiz(niz);

                Console.WriteLine($"Bucket-Sort za {wordsCount[i].ToString("N", nfi)} elemenata zavrsen! Vreme: {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n\n");
            } */
            #endregion
        }




        private static void StampajNiz(int[] niz)
        {
            int size = niz.Length;
            for (int i = 0; i < size; i++)
                Console.Write(niz[i] + " ");
            Console.WriteLine();
        }

        private static void GenerisiNiz(int velicina)
        {
            int[] niz = new int[velicina];
            Random rnd = new Random();
            for (int i = 0; i < velicina; i++)
                niz[i] = rnd.Next(0, 10001);

            using (StreamWriter sw = new StreamWriter($"{velicina}.txt"))
            {
                string json = JsonConvert.SerializeObject(niz);
                sw.WriteLine(json);
            }
        }
    }
}

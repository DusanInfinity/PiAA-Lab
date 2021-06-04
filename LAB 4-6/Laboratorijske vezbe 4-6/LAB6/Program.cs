using LAB6.Klase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace LAB6
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] brCvorova = { 10, 100, 1000, 10000, 100000 };
            int[] brPotegaCoef = { 1, 2, 5, 10 };
            Stopwatch stopWatch = new Stopwatch();

            #region Generisanje grafova
            /*Console.WriteLine("Sledi generisanje grafova...");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 0 && j >= 2) // ako je broj cvorova 10, max br potega je 10*9/2 = 45 (svaki sa svakim)
                        continue;
                    GenerisiGrafove(brCvorova[i], brCvorova[i] * brPotegaCoef[j]);
                }
            } */
            #endregion


            #region Bellman-Fordov Algoritam
            /*Console.Write("Pokrecem Bellman-Fordov algoritam");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Console.Write($".{(i == 2 ? "\n\n\n" : "")}");
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 0 && j >= 2) // ako je broj cvorova 10, max br potega je 10*9/2 = 45 (svaki sa svakim)
                        continue;
                    Console.WriteLine($"Bellman-Fordov algoritam za {brCvorova[i]} cvorova i {brCvorova[i] * brPotegaCoef[j]} potega.");
                    Console.WriteLine($"Sledi citanje fajlova i kreiranje objekta grafa...");
                    Graf graf = Graf.ProcitajGraf(brCvorova[i], $"graf({brCvorova[i]}, {brCvorova[i] * brPotegaCoef[j]}).txt");
                    Console.WriteLine($"Graf procitan iz fajla, objekat kreiran, sledi izvrsenje Bellman-Fordovog algoritma...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    graf.BellmanFord(brCvorova[i] / 2);
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje Bellman-Fordovog algoritma za {brCvorova[i]} cvorova i {brCvorova[i] * brPotegaCoef[j]} potega je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n\n");
                }
            } */
            #endregion

            #region Dijkstra Algoritam
            Console.Write("Pokrecem Dijkstra algoritam");
            for (int i = 1, max = 8; i <= max; i++)
            {
                Thread.Sleep(1000);
                Console.Write($".{(i == max ? "\n\n\n" : "")}");
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 0 && j >= 2) // ako je broj cvorova 10, max br potega je 10*9/2 = 45 (svaki sa svakim)
                        continue;
                    Console.WriteLine($"Dijkstra algoritam za {brCvorova[i]} cvorova i {brCvorova[i] * brPotegaCoef[j]} potega.");
                    Console.WriteLine($"Sledi citanje fajlova i kreiranje objekta grafa...");
                    Graf graf = Graf.ProcitajGraf(brCvorova[i], $"graf({brCvorova[i]}, {brCvorova[i] * brPotegaCoef[j]}).txt");
                    Console.WriteLine($"Graf procitan iz fajla, objekat kreiran, sledi izvrsenje Dijkstra algoritma...");
                    stopWatch.Reset();
                    stopWatch.Start();
                    graf.Dijkstra(brCvorova[i] / 2);
                    stopWatch.Stop();
                    Console.WriteLine($"Izvrsenje Dijkstra algoritma za {brCvorova[i]} cvorova i {brCvorova[i] * brPotegaCoef[j]} potega je zavrsen za {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n\n");
                }
            }
            #endregion
        }

        private static void GenerisiGrafove(int brCvorova, int brPotega)
        {
            try
            {
                Random rnd = new Random();
                Dictionary<string, bool> svipotezi = new Dictionary<string, bool>();
                using (StreamWriter sw = new StreamWriter($"graf({brCvorova}, {brPotega}).txt"))
                {
                    // Da bi graf bio povezan, prvo generisemo potege izmedju susednih cvorova kako bi bili sigurni da je generisan povezani graf

                    int u, v, cena;
                    for (int i = 0; i < brCvorova - 1; i++)
                    {
                        u = i;
                        v = i + 1;
                        cena = rnd.Next(1, 101);
                        sw.WriteLine($"{u}_{v}_{cena}");
                        svipotezi.Add($"{u}_{v}", true);
                    }
                    brPotega -= (brCvorova - 1); // oduzimamo potege koji povezuju susedne cvorove iz ukupnog broja

                    for (int br = 1; br <= brPotega; br++)
                    {
                        do
                        {
                            u = rnd.Next(0, brCvorova);
                            v = rnd.Next(0, brCvorova);
                            while (v == u) v = rnd.Next(0, brCvorova);
                        } while (svipotezi.ContainsKey($"{u}_{v}") || svipotezi.ContainsKey($"{v}_{u}"));

                        cena = rnd.Next(1, 101);

                        sw.WriteLine($"{u}_{v}_{cena}");
                        svipotezi.Add($"{u}_{v}", true);
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

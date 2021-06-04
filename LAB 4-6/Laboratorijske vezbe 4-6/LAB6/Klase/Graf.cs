using LAB4.FibonacciHeap;
using System;
using System.Collections.Generic;
using System.IO;

namespace LAB6.Klase
{
    class Graf
    {
        protected int BrCvorova { get; set; }
        protected List<Cvor> ListaCvorova { get; set; }
        protected List<Poteg> ListaPotega;

        public Graf(int br)
        {
            BrCvorova = br;
            ListaCvorova = new List<Cvor>();

            for (int i = 0; i < BrCvorova; i++)
                ListaCvorova.Add(new Cvor(i, int.MaxValue, null));

            ListaPotega = new List<Poteg>();
        }

        public static Graf ProcitajGraf(int brCvorova, string file)
        {
            Graf graf = new Graf(brCvorova);
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split("_");
                    if (!int.TryParse(args[0], out int brPrvog))
                        throw new Exception($"Broj prvog cvora nije int! ({args[0]})");
                    if (!int.TryParse(args[1], out int brDrugog))
                        throw new Exception($"Broj drugog cvora nije int! ({args[1]})");
                    if (!int.TryParse(args[2], out int cena))
                        throw new Exception($"Cena potega nije int! ({args[2]})");

                    graf.ListaPotega.Add(new Poteg(graf.ListaCvorova[brPrvog], graf.ListaCvorova[brDrugog], cena));
                }
            }
            return graf;
        }


        public void InitSingleSource(int source)
        {
            if (source >= BrCvorova)
                return;

            // Dodavanje cvorova u listu cvorova se radi u konstruktoru zbog ideje citanja iz fajla
            foreach (Cvor cvor in ListaCvorova)
            {
                cvor.Prethodnik = null;
                cvor.FNode = null;
                cvor.Distanca = int.MaxValue;
            }

            ListaCvorova[source].Distanca = 0;
        }

        public static void Relax(Cvor izvor, Cvor odrediste, int rastojanje)
        {
            try // Dodajemo prazan try/catch zbog checked-a (checked proverava da li je doslo do prekoracenja, ako jeste -> exception)
            {
                if (odrediste.Distanca > checked(izvor.Distanca + rastojanje))
                {
                    odrediste.Distanca = izvor.Distanca + rastojanje;
                    odrediste.Prethodnik = izvor;
                }
            }
            catch { }
        }

        public void Print(int src)
        {
            if (src >= BrCvorova)
                return;

            Console.WriteLine($"Pocetni cvor: {src}");

            foreach (Cvor cvor in ListaCvorova)
            {
                if (cvor.Oznaka != src)
                {
                    int prethodnik = cvor.Prethodnik != null ? cvor.Prethodnik.Oznaka : -1;
                    Console.WriteLine($"[Cvor: {cvor.Oznaka}] Distanca do pocetnog cvora: {cvor.Distanca} Prethodnik: {prethodnik}");
                }
            }
        }



        public void BellmanFord(int source)
        {
            InitSingleSource(source);

            for (int i = 1; i < BrCvorova; i++)
            {
                foreach (Poteg poteg in ListaPotega)
                {
                    Relax(poteg.Izvor, poteg.Odrediste, poteg.Cena);
                }
            }

            //Print(source);
        }

        public void Dijkstra(int source)
        {
            InitSingleSource(source);

            Dictionary<int, bool> ObradjeniCvorovi = new Dictionary<int, bool>();

            FHeap heap = new FHeap();

            foreach (Cvor cvor in ListaCvorova)
            {
                cvor.FNode = heap.Insert(cvor.Distanca, cvor);
            }

            while (heap.N > 0)
            {
                FNode node = heap.ExtractMin();
                Cvor cvor = (Cvor)node.Data;

                if (cvor.Distanca == int.MaxValue)
                    break; // prekid u grafu


                ObradjeniCvorovi.Add(cvor.Oznaka, true);
                foreach (Poteg poteg in ListaPotega)
                {
                    if (poteg.Izvor == cvor && !ObradjeniCvorovi.ContainsKey(poteg.Odrediste.Oznaka))
                    {
                        int prethodnaDistanca = poteg.Odrediste.Distanca;

                        Relax(cvor, poteg.Odrediste, poteg.Cena);

                        if (prethodnaDistanca != poteg.Odrediste.Distanca)
                            heap.DecreaseKey(poteg.Odrediste.FNode, poteg.Odrediste.Distanca);
                    }
                }
            }

        }
    }
}

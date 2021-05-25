using System;
using System.Collections.Generic;
using System.IO;

namespace LAB5.Klase
{
    public class Graf : Subset
    {
        protected readonly List<Cvor> ListaCvorova;
        protected List<Poteg> ListaPotega;

        public Graf(int brCvorova)
        {
            ListaCvorova = new List<Cvor>(brCvorova);
            for (int i = 0; i < brCvorova; i++)
                ListaCvorova.Add(new Cvor(i));

            ListaPotega = new List<Poteg>();
        }

        protected void SelectionSort()
        {
            int brPotega = ListaPotega.Count;
            for (int i = 0; i < brPotega - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < brPotega; j++)
                {
                    if (ListaPotega[min].Cena > ListaPotega[j].Cena)
                        min = j;
                }

                if (min != i)
                {
                    Poteg temp = ListaPotega[i];
                    ListaPotega[i] = ListaPotega[min];
                    ListaPotega[min] = temp;
                }
            }
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

        #region Kruskalov Algoritam
        public void KruskalMST()
        {
            int brCvorova = ListaCvorova.Count;
            List<Poteg> mstLista = new List<Poteg>(brCvorova);

            //SelectionSort();
            ListaPotega.Sort();

            // Lista podskupova za svaki cvor
            List<SubsetCvor> subsets = new List<SubsetCvor>(brCvorova);
            MakeSet(subsets, ListaCvorova);

            int brDodatihPotega = 0; // An index variable, used for result[]
            int i = 0; // Index u sortiranoj listi potega

            // Broj dodatih potega treba da bude jednak brCvorova-1 (npr. za 3 cvora dovoljne su 2 grane da bi graf bio povezan)
            while (brDodatihPotega < brCvorova - 1)
            {
                // Uzimamo poteg sa najmanjom cenom i povecavamo brojac
                Poteg next_edge = ListaPotega[i++];

                // Pronalazimo root cvorove za prvi i drugi cvor datog potega
                Cvor x = FindSet(subsets, next_edge.Prvi);
                Cvor y = FindSet(subsets, next_edge.Drugi);

                // Ako root nije isti, to znaci da su u pitanju 2 razlicita podskupa, vrsimo uniju i dodajemo dati poteg u listu mst potega
                // Ako je root isti, to znaci da pripadaju istom podskupu i da je dati poteg nepotreban, pa se odbacuje
                if (x != y)
                {
                    mstLista.Add(next_edge);
                    brDodatihPotega++;
                    Union(subsets, x, y);
                }
            }

            Console.WriteLine("Minimalno sprezno stablo nakon Kruskalovog algoritma izgleda ovako:");
            int mstCena = 0;
            foreach (Poteg poteg in mstLista)
            {
                //Console.WriteLine($"{poteg.Prvi.Oznaka} ---- {poteg.Drugi.Oznaka} => Cena: {poteg.Cena}");
                mstCena += poteg.Cena;
            }
            Console.WriteLine($"Minimalna cena spreznog stabla: {mstCena}");
        }
        #endregion

        #region Boruvkin Algoritam
        public void BoruvkaMST()
        {
            int brCvorova = ListaCvorova.Count;
            List<Poteg> mstLista = new List<Poteg>(brCvorova);

            // Lista podskupova za svaki cvor
            List<SubsetCvor> subsets = new List<SubsetCvor>(brCvorova);
            MakeSet(subsets, ListaCvorova);

            // Lista najjeftinijih grana za svaki podskup
            List<int> najjeftinijeGrane = new List<int>(brCvorova);
            for (int i = 0; i < brCvorova; i++)
                najjeftinijeGrane.Add(-1); // inicijalizacija najjeftinijih grana


            int brSkupova = brCvorova;
            int brPotega = ListaPotega.Count;

            // Izvrsavamo kod u petlji sve dok ne dobijemo 1 podskup -> spojeni svi cvorovi
            while (brSkupova > 1)
            {
                // Ponavljamo inicijalizaciju svaki put
                for (int i = 0; i < brCvorova; i++)
                    najjeftinijeGrane[i] = -1;

                // Unos cene za sve aktivne podskupove
                for (int i = 0; i < brPotega; i++)
                {
                    // Pronalazimo oznake root-a u podskupu za prvi i drugi cvor datog potega
                    int set1 = FindSet(subsets, ListaPotega[i].Prvi).Oznaka;
                    int set2 = FindSet(subsets, ListaPotega[i].Drugi).Oznaka;

                    // Ako oznake nisu iste, to znaci da su u pitanju 2 razlicita podskupa, proveravamo da li imamo novi najjeftiniji poteg
                    if (set1 != set2)
                    {
                        if (najjeftinijeGrane[set1] == -1 ||
                            ListaPotega[najjeftinijeGrane[set1]].Cena > ListaPotega[i].Cena)
                            najjeftinijeGrane[set1] = i;

                        if (najjeftinijeGrane[set2] == -1 ||
                            ListaPotega[najjeftinijeGrane[set2]].Cena > ListaPotega[i].Cena)
                            najjeftinijeGrane[set2] = i;
                    }
                }


                for (int i = 0; i < brCvorova; i++)
                {
                    // Provera da li postoji najjeftiniji poteg za dati podskup
                    if (najjeftinijeGrane[i] != -1)
                    {
                        Poteg poteg = ListaPotega[najjeftinijeGrane[i]];

                        Cvor set1 = FindSet(subsets, poteg.Prvi);
                        Cvor set2 = FindSet(subsets, poteg.Drugi);

                        // Ako root nije isti, to znaci da su u pitanju 2 razlicita podskupa, vrsimo uniju i dodajemo dati poteg u listu mst potega
                        if (set1 != set2)
                        {
                            mstLista.Add(poteg);
                            Union(subsets, set1, set2);
                            brSkupova--;
                        }
                    }
                }
            }

            Console.WriteLine("Minimalno sprezno stablo nakon Boruvkinog algoritma izgleda ovako:");
            int mstCena = 0;
            foreach (Poteg poteg in mstLista)
            {
                //Console.WriteLine($"{poteg.Prvi.Oznaka} ---- {poteg.Drugi.Oznaka} => Cena: {poteg.Cena}");
                mstCena += poteg.Cena;
            }
            Console.WriteLine($"Minimalna cena spreznog stabla: {mstCena}");
        }
        #endregion
    }
}

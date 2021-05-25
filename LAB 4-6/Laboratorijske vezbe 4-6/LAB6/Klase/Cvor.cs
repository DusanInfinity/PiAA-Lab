using LAB4.FibonacciHeap;

namespace LAB6.Klase
{
    class Cvor
    {
        public int Oznaka { get; set; }
        public int Distanca { get; set; }
        public Cvor Prethodnik { get; set; }

        public FNode FNode { get; set; } // Koristi se za dijkrstra algoritam

        public Cvor(int oznaka, int dist, Cvor prethodnik)
        {
            Oznaka = oznaka;
            Distanca = dist;
            Prethodnik = prethodnik;
            FNode = null;
        }
    }
}

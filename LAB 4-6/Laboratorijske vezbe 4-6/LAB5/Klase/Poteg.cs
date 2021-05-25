using System;

namespace LAB5.Klase
{
    public class Poteg : IComparable<Poteg>
    {
        public Cvor Prvi { get; set; }
        public Cvor Drugi { get; set; }
        public int Cena { get; set; }

        public Poteg(Cvor cvor1, Cvor cvor2, int cena)
        {
            Prvi = cvor1;
            Drugi = cvor2;
            Cena = cena;
        }

        public int CompareTo(Poteg poteg)
        {
            return Cena.CompareTo(poteg.Cena);
        }
    }
}

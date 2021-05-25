using System;

namespace LAB6.Klase
{
    class Poteg : IComparable<Poteg>
    {
        public Cvor Izvor { get; set; }
        public Cvor Odrediste { get; set; }
        public int Cena { get; set; }

        public Poteg(Cvor izvor, Cvor odrediste, int cena)
        {
            Izvor = izvor;
            Odrediste = odrediste;
            Cena = cena;
        }

        public int CompareTo(Poteg poteg)
        {
            return Cena.CompareTo(poteg.Cena);
        }
    }
}

namespace LAB5.Klase
{
    class SubsetCvor
    {
        public Cvor Roditelj { get; set; }
        public int Rank { get; set; }
        public int NajmanjaCena { get; set; }

        public SubsetCvor(Cvor roditelj, int rank, int najmanjaCena = -1)
        {
            Roditelj = roditelj;
            Rank = rank;
            NajmanjaCena = najmanjaCena;
        }
    }
}

using System.Collections.Generic;

namespace LAB5.Klase
{
    class Subset
    {
        protected void MakeSet(List<SubsetCvor> subsets, List<Cvor> listaCvorova)
        {
            subsets.Clear(); // brisemo sve za svaki slucaj
            foreach (Cvor cvor in listaCvorova)
            {
                subsets.Add(new SubsetCvor(cvor, 0));
            }
        }

        protected Cvor FindSet(List<SubsetCvor> subsets, Cvor i)
        {
            // path compression
            if (subsets[i.Oznaka].Roditelj != i)
                subsets[i.Oznaka].Roditelj = FindSet(subsets, subsets[i.Oznaka].Roditelj);

            return subsets[i.Oznaka].Roditelj;
        }

        // union by rank
        protected void Union(List<SubsetCvor> subsets, Cvor x, Cvor y)
        {
            Cvor xroot = FindSet(subsets, x);
            Cvor yroot = FindSet(subsets, y);

            // podskup sa manjim rankom se nadovezuje na podskup sa vecim 
            if (subsets[xroot.Oznaka].Rank < subsets[yroot.Oznaka].Rank)
                subsets[xroot.Oznaka].Roditelj = yroot;
            else if (subsets[xroot.Oznaka].Rank > subsets[yroot.Oznaka].Rank)
                subsets[yroot.Oznaka].Roditelj = xroot;
            else
            { // rankovi isti, biramo jednog da bude roditelj (x)
                subsets[yroot.Oznaka].Roditelj = xroot;
                subsets[xroot.Oznaka].Rank++;
            }
        }
    }
}

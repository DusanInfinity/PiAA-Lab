using System;
using System.IO;

namespace LAB4.BinomniHeap
{
    public class BHeap
    {
        public BNode Head { get; set; }

        public BHeap()
        {
            Head = null;
        }
        public BHeap(BNode head)
        {
            Head = head;
        }

        public static BHeap ProcitajHeap(string file)
        {
            BHeap heap = new BHeap();
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    string brString = sr.ReadLine();
                    if (!int.TryParse(brString, out int broj))
                        throw new Exception($"Vrednost elementa nije broj! ({brString})");

                    heap = heap.Insert(broj);
                }
            }
            return heap;
        }

        public BNode Minimum()
        {
            BNode currMin = null, x = Head;
            int min = int.MaxValue;
            while (x != null)
            {
                if (x.Key < min)
                {
                    min = x.Key;
                    currMin = x;
                }
                x = x.Sibling;
            }
            return currMin;
        }

        public BNode LinkNodes(BNode newParent, BNode newChild)
        {
            if (newParent.Key > newChild.Key)
            { // Osiguramo se da novi roditelj ima manju vrednost kljuca
                BNode temp = newParent;
                newParent = newChild;
                newChild = temp;
            }

            newChild.Parent = newParent;
            newChild.Sibling = newParent.Child;
            newParent.Child = newChild;
            newParent.Degree++;

            return newParent;
        }

        public BHeap Insert(int key)
        {
            BNode node = new BNode(null, null, null, key, 0);
            BHeap newHeap = new BHeap(node);
            return Union(newHeap);
        }

        private void LinkNextRootNode(ref BNode curr, ref BNode next)
        {
            if (curr == null)
                Head = next;
            else
                curr.Sibling = next;

            curr = next;
            next = next.Sibling;
        }

        public BNode Union_Merge(BHeap firstHeap, BHeap secHeap)
        {
            BNode first = firstHeap.Head;
            BNode second = secHeap.Head;
            BNode newHeapNode = null;

            while (first != null && second != null)
            {
                // Redjamo stabla u rastucem redosledu po stepenu
                if (first.Degree <= second.Degree)
                {
                    LinkNextRootNode(ref newHeapNode, ref first);
                }
                else
                {
                    LinkNextRootNode(ref newHeapNode, ref second);
                }
            }

            // Ako su preostali neka stabla iz prvog heap-a, nadovezujemo ih
            while (first != null)
                LinkNextRootNode(ref newHeapNode, ref first);

            // Ako su preostali neka stabla iz drugog heap-a, nadovezujemo ih
            while (second != null)
                LinkNextRootNode(ref newHeapNode, ref second);

            return Head;
        }

        public BHeap Union(BHeap secHeap)
        {
            BHeap newHeap = new BHeap();
            newHeap.Union_Merge(this, secHeap);

            if (newHeap.Head == null)
                return newHeap;

            BNode prev = null, cur = newHeap.Head, next = cur.Sibling;

            while (next != null)
            {
                if (cur.Degree != next.Degree || next.Sibling != null && next.Sibling.Degree == cur.Degree)
                { // slucaj 1 i 2
                    prev = cur;
                    cur = next;
                }
                else
                {
                    if (cur.Key <= next.Key)
                    { // slucaj 3
                        cur.Sibling = next.Sibling;
                        LinkNodes(cur, next);
                    }
                    else
                    {
                        // slucaj 4
                        if (prev == null)
                        {
                            newHeap.Head = next;
                        }
                        else
                            prev.Sibling = next;
                        LinkNodes(next, cur);
                        cur = next;
                    }
                }
                next = cur.Sibling;
            }
            return newHeap;
        }


        public BNode FindMin()
        {
            if (Head == null)
                return null;

            BNode min = Head;
            BNode temp = Head.Sibling;

            while (temp != null)
            {
                if (temp.Key < min.Key)
                    min = temp;

                temp = temp.Sibling;
            }
            return min;
        }

        public static BHeap ReverseOrderAndReturnHeap(BNode min)
        {
            BHeap heap = new BHeap();
            BNode temp = min.Child;
            BNode sibling;


            BNode curr = null;

            while (temp != null)
            {
                temp.Parent = null; // vise nema oca, obrisan je
                sibling = temp.Sibling;
                if (curr != null)
                {
                    temp.Sibling = curr;
                    curr = temp;
                }
                else
                {
                    curr = temp;
                    temp.Sibling = null;
                }
                //heap.LinkNextRootNode(ref curr, ref temp);
                temp = sibling;
            }
            heap.Head = curr;
            return heap;
        }

        public BHeap ExtractMin()
        {
            BNode min = FindMin(); // Pronalazak minimuma
            if (min == null)
                return this; // ne vracamo null da ne bi pucao program

            BHeap newHeap = new BHeap();
            BNode node = this.Head;
            BNode last = null;

            while (node != null) // Dodavanje svih osim minimuma
            {
                if (node != min)
                    newHeap.LinkNextRootNode(ref last, ref node);
                else
                {
                    node = node.Sibling;

                    if (last != null) // Provera da slucajno minimum nije prvi u lancu(head)
                        last.Sibling = null; // Ako je min poslednji u lancu, prethodnik treba da ukazuje na null
                }

            }

            BHeap secondNewHeap = ReverseOrderAndReturnHeap(min); // Reverse
            newHeap = newHeap.Union(secondNewHeap); // unija
            return newHeap;
        }

        public void DecreaseKey(BNode node, int newKeyValue)
        {
            if (node == null || newKeyValue > node.Key)
                return;

            node.Key = newKeyValue;

            BNode curr = node;
            BNode parent = curr.Parent;

            while (parent != null && curr.Key < parent.Key)
            {
                curr.ExchangeWith(parent);
                curr = parent;
                parent = curr.Parent;
            }
        }

        public BNode FindNode(BNode node, int key)
        {
            if (node == null)
                return null;

            if (node.Key == key)
                return node;

            if (node.Key < key) // obzirom da je minHeap, ako je dati cvor veci od vrednosti key, svi njegovi potomci su isto veci od vrednosti jer su veci od njega
            {
                BNode searchChild = FindNode(node.Child, key);
                if (searchChild != null)
                    return searchChild;
            }

            return FindNode(node.Sibling, key);
        }


        public BHeap Delete(BNode node)
        {
            DecreaseKey(node, int.MinValue);

            return ExtractMin();
        }

        private void PrintNodeData(BNode h)
        {
            while (h != null)
            {
                string childOf = h.Parent != null ? $"[P:{h.Parent.Key}]" : "";
                string nextSibling = h.Sibling != null ? $"[NextS: {h.Sibling.Key}] " : "";

                Console.WriteLine($"Key: {h.Key} {childOf}{nextSibling}");
                PrintNodeData(h.Child);
                h = h.Sibling;
            }
        }

        public void PrintHeap()
        {
            PrintNodeData(Head);
        }
    }
}

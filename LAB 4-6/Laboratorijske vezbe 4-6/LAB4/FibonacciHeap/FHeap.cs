using System;
using System.Collections.Generic;

namespace LAB4.FibonacciHeap
{
    public class FHeap
    {
        public FNode Min { get; set; }
        public int N { get; set; }

        public FHeap()
        {
            Min = null;
            N = 0;
        }

        public FNode Insert(int key, object data = null)
        {
            FNode node = new FNode(null, null, key, 0, false, data);

            node.AddToList(Min);
            if (Min == null || node.Key < Min.Key)
                Min = node;

            N++;

            return node;
        }

        public FNode FindMin()
        {
            return Min;
        }


        /*public FHeap Union(FHeap second)
        {
            FHeap newHeap = new FHeap();
            newHeap.Min = this.Min;
            // TO-DO konkatenacija root listi
            if (this.Min == null || (second.Min != null && second.Min.Key < this.Min.Key))
                newHeap.Min = second.Min;

            newHeap.N = this.N + second.N;
            return newHeap;
        } */

        public FNode ExtractMin()
        {
            FNode min = Min;
            if (min != null)
            {
                FNode child = min.Child;
                if (child != null)
                {
                    child.Parent = null;
                    FNode next = child.Right;
                    child.AddToList(min);

                    while (next != null && next != child)
                    {
                        next.Parent = null;
                        FNode temp = next.Right;
                        next.AddToList(min);
                        next = temp;
                    }
                }

                if (min == min.Right)
                { // Z je ustvari jedini element u root listi
                    Min = null;
                }
                else
                {
                    Min = min.Right;
                    min.RemoveFromList();
                    Consolidate();
                }
                N--;
            }
            return min;
        }

        private void Consolidate()
        {
            List<FNode> A = new List<FNode>(N);
            for (int i = 0; i < N; i++)
                A.Add(null);

            FNode node = Min != null ? Min.Right : null;
            while (node != null)
            {
                FNode x = node;
                int d = node.Degree;
                FNode next = node.Right;
                while (A[d] != null)
                {

                    FNode y = A[d];
                    if (x.Key > y.Key)
                    {
                        FNode temp = x;
                        x = y;
                        y = temp;
                    }

                    Link(x, y);
                    A[d] = null;
                    d++;
                }
                A[d] = x;

                // Ako je poslednji obradjeni cvor Min, to znaci da smo obradili sve cvorove u root listi
                if (node == Min)
                    break;

                node = next;
            }

            Min = null;
            for (int i = 0; i < N; i++)
            {
                if (A[i] != null)
                {
                    A[i].AddToList(Min);
                    if (Min == null || Min.Key > A[i].Key)
                        Min = A[i];
                }
            }
        }

        private void Link(FNode parent, FNode newChild)
        {
            newChild.RemoveFromList();

            newChild.AddToList(parent.Child);
            parent.Child = newChild;
            parent.Degree++;
            newChild.Parent = parent;
            newChild.Mark = false;
        }

        public void DecreaseKey(FNode node, int newKeyValue)
        {
            if (node == null || newKeyValue > node.Key)
                return;

            node.Key = newKeyValue;


            FNode parent = node.Parent;

            if (parent != null && node.Key < parent.Key)
            {
                Cut(node, parent);
                CascadingCut(parent);
            }

            if (Min == null || node.Key < Min.Key)
            {
                Min = node;
            }
        }

        private void Cut(FNode child, FNode parent)
        {
            if (parent.Child == child)
            {
                if (child.Right != child)
                    parent.Child = child.Right;
                else
                    parent.Child = null; // Da li ovde treba null ili ipak child.Child? Da li odvajamo decu od child cvora
            }
            parent.Degree -= child.Degree + 1; // Ako ne odvajamo decu od child-a, onda bi trebao da se oduzme child.Degree + 1 jer se odvajaju svi child potomci +1 za dati child cvor

            child.RemoveFromList();
            child.AddToList(Min);
            child.Parent = null;
            child.Mark = false;
        }

        private void CascadingCut(FNode y)
        {
            FNode z = y.Parent;
            if (z != null)
            {
                if (y.Mark == false)
                    y.Mark = true;
                else
                {
                    Cut(y, z);
                    CascadingCut(z);
                }
            }
        }

        public FNode Delete(FNode node)
        {
            DecreaseKey(node, int.MinValue);

            return ExtractMin();
        }


        private void PrintNodeData(FNode h)
        {
            string childOf = h.Parent != null ? $"[P:{h.Parent.Key}]" : "";
            string leftSibling = h.Left != null ? $"[Left: {h.Left.Key}]" : "";
            string rightSibling = h.Right != null ? $"[Right: {h.Right.Key}]" : "";
            string child = h.Child != null ? $"[Child: {h.Child.Key}]" : "";

            Console.WriteLine($"Key: {h.Key} {childOf}{leftSibling}{rightSibling}{child}");
            PrintNodes(h.Child);
        }

        private void PrintNodes(FNode h)
        {
            if (h == null)
                return;

            FNode thisNode = h;
            PrintNodeData(h);
            h = h.Right;
            while (h != null && h != thisNode)
            {
                PrintNodeData(h);
                h = h.Right;
            }
        }


        public void PrintHeap()
        {
            PrintNodes(Min);
        }
    }
}

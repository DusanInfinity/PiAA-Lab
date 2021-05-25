namespace LAB4.FibonacciHeap
{
    public class FNode
    {
        public FNode Parent { get; set; }
        public FNode Child { get; set; }
        public FNode Left { get; set; }
        public FNode Right { get; set; }
        public int Key { get; set; }
        public int Degree { get; set; }
        public bool Mark { get; set; }

        public object Data { get; set; } // koristi se za Dijkstra algoritam

        public FNode(FNode parent, FNode child, int key, int degree, bool mark, object data)
        {
            Parent = parent;
            Child = child;
            Left = this;
            Right = this;
            Key = key;
            Degree = degree;
            Mark = mark;
            Data = data;
        }


        public void AddToList(FNode min)
        {
            if (min != null)
            {
                this.Left = min.Left;
                min.Left.Right = this;

                this.Right = min;
                min.Left = this;
            }
            else // Ako je prazna trenutna lista
            {
                this.Left = this;
                this.Right = this;
            }

        }

        public void RemoveFromList()
        {
            if (this.Left != this && this.Right != this) // Ako se trenutna lista NE sastoji samo od this cvora
            {
                this.Left.Right = this.Right;
                this.Right.Left = this.Left;
            }

            this.Left = this;
            this.Right = this;
        }
    }
}

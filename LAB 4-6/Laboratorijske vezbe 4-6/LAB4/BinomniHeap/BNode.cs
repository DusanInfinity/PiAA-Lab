namespace LAB4.BinomniHeap
{
    public class BNode
    {
        public BNode Parent { get; set; }
        public BNode Child { get; set; }
        public BNode Sibling { get; set; }
        public int Key { get; set; }
        public int Degree { get; set; }

        public BNode(BNode parent, BNode child, BNode sibling, int key, int degree)
        {
            Parent = parent;
            Child = child;
            Sibling = sibling;
            Key = key;
            Degree = degree;
        }


        public void ExchangeWith(BNode b2)
        {
            BNode temp = new BNode(Parent, Child, Sibling, Key, Degree);
            Parent = b2.Parent;
            Child = b2.Child;
            Sibling = b2.Sibling;
            Key = b2.Key;
            Degree = b2.Degree;

            b2.Parent = temp.Parent;
            b2.Child = temp.Child;
            b2.Sibling = temp.Sibling;
            b2.Key = temp.Key;
            b2.Degree = temp.Degree;
        }
    }
}

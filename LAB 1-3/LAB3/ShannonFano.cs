namespace LAB3
{
    public class ShannonFano
    {
        public char Char { get; set; }
        public string Code { get; set; }
        public int AppearenceNum { get; set; }
        public float Frequency { get; set; }

        public ShannonFano(char ch)
        {
            Char = ch;
            AppearenceNum = 1;
            Frequency = 0.0f;
            Code = "";
        }
    }
}

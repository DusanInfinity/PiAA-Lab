using LAB3.Strukture_podataka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LAB3
{
    public class Algoritmi
    {
        #region Shannon Fano
        public static void ShannonFano(string fileName, string text)
        {
            Dictionary<char, ShannonFano> AllCharacters = new Dictionary<char, ShannonFano>();
            int charactersNum = 0;
            foreach (char ch in text)
            {
                charactersNum++;

                if (AllCharacters.TryGetValue(ch, out ShannonFano sf))
                {
                    sf.AppearenceNum++;
                    continue;
                }

                AllCharacters.Add(ch, new ShannonFano(ch));
            }

            int charNum = AllCharacters.Count;
            SortList sortedChars = new SortList(charNum);

            foreach (var kvp in AllCharacters)
            {
                ShannonFano sf = kvp.Value;
                sf.Frequency = (float)sf.AppearenceNum / charactersNum;
                sortedChars.Add(sf);
            }

            SfRecursive(sortedChars);
            PrintElements(sortedChars);


            using (StreamWriter sw = new StreamWriter($"{fileName}.txt"))
            {
                sw.Write(text);
            }

            StringBuilder sbEncodedText = new StringBuilder();
            int bitsNum = 0;
            foreach (char ch in text)
            {
                sbEncodedText.Append(AllCharacters[ch].Code);
                bitsNum += AllCharacters[ch].Code.Length;
            }
            string encodedStr = sbEncodedText.ToString();

            using (BinaryWriter sw = new BinaryWriter(File.Open($"{fileName}.compressed", FileMode.Create), Encoding.ASCII))
            {
                byte[] array = GetBytes(encodedStr);
                foreach (byte b in array)
                    sw.Write(b);
            }

            using (StreamWriter sw = new StreamWriter($"CODE_{fileName}.txt"))
            {
                foreach (var kvp in AllCharacters)
                {
                    sw.Write($"{kvp.Value.Char}={kvp.Value.Code} ");
                }
                sw.Write(bitsNum);
            }
        }

        public static void SfDeCompressFile(string fileName)
        {
            StringBuilder data = new StringBuilder();
            Dictionary<string, char> AllCharacters = new Dictionary<string, char>();
            int bitNum = 0;
            using (StreamReader sr = new StreamReader($"CODE_{fileName}.txt"))
            {
                string str = sr.ReadToEnd();
                string[] textSplit = str.Split(" ");

                foreach (string ch in textSplit)
                {
                    string[] sfArray = ch.Split("=");
                    if (sfArray.Length == 2)
                    {
                        AllCharacters.Add(sfArray[1], sfArray[0][0]);
                    }
                }
                bitNum = int.Parse(textSplit[textSplit.Length - 1]);
            }

            int bytesNum = bitNum / 8 + ((bitNum % 8 != 0) ? 1 : 0);
            int readedBytes = 0;
            using (BinaryReader br = new BinaryReader(File.Open($"{fileName}.compressed", FileMode.Open), Encoding.ASCII))
            {
                while (readedBytes < bytesNum && br.BaseStream.Position < br.BaseStream.Length)
                {
                    data.Append(Convert.ToString(br.ReadByte(), 2).PadLeft(8, '0'));
                    readedBytes++;
                }
            }

            int dataLength = data.Length;
            string code = "";
            for (int i = 0; i < dataLength && i < bitNum; i++)
            {
                code += data[i];
                if (AllCharacters.TryGetValue(code, out char ch))
                {
                    Console.Write(ch);
                    code = ""; // resetujemo kod
                }
            }
        }

        public static byte[] GetBytes(string bitString)
        { // https://stackoverflow.com/questions/2989695/how-to-convert-a-string-of-bits-to-byte-array
            int strlen = bitString.Length;
            int size = strlen / 8 + ((strlen % 8 != 0) ? 1 : 0); // strlen/8 zbog bajtova i ukoliko strlen%8 nije 0 to znaci da imamo jos jedan bajt koji nije celi(manje od 8 bitova) pa ga dopunjavamo nulama
            byte[] output = new byte[size];

            for (int i = 0; i < size; i++)
            {
                for (int b = 0; b <= 7; b++)
                {
                    char ch = (strlen > i * 8 + b) ? bitString[i * 8 + b] : '0'; // ukoliko je prekoracenje, postavljamo 0 (za poslednji bajt, ukoliko strlen%8 nije 0
                    output[i] |= (byte)((ch == '1' ? 1 : 0) << (7 - b));
                }
            }

            return output;
        }

        public static void SfRecursive(SortList list)
        {
            int elNum = list.Count;
            if (elNum < 2)
                return;

            SortList leftList = new SortList(elNum);
            SortList rightList = new SortList(elNum);

            ShannonFano firstElement = list[0];

            leftList.Add(firstElement); // prvi cvor je najveci i sigurno ide u levu listu
            firstElement.Code += "0"; // kod 0

            int addedLeft = 1;
            float freqLeft = firstElement.Frequency;
            float freqRight = 0;

            int i = elNum - 1; // indeks poslednjeg elementa u listi
            while (i >= addedLeft)
            {
                ShannonFano currEl = list[i]; // naredni element sa desne nalevo
                float elementFreq = currEl.Frequency;

                ShannonFano nextEl = list[addedLeft]; // naredni element sa leve strane
                float nextElFreq = nextEl.Frequency;

                float addToLeftDiff = freqLeft + nextElFreq; // vrednost ukupne frekvencije ako dodamo naredni levi element u levu listu
                float addToRightDiff = freqRight + elementFreq; // vrednost ukupne frekvencije ako dodamo naredni desni element u desnu listu

                if (Math.Abs(addToLeftDiff - freqRight) > Math.Abs(freqLeft - addToRightDiff))
                { // ako je razlika izmedju leve i desne liste u slucaju dodavanja u levu listu veca od razlike prilikom dodavanja u desnu listu, dodajemo u desnu
                    rightList.Add(currEl);
                    freqRight += elementFreq;
                    currEl.Code += "1";
                    i--; // dekrementiramo i, jer smo dodali dati element u listu
                }
                else
                {
                    leftList.Add(nextEl);
                    freqLeft += nextElFreq;
                    nextEl.Code += "0";
                    addedLeft++;
                }
            }

            SfRecursive(leftList);
            SfRecursive(rightList);
        }


        public static void PrintElements(SortList list)
        {
            /*SortedList<float, string> listt = new SortedList<float, string>();
            listt[5.0f] 
            foreach (var kvp in list) // Koriscena je SortedList-a
            {
                ShannonFano element = kvp.Value; // PITANJE ZA PROFESORA OVDE - Da li je bolje da kreiramo referencu na element ili da stalno koristimo getter Value, koliko to utice na performanse?

                Console.WriteLine($"Element: {element.Char} Frekvencija pojavljivanja: {element.Frequency} Kod: {element.Code}");
            }*/

            foreach (ShannonFano element in list)
            {
                Console.WriteLine($"Element: {element.Char} Frekvencija pojavljivanja: {element.Frequency} Kod: {element.Code}");
            }
        }
        #endregion

        #region LZW

        public static List<short> LZWEncoding(string fileName, string text)
        {
            int size = text.Length;
            List<short> output_code = new List<short>();
            if (size < 1)
                return output_code;

            Dictionary<string, short> dict = new Dictionary<string, short>();
            for (short i = 0; i < 256; i++)
            {
                string ch = "";
                ch += (char)i;
                dict[ch] = i;
            }

            string w = "", c = "", wc = "";
            w += text[0];
            wc = w + c;
            short code = 256;
            Console.WriteLine("w\tc\twc\toutput\tdictionary");
            Console.WriteLine($"{w}\t{c}\t{wc}\t\t");
            for (int i = 0; i < size; i++)
            {
                if (i != size - 1)
                    c += text[i + 1];

                wc = w + c;
                if (dict.ContainsKey(wc))
                {
                    if (i != size - 1)
                    {
                        Console.WriteLine($"{w}\t{c}\t{wc}\t\t");
                        w = wc;
                    }

                }
                else
                {
                    dict[wc] = code++;
                    output_code.Add(dict[w]);
                    Console.WriteLine($"{w}\t{c}\t{wc}\t{(dict[w] < 256 ? w : dict[w])}\t{wc}={dict[wc]}");
                    w = c;
                }
                c = "";
            }
            Console.WriteLine($"{w}\t{c}\t{wc}\t{(dict[w] < 256 ? w : dict[w])}\t");
            output_code.Add(dict[w]);


            using (StreamWriter sw = new StreamWriter($"LZW_{fileName}.txt"))
            {
                sw.Write(text);
            }

            using (BinaryWriter bw = new BinaryWriter(File.Open($"LZW_{fileName}.compressed", FileMode.Create)))
            {
                foreach (int cd in output_code)
                {
                    bw.Write(ConvertToMinimumBytes(cd));
                }
            }

            return output_code;
        }

        public static byte[] ConvertToMinimumBytes(int num)
        {
            List<byte> list = new List<byte>();
            byte[] bytes = new byte[] { (byte)(num), (byte)(num >> 8), (byte)(num >> 16), (byte)(num >> 24) };
            bool prethodniVeci = false;

            for (int i = 3; i >= 0; i--)
            {
                if (bytes[i] > 0 || prethodniVeci)
                {
                    list.Add(bytes[i]);
                    prethodniVeci = true;
                }
            }

            return list.ToArray();
        }


        public static void LZWFileDecoding(string fileName)
        {
            List<byte> inputCode = new List<byte>();
            using (BinaryReader br = new BinaryReader(File.Open($"LZW_{fileName}.compressed", FileMode.Open)))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    inputCode.Add(br.ReadByte());
                }
            }


            int size = inputCode.Count;
            if (size < 1) return;

            Dictionary<short, string> dict = new Dictionary<short, string>();
            for (short i = 0; i < 256; i++)
            {
                string ch = "";
                ch += (char)i;
                dict[i] = ch;
            }

            short old = inputCode[0], nb;
            string s;
            string c = "";
            StringBuilder str = new StringBuilder();
            short code = 256;
            for (int i = 0, max = size - 1; i < max; i++)
            {
                byte currentByte = inputCode[i];
                byte nextByte = inputCode[i + 1];
                short twoBytes = (short)((currentByte << 8) + nextByte);

                if (dict.ContainsKey(twoBytes))
                {
                    s = dict[twoBytes];
                    str.Append(s);
                    c = "";
                    c += s[0];
                    dict[code++] = dict[old] + c;
                    old = twoBytes;
                    i++;
                }
                else
                {
                    if (dict.ContainsKey(nextByte))
                        s = dict[nextByte];
                    else
                    {
                        s = dict[old];
                        s += c;
                    }
                    str.Append(s);
                    c = "";
                    c += s[0];
                    dict[code++] = dict[old] + c;
                    old = currentByte;
                }
            }

            Console.WriteLine($"Dekodirani string: {str.ToString()}");
        }

        public static void LZWDecoding(List<short> inputCode)
        {
            int size = inputCode.Count;
            if (size < 1) return;

            Dictionary<int, string> dict = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
            {
                string ch = "";
                ch += (char)i;
                dict[i] = ch;
            }

            int old = inputCode[0], n;
            string s = dict[old];
            string c = "";
            c += s[0];
            StringBuilder str = new StringBuilder();
            str.Append(s); // prvi karakter je sigurno u ascii tabeli, dodajemo ga odmah
            int code = 256;
            for (int i = 0, max = size - 1; i < max; i++)
            {
                n = inputCode[i + 1];
                if (dict.ContainsKey(n))
                    s = dict[n];
                else
                {
                    s = dict[old];
                    s += c;
                }
                str.Append(s);
                c = "";
                c += s[0];
                dict[code++] = dict[old] + c;
                old = n;
            }

            Console.WriteLine($"Dekodirani string: {str.ToString()}");
        }
        #endregion

    }
}

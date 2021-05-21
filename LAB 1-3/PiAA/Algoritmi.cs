using System;
using System.IO;
using System.Text;

namespace LAB1
{
    public static class Algoritmi
    {

        #region RabinKarp
        //private const int d = 256; // broj razlicith karaktera - ASCII -> 2^8 = 256
        //private const int q = 13; // neki prost broj, d*q <= 31 bit -> zbog int-a

        private static void SaveInFile(string fileName, string text)
        {
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(text);
            }
        }


        private static void RabinKarpDQ(bool HEX, out int d, out int q)
        {
            if (HEX)
                d = 16; // Broj razlicitih HEX cifara
            else
                d = 256; // broj razlicith karaktera - ASCII -> 2^8 = 256

            q = 13; // neki prost broj, d*q <= 31 bit -> zbog int-a
        }

        public static int RabinKarp(string text, string pattern, bool HEX)
        {
            int brPogodaka = 0;

            int N = text.Length;
            int M = pattern.Length;
            if (N < M) return 0;
            RabinKarpDQ(HEX, out int d, out int q);
            int t = 0;
            int p = 0;

            int h = 1;
            //h = Convert.ToInt32(Math.Pow(MaxChar, M - 1)) % q;
            for (int i = 0; i < M - 1; i++) // (a*b)modc = (amodc * bmodc)modc
                h = (h * d) % q;
            //h = h % q; // ne moze ovako zbog prekoracenja int-a


            for (int i = 0; i < M; i++)
            {
                p = (d * p + pattern[i]) % q;
                t = (d * t + text[i]) % q;
            }
            int poslednjiIndex = N - M; // razlika, oduzimamo velicinu patterna
            for (int i = 0; i <= poslednjiIndex; i++)
            {
                // Ako je p == t, ne mora da znaci da je u pitanju poklapanje, zato sada proveravamo karakter po karakter
                if (p == t)
                {
                    int j;
                    for (j = 0; j < M; j++)
                    {
                        if (text[i + j] != pattern[j])
                            break;
                    }

                    if (j == M)
                    {
                        brPogodaka++;
                        Console.WriteLine($"Pattern je pronadjen na indeksu {i}");
                        SaveInFile($"Result_RabinCarp{(HEX ? "HEX" : "")}.txt", $"Pattern '{pattern}' je pronadjen na indeksu {i}");
                    }

                }

                if (i < poslednjiIndex)
                {
                    t = (d * (t - text[i] * h) + text[i + M]) % q; // izbacujemo prvi karakter i dodajemo naredni iz teksta

                    if (t < 0) // prekoracenje int-a, prelazi u minus
                        t = (t + q);
                }
            }
            return brPogodaka;
        }
        #endregion

        #region Knuth-Morris
        public static int KnuthMorris(string text, string pattern)
        {
            int brPogodaka = 0;

            int N = text.Length;
            int M = pattern.Length;
            int[] P = KnuthMorrisPreprocess(pattern);
            //int q = 0; // broj karaktera koji se poklapa


            int j = 0;
            int i = 0;
            while (i < N)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }

                if (j == M)
                {
                    //Console.WriteLine($"Pattern je pronadjen na indeksu {i - j}");
                    //SaveInFile("Result_KnuthMorris.txt", $"Pattern '{pattern}' je pronadjen na indeksu {i - j}");
                    j = P[j - 1];
                    brPogodaka++;
                }
                else if (i < N && pattern[j] != text[i])
                {
                    if (j != 0)
                        j = P[j - 1];
                    else
                        i = i + 1;
                }
            }
            return brPogodaka;
        }
        private static int[] KnuthMorrisPreprocess(string pattern)
        {

            int M = pattern.Length;
            int K = 0;
            int[] P = new int[M];
            P[0] = 0;


            int i = 1;
            while (i < M)
            {
                if (pattern[i] == pattern[K])
                {
                    K++;
                    P[i] = K;
                    i++;
                }
                else // (pat[i] != pat[len]) // www.geeksforgeeks.org
                {
                    // This is tricky. Consider the example.
                    // AAACAAAA and i = 7. The idea is similar
                    // to search step.
                    if (K != 0)
                    {
                        K = P[K - 1];

                        // Also, note that we do not increment
                        // i here
                    }
                    else
                    {
                        P[i] = K;
                        i++;
                    }
                }
            }
            return P;
        }
        #endregion

        #region SoundEx
        public static void SoundEx(string text, string word)
        {
            string[] reci = text.Split(" ");

            string encodedWord = GetEncodedSoundEx(word);

            foreach (string rec in reci)
            {
                if (GetEncodedSoundEx(rec) == encodedWord)
                {
                    Console.WriteLine($"Slicna rec pronadjena! Rec: {rec}");
                    SaveInFile("Result_SoundEx.txt", $"Slicna rec '{rec}' pronadjena! ({word})");
                }
            }
        }

        public static string GetEncodedSoundEx(string word)
        {
            int length = word.Length;
            if (length < 1) return "";
            StringBuilder encoded = new StringBuilder();
            encoded.Append(Char.ToUpper(word[0]));
            word = word.ToLower(); //

            int encodedNum = 1;
            for (int i = 1, max = length - 1; i < max; i++)
            {
                char first = word[i - 1];
                char middle = word[i];
                char last = word[i + 1];

                char firstEn = GetSoundExChar(first);
                char middleEn = GetSoundExChar(middle);
                char lastEn = GetSoundExChar(last);

                if (firstEn == middleEn)
                    continue;

                if (middleEn == char.MinValue && firstEn == lastEn)
                {
                    if (middle == 'h' || middle == 'w') // h ili w u sredini, ostavljamo samo prvi karakter
                        continue;
                    if (middle == 'a' || middle == 'e' || middle == 'i' || middle == 'o' || middle == 'u') // samoglasnik u sredini, upisujemo i taj naredni i preskacemo naredni element jer smo ga obradili - upisali
                    {
                        encoded.Append(lastEn);
                        encodedNum++;
                        i++;
                        continue;
                    }
                }

                if (middleEn == char.MinValue) // Neki je od a, e, i, o, u, y, h, w, preskacemo ga -> ne upisujemo
                    continue;

                encodedNum++;
                encoded.Append(middleEn);
            }

            // Obrada poslednjeg slova
            char lastCharEn = GetSoundExChar(word[length - 1]);
            if (encoded[encodedNum - 1] != lastCharEn)
            {
                encodedNum++;
                encoded.Append(lastCharEn);
            }

            while (encodedNum < 4) // dodavanje nula, SLOVO+3 CIFRE, ako nemamo 3 cifre dodajemo nule
            {
                encoded.Append('0');
                encodedNum++;
            }
            encoded.Length = 4;// Secemo u slucaju da je broj cifara veci od 3 (slovo + 3 cifre = 4)
            return encoded.ToString();
        }

        private static char GetSoundExChar(char ch)
        {
            return ch switch
            {
                'b' or 'f' or 'p' or 'v' => '1',
                'c' or 'g' or 'j' or 'k' or 'q' or 's' or 'x' or 'z' => '2',
                'd' or 't' => '3',
                'l' => '4',
                'm' or 'n' => '5',
                'r' => '6',
                _ => char.MinValue,
            };
        }
        #endregion

        #region Levenstein
        public static void Levenstain(string text, string word, int maxDistance)
        {
            string[] reci = text.Split(" ");

            //word = word.ToLower(); // ako ne racunamo mala i velika slova

            foreach (string rec in reci)
            {
                int dist = LevensteinDistance(rec, word); // LevensteinDistance(rec.ToLower(), word)
                if (dist <= maxDistance)
                {
                    Console.WriteLine($"Rec {rec} ispunjava uslov! Distanca: {dist}");
                    SaveInFile("Result_Levenstain.txt", $"Rec {rec} ispunjava uslov! Distanca: {dist} ({word})");
                }
            }
        }

        public static int LevensteinDistance(string str, string txt)
        {
            int n = str.Length;
            int m = txt.Length;

            int[,] d = new int[n + 1, m + 1];


            for (int i = 1; i <= n; i++)
                d[i, 0] = i;

            for (int j = 1; j <= m; j++)
                d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = 0;
                    if (str[i - 1] != txt[j - 1])
                        cost = 1;

                    d[i, j] = Math.Min(d[i - 1, j] + 1, Math.Min(d[i, j - 1] + 1, d[i - 1, j - 1] + cost));
                }
            }

            // Debug print matrice
            /*for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    Console.Write(d[i, j] + " ");
                }
                Console.WriteLine();
            } */
            return d[n, m];
        }
        #endregion
    }
}

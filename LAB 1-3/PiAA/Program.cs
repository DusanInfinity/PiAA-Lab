using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LAB1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                List<string> wordsCount = new List<string>() { "100", "1,000", "10,000", "100,000" };
                List<string> reci = new List<string>() { "dolor", "adipiscing", "consecteturabcdabcde", "consecteturabcdabcdeconsecteturabcdabcdeadipiscing" };
                List<string> textWCount = new List<string>() { "100", "1000", "10000", "100000" };
                List<string> findWords = new List<string>() { "FMJMB", "EJANHCFHNA", "HCHBJLNHMFEBJPDFEIH", "AODGLNBFPKPLAMNPONPOHKGHDBJPJNIKAGIMKABHDLKFDJDLJ" };

                Stopwatch stopWatch = new Stopwatch();

                #region Text & HEX generator
                /*TextGenerator(100, 20);
                TextGenerator(1000, 10);
                TextGenerator(10000, 5);
                TextGenerator(100000, 2); */

                /*HEXGenerator(100, 20);
                HEXGenerator(1000, 10);
                HEXGenerator(10000, 5);
                HEXGenerator(100000, 2); */
                #endregion

                #region Rabin-Karp 
                /*foreach (string rec in findWords)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"Pokrecem Rabin-Karp za {textWCount[i]} reci, trazim rec {rec}...");
                        int brPogodaka;
                        using (StreamReader sr = new StreamReader($"WORD{textWCount[i]}.txt"))
                        {
                            string text = sr.ReadToEnd();
                            stopWatch.Reset();
                            stopWatch.Start();
                            brPogodaka = Algoritmi.RabinKarp(text, rec, false);
                            stopWatch.Stop();
                        }
                        Console.WriteLine($"Rabin-Karp za rec {rec} u {textWCount[i]} reci zavrsen! Vreme: {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\nUkupan broj pogodaka: {brPogodaka}\n\n");
                    }
                } */
                #endregion

                #region Rabin-Karp HEX
                /*List<string> hexNumbers = new List<string>() { "3F535", "123456789A", "123456789A123456789A", "123456789A123456789A123456789A123456789A123456789A" };
                foreach (string rec in hexNumbers)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"Pokrecem Rabin-Karp HEX za {textWCount[i]} brojeva, trazim rec {rec}...");
                        int brPogodaka;
                        using (StreamReader sr = new StreamReader($"HEX{textWCount[i]}.txt"))
                        {
                            string text = sr.ReadToEnd();
                            stopWatch.Reset();
                            stopWatch.Start();
                            brPogodaka = Algoritmi.RabinKarp(text, rec, true);
                            stopWatch.Stop();
                        }
                        Console.WriteLine($"Rabin-Karp HEX za broj {rec} u {textWCount[i]} brojeva zavrsen! Vreme: {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\nUkupan broj pogodaka: {brPogodaka}\n\n");
                    }
                } */
                #endregion

                #region Knuth-Morris
                foreach (string rec in findWords)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"Pokrecem Knuth-Morris za {textWCount[i]} reci, trazim rec {rec}...");
                        int brPogodaka;
                        using (StreamReader sr = new StreamReader($"WORD{textWCount[i]}.txt"))
                        {
                            string text = sr.ReadToEnd();
                            stopWatch.Reset();
                            stopWatch.Start();
                            brPogodaka = Algoritmi.KnuthMorris(text, rec);
                            stopWatch.Stop();
                        }
                        Console.WriteLine($"Knuth-Morris za rec {rec} u {textWCount[i]} reci zavrsen! Vreme: {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\nUkupan broj pogodaka: {brPogodaka}\n\n");
                    }
                }
                #endregion 
                #region SoundEx
                //Console.WriteLine($"[SOUNDEX] Robert = {Algoritmi.GetEncodedSoundEx("Robert")}");
                //Console.WriteLine($"[SOUNDEX] Rupert = {Algoritmi.GetEncodedSoundEx("Rupert")}");
                //Console.WriteLine($"[SOUNDEX] Rubin = {Algoritmi.GetEncodedSoundEx("Rubin")}");
                //Console.WriteLine($"[SOUNDEX] Tymczak = {Algoritmi.GetEncodedSoundEx("Tymczak")}");
                //Console.WriteLine($"[SOUNDEX] Pfister = {Algoritmi.GetEncodedSoundEx("Pfister")}");
                //Console.WriteLine($"[SOUNDEX] Popokatepetl = {Algoritmi.GetEncodedSoundEx("Popokatepetl")}");


                List<string> soundExReci = new List<string>() { "world", "Informatio", "Compartmentalization", "Pneumonoultramicroscopicsilicovolcanoconiosisabcde" }; // nemaju utica neka slova, uzima se samo pocetak, ostatak soundEx koda se odbacuje kada se dostignu 3 broja u kodu
                /*foreach (string rec in soundExReci)
                {
                    Console.WriteLine($"[SOUNDEX] '{rec}' = {Algoritmi.GetEncodedSoundEx(rec)}");
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"Pokrecem SoundEx za rec '{rec}' u {textWCount[i]} reci...");
                        using (StreamReader sr = new StreamReader($"soundEx{textWCount[i]}.txt"))
                        {
                            string text = sr.ReadToEnd();
                            stopWatch.Reset();
                            stopWatch.Start();
                            Algoritmi.SoundEx(text, rec);
                            stopWatch.Stop();
                        }
                        Console.WriteLine($"SoundEx za rec '{rec}' u {textWCount[i]} reci zavrsen! Vreme: {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n");
                    }
                }*/
                #endregion

                #region Levenstain
                //Console.WriteLine($"[LEVENSTAIN] Reci 'sitting' i 'kitten': {Algoritmi.LevensteinDistance("sitting", "kitten")}");
                //Console.WriteLine($"[LEVENSTAIN] Rec 'sound' i 'soundex': {Algoritmi.LevensteinDistance("sound", "soundex")}");

                /*foreach (string rec in soundExReci)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"Pokrecem Levenstain za rec '{rec}' u {textWCount[i]} reci, max. duzina 3...");
                        using (StreamReader sr = new StreamReader($"soundEx{textWCount[i]}.txt"))
                        {
                            string text = sr.ReadToEnd();
                            stopWatch.Reset();
                            stopWatch.Start();
                            Algoritmi.Levenstain(text, rec, 3);
                            stopWatch.Stop();
                        }
                        Console.WriteLine($"Levenstain za rec '{rec}' u {textWCount[i]} reci zavrsen! Vreme: {stopWatch.ElapsedMilliseconds}ms [{stopWatch.ElapsedTicks}]\n\n");
                    }
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ex: {ex.Message}");
            }
        }


        private static void HEXGenerator(int velicina, int sansaZaTrazeniBr)
        {
            StringBuilder[] hexList = new StringBuilder[velicina];
            List<char> hexBrojevi = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            Random rnd = new Random();
            List<string> hexNumbers = new List<string>() { "3F535", "123456789A", "123456789A123456789A", "123456789A123456789A123456789A123456789A123456789A" };

            for (int i = 0; i < velicina; i++)
            {
                hexList[i] = new StringBuilder();
                int sansa = rnd.Next(0, 100);
                if (sansa < sansaZaTrazeniBr)
                {
                    int sansaPoReci = rnd.Next(0, 100);
                    int indexReci = sansaPoReci < 50 ? 0 : sansaPoReci < 80 ? 1 : sansaPoReci < 95 ? 2 : 3;
                    hexList[i].Append(hexNumbers[indexReci]);
                }
                else
                {
                    int velicinaBroja = rnd.Next(1, 75);
                    while (velicinaBroja > 0)
                    {
                        int hexBrRand = rnd.Next(0, 16);
                        hexList[i].Append(hexBrojevi[hexBrRand]);
                        velicinaBroja--;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter($"HEX{velicina}.txt"))
            {
                foreach (StringBuilder sb in hexList)
                {
                    sw.WriteLine(sb.ToString());
                }
            }
        }


        private static void TextGenerator(int brReci, int sansaZaTrazenuRec)
        {
            StringBuilder[] text = new StringBuilder[brReci];
            List<char> slova = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();
            Random rnd = new Random();
            List<string> findWords = new List<string>() { "FMJMB", "EJANHCFHNA", "HCHBJLNHMFEBJPDFEIH", "AODGLNBFPKPLAMNPONPOHKGHDBJPJNIKAGIMKABHDLKFDJDLJ" };

            for (int i = 0; i < brReci; i++)
            {
                text[i] = new StringBuilder();

                int sansa = rnd.Next(0, 100);
                if (sansa < sansaZaTrazenuRec)
                {
                    int sansaPoReci = rnd.Next(0, 100);
                    int indexReci = sansaPoReci < 50 ? 0 : sansaPoReci < 80 ? 1 : sansaPoReci < 95 ? 2 : 3;
                    text[i].Append(findWords[indexReci]);
                }
                else
                {
                    int velicinaBroja = rnd.Next(3, 75);
                    while (velicinaBroja > 0)
                    {
                        int hexBrRand = rnd.Next(0, 16);
                        text[i].Append(slova[hexBrRand]);
                        velicinaBroja--;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter($"WORD{brReci}.txt"))
            {
                foreach (StringBuilder sb in text)
                {
                    sw.WriteLine(sb.ToString());
                }
            }
        }
    }
}

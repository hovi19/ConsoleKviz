using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA181208
{
    struct KvizKerdes
    {
        public string kerdes;
        public string A;
        public string B;
        public string C;
        public char megoldas;
    }

    class Program
    {
        static List<KvizKerdes> teszt;
        static int pontszam;

        static void Main()
        {
            PreSet();
            Game();
            Aftermath();

        }

        static void PreSet()
        {
            teszt = new List<KvizKerdes>();
            pontszam = 0;
            Console.CursorVisible = false;

            var srTeszt = new StreamReader("teszt.txt", Encoding.UTF8);
            var srMegoldas = new StreamReader("megoldas.txt", Encoding.UTF8);

            while (!srMegoldas.EndOfStream && !srTeszt.EndOfStream)
            {
                var kk = new KvizKerdes
                {
                    kerdes = srTeszt.ReadLine(),
                    A = srTeszt.ReadLine(),
                    B = srTeszt.ReadLine(),
                    C = srTeszt.ReadLine(),
                    megoldas = char.Parse(srMegoldas.ReadLine().ToLower())
                };

                teszt.Add(kk);
            }

            srTeszt.Close();
            srMegoldas.Close();

            Console.WriteLine("Szókincs\n");

            var srFeladat = new StreamReader(@"feladat.txt", Encoding.UTF8);
            string[] t = srFeladat.ReadLine().Split(' ');

            for (int i = 0; i < t.Length; i++)
            {
                if (Console.CursorLeft + t[i].Length > Console.WindowWidth)
                    Console.Write($"\n");
                Console.Write($"{t[i]} ");
            }
            srFeladat.Close();

            Enter("A kérdésekhez");
        }
        static void Game()
        {
            char[] val = { 'a', 'b', 'c' };
            foreach (var kk in teszt)
            {
                Console.Clear();

                Console.WriteLine(kk.kerdes);
                Console.WriteLine($"\t{kk.A}");
                Console.WriteLine($"\t{kk.B}");
                Console.WriteLine($"\t{kk.C}");

                ConsoleKeyInfo valasz;
                Console.Write("\nAdja meg a helyes válasz betűjelét: ");

                int post = Console.CursorTop;
                int posl = Console.CursorLeft;

                Console.ForegroundColor = Console.BackgroundColor;
                do
                {
                    valasz = Console.ReadKey();
                    Console.SetCursorPosition(posl, post);
                    Console.Write(" ");
                    Console.SetCursorPosition(posl, post);
                } while (!val.Contains(valasz.KeyChar));
                Console.ForegroundColor = ConsoleColor.Gray;

                if(kk.megoldas == valasz.KeyChar)
                {
                    pontszam++;
                    Console.WriteLine("\n\tJó válasz");
                }
                else Console.WriteLine("\n\tRossz válasz");

                if (teszt.IndexOf(kk) == teszt.Count - 1)
                {
                    Enter("A kiértékeléshez");
                }
                else Enter("A következő kérdéshez");
            }
        }
        static void Aftermath()
        {
            Console.Clear();
            Console.WriteLine($"helyes válaszok száma: {pontszam}/{teszt.Count}");
            Enter("A kilépéshez");
        }
        static void Enter(string pre)
        {
            Console.WriteLine($"\n\n{pre} nomja meg az ENTERT...");

            int pos = Console.CursorTop;
            Console.ForegroundColor = Console.BackgroundColor;

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.SetCursorPosition(0, pos);
                Console.Write(" ");
                Console.SetCursorPosition(0, pos);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

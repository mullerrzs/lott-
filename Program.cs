using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hányszor szeretné megpróbálni?");
        if (int.TryParse(Console.ReadLine(), out int probalkozasokSzama) && probalkozasokSzama > 0)
        {
            for (int i = 0; i < probalkozasokSzama; i++)
            {
                JatekEgyKor();
            }
        }
        else
        {
            Console.WriteLine("Hibás bemenet. A próbálkozások számának pozitívnak kell lennie.");
        }
    }

    static void JatekEgyKor()
    {
        List<int> nyeroSzamok = GeneralNyeroket();
        List<int> jatekosTipp = KeriJatekosTippet();

        int talalatok = SzamolTalalatokat(nyeroSzamok, jatekosTipp);

        Console.WriteLine($"Nyertes számok: {string.Join(", ", nyeroSzamok)}");
        Console.WriteLine($"Játékos tippje: {string.Join(", ", jatekosTipp)}");
        Console.WriteLine($"Találatok száma: {talalatok}");

        string eredmenyek = $"Játékos tippje: {string.Join(", ", jatekosTipp)}, Nyertes számok: {string.Join(", ", nyeroSzamok)}, Találatok száma: {talalatok}";
        File.AppendAllText("lotto_eredmenyek.txt", eredmenyek + Environment.NewLine);

        Console.WriteLine();
    }

    static List<int> GeneralNyeroket()
    {
        Random random = new Random();
        return Enumerable.Range(1, 45).OrderBy(_ => random.Next()).Take(6).ToList();
    }

    static List<int> KeriJatekosTippet()
    {
        Console.WriteLine("Válasszon 6 számot 1 és 45 között (számokat szóközzel válassza el):");
        string[] tippSzoveg = Console.ReadLine().Split(' ');

        List<int> jatekosTipp;
        while (!ValidalJatekosTippeket(tippSzoveg, out jatekosTipp))
        {
            Console.WriteLine("Hibás választás. Kérjük, válasszon újra.");
            Console.WriteLine("Válasszon 6 számot 1 és 45 között (számokat szóközzel válassza el):");
            tippSzoveg = Console.ReadLine().Split(' ');
        }

        return jatekosTipp;
    }

    static bool ValidalJatekosTippeket(string[] tippSzoveg, out List<int> jatekosTipp)
    {
        jatekosTipp = new List<int>();
        if (tippSzoveg.Length != 6)
            return false;

        foreach (var szoveg in tippSzoveg)
        {
            if (!int.TryParse(szoveg, out int szam) || szam < 1 || szam > 45)
                return false;

            jatekosTipp.Add(szam);
        }

        return jatekosTipp.Distinct().Count() == 6;
    }

    static int SzamolTalalatokat(List<int> nyeroSzamok, List<int> jatekosTipp)
    {
        return nyeroSzamok.Intersect(jatekosTipp).Count();
    }
}

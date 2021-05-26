using System;
using paz.eyal._4h.saverecord.Models;

namespace paz.eyal._4h.saverecord
{

    class Program
    {
        static void Main(string[] args)
        {
            Comuni c = new Comuni("Comuni.csv");

            Console.WriteLine($"Ho letto {c.Count} righe dal file csv.");
            c.Save();
            c.Load();
            Console.WriteLine($"Ho letto {c.Count} righe dal file binario");
            int index = 100;
            Console.WriteLine($"Ecco la riga {index}: {c[index]}");

        }
    }
}

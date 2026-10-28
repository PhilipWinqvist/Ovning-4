using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PersonLab
{
    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("**********************************************");
            Console.WriteLine("        LÖNEÖKNING - REGISTRERA PERSONER");
            Console.WriteLine("**********************************************");
            Console.WriteLine();

            Console.WriteLine("\nHur många personer vill du registrera?");
            int lines = ReadPositiveInt("Antal personer: ");
            Console.WriteLine();

         
            Console.WriteLine("\nSkriv in varje person på en egen rad.");
            Console.WriteLine("Format:  Förnamn Efternamn Ålder Lön");
            Console.WriteLine();

            var persons = new List<Person>();
            for (int i = 1; i <= lines; i++)
            {
                persons.Add(ReadPerson(i, lines));
            }
            Console.WriteLine();

            Console.WriteLine("\nHur många procent bonus ska lönerna öka med?");
            Console.WriteLine("Personer under 30 år får bara hälften av ökningen.");
            decimal bonus = ReadDecimal("Bonus i procent (t.ex. 20): ");
            Console.WriteLine();

            
            Console.WriteLine("\nResultat");
            Console.WriteLine("----------------------------------------------");
            persons.ForEach(p => p.IncreaseSalary(bonus));
            persons.ForEach(p => Console.WriteLine(p.ToString()));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Klart!");
        }

        private static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int number) && number > 0)
                {
                    return number;
                }
                Console.WriteLine("Fel: skriv ett heltal större än 0.");
            }
        }

        private static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";

                if (decimal.TryParse(input, out decimal number) && number >= 0)
                {
                    return number;
                }
                Console.WriteLine("Fel: skriv ett tal, t.ex. 20 eller 12.5 (punkt som decimaltecken).");
            }
        }

        private static Person ReadPerson(int number, int total)
        {
            while (true)
            {
                Console.Write($"Person {number} av {total}: ");
                string[] cmdArgs = (Console.ReadLine() ?? "")
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (cmdArgs.Length != 4)
                {
                    Console.WriteLine("Fel: du måste skriva fyra delar: Förnamn Efternamn Ålder Lön");
                    continue;
                }

                if (!int.TryParse(cmdArgs[2], out int age) ||
                    !decimal.TryParse(cmdArgs[3], out decimal salary))
                {
                    Console.WriteLine("Fel: ålder måste vara ett heltal och lön ett tal (punkt som decimaltecken).");
                    continue;
                }

                try
                {
                    return new Person(cmdArgs[0], cmdArgs[1], age, salary);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Fel: " + ex.Message);
                }
            }
        }
    }
}
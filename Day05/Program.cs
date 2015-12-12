using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DayFive
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> matches = new List<string>();
            List<string> matches2 = new List<string>();
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, "[aeiou].*[aeiou].*[aeiou]") && Regex.IsMatch(line, "(.)\\1") && !Regex.IsMatch(line, "(ab|cd|pq|zy)"))
                        {
                            matches.Add(line);
                        }
                        if (Regex.IsMatch(line, "(.)(.).*(\\1\\2)") && Regex.IsMatch(line, "(.).\\1"))
                        {
                            matches2.Add(line);
                        }
                    }
                }
            }
            Console.WriteLine("Found {0} matches", matches.Count);
            Console.WriteLine("Found {0} secondary matches", matches2.Count);
            Console.ReadLine();
        }
    }
}

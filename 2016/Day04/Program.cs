using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day04
{
    class Program
    {
        static Regex inputRegex = new Regex(@"((\w+\-?)+)\-(\d+)\[(\w+)\]");
        static char[] letters = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            Int64 sum = 0;
            using (var outFile = File.CreateText("Output.txt"))
            {
                foreach (var line in input)
                {
                    Match match = inputRegex.Match(line);
                    var name = match.Groups[1].Value;
                    var sector = Int32.Parse(match.Groups[3].Value);
                    var checksum = match.Groups[4].Value;

                    var nameCheck = new String(name.Replace("-", "").GroupBy(c => c, (c, s) => Tuple.Create(c, s.Count())).OrderByDescending(i => i.Item2).ThenBy(i => i.Item1).Take(5).Select(i => i.Item1).ToArray());

                    if (nameCheck == checksum)
                    {
                        sum += sector;

                        var decryptedName = "";
                        foreach (var character in name)
                        {
                            if (character == '-')
                            {
                                decryptedName += ' ';
                            }
                            else
                            {
                                var index = Array.IndexOf(letters, character);
                                index = (index + sector) % letters.Length;
                                decryptedName += letters[index];
                            }
                        }

                        Console.WriteLine("Name: ({0}) sector: ({1}) ", decryptedName, sector);
                        outFile.WriteLine("Name: ({0}) sector: ({1}) ", decryptedName, sector);
                    }
            }
            }
            Console.WriteLine("total sum: {0}", sum);
        }
    }
}

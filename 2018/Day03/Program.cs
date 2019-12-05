using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            var claims = new Dictionary<string, List<string>>();
            var claimIds = new HashSet<string>();
            foreach (var line in input)
            {
                var match = Regex.Match(line, @"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)");
                var claimId = match.Groups[1].Value;
                var startx = int.Parse(match.Groups[2].Value);
                var starty = int.Parse(match.Groups[3].Value);
                var width = int.Parse(match.Groups[4].Value);
                var height = int.Parse(match.Groups[5].Value);
                claimIds.Add(claimId);
                for (int x = startx; x < startx + width; x++)
                {
                    for (int y = starty; y < starty + height; y++)
                    {
                        var key = x + "," + y;
                        if (!claims.ContainsKey(key))
                        {
                            claims[key] = new List<string>() { claimId };
                        }
                        else
                        {
                            claims[key].Add(claimId);
                        }
                    }
                }
            }

            int count = 0;

            foreach (var claim in claims)
            {
                if (claim.Value.Count > 1)
                {
                    count++;
                    foreach (var claimId in claim.Value)
                    {
                        claimIds.Remove(claimId);
                    }
                }
            }
            Console.WriteLine("Result {0}", count);
            Console.WriteLine("Result2 {0}", claimIds.First());
            Console.ReadLine();
        }
    }
}

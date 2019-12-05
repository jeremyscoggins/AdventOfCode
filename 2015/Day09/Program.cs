using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DayNine
{
    class Program
    {
        const string inputRegex = "(\\w+) to (\\w+) \\= (\\d+)";
        static Dictionary<string, Dictionary<string, int>> distances = new Dictionary<string, Dictionary<string, int>>();
        static Dictionary<string, int> routes = new Dictionary<string, int>();
        static int minDist = int.MaxValue;
        static int maxDist = int.MinValue;
        static void Main(string[] args)
        {
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var match = Regex.Match(line, inputRegex);
                        var destA = match.Groups[1].Value;
                        var destB = match.Groups[2].Value;
                        var distance = int.Parse(match.Groups[3].Value);
                        if (!distances.ContainsKey(destA))
                        {
                            distances[destA] = new Dictionary<string, int>();
                        }
                        distances[destA][destB] = distance;
                        if (!distances.ContainsKey(destB))
                        {
                            distances[destB] = new Dictionary<string, int>();
                        }
                        distances[destB][destA] = distance;
                    }
                }
            }

            foreach (var destination in distances.Keys)
            {
                walk(destination, destination, 0);
            }

            //Console.WriteLine("Found {0} matches", matches.Count);
            //Console.WriteLine("Found {0} secondary matches", matches2.Count);

            var sorted = routes.OrderBy(r => r.Value).Take(1).Concat(routes.OrderByDescending(r => r.Value).Take(1));
            foreach (var item in sorted)
            {
                Console.WriteLine("Route: " + item.Key);
                Console.WriteLine("Distance: " + item.Value);
            }

            var sorted2 = routes.OrderBy(r => r.Value);
            using (var fileStream = File.Create("output.txt"))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var item in sorted2)
                    {
                        streamWriter.WriteLine(item.Key + " = " + item.Value);
                    }
                }
            }

            Console.ReadLine();
        }

        private static void walk(string last, string route, int distance, string forceRoute = null)
        {
            bool finished = true;
            foreach (var next in distances[last].Keys)
            {
                if (!route.Contains(next))
                {
                    var newRoute = route + " -> " + next;
                    if (forceRoute == null || forceRoute.StartsWith(newRoute))
                    {
                        walk(next, newRoute, distance + distances[last][next], forceRoute);
                        finished = false;
                    }
                }
            }
            if (finished)
            {
                minDist = Math.Min(minDist, distance);
                maxDist = Math.Max(maxDist, distance);
                routes.Add(route, distance);
            }
        }
    }
}

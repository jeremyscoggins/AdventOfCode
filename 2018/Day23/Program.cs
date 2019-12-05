using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        static void Main(string[] args)
        {
            Program2.DoWork();
            var inputRegex = new Regex("pos=<(-?\\d+),(-?\\d+),(-?\\d+)>, r=(-?\\d+)");
            var input = File.ReadAllLines("Input.txt");
            var bots = new List<(int x, int y, int z, int range)>();
            foreach (var line in input)
            {
                var match = inputRegex.Match(line);
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                var z = int.Parse(match.Groups[3].Value);
                var range = int.Parse(match.Groups[4].Value);
                bots.Add((x, y, z, range));
            }
            var strongest = bots.OrderByDescending(b => b.range).First();
            var inRange = bots.Where(b => distance((b.x, b.y, b.z), (strongest.x, strongest.y, strongest.z)) <= strongest.range);
            Console.WriteLine(inRange.Count());

            var minX = bots.Min(b => b.x);
            var minY = bots.Min(b => b.y);
            var minZ = bots.Min(b => b.z);
            var maxX = bots.Max(b => b.x);
            var maxY = bots.Max(b => b.y);
            var maxZ = bots.Max(b => b.z);

            var precision = 100000000;
            (int x, int y, int z, int count) bestCoord = (0,0,0,0);
            while (precision >= 1)
            {
                var found = new List<(int x, int y, int z, int count)>();
                for (int x = minX; x < maxX + precision; x+= precision)
                {
                    for (int y = minY; y < maxY + precision; y+= precision)
                    {
                        for (int z = minZ; z < maxZ + precision; z+= precision)
                        {
                            var numInRange = bots.Where(b => Math.Abs(distance((b.x, b.y, b.z), (x, y, z)) - b.range) <= precision).Count();
                            if (numInRange > 0)
                            {
                                found.Add((x,y,z,numInRange));
                            }
                        }
                    }
                }
                bestCoord = found.OrderByDescending(f => f.count).ThenBy(f => f.x).ThenBy(f => f.y).ThenBy(f => f.z).First();
                minX = bestCoord.x - (precision / 2 );
                minY = bestCoord.y - (precision / 2 );
                minZ = bestCoord.z - (precision / 2 );
                maxX = bestCoord.x + (precision / 2 );
                maxY = bestCoord.y + (precision / 2 );
                maxZ = bestCoord.z + (precision / 2 );
                precision /= 2;

            }
            var result = distance((bestCoord.x, bestCoord.y, bestCoord.z), (0, 0, 0));
            Console.WriteLine(result);

            Console.ReadLine();

            //91342343 == too low
            //91342344 == too low
            //93750870 == just righ
            //93750871 == too high
        }

        private static int distance((int x, int y, int z) p1, (int x, int y, int z) p2)
        {
            return Math.Abs(p2.x - p1.x) + Math.Abs(p2.y - p1.y) + Math.Abs(p2.z - p1.z);
        }
    }
}

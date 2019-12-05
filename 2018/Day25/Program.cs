using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            var stars = new List<(int x, int y, int z, int t)>();
            foreach (var line in input)
            {
                var vals = line.Split(',');
                stars.Add((int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]), int.Parse(vals[3])));
            }
            var neighborSets = new Dictionary<(int x, int y, int z, int t), List<(int x, int y, int z, int t)>>();
            foreach (var star in stars)
            {
                var neighbors = new List<(int x, int y, int z, int t)>();
                foreach (var neighbor in stars.Where(s => s != star))
                {
                    var dist = Math.Abs(star.x - neighbor.x) + Math.Abs(star.y - neighbor.y) + Math.Abs(star.z - neighbor.z) + Math.Abs(star.t - neighbor.t);
                    if (dist <= 3)
                    {
                        neighbors.Add(neighbor);
                    }
                }
                neighborSets[star] = neighbors;
            }
            var constellations = new List<HashSet<(int x, int y, int z, int t)>>();

            foreach (var star in stars)
            {
                if (!constellations.Any(c => c.Contains(star)))
                {
                    var constellation = new HashSet<(int x, int y, int z, int t)>(new[] { star });
                    constellations.Add(constellation);
                    var queue = new Queue<(int x, int y, int z, int t)>();
                    queue.Enqueue(star);
                    while (queue.Count > 0)
                    {
                        var item = queue.Dequeue();
                        var neighbors = neighborSets[item];
                        foreach (var neighbor in neighbors)
                        {
                            if (constellation.Add(neighbor))
                            {
                                queue.Enqueue(neighbor);
                            }
                        }
                    }
                }
            }
            var result = constellations.Count;
            Console.WriteLine(result);
            //618 == too high
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt").Select(line => line.Split(',')).ToArray();
            //var map = new List<List<char>>();
            var lines = new Dictionary<(int x, int y),int>[2];
            for (int l = 0; l < input.Length; l++)
            {
                int x = 0;
                int y = 0;
                int steps = 0;
                lines[l] = new Dictionary<(int, int), int>();
                foreach(var instruction in input[l])
                {
                    var direction = instruction[0];
                    var distance = int.Parse(instruction.Substring(1));
                    for (int i = 0; i < distance; i++)
                    {
                        switch (direction)
                        {
                            case 'U':
                                y--;
                                break;
                            case 'R':
                                x++;
                                break;
                            case 'D':
                                y++;
                                break;
                            case 'L':
                                x--;
                                break;
                        }
                        steps++;
                        if (!lines[l].ContainsKey((x, y)))
                        {
                            lines[l].Add((x, y), steps);
                        }
                    }
                }
            }
            var intersections = lines[0].Keys.Intersect(lines[1].Keys).Select(i => new { i.x, i.y, dist = Math.Abs(i.x) + Math.Abs(i.y) }).OrderBy(i => i.dist);
            var closest = intersections.First();
            Console.WriteLine(closest.x + " " + closest.y + " " + closest.dist);

            var intersections2 = intersections.Select(i => new { i.x, i.y, steps = lines[0][(i.x, i.y)] + lines[1][(i.x, i.y)] }).OrderBy(i => i.steps);
            var shortestSteps = intersections2.First();
            Console.WriteLine(shortestSteps.x + " " + shortestSteps.y + " " + shortestSteps.steps);

            Console.ReadLine();
        }
    }
}

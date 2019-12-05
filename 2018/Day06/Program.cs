using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var coords = File.ReadAllLines("Input.txt").Select(line => new Point(int.Parse(line.Split(',')[0].Trim()), int.Parse(line.Split(',')[1].Trim()))).ToArray();
            var maxX = coords.Select(c => c.x).Max() + 1;
            var maxY = coords.Select(c => c.y).Max() + 1;
            var closest = new Point[maxY, maxX];
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    var sorted = coords.Select(coord => new { coord, dist = distance(coord, new Point(x, y)) }).OrderBy(o => o.dist).ToList();
                    if (sorted[0].dist == sorted[1].dist)
                    {
                        closest[y, x] = new Point(-1, -1);
                    }
                    else
                    {
                        closest[y, x] = sorted[0].coord;
                    }
                }
            }

            var areas = new Dictionary<Point, int>();

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (areas.ContainsKey(closest[y, x]))
                    {
                        areas[closest[y, x]]++;
                    }
                    else
                    {
                        areas[closest[y, x]] = 1;
                    }
                }
            }

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (x == 0 || y == 0 || x == maxX || y == maxY)
                    {
                        areas[closest[y, x]] = -1 ;
                    }
                }
            }

            var area = areas.OrderByDescending(a => a.Value).First();
            Console.WriteLine("Result: " + area.Value);


            int threshold = 0;

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    var total = coords.Select(coord => distance(coord, new Point(x, y))).Sum();
                    if (total < 10000)
                    {
                        threshold++;
                    }
                }
            }

            Console.WriteLine("Result2: " + threshold);
            Console.ReadLine();
        }

        private static int distance(Point coord, Point point)
        {
            return Math.Abs(coord.x - point.x) + Math.Abs(coord.y - point.y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DaySix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] grid = new int[1000,1000];
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    grid[x, y] = 0;
                }
            }
            int lightCount = 0;

            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.StartsWith("turn on"))
                        {
                            var coordinates = Regex.Match(line, "(turn off|turn on|toggle) (\\d+),(\\d+) through (\\d+),(\\d+)");
                            int x1 = Int32.Parse(coordinates.Groups[2].Value);
                            int y1 = Int32.Parse(coordinates.Groups[3].Value);
                            int x2 = Int32.Parse(coordinates.Groups[4].Value);
                            int y2 = Int32.Parse(coordinates.Groups[5].Value);
                            for (int x = x1; x <= x2; x++)
                            {
                                for (int y = y1; y <= y2; y++)
                                {
                                    if (grid[x, y] == 0)
                                    {
                                        lightCount++;
                                    }
                                    grid[x, y]++;
                                }
                            }
                        }
                        if (line.StartsWith("turn off"))
                        {
                            var coordinates = Regex.Match(line, "(turn off|turn on|toggle) (\\d+),(\\d+) through (\\d+),(\\d+)");
                            int x1 = Int32.Parse(coordinates.Groups[2].Value);
                            int y1 = Int32.Parse(coordinates.Groups[3].Value);
                            int x2 = Int32.Parse(coordinates.Groups[4].Value);
                            int y2 = Int32.Parse(coordinates.Groups[5].Value);
                            for (int x = x1; x <= x2; x++)
                            {
                                for (int y = y1; y <= y2; y++)
                                {
                                    if (grid[x, y] != 0)
                                    {
                                        lightCount--;
                                    }
                                    grid[x, y] = Math.Max(grid[x, y]-1, 0);
                                }
                            }
                        }
                        if (line.StartsWith("toggle"))
                        {
                            var coordinates = Regex.Match(line, "(turn off|turn on|toggle) (\\d+),(\\d+) through (\\d+),(\\d+)");
                            int x1 = Int32.Parse(coordinates.Groups[2].Value);
                            int y1 = Int32.Parse(coordinates.Groups[3].Value);
                            int x2 = Int32.Parse(coordinates.Groups[4].Value);
                            int y2 = Int32.Parse(coordinates.Groups[5].Value);
                            for (int x = x1; x <= x2; x++)
                            {
                                for (int y = y1; y <= y2; y++)
                                {
                                    if (grid[x, y] != 0)
                                    {
                                        lightCount--;
                                    }
                                    else
                                    {
                                        lightCount++;
                                    }
                                    //grid[x, y] = !grid[x, y];
                                    grid[x, y] += 2;
                                }
                            }
                        }
                    }
                }
            }
            int totalBrightness = 0;
            lightCount = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    totalBrightness += grid[x,y];
                    if (grid[x, y] != 0)
                    {
                        lightCount++;
                    }
                }
            }

            Console.WriteLine("Found {0} lit lights", lightCount);
            Console.WriteLine("Found {0} total brighness", totalBrightness);
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            //var timeTarget = 10;
            var timeTarget = 1000000000;
            var map = File.ReadAllLines("Input.txt").Select(line => line.ToArray()).ToArray();
            var states = new List<string>();
            Print(map);
            var startDupe = 0;
            var endDupe = 0;
            for (int timer = 0; timer < timeTarget; timer++)
            {
                var next = map.Select(line => line.ToArray()).ToArray();
                for (int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[0].Length; x++)
                    {
                        var current = map[y][x];
                        switch (current)
                        {
                            case '.':
                                if (Adjacent(map, x, y).Count(a => a == '|') >= 3)
                                {
                                    next[y][x] = '|';
                                }
                                break;
                            case '|':
                                if (Adjacent(map, x, y).Count(a => a == '#') >= 3)
                                {
                                    next[y][x] = '#';
                                }
                                break;
                            case '#':
                                if (Adjacent(map, x, y).Count(a => a == '#') == 0 || Adjacent(map, x, y).Count(a => a == '|') == 0)
                                {
                                    next[y][x] = '.';
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                map = next;
                var stateString = new String(map.SelectMany(row => row.Select(col => col)).ToArray());
                if (states.Contains(stateString))
                {
                    Print(map);
                    var dupe = states.IndexOf(stateString);
                    Console.WriteLine("Unique state detected: " + timer + " == " + dupe);
                    startDupe = dupe;
                    endDupe = timer;
                    break;
                }
                else
                {
                    states.Add(stateString);
                }
                //Print(map);
            }
            Print(map);

            var target = ((timeTarget-1) - endDupe) % (endDupe - startDupe);
            var mapString = states[startDupe + target];
            map = Enumerable.Range(0, mapString.Length / map[0].Length).Select(i => mapString.Substring(i * map[0].Length, map[0].Length).ToArray()).ToArray();
            var trees = map.SelectMany(row => row.Where(col => col == '|')).Count();
            var lumberyards = map.SelectMany(row => row.Where(col => col == '#')).Count();
            var result = trees * lumberyards;
            Print(map);
            Console.WriteLine("Result: " + result);
            Console.ReadLine();

            //226080 too low
        }
        private static void Print(char[][] map)
        {
            Console.WriteLine();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    Console.Write(map[y][x]);
                }
                Console.WriteLine();
            }
        }
        static char[] Adjacent(char[][] map, int x, int y)
        {
            return new[] {
                y > 0 ? map[y - 1][x] : '.',
                y > 0 && x < map[0].Length - 1 ? map[y - 1][x + 1] : '.',
                x < map[0].Length - 1 ? map[y][x + 1] : '.',
                y < map.Length - 1 && x < map[0].Length - 1 ? map[y + 1][x + 1] : '.',
                y < map.Length - 1 ? map[y + 1][x] : '.',
                y < map.Length - 1 && x > 0 ? map[y + 1][x - 1] : '.',
                x > 0 ? map[y][x - 1] : '.',
                y > 0 && x > 0 ? map[y - 1][x - 1] : '.'
            };
        }
    }
}

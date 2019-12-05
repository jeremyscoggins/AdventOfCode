using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day17
{
    class Clay
    {
        public int x;
        public int y;

        public Clay(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Water
    {
        public int x;
        public int y;
        public int dir;

        public Water(int x, int y, int dir)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var free = new[] { '|', '.' };
            var clay = new List<Clay>();
            var lineMatch = new Regex("(x|y)=(\\d+), (x|y)=(\\d+)\\.\\.(\\d+)");
            var input = File.ReadAllLines("Input.txt");
            foreach (var line in input)
            {
                var match = lineMatch.Match(line);
                var coord1type = match.Groups[1].Value;
                var coord1 = int.Parse(match.Groups[2].Value);
                var coord2type = match.Groups[3].Value;
                var coord2 = int.Parse(match.Groups[4].Value);
                var coord3 = int.Parse(match.Groups[5].Value);

                for (int i = coord2; i <= coord3; i++)
                {
                    if (coord1type == "x")
                    {
                        clay.Add(new Clay(coord1, i));
                    }
                    else
                    {
                        clay.Add(new Clay(i, coord1));
                    }
                }
            }
            var minY = 0;
            var maxY = clay.Max(c => c.y);
            var minX = clay.Min(c => c.x) - 1;
            var maxX = clay.Max(c => c.x) + 1;

            var map = new char[(maxY-minY) + 1][];

            for (int y = minY; y <= maxY; y++)
            {
                map[y - minY] = new char[(maxX - minX) + 1];
                for (int x = minX; x <= maxX; x++)
                {
                    //if (clay.Any(c => c.x == x && c.y == y))
                    //{
                        //Console.Write("#");
                        //map[y - minY][x - minX] = '#';
                    //}
                    //else
                    //{
                        //Console.Write(".");
                        map[y - minY][x - minX] = '.';
                    //}
                }
                //Console.WriteLine();
            }
            foreach(var c in clay)
            {
                map[c.y - minY][c.x - minX] = '#';
            }
            Print2(map);

            var waterX = 500;
            var waterY = 0;
            var queue = new Queue<Water>();
            queue.Enqueue(new Water(waterX - minX, waterY - minY, 0));
            try
            {
                while (queue.Count > 0)
                {
                    if (queue.Count > 100)
                    {
                        break;
                    }
                    var water = queue.Dequeue();
                    //bool done = false;
                    //while (!done)
                    {
                        if (water.y + 1 >= map.Length)
                        {
                            //fell off the map
                            map[water.y][water.x] = '|';
                            //done = true;
                        }
                        else
                        {
                            while (water.y + 1 < map.Length && free.Contains(map[water.y + 1][water.x]))
                            {
                                //can move down?
                                map[water.y][water.x] = '|';
                                water.y++;
                                water.dir = 0;
                                //if (water.y < map.Length)
                                //{
                                //    queue.Enqueue(water);
                                //}
                            }
                            //else if (water.dir == 0)
                            if (water.y + 1 < map.Length)
                            {
                                map[water.y][water.x] = '|';
                                //can't move down anymore

                                //check left and right to see if we should rest

                                //scan left
                                var checkX = water.x;
                                bool fall = false;
                                while (!free.Contains(map[water.y + 1][checkX]) && free.Contains(map[water.y][checkX - 1]))
                                {
                                    checkX--;
                                    map[water.y][checkX] = '|';
                                }

                                if (free.Contains(map[water.y + 1][checkX]))
                                {
                                    if (map[water.y + 1][checkX] == '.')
                                    {
                                        queue.Enqueue(new Water(checkX, water.y + 1, 0));
                                    }
                                    fall = true;
                                }

                                var checkX2 = water.x;
                                while (!free.Contains(map[water.y + 1][checkX2]) && free.Contains(map[water.y][checkX2 + 1]))
                                {
                                    checkX2++;
                                    map[water.y][checkX2] = '|';
                                }

                                if (free.Contains(map[water.y + 1][checkX2]))
                                {
                                    if (map[water.y + 1][checkX2] == '.')
                                    {
                                        queue.Enqueue(new Water(checkX2, water.y + 1, 0));
                                    }
                                    fall = true;
                                }

                                if (!fall)
                                {
                                    for (int fillx = checkX; fillx <= checkX2; fillx++)
                                    {
                                        map[water.y][fillx] = '~';
                                    }
                                    queue.Enqueue(new Water(water.x, water.y - 1, 0));
                                }


                                //var canMove = false;
                                //if (map[water.y][water.x - 1] == '.' || map[water.y][water.x - 1] == '|')
                                //{
                                //    //can move left?
                                //    map[water.y][water.x] = '|';
                                //    queue.Enqueue(new Water(water.x - 1, water.y, 1));
                                //    canMove = true;
                                //}
                                //if (map[water.y][water.x + 1] == '.' || map[water.y][water.x + 1] == '|')
                                //{
                                //    //can move right?
                                //    map[water.y][water.x] = '|';
                                //    queue.Enqueue(new Water(water.x + 1, water.y, 2));
                                //    canMove = true;
                                //}
                                //if (!canMove)
                                //{
                                //    map[water.y][water.x] = '~';
                                //    if (queue.Count == 0)
                                //    {
                                //        queue.Enqueue(new Water(waterX - minX, waterY - minY, 0));
                                //    }
                                //}
                            }
                            else
                            {
                                //bottom of the map, mark that one
                                map[water.y][water.x] = '|';
                            }
                            //else if ((map[water.y][water.x - 1] == '.' || map[water.y][water.x - 1] == '|') && water.dir == 1)
                            //{
                            //    //can move left?
                            //    map[water.y][water.x] = '|';
                            //    water.x--;
                            //    water.dir = 1;
                            //    queue.Enqueue(water);
                            //}
                            //else if ((map[water.y][water.x + 1] == '.' || map[water.y][water.x + 1] == '|') && water.dir == 2)
                            //{
                            //    //can move left?
                            //    map[water.y][water.x] = '|';
                            //    water.x++;
                            //    water.dir = 2;
                            //    queue.Enqueue(water);
                            //}
                            //else
                            //{
                            //    //done = true;
                            //    map[water.y][water.x] = '~';
                            //    if (queue.Count == 0)
                            //    {
                            //        queue.Enqueue(new Water(waterX - minX, waterY - minY, 0));
                            //    }
                            //}
                            //if (curY > maxY - minY)
                            //{
                            //    done = true;
                            //}
                        }
                    }
                    //Print(map);
                    //Print2(map);
                }

                //Print(map);
                Print2(map);

                var skip = clay.Min(c => c.y);
                var result = map.Skip(skip).SelectMany(r => r).Where(c => c == '|' || c == '~').Count();
                Console.WriteLine("Result: " + result);
                var result2 = map.Skip(skip).SelectMany(r => r).Where(c => c == '~').Count();
                Console.WriteLine("Result2: " + result2);
                //39164
                //9578 = too low
            }
            catch (Exception)
            {
                Print2(map);
            }
            Console.ReadLine();
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
        private static void Print2(char[][] map)
        {
            using (var file = File.CreateText("Output.txt"))
            {
                for (int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[0].Length; x++)
                    {
                        if (map[y][x] == '.')
                        {
                            file.Write(' ');
                        }
                        else
                        {
                            file.Write(map[y][x]);
                        }
                    }
                    file.WriteLine();
                }
            }
        }
    }
}

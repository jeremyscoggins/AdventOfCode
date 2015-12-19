using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static bool[,] grid = new bool[102,102];
        static bool[,] grid2 = new bool[102, 102];
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 101);
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    uint y = 1;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        uint x = 1;
                        foreach (var c in line)
                        {
                            grid[x, y] = c == '#';
                            if ((x == 1 && y == 1)
                                || (x == 100 && y == 1)
                                || (x == 100 && y == 100)
                                || (x == 1 && y == 100))
                            {
                                grid[x, y] = true;
                            }
                            x++;
                        }
                        y++;
                    }
                }
            }
            for (uint t = 0; t < 100; t++)
            {
                Console.SetCursorPosition(0, 0);
                for (int y = 1; y < 101; y++)
                {
                    for (int x = 1; x < 101; x++)
                    {
                        bool state = grid[x, y];
                        Console.Write(state ? "#" : ".");
                        bool[] neighbors = new bool[8];
                        neighbors[0] = grid[wrap(x-1), wrap(y-1)];
                        neighbors[1] = grid[x, wrap(y - 1)];
                        neighbors[2] = grid[wrap(x + 1), wrap(y - 1)];
                        neighbors[3] = grid[wrap(x + 1), y];
                        neighbors[4] = grid[wrap(x + 1), wrap(y + 1)];
                        neighbors[5] = grid[x, wrap(y + 1)];
                        neighbors[6] = grid[wrap(x - 1), wrap(y + 1)];
                        neighbors[7] = grid[wrap(x - 1), y];
                        uint neighborCount = (uint)neighbors.Sum(n => n ? (uint)1 : 0);
                        if (state && (neighborCount == 2 || neighborCount == 3))
                        {
                            grid2[x, y] = true;
                        }
                        else if (!state && neighborCount == 3)
                        {
                            grid2[x, y] = true;
                        }
                        else
                        {
                            grid2[x, y] = false;
                            if ((x == 1 && y == 1)
                                || (x == 100 && y == 1)
                                || (x == 100 && y == 100)
                                || (x == 1 && y == 100))
                            {
                                grid2[x, y] = true;
                            }
                        }
                    }
                }
                var temp = grid;
                grid = grid2;
                grid2 = temp;
                Thread.Sleep(100);
            }

            uint count = 0;
            for (uint y = 1; y < 101; y++)
            {
                for (uint x = 1; x < 101; x++)
                {
                    count += grid[x, y] ? (uint)1 : 0;
                }
            }
            Console.WriteLine("the count after 100 steps is {0}", count);
            Console.ReadLine();
        }

        private static uint wrap(int input)
        {
            if (input < 0)
            {
                return (uint)(input + 101);
            }
            if (input > 100)
            {
                return (uint)(input - 101);
            }
            return (uint)input;
        }
    }
}

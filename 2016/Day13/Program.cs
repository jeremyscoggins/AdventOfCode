using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Item
    {
        public int Steps { get; set; }
        public Point[] History { get; set; }
    }

    class Program
    {
        //const int input = 10;
        const int input = 1350;

        static Point[] possibleDirections = new Point[] { new Point { X = 0, Y = -1 }, new Point { X = 1, Y = 0 }, new Point { X = 0, Y = 1 }, new Point { X = -1, Y = 0 } };
        //static Point[] possibleDirections = new Point[] { new Point { X = -1, Y = -1 }, new Point { X = 0, Y = -1 }, new Point { X = 1, Y = -1 }, new Point { X = 1, Y = 0 }, new Point { X = 1, Y = 1 }, new Point { X = 0, Y = 1 }, new Point { X = -1, Y = 1 }, new Point { X = -1, Y = 0 } };
        static void Main(string[] args)
        {
            //var width = 10;
            //var height = 7;
            var width = 40;
            var height = 48;

            //var destX = 7;
            //var destY = 4;
            var destX = 31;
            var destY = 39;


            var walls = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var isWall = CountBits((x * x + 3 * x + 2 * x * y + y + y * y) + input) % 2 != 0;
                    walls[x, y] = isWall ? '#' : '.';
                }
            }


            var queue = new Queue<Item>();
            var seen = new HashSet<string>();


            queue.Enqueue(new Item { Steps = 0, History = new Point[] { new Point { X = 1, Y = 1 } } });
            seen.Add("1,1");

            Item winner = null;

            bool count50 = false;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var history = current.History;
                var currentPoint = history[history.Length - 1];
                var steps = current.Steps;
                if (steps == 50 && ! count50)
                {
                    count50 = true;
                    Console.WriteLine("locations reached by 50 steps is: {0}", seen.Count);
                }
                foreach (var direction in possibleDirections)
                {
                    var newPoint = new Point { X = currentPoint.X + direction.X, Y = currentPoint.Y + direction.Y };
                    if (newPoint.X >= 0 && newPoint.X < width && newPoint.Y >= 0 && newPoint.Y < height)
                    {
                        if (walls[newPoint.X, newPoint.Y] == '.')
                        {
                            if (seen.Add(newPoint.X + "," + newPoint.Y))
                            {
                                var newHistory = new Point[history.Length + 1];
                                Array.Copy(history, newHistory, history.Length);
                                newHistory[newHistory.Length - 1] = newPoint;


                                if (newPoint.X == destX && newPoint.Y == destY)
                                {
                                    //winner winner chicken dinner
                                    winner = new Item { Steps = steps + 1, History = newHistory };
                                    queue.Clear();
                                    break;
                                }
                                else
                                {
                                    queue.Enqueue(new Item { Steps = steps + 1, History = newHistory });
                                }
                            }
                        }
                    }
                }

            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool path = false;
                    foreach (var historyItem in winner.History)
                    {
                        if (historyItem.X == x && historyItem.Y == y)
                        {
                            path = true;
                            break;
                        }
                    }
                    if (!path)
                    {
                        Console.Write(walls[x, y]);
                    }
                    else
                    {
                        Console.Write("O");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("winning steps: {0}", winner.Steps);


        }

        public static int CountBits(long value)
        {
            return Convert.ToString(value, 2).ToArray().Count(c => c == '1');
        }
    }
}

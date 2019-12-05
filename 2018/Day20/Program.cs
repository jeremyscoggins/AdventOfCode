using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    internal class PointComparer : IEqualityComparer<Point>
    {
        bool IEqualityComparer<Point>.Equals(Point x, Point y)
        {
            return x == y;
        }

        int IEqualityComparer<Point>.GetHashCode(Point obj)
        {
            var hashcode = 23;
            hashcode = (hashcode * 37) + obj.x;
            hashcode = (hashcode * 37) + obj.y;
            return hashcode;
        }
    }
    internal class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Point a, Point b)
        {
            return (object)a != null && (object)b != null && a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Point a, Point b)
        {
            if ((object)a == null && (object)b == null)
            {
                return false;
            }
            return ((object)a == null && (object)b != null) || ((object)a != null && (object)b == null) || a.x != b.x || a.y != b.y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }
    }
    internal class Door
    {
        public Point a;
        public Point b;

        public Door(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadAllText("Input.txt").ToArray();
            instructions = instructions.Skip(1).Take(instructions.Length - 2).ToArray();
            //var queue = new Queue<Tuple<int, Point, Point>>();
            //queue.Enqueue(Tuple.Create(0, new Point(0, 0), new Point(0, 0)));
            var doors = new List<Door>();
            var stack = new Stack<Point>();
            var current = new Point(0, 0);
            for (int i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i];
                Point next;
                switch (instruction)
                {
                    case 'N':
                        next = new Point(current.x, current.y - 1);
                        doors.Add(new Door(next, current));
                        current = next;
                        break;
                    case 'E':
                        next = new Point(current.x + 1, current.y);
                        doors.Add(new Door(current, next));
                        current = next;
                        break;
                    case 'S':
                        next = new Point(current.x, current.y + 1);
                        doors.Add(new Door(current, next));
                        current = next;
                        break;
                    case 'W':
                        next = new Point(current.x - 1, current.y);
                        doors.Add(new Door(next, current));
                        current = next;
                        break;
                    case '(':
                        stack.Push(current);
                        break;
                    case '|':
                        current = stack.Pop();
                        stack.Push(current);
                        break;
                    case ')':
                        stack.Pop();
                        break;
                }
            }



            doors = doors.OrderBy(d => d.a.y).ThenBy(d => d.a.x).ToList();

            //var points = doors.SelectMany(d => new[] { d.a, d.b }).Distinct();
            var minX = doors.Min(p => p.a.x);
            var maxX = doors.Max(p => p.b.x);
            var minY = doors.Min(p => p.a.y);
            var maxY = doors.Max(p => p.b.y);
            var sizeX = (maxX - minX) + 1;
            var sizeY = (maxY - minY) + 1;
            var map = Enumerable.Range(0, sizeY * 2).Select(row => Enumerable.Range(0, sizeX * 2).Select(col => row % 2 == 0 && col % 2 == 0 ? '.' : '#').ToArray()).ToArray();

            foreach (var door in doors)
            {
                var offset = new Point((door.a.x - minX) * 2, (door.a.y - minY) * 2);

                if (door.a.x < door.b.x)
                {
                    //door to the right
                    map[offset.y][offset.x + 1] = '|';
                }
                else
                {
                    //door below
                    map[offset.y + 1][offset.x] = '-';
                }
            }


            var start = new Point(minX * -1 * 2, minY * -1 * 2);
            List<Tuple<Point, int>> distances = GetDistances(map, start);

            //print(map, sizeX, sizeY, start);

            var maxDist = distances.Max(d => d.Item2);

            Console.WriteLine("MaxDist: " + maxDist);

            var shortest = new List<Tuple<Point, int>>();

            for (int y = 0; y < sizeY * 2; y++)
            {
                for (int x = 0; x < sizeX * 2; x++)
                {
                    if (map[y][x] == '.')
                    {
                        var minDist = GetDistances(map, new Point(x, y), start)[0];
                        if (minDist.Item2 >= 1000)
                        {
                            shortest.Add(minDist);
                        }
                    }
                }
            }

            Console.WriteLine("PathCount: " + shortest.Count);

            Console.ReadLine();
        }

        private static List<Tuple<Point, int>> GetDistances(char[][] map, Point start, Point target = null)
        {

            //print(map, sizeX, sizeY, start);

            var distances = new List<Tuple<Point, int>>();

            var visited = new HashSet<Point>(new PointComparer());
            var queue = new Queue<Tuple<Point, int>>();
            queue.Enqueue(Tuple.Create(start, 0));
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                if (visited.Add(item.Item1))
                {
                    if (target != null && target.x == item.Item1.x && target.y == item.Item1.y)
                    {
                        return new List<Tuple<Point, int>>() { item };
                    }
                    distances.Add(item);
                    //map[item.Item1.y][item.Item1.x] = item.Item2.ToString()[0];
                    if (item.Item1.y > 0 && map[item.Item1.y - 1][item.Item1.x] == '-')
                    {
                        //up
                        queue.Enqueue(Tuple.Create(new Point(item.Item1.x, item.Item1.y - 2), item.Item2 + 1));
                    }
                    if (map[item.Item1.y][item.Item1.x + 1] == '|')
                    {
                        //right
                        queue.Enqueue(Tuple.Create(new Point(item.Item1.x + 2, item.Item1.y), item.Item2 + 1));
                    }
                    if (map[item.Item1.y + 1][item.Item1.x] == '-')
                    {
                        //down
                        queue.Enqueue(Tuple.Create(new Point(item.Item1.x, item.Item1.y + 2), item.Item2 + 1));
                    }
                    if (item.Item1.x > 0 && map[item.Item1.y][item.Item1.x - 1] == '|')
                    {
                        //left
                        queue.Enqueue(Tuple.Create(new Point(item.Item1.x - 2, item.Item1.y), item.Item2 + 1));
                    }
                }
            }

            return distances;
        }

        static void print(char[][] map, int sizeX, int sizeY, Point start)
        {
            for (int y = -1; y < sizeY * 2; y++)
            {
                for (int x = -1; x < sizeX * 2; x++)
                {
                    if (x == -1 || y == -1)
                    {
                        Console.Write('#');
                    }
                    else if (start.x == x && start.y == y)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(map[y][x]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
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

        public override bool Equals(object obj)
        {
            var point = obj as Point;
            return point != null &&
                   x == point.x &&
                   y == point.y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
    class PriorityQueue<T>
    {
        List<Tuple<int, T>> items = new List<Tuple<int, T>>();

        public void Enqueue(T item, int priority)
        {
            items.Add(Tuple.Create(priority, item));
            items.Sort((a, b) => { return a.Item1 - b.Item1; });
        }

        public T Dequeue()
        {
            var result = items[0];
            items.RemoveAt(0);
            return result.Item2;
        }

        public int Count()
        {
            return items.Count;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Program2.Main2();
            //INPUT
            //depth: 5355
            //target: 14,796
            var depth = 5355;
            var target = new[] { 14, 796 };
            //var depth = 510;
            //var target = new[] { 10, 10 };
            //var depth = 510;
            //var target = new[] { 29, 9 };
            var erosionLevels = new int[target[1]+1, target[0]+1];
            var map = new char[target[1]+1, target[0]+1];
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    int geoIndex;
                    if (x == 0 && y == 0)
                    {
                        geoIndex = 0;
                    }
                    else if (x == target[1] && y == target[0])
                    {
                        geoIndex = 0;
                    }
                    else if (y == 0)
                    {
                        geoIndex = x * 16807;
                    }
                    else if (x == 0)
                    {
                        geoIndex = y * 48271;
                    }
                    else
                    {
                        geoIndex = erosionLevels[y, x - 1] * erosionLevels[y - 1, x];
                    }

                    int erosionLevel = (geoIndex + depth) % 20183;
                    erosionLevels[y, x] = erosionLevel;
                    if (erosionLevel % 3 == 0)
                    {
                        map[y, x] = '.';//rocky
                    }
                    else if (erosionLevel % 3 == 1)
                    {
                        map[y, x] = '=';//wet
                    }
                    if (erosionLevel % 3 == 2)
                    {
                        map[y, x] = '|';//narrow
                    }

                }
            }
            map[0, 0] = 'M';
            map[target[1], target[0]] = 'T';
            var riskLevel = map.Cast<char>().Sum(v => v == '=' ? 1 : v == '|' ? 2 : 0);
            Print(map, target);



            Search(new Point(target[1], target[0]), depth);



            Console.ReadLine();
            //11974 too high
        }
        static void Print(char[,] map, int[] target)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (x == 0 && y == 0)
                    {
                        Console.Write('M');
                    }
                    else if (x == target[0] && y == target[1])
                    {
                        Console.Write('T');
                    }
                    else
                    {
                        Console.Write(map[y, x]);
                    }
                }
                Console.WriteLine();
            }
        }

        static Dictionary<Point, int> geoIndexes = new Dictionary<Point, int>();
        static int getGeoIndex(Point location, Point target, int depth)
        {
            if (geoIndexes.ContainsKey(location))
            {
                return geoIndexes[location];
            }
            int geoIndex;
            if (location.x == 0 && location.y == 0)
            {
                geoIndex = 0;
            }
            else if (location.x == target.x && location.y == target.y)
            {
                geoIndex = 0;
            }
            else if (location.y == 0)
            {
                geoIndex = location.x * 16807;
            }
            else if (location.x == 0)
            {
                geoIndex = location.y * 48271;
            }
            else
            {
                geoIndex = getErosionLevel(new Point(location.x - 1, location.y), target, depth) * getErosionLevel(new Point(location.x, location.y - 1), target, depth);
            }
            geoIndexes[location] = geoIndex;
            return geoIndex;
        }

        static Dictionary<Point, char> locTypes = new Dictionary<Point, char>();
        static char getLocType(Point location, Point target, int depth)
        {
            if (locTypes.ContainsKey(location))
            {
                return locTypes[location];
            }
            var erosionLevel = getErosionLevel(location, target, depth);
            if (erosionLevel % 3 == 0)
            {
                locTypes[location] = '.';
                return '.';//rocky
            }
            else if (erosionLevel % 3 == 1)
            {
                locTypes[location] = '=';
                return '=';//wet
            }
            if (erosionLevel % 3 == 2)
            {
                locTypes[location] = '|';
                return '|';//narrow
            }
            locTypes[location] = ' ';
            return ' '; //not possible
        }

        static int getErosionLevel(Point location, Point target, int depth)
        {
            int geoIndex = getGeoIndex(location, target, depth);
            int erosionLevel = (geoIndex + depth) % 20183;
            return erosionLevel;
        }

        static Point[] directions = new Point[] { new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) };
        static void Search(Point target, int depth)
        {
            var queue = new Queue<(Point location, char tool, int minutes, int switchTimer)>();
            var visited = new HashSet<(Point location, char tool)>();
            queue.Enqueue((new Point(0, 0), 'T', 0, 0));//T = Torch, G = Climbing Gear, N = neither
            while (queue.Count > 0)
            {
                (Point location, char tool, int minutes, int switchTimer) = queue.Dequeue();
                if (switchTimer > 0)
                {
                    queue.Enqueue((location, tool, minutes + 1, switchTimer - 1));
                }
                else if (visited.Add((location, tool)))
                {
                    if (location == target)// && tool == 'T')
                    {
                        if (tool == 'T')
                        {
                            Console.WriteLine("fastest: " + minutes);
                        }
                        else
                        {
                            Console.WriteLine("fastest: " + (minutes + 7));
                        }
                    }
                    else
                    {
                        //Print2(visited);
                        foreach (var direction in directions)
                        {
                            var targetLoc = location + direction;
                            //if (targetLoc.y == target.y && targetLoc.x == target.x)
                            //{
                            //    if (tool == 'T')
                            //    {
                            //        queue.Enqueue((targetLoc, tool, minutes + 1, 0));
                            //    }
                            //    else
                            //    {
                            //        queue.Enqueue((location, 'T', minutes + 1, 6));
                            //    }
                            //}
                            //else if (targetLoc.y >= 0 && targetLoc.x >= 0)
                            if (targetLoc.y >= 0 && targetLoc.x >= 0)
                            {
                                {
                                    //valid move
                                    var targetType = getLocType(targetLoc, target, depth);
                                    switch (targetType)
                                    {
                                        case '.'://rocky
                                            if (tool == 'T' || tool == 'G')
                                            {
                                                //we can proceed freely
                                                queue.Enqueue((targetLoc, tool, minutes + 1, 0));
                                            }
                                            if(tool != 'T')
                                            {
                                                queue.Enqueue((location, 'T', minutes + 1, 6));
                                            }
                                            if (tool != 'G')
                                            {
                                                queue.Enqueue((location, 'G', minutes + 1, 6));
                                            }
                                            break;
                                        case '='://wet
                                            if (tool == 'G' || tool == 'N')
                                            {
                                                //we can proceed freely
                                                queue.Enqueue((targetLoc, tool, minutes + 1, 0));
                                            }
                                            if (tool != 'G')
                                            {
                                                queue.Enqueue((location, 'G', minutes + 1, 6));
                                            }
                                            if (tool != 'N')
                                            {
                                                queue.Enqueue((location, 'N', minutes + 1, 6));
                                            }
                                            break;
                                        case '|'://narrow
                                            if (tool == 'T' || tool == 'N')
                                            {
                                                //we can proceed freely
                                                queue.Enqueue((targetLoc, tool, minutes + 1, 0));
                                            }
                                            if (tool != 'N')
                                            {
                                                queue.Enqueue((location, 'N', minutes + 1, 6));
                                            }
                                            if (tool != 'T')
                                            {
                                                queue.Enqueue((location, 'T', minutes + 1, 6));
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //private static void Print2(HashSet<(Point location, char tool)> visited)
        //{
        //    var organized = visited.OrderBy(v => v.location.y).ThenBy(v => v.location.x).Select(v => v.location).Distinct();
        //    foreach (var item in organized)
        //    {
        //        if(item.x > )
        //    }
        //}
    }
}

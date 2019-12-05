using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        const char wall = '#';
        static Point[] adjacent = { new Point(0,-1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) };
        static void Main(string[] args)
        {
            var map = File.ReadAllLines("Input.txt").Select(l => l.ToArray()).ToArray();


            //var result = PathFind(map, new Point(1, 1), new Point(8, 8));
            //Print(map, new HashSet<Point>(result, new PointComparer()));

            //var result = PathFind2(map, new Point(10, 1));
            //var resultMap = map.Select((row,r) => row.Select((col, c) => col == '.' && result[r][c] != int.MaxValue ? (char)(result[r][c]+48) : col).ToArray()).ToArray();
            //Print(resultMap, new HashSet<Point>());


            int elfAttackPower = 3;
            bool haultOnElfDeath = true;
            while(true)
            {
                List<Unit> units;
                var rounds = Simulate(map, out units, elfAttackPower, haultOnElfDeath);
                Print(map, units);
                var remainingHp = units.Where(u => u.hp > 0).Sum(u => u.hp);
                Console.WriteLine("Rounds: " + rounds + " Remining HP: " + remainingHp + " Total: " + (rounds*remainingHp));
                if (units.Where(u => u.type == 0 && u.hp <= 0).Count() > 0)
                {
                    elfAttackPower++;
                }
                else
                {
                    break;
                }
            };

            //260360 = too low
            //263327 = right answer
            Console.ReadLine();
        }

        static int Simulate(char[][] initial, out List<Unit> units, int elfAttackPower, bool haultOnElfDeath)
        {
            var map = initial.Select(r => r.Select(c => c).ToArray()).ToArray();
            //var goblins = new List<Goblin>();
            //var elves = new List<Elf>();
            units = new List<Unit>();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    var loc = map[y][x];
                    if (loc == 'G')
                    {
                        units.Add(new Goblin(new Point(x, y)));
                        //map[y][x] = '.';
                    }
                    if (loc == 'E')
                    {
                        units.Add(new Elf(new Point(x, y)));
                        //map[y][x] = '.';
                    }
                }
            }

            int rounds = 0;
            bool play = true;
            while (play)
            {
                //for (int y = 0; y < map.Length; y++)
                //{
                //    for (int x = 0; x < map[0].Length; x++)
                //    {
                //    }
                //}
                foreach (var unit in units.Where(u => u.hp > 0).OrderBy(u => u.loc.y).ThenBy(u => u.loc.x).ToArray())
                {
                    if (unit.hp <= 0)
                    {
                        continue;
                    }
                    var targets = units.Where(u => u.hp > 0 && u.type != unit.type).ToArray();
                    if (targets.Length > 0)
                    {
                        var adjacentTarget = targets.Where(t => IsAdjacent(t.loc, unit.loc)).OrderBy(t => t.hp).ThenBy(u => u.loc.y).ThenBy(u => u.loc.x).FirstOrDefault();
                        if (adjacentTarget == null)
                        {
                            var costs = PathFind2(map, unit.loc);
                            var targetLoc = targets.SelectMany(t => Adjacents(t.loc).Where(a => map[a.y][a.x] == '.').Select(a => new { target = t, adjacent = a, cost = costs[a.y][a.x] })).OrderBy(l => l.cost).ThenBy(l => l.adjacent.y).ThenBy(l => l.adjacent.x).FirstOrDefault();
                            if (targetLoc != null && targetLoc.cost < int.MaxValue)
                            {
                                var costs2 = PathFind2(map, targetLoc.adjacent);
                                var startLoc = Adjacents(unit.loc).Where(a => map[a.y][a.x] == '.').Select(a => new { adjacent = a, cost = costs2[a.y][a.x] }).OrderBy(s => s.cost).ThenBy(s => s.adjacent.y).ThenBy(s => s.adjacent.x).First();
                                map[unit.loc.y][unit.loc.x] = '.';
                                unit.loc = startLoc.adjacent;
                                map[unit.loc.y][unit.loc.x] = unit.type == 1 ? 'G' : 'E';
                                //move
                                adjacentTarget = targets.Where(t => IsAdjacent(t.loc, unit.loc)).OrderBy(t => t.hp).ThenBy(u => u.loc.y).ThenBy(u => u.loc.x).FirstOrDefault();
                            }
                        }
                        if (adjacentTarget != null)
                        {
                            adjacentTarget.hp -= unit.type == 1 ? 3 : elfAttackPower;
                            if (adjacentTarget.hp <= 0)
                            {
                                map[adjacentTarget.loc.y][adjacentTarget.loc.x] = '.';
                                if (adjacentTarget.type == 0 && haultOnElfDeath)
                                {
                                    play = false;
                                    break;
                                }
                            }
                            //attack

                        }
                    }
                    else
                    {
                        //no targets, what do?
                        play = false;
                    }

                }
                if (play)
                {
                    //Print(map, units);
                    rounds++;
                }
            }
            return rounds;
        }

        static Point[] Adjacents(Point point)
        {
            var results = new List<Point>();
            foreach (var test in adjacent)
            {
                results.Add(point + test);
            }
            return results.ToArray();
        }
        static bool IsAdjacent(Point a, Point b)
        {
            foreach (var test in adjacent)
            {
                if (a + test == b)
                {
                    return true;
                }
            }
            return false;
        }

        static void Print(char[][] map, ICollection<Point> visited)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    if (visited.Contains(new Point(x, y)))
                    {
                        Console.Write('V');
                    }
                    else
                    {
                        Console.Write(map[y][x]);
                    }
                }
                Console.WriteLine();
            }
        }

        private static void Print(char[][] map, List<Unit> units)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    Console.Write(map[y][x]);
                }
                foreach (var unit in units.Where(u => u.hp > 0 && u.loc.y == y).OrderBy(u => u.loc.x))
                {
                    Console.Write("  " + (unit.type == 1 ? 'G' : 'E') + "(" + unit.hp + ")");
                }
                Console.WriteLine();
            }
        }


        static Point[] PathFind(char[][] map, Point start, Point end)
        {
            HashSet<Point> visited = new HashSet<Point>(new PointComparer());
            Queue<List<Point>> queue = new Queue<List<Point>>();
            //visited.Add(start);
            queue.Enqueue(new List<Point> { start });

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Add(current.Last()))
                {
                    var adjacents = Adjacents(current.Last()).Where(a => map[a.y][a.x] == '.');
                    foreach (var adjacent in adjacents)
                    {
                        if (adjacent == end)
                        {
                            return current.Concat(new List<Point> { adjacent }).ToArray();
                        }
                        queue.Enqueue(current.Concat(new List<Point> { adjacent }).ToList());
                    }
                    //Print(map, visited);
                }
            }
            return null;
        }

        static int[][] PathFind2(char[][] map, Point start)
        {
            var results = map.Select(row => row.Select(col => int.MaxValue).ToArray()).ToArray();
            results[start.y][start.x] = 0;
            HashSet<Point> visited = new HashSet<Point>(new PointComparer());
            Queue<List<Point>> queue = new Queue<List<Point>>();
            //visited.Add(start);
            queue.Enqueue(new List<Point> { start });

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Add(current.Last()))
                {
                    results[current.Last().y][current.Last().x] = current.Count - 1;
                    var adjacents = Adjacents(current.Last()).Where(a => map[a.y][a.x] == '.');
                    foreach (var adjacent in adjacents)
                    {
                        queue.Enqueue(current.Concat(new List<Point> { adjacent }).ToList());
                    }
                    //Print(map, visited);
                }
            }
            return results;
        }
    }

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
            return ((object)a == null && (object)b != null) || ((object)a != null && (object)b == null) || a.x != b.x || a.y != b.y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }
    }

    internal class Unit
    {
        public int type;
        public int hp = 200;
        public Point loc;
    }

    internal class Elf : Unit
    {

        public Elf(Point loc)
        {
            this.loc = loc;
            this.type = 0;
        }
    }

    internal class Goblin : Unit
    {

        public Goblin(Point loc)
        {
            this.loc = loc;
            this.type = 1;
        }
    }
}

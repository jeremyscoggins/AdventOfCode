using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day22
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Avail { get; set; }
        public int Pct { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = obj as Node;
            return X == other.X && Y == other.Y;
        }
    }

    class State
    {
        public int EmptyX { get; set; }
        public int EmptyY { get; set; }
        public int TargetDataX { get; set; }
        public int TargetDataY { get; set; }
        public List<string> Moves { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = obj as State;
            return EmptyX == other.EmptyX && EmptyY == other.EmptyY && TargetDataX == other.TargetDataX && TargetDataY == other.TargetDataY;
        }
    }

    class Program
    {
        static Regex regex = new Regex(@"/dev/grid/node-x(\d+)-y(\d+)\s+(\d+)T\s+(\d+)T\s+(\d+)T\s+(\d+)%");
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt").ToArray();
            var width = 39;
            var height = 25;
            //var width = 3;
            //var height = 3;
            var nodes = new Node[width, height];
            Node emptyNode = null;
            var pairs = new List<string>();
            foreach (var line in input)
            {
                if (regex.IsMatch(line))
                {
                    var match = regex.Match(line);
                    var x = int.Parse(match.Groups[1].Value);
                    var y = int.Parse(match.Groups[2].Value);
                    var size = int.Parse(match.Groups[3].Value);
                    var used = int.Parse(match.Groups[4].Value);
                    var avail = int.Parse(match.Groups[5].Value);
                    var pct = int.Parse(match.Groups[6].Value);
                    nodes[x, y] = new Node { X = x, Y = y, Size = size, Used = used, Avail = avail, Pct = pct };
                    if (used == 0)
                    {
                        emptyNode = nodes[x, y];
                    }
                }
            }

            //for (int x = 0; x < 38; x++)
            //{
            //    for (int y = 0; y < 38; x++)
            //    {
            //    }
            //}
            foreach (var nodeA in nodes)
            {
                foreach (var nodeB in nodes)
                {
                    if (nodeA != nodeB)
                    {
                        if (nodeA.Used > 0 && nodeA.Used <= nodeB.Avail)
                        {
                            pairs.Add(nodeA.X + "," + nodeA.Y + "->" + nodeB.X + "," + nodeB.Y);
                        }
                    }
                }
            }
            Console.WriteLine("number of viable pairs: {0}", pairs.Count);

            //var sorted = nodes.Cast<Node>().OrderBy(n => n.Used).Select(n => n.Used).ToArray();

            var queue = new Queue<State>();
            var seen = new HashSet<string>();
            
            queue.Enqueue(new State { EmptyX = emptyNode.X, EmptyY = emptyNode.Y, Moves = new List<string>(), TargetDataX = width-1, TargetDataY = 0});
            
            
            var possibleDirections = new Point[] { new Point { X = 0, Y = -1 }, new Point { X = 1, Y = 0 }, new Point { X = 0, Y = 1 }, new Point { X = -1, Y = 0 } };
            
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var direction in possibleDirections)
                {
                    var newX = current.EmptyX + direction.X;
                    var newY = current.EmptyY + direction.Y;
                    if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                    {
                        var target = nodes[newX, newY];
                        if (target.Used < 400)
                        {
                            var next = new State();


                            //not a wall
                            if (target.X == current.TargetDataX && target.Y == current.TargetDataY)
                            {
                                next.TargetDataX = current.EmptyX;
                                next.TargetDataY = current.EmptyY;
                            }
                            else
                            {
                                next.TargetDataX = current.TargetDataX;
                                next.TargetDataY = current.TargetDataY;
                            }

                            next.EmptyX = target.X;
                            next.EmptyY = target.Y;

                            if (seen.Add(next.EmptyX + " " + next.EmptyY + " " + next.TargetDataX + " " + next.TargetDataY))
                            {
                                next.Moves = new List<string>(current.Moves);
                                next.Moves.Add(next.EmptyX + " " + next.EmptyY + " " + next.TargetDataX + " " + next.TargetDataY);
                                if (next.TargetDataX == 0 && next.TargetDataY == 0)
                                {
                                    //winner winner chicken dinner
                                    Console.WriteLine("the winning number of moves is {0}", next.Moves.Count);
                                    return;
                                }
                                queue.Enqueue(next);
                            }
                        }
                    }
                }
            }

        }
    }
}

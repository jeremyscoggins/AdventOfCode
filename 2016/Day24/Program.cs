using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    class State
    {
        public Point Location { get; set; }
        public int Steps { get; set; }
        public HashSet<int> Visited { get; set; }
    }
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var possibleDirections = new Point[] { new Point { X = 0, Y = -1 }, new Point { X = 1, Y = 0 }, new Point { X = 0, Y = 1 }, new Point { X = -1, Y = 0 } };
            var input = File.ReadAllLines("Input.txt").ToArray();
            var height = input.Length;
            var width = input[0].Length;
            int locations = 8;
            var start = new Point { X = 1, Y = 1 };
            var seen = new HashSet<string>();
            seen.Add("1 1 0");
            var queue = new Queue<State>();
            queue.Enqueue(new State { Location = new Point { X = 29, Y = 17 }, Steps = 0, Visited = new HashSet<int>(new[] { 0 }) });

            var collectedThem = false;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var direction in possibleDirections)
                {
                    var newLocation = new Point { X = current.Location.X + direction.X, Y = current.Location.Y + direction.Y };
                    if (newLocation.X >= 0 && newLocation.X < width && newLocation.Y >= 0 && newLocation.Y < height)
                    {
                        var character = input[newLocation.Y][newLocation.X];
                        if (character != '#')
                        {
                            var newState = new State { Location = newLocation, Steps = current.Steps + 1, Visited = new HashSet<int>(current.Visited) };
                            int visit;
                            if (int.TryParse(character.ToString(), out visit))
                            {
                                newState.Visited.Add(visit);
                                if (newState.Visited.Count == locations)
                                {
                                    //winner winner chicken dinner
                                    if (!collectedThem)
                                    {
                                        Console.WriteLine("the final number of steps is: {0}", newState.Steps);
                                        queue.Clear();
                                        collectedThem = true;
                                    }
                                    else if(visit == 0)
                                    {
                                        Console.WriteLine("the final number of steps is: {0}", newState.Steps);
                                        return;
                                    }
                                }

                            }
                            if (seen.Add(newLocation.X + " " + newLocation.Y + String.Join(" ", newState.Visited.ToArray())))
                            {
                                queue.Enqueue(newState);
                            }
                        }
                    }
                }
            }
        }
    }
}

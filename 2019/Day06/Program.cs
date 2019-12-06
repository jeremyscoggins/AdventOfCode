using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt").Select(l => l.Split(')')).ToArray();
            var connections = new List<(string orbiter, string orbitee, bool direct)>();
            var direct = new Dictionary<string, List<string>>();
            foreach (var item in input)
            {
                var orbitee = item[0];
                var orbiter = item[1];
                connections.Add((orbiter, orbitee, true));
                if (!direct.ContainsKey(orbiter))
                {
                    direct.Add(orbiter, new List<string>());
                }
                direct[orbiter].Add(orbitee);
            }
            foreach (var item in input)
            {
                var orbitee = item[0];
                var orbiter = item[1];
                var queue = new Queue<string>();
                queue.Enqueue(orbitee);
                while (queue.Count > 0)
                {
                    var item2 = queue.Dequeue();
                    if (direct.ContainsKey(item2))
                    {
                        foreach (var directConnection in direct[item2])
                        {
                            if (item2 != orbiter)
                            {
                                connections.Add((orbiter, directConnection, false));
                            }
                            queue.Enqueue(directConnection);
                        }
                    }
                }
            }
            var result = connections.Count;
            Console.WriteLine(result);


            var start = direct["YOU"];
            var end = direct["SAN"];


            var queue2 = new Queue<List<string>>(start.Select(s => new List<string>(new[] { s })));
            var visited = new HashSet<string>(new[] { "YOU", "SAN" });
            List<string> result2 = null;
            while (queue2.Count > 0)
            {
                var item = queue2.Dequeue();
                if (visited.Add(item.Last()))
                {
                    if (direct.ContainsKey(item.Last()))
                    {
                        var orbitees = direct[item.Last()];
                        foreach (var orbitee in orbitees)
                        {
                            if (end.Contains(orbitee))
                            {
                                result2 = item;
                                queue2.Clear();
                                break;
                            }
                            queue2.Enqueue(item.Concat(new[] { orbitee }).ToList());
                        }
                    }
                    var orbiters = direct.Where(d => d.Value.Contains(item.Last())).Select(d => d.Key).ToList();
                    foreach (var orbiter in orbiters)
                    {
                        if (end.Contains(orbiter))
                        {
                            result2 = item;
                            queue2.Clear();
                            break;
                        }
                        queue2.Enqueue(item.Concat(new[] { orbiter }).ToList());
                    }
                }
            }
            Console.WriteLine(result2.Count);
            Console.ReadLine();
        }
    }
}

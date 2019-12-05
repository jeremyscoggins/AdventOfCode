using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        const string pattern = "(\\w+) would (\\w+) (\\d+) happiness units by sitting next to (\\w+)\\.";
        static Dictionary<string, int> seatingArrangments = new Dictionary<string, int>();
        static Dictionary<string, Dictionary<string, int>> seatingValues = new Dictionary<string, Dictionary<string, int>>();
        static void Walk(string last, int value, string arrangement)
        {
            bool finished = true;
            foreach (var next in seatingValues.Keys)
            {
                if (!arrangement.Contains(next))
                {
                    finished = false;
                    int nextValue = value;
                    nextValue = nextValue + seatingValues[last][next];
                    nextValue = nextValue + seatingValues[next][last];

                    Walk(next, nextValue, arrangement + " " + next);
                }
            }
            if (finished)
            {
                string[] names = arrangement.Split(' ');
                value = value + seatingValues[names[0]][names[names.Length - 1]];
                value = value + seatingValues[names[names.Length - 1]][names[0]];
                seatingArrangments[arrangement] = value;
            }
        }
        static void Main(string[] args)
        {
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        Match match = Regex.Match(line, pattern);
                        string nameA = match.Groups[1].Value;
                        string direction = match.Groups[2].Value;
                        int value = int.Parse(match.Groups[3].Value);
                        string nameB = match.Groups[4].Value;
                        if(!seatingValues.ContainsKey(nameA))
                        {
                            seatingValues[nameA] = new Dictionary<string,int>();
                        }
                        seatingValues[nameA][nameB] = direction == "gain" ? value : -value;
                    }
                }
            }
            foreach (var name in seatingValues.Keys.ToArray())
            {
                seatingValues[name]["Me"] = 0;
                if (!seatingValues.ContainsKey("Me"))
                {
                    seatingValues["Me"] = new Dictionary<string, int>();
                }
                seatingValues["Me"][name] = 0;
            }
            foreach (var name in seatingValues.Keys)
            {
                Walk(name, 0, name);
            }
            var best = seatingArrangments.OrderByDescending(a => a.Value).First();
            Console.WriteLine("the best seating arrangement was\n({0})\nwith a total happiness gain of {1}", best.Key, best.Value);
            Console.ReadLine();
        }
    }
}

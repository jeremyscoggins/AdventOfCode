using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day14
{
    class Reindeer
    {
        public string Name;
        public int Speed;
        public int EnduranceTime;
        public int RestTime;
        public int CurrentTime;
        public bool Resting;
        public int Distance;
        public int Points;
    }
    class Program
    {
        const string pattern = "(\\w+) can fly (\\d+) km/s for (\\d+) seconds, but then must rest for (\\d+) seconds.";
        static void Main(string[] args)
        {
            List<Reindeer> reindeer = new List<Reindeer>();
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        Match match = Regex.Match(line, pattern);
                        string name = match.Groups[1].Value;
                        int speed = int.Parse(match.Groups[2].Value);
                        int endurance = int.Parse(match.Groups[3].Value);
                        int rest = int.Parse(match.Groups[4].Value);
                        reindeer.Add(new Reindeer() { Name = name, EnduranceTime = endurance, Speed = speed, RestTime = rest, CurrentTime = 0, Resting = false, Distance = 0, Points = 0 });
                    }
                }
            }
            for (int i = 0; i < 2503; i++)
            {
                foreach (var single in reindeer)
                {
                    single.CurrentTime++;
                    if (!single.Resting)
                    {
                        single.Distance += single.Speed;
                    }
                    if (single.Resting && single.CurrentTime >= single.RestTime)
                    {
                        single.Resting = false;
                        single.CurrentTime = 0;
                    }
                    else if (!single.Resting && single.CurrentTime >= single.EnduranceTime)
                    {
                        single.Resting = true;
                        single.CurrentTime = 0;
                    }
                }
                var leadDist = reindeer.Max(r => r.Distance);
                foreach (var single in reindeer)
                {
                    if (single.Distance == leadDist)
                    {
                        single.Points++;
                    }
                }
            }
            var bestDist = reindeer.OrderByDescending(a => a.Distance).First();
            Console.WriteLine("{0} was the fastest Reindeer with a distance of {1}", bestDist.Name, bestDist.Distance);
            var bestPoints = reindeer.OrderByDescending(a => a.Points).First();
            Console.WriteLine("{0} earned the most points with a score of {1} points", bestPoints.Name, bestPoints.Points);
            Console.ReadLine();
        }
    }
}

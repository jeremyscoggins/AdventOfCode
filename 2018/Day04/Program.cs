using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            var guards = new Dictionary<int, GuardTime>();
            var entries = new List<Entry>();
            foreach (var line in input)
            {
                var match = Regex.Match(line, @"\[(\d+)-(\d+)-(\d+) (\d+):(\d+)\] (Guard #(\d+) begins shift|wakes up|falls asleep)");
                var entry = new Entry()
                {
                    year = int.Parse(match.Groups[1].Value),
                    month = int.Parse(match.Groups[2].Value),
                    day = int.Parse(match.Groups[3].Value),
                    hour = int.Parse(match.Groups[4].Value),
                    minute = int.Parse(match.Groups[5].Value),
                    action = match.Groups[6].Value,
                    guard = int.Parse(match.Groups[7].Value.Length > 0 ? match.Groups[7].Value : "0")
                };
                entry.date = new DateTime(entry.year + 500, entry.month, entry.day, entry.hour, entry.minute, 0);
                entries.Add(entry);
            }
            entries = entries.OrderBy(e => e.date).ToList();
            DateTime last = DateTime.MinValue;
            int currentGuard = 0;
            foreach (var entry in entries)
            {
                switch (entry.action)
                {
                    case "wakes up":
                        var totalMinutes = (int)(entry.date - last).TotalMinutes;
                        guards[currentGuard].total += totalMinutes;
                        for (int i = last.Minute; i < last.Minute+totalMinutes; i++)
                        {
                            guards[currentGuard].minutes[i]++;

                        }
                        break;
                    case "falls asleep":
                        last = entry.date;
                        break;
                    default:
                        currentGuard = entry.guard;
                        if (!guards.ContainsKey(currentGuard))
                        {
                            guards[currentGuard] = new GuardTime(entry.guard);
                        }
                        break;
                }
            }
            var sleepy = guards.OrderByDescending(g => g.Value.total).First().Value;
            var sleepyminute = sleepy.minutes.OrderByDescending(m => m.Value).First().Key;
            Console.WriteLine("Sleepy guard {0}, minute {1} : answer {2}", sleepy.guard, sleepyminute, sleepy.guard * sleepyminute);


            var sleepy2 = guards.OrderByDescending(g => g.Value.minutes.Max(m => m.Value)).First().Value;
            var sleepyminute2 = sleepy2.minutes.OrderByDescending(m => m.Value).First().Key;
            Console.WriteLine("Sleepy guard {0}, minute {1} : answer2 {2}", sleepy2.guard, sleepyminute2, sleepy2.guard * sleepyminute2);
            Console.ReadLine();
        }
    }

    internal class GuardTime
    {
        internal int total = 0;
        internal Dictionary<int, int> minutes = Enumerable.Range(0, 60).ToDictionary(r => r, r => 0);
        internal int guard;

        public GuardTime(int guard)
        {
            this.guard = guard;
        }
    }

    internal class Entry
    {
        internal int year;
        internal int month;
        internal int day;
        internal int hour;
        internal int minute;
        internal string action;
        internal int guard;
        internal DateTime date;
    }
}

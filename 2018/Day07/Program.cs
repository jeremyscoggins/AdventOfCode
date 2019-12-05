using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var regex = new Regex(@"Step (\w) must be finished before step (\w) can begin.");
            var input = File.ReadAllLines("Input.txt");

            var steps = input.Select(line => new Step { primary = regex.Match(line).Groups[1].Value, subordinate = regex.Match(line).Groups[2].Value }).ToList();
            var output = "";
            var allLetters = steps.Select(p => p.primary).Concat(steps.Select(s => s.subordinate)).Distinct().OrderBy(s => s).ToList();
            while (allLetters.Count > 0)
            {
                var next = allLetters.Where(primary => !steps.Any(step => step.subordinate == primary)).First();
                output += next;
                allLetters.Remove(next);
                steps.RemoveAll(step => step.primary == next);
            }

            Console.WriteLine("Result: " + output);


            steps = input.Select(line => new Step { primary = regex.Match(line).Groups[1].Value, subordinate = regex.Match(line).Groups[2].Value }).ToList();
            allLetters = steps.Select(p => p.primary).Concat(steps.Select(s => s.subordinate)).Distinct().OrderBy(s => s).ToList();
            var time = 0;
            var workers = Enumerable.Range(0, 5).Select(i => new Worker()).ToArray();

            //var done = new List<Done>();

            while (allLetters.Count > 0 || workers.Any(worker => worker.complete > time))
            {
                //done.Where(d => d.complete <= time).Select(x => steps.RemoveAll(d => d.primary == x.letter));
                //done.RemoveAll(d => d.complete <= time);

                for (var w = 0; w < workers.Length; w++)
                {
                    if (workers[w].complete <= time)
                    {
                        if (workers[w].letter != null)
                        {
                            steps.RemoveAll(step => step.primary == workers[w].letter);
                            workers[w].letter = null;
                        }
                    }
                }

                var next = allLetters.Where(primary => !steps.Any(step => step.subordinate == primary)).ToList();

                for (var w = 0; w < workers.Length; w++)
                {
                    if (workers[w].complete <= time)
                    {
                        if (next.Count > 0)
                        {
                            workers[w].complete = GetTime(next.First()) + time;
                            workers[w].letter = next.First();
                            allLetters.Remove(next.First());
                            //done.Add(new Done() { letter = next.First(), complete = workers[w] });
                            next.RemoveAt(0);
                        }
                    }
                }

                time++;
            }

            Console.WriteLine("Result: " + time);


            Console.ReadLine();
        }

        private static int GetTime(string p)
        {
            return (p[0] - 'A') + 61;
        }
    }

    internal class Worker
    {
        public string letter { get; set; }
        public int complete { get; set; }
    }

    internal class Step
    {
        public string primary { get; set; }
        public string subordinate { get; set; }
    }
}

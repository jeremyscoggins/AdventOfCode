using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static Regex instructionA = new Regex(@"bot (\d+) gives low to (output|bot) (\d+) and high to (output|bot) (\d+)");
        static Regex instructionB = new Regex(@"value (\d+) goes to bot (\d+)");

        static Dictionary<int, Action<int>> bots = new Dictionary<int, Action<int>>();
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt").OrderBy(x => x).ToArray();
            var outputs = new Dictionary<int, int>();
            foreach (var line in input)
            {
                if (instructionB.IsMatch(line))
                {
                    var match = instructionB.Match(line);
                    var value = int.Parse(match.Groups[1].Value);
                    var bot = int.Parse(match.Groups[2].Value);
                    bots[bot](value);
                }
                else if (instructionA.IsMatch(line))
                {
                    var match = instructionA.Match(line);
                    var bot = int.Parse(match.Groups[1].Value);
                    var values = new List<int>();

                    bots[bot] = (int value) =>
                    {
                        values.Add(value);
                        if (values.Count == 2)
                        {
                            if (values.Min() == 17 && values.Max() == 61)
                            {
                                Console.Write("Bot {0} is part 1 winner", bot);
                            }
                            if (match.Groups[2].Value == "bot")
                            {
                                var lowBot = int.Parse(match.Groups[3].Value);
                                bots[lowBot](values.Min());
                            }
                            else
                            {
                                var lowOutput = int.Parse(match.Groups[3].Value);
                                outputs[lowOutput] = values.Min();
    
                        }
                            if (match.Groups[4].Value == "bot")
                            {
                                var highBot = int.Parse(match.Groups[5].Value);
                                bots[highBot](values.Max());
                            }
                            else
                            {
                                var highOutput = int.Parse(match.Groups[5].Value);
                                outputs[highOutput] = values.Max();
                            }
                        }
                    };


                }
                else
                {
                    throw new Exception(String.Format("Invalid instruction {0}", input));
                }
            }
            Console.Write("part 2 output is {0}", outputs[0] * outputs[1] * outputs[2]);

        }
    }
}

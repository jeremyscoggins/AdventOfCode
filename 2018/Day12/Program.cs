using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("Input.txt");

            var state = lines[0].Split(':')[1].Trim();

            var rules = new Dictionary<string, char>();

            foreach (var line in lines.Skip(2))
            {
                //.#.## => #
                var rule = line.Substring(0, 5);
                var result = line.Substring(9, 1)[0];

                rules.Add(rule, result);
            }


            var stateStart = 0;
            int twentyCount = 0;
            int lastCount = 0;

            for (int gen = 0; gen < 2000; gen++)
            {
                var newstate = new StringBuilder();
                for (int i = -2; i < state.Length + 2; i++)
                {
                    var result = '.';
                    foreach (var rule in rules)
                    {
                        var match = true;
                        for (int c = -2; c < 3; c++)
                        {
                            var compare = rule.Key[c + 2];
                            if (i + c < 0 || i + c > state.Length-1)
                            {
                                if (compare != '.')
                                {
                                    match = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (compare != state[i + c])
                                {
                                    match = false;
                                    break;
                                }
                            }
                        }
                        if (match)
                        {
                            result = rule.Value;
                        }
                    }
                    if (newstate.Length == 0 && i < 0)
                    {
                        if (result == '#')
                        {
                            newstate.Append(result);
                            stateStart += i;
                        }
                    }
                    else if (i > newstate.Length)
                    {
                        if (result == '#')
                        {
                            if (newstate.Length - i > 1)
                            {
                                newstate.Append('.');
                            }
                            newstate.Append(result);
                        }
                    }
                    else
                    {
                        newstate.Append(result);
                    }
                }

                state = newstate.ToString();

                int counter = 0;
                for (int i = 0; i < state.Length; i++)
                {
                    if (state[i] == '#')
                    {
                        counter += i + stateStart;
                    }
                }

                Console.WriteLine("Gen:{0}, Count:{1}, Diff:{2}", gen+1, counter, counter - lastCount);

                if (gen == 19)
                {
                    twentyCount = counter;
                }

                lastCount = counter;
            }

            Console.WriteLine("Twenty count: {0}", twentyCount);

            var final = ((50000000000 - 2000) * 15) + lastCount;
            Console.WriteLine("Final count: {0}", final);
        }
    }
}

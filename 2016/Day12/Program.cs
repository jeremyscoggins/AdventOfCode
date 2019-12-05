using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        const string input = @"cpy 1 a
cpy 1 b
cpy 26 d
jnz c 2
jnz 1 5
cpy 7 c
inc d
dec c
jnz c -2
cpy a c
inc a
dec b
jnz b -2
cpy c b
dec d
jnz d -6
cpy 13 c
cpy 14 d
inc a
dec d
jnz d -2
dec c
jnz c -5";

        static Regex instructionPattern = new Regex(@"(\w+) (-?\d+|\w)( (-?\d+|\w))?");
        //static Dictionary<string, int> registers = new Dictionary<string, int>() { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
        static Dictionary<string, int> registers = new Dictionary<string, int>() { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } };
        static void Main(string[] args)
        {

            var instructions = input.Split('\n');
            var counter = 0;

            while(counter < instructions.Length)
            {
                var line = instructions[counter];
                var match = instructionPattern.Match(line);
                var instruction = match.Groups[1].Value;
                var paramA = match.Groups[2].Value;
                var paramB = match.Groups[4].Value;
                switch (instruction)
                {
                    case "cpy":
                        registers[paramB] = getVal(paramA);
                        break;
                    case "jnz":
                        if (getVal(paramA) != 0)
                        {
                            counter += getVal(paramB);
                            continue;
                        }
                        break;
                    case "inc":
                        registers[paramA]++;
                        break;
                    case "dec":
                        registers[paramA]--;
                        break;
                    default:
                        break;
                }
                counter++;
            }
            foreach (var register in registers)
            {
                Console.WriteLine("register{0} = {1}", register.Key, register.Value);
            }
            Console.WriteLine("finished");
        }

        static int getVal(string input)
        {
            if (registers.ContainsKey(input))
            {
                return registers[input];
            }
            else
            {
                return int.Parse(input);
            }
        }
    }
}

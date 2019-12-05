using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day25
{
    class Program
    {
        const string input = @"cpy a d
cpy 4 c
cpy 643 b
inc d
dec b
jnz b -2
dec c
jnz c -5
cpy d a
jnz 0 0
cpy a b
cpy 0 a
cpy 2 c
jnz b 2
jnz 1 6
dec b
dec c
jnz c -4
inc a
jnz 1 -7
cpy 2 b
jnz c 2
jnz 1 4
dec b
dec c
jnz 1 -4
jnz 0 0
out b
jnz a -19
jnz 1 -21";

        //        const string input = @"cpy 2 a
        //tgl a
        //tgl a
        //tgl a
        //cpy 1 a
        //dec a
        //dec a";

        static Regex instructionPattern = new Regex(@"^(\w+)( (-?\d+|\w))?( (-?\d+|\w))?( (-?\d+|\w))?");
        //static Dictionary<string, int> registers = new Dictionary<string, int>() { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
        static Dictionary<string, int> registers = new Dictionary<string, int>() { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } };
        static void Main(string[] args)
        {

            var instructions = input.Replace("\r", "").Split('\n');
            int start = 0;
            while (true)
            {
                var counter = 0;
                string output = "";
                bool run = true;

                registers["a"] = start;
                //registers["a"] = 12;

                //var instructionMap = new Dictionary<string, int>();

                while (counter < instructions.Length && run)
                {
                    var line = instructions[counter];
                    //Console.WriteLine("{0}   {1}", counter, line);

                    //if (!instructionMap.ContainsKey(line))
                    //{
                    //    instructionMap[line] = 0;
                    //}
                    //instructionMap[line]++;

                    var match = instructionPattern.Match(line);
                    var instruction = match.Groups[1].Value;
                    var paramA = match.Groups[3].Value;
                    var paramB = match.Groups[5].Value;
                    var paramC = match.Groups[7].Value;
                    switch (instruction)
                    {
                        case "nop":
                            break;
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
                        case "tgl":
                            {
                                var dist = getVal(paramA);
                                if (counter + dist < instructions.Length)
                                {
                                    instructions[counter + dist] = toggle(instructions[counter + dist]);
                                }
                                break;
                            }
                        case "mlt":
                            {
                                var val1 = getVal(paramA);
                                var val2 = getVal(paramB);
                                registers[paramC] = val1 * val2;
                                break;
                            }
                        case "out":
                            {
                                var val1 = getVal(paramA);
                                //Console.WriteLine(val1);
                                output += val1;
                                if (output.Length == 10)
                                {
                                    run = false;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                    counter++;
                }
                Console.WriteLine(output);
                if (output == "0101010101" || output == "1010101010")
                {
                    break;
                }
                start++;
            }
            foreach (var register in registers)
            {
                Console.WriteLine("register{0} = {1}", register.Key, register.Value);
            }
            Console.WriteLine("finished");
        }

        static string toggle(string input)
        {
            var match = instructionPattern.Match(input);
            var instruction = match.Groups[1].Value;
            var paramA = match.Groups[2].Value;
            var paramB = match.Groups[4].Value;
            if (paramB.Length == 0)
            {
                if (instruction == "inc")
                {
                    return input.Replace("inc", "dec");
                }
                else
                {
                    return input.Replace(instruction, "inc");
                }
            }
            else
            {
                if (instruction == "jnz")
                {
                    return input.Replace("jnz", "cpy");
                }
                else
                {
                    return input.Replace(instruction, "jnz");
                }
            }
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

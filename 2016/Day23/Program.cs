using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
                const string input = @"cpy a b
dec b
nop //cpy a d
nop //cpy 0 a
nop //cpy b c
nop //inc a
nop //dec c
nop //jnz c -2
nop //dec d
mlt b a a //jnz d -5
dec b
cpy b c
cpy c d
dec d
inc c
jnz d -2
tgl c
cpy -16 c
jnz 1 c
cpy 70 c
jnz 87 d
inc a
inc d
jnz d -2
inc c
jnz c -5";

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
            var counter = 0;

            //registers["a"] = 7;
            registers["a"] = 12;

            //var instructionMap = new Dictionary<string, int>();

            while (counter < instructions.Length)
            {
                var line = instructions[counter];
                Console.WriteLine("{0}   {1}", counter, line);

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
                        var dist = getVal(paramA);
                        if(counter + dist < instructions.Length)
                        {
                            instructions[counter + dist] = toggle(instructions[counter + dist]);
                        }
                        break;
                    case "mlt":
                        var val1 = getVal(paramA);
                        var val2 = getVal(paramB);
                        registers[paramC] = val1 * val2;
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

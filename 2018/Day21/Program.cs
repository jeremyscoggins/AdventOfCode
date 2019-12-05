using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    enum OpCodes
    {
        addr = 0,
        addi = 1,
        mulr = 2,
        muli = 3,
        banr = 4,
        bani = 5,
        borr = 6,
        bori = 7,
        setr = 8,
        seti = 9,
        gtir = 10,
        gtri = 11,
        gtrr = 12,
        eqir = 13,
        eqri = 14,
        eqrr = 15
    }
    class Program
    {
        static void Main(string[] args)
        {
            var seen = new HashSet<int>();
            var seen2 = new List<int>();
            //optimized();
            var input = File.ReadAllLines("Input.txt").Select(row => row.Split(' ')).ToArray();
            var ip = int.Parse(input[0][1]);
            input = input.Skip(1).ToArray();

            //int i = 0;
            //foreach (var line in input)
            //{
            //    Console.Write(i + ": ");
            //    print(new int[] { (int)Enum.Parse(typeof(OpCodes), line[0]), int.Parse(line[1]), int.Parse(line[2]), int.Parse(line[3]) });
            //    i++;
            //}


            var registers = new int[] { 0, 0, 0, 0, 0, 0 };

            registers[ip] = -1;

            var preVal = new Dictionary<int, int[]>();

            while (registers[ip] + 1 < input.Length)
            {
                registers[ip]++;
                //if (registers[ip] == 15 || registers[ip] == 16)
                //if (registers[ip] == 28)
                //{
                //    Console.WriteLine("Before: " + String.Join(", ", registers));
                //    Console.Write(registers[ip] + ": ");
                //    print(new int[] { (int)Enum.Parse(typeof(OpCodes), instruction[0]), int.Parse(instruction[1]), int.Parse(instruction[2]), int.Parse(instruction[3]) });
                //}
                int[] instructionParsed;
                if (!preVal.ContainsKey(registers[ip]))
                {
                    var instruction = input[registers[ip]];
                    instructionParsed = new int[] { (int)Enum.Parse(typeof(OpCodes), instruction[0]), int.Parse(instruction[1]), int.Parse(instruction[2]), int.Parse(instruction[3]) };
                    preVal[registers[ip]] = instructionParsed;
                }
                else
                {
                    instructionParsed = preVal[registers[ip]];
                }
                registers = evaluate(registers, instructionParsed);
                //if (registers[ip] == 15 || registers[ip] == 16)
                if (registers[ip] == 28)
                {
                    if (!seen.Add(registers[4]))
                    {
                        Console.WriteLine("After: " + String.Join(", ", registers));
                    }
                    else
                    {
                        seen2.Add(registers[4]);
                    }
                    //Console.WriteLine(registers[4]);
                }
            }
            Console.WriteLine("Result: " + String.Join(", ", registers));
            Console.ReadLine();

        }
        static int[] evaluate(int[] input, int[] instruction)
        {
            var result = new[] { input[0], input[1], input[2], input[3], input[4], input[5] };
            var opcode = (OpCodes)instruction[0];
            var argA = instruction[1];
            var argB = instruction[2];
            var argC = instruction[3];
            //Console.WriteLine("Instruction: " + opcode + " " + argA + " " + argB + " " + argC);
            switch (opcode)
            {
                case OpCodes.addr:
                    //addr
                    result[argC] = result[argA] + result[argB];
                    //Console.WriteLine("register[" + argC + "] = register["+argA+"] + register["+argB+"];");
                    break;
                case OpCodes.addi:
                    //addi
                    result[argC] = result[argA] + argB;
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] + " + argB + ";");
                    break;
                case OpCodes.mulr:
                    //mulr
                    result[argC] = result[argA] * result[argB];
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] * register[" + argB + "];");
                    break;
                case OpCodes.muli:
                    //muli
                    result[argC] = result[argA] * argB;
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] * " + argB + ";");
                    break;
                case OpCodes.banr:
                    //banr
                    result[argC] = result[argA] & result[argB];
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] & register[" + argB + "];");
                    break;
                case OpCodes.bani:
                    //bani
                    result[argC] = result[argA] & argB;
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] & " + argB + ";");
                    break;
                case OpCodes.borr:
                    //borr
                    result[argC] = result[argA] | result[argB];
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] | register[" + argB + "];");
                    break;
                case OpCodes.bori:
                    //bori
                    result[argC] = result[argA] | argB;
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] | " + argB + ";");
                    break;
                case OpCodes.setr:
                    //setr
                    result[argC] = result[argA];
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "];");
                    break;
                case OpCodes.seti:
                    //seti
                    result[argC] = argA;
                    //Console.WriteLine("register[" + argC + "] = " + argA + ";");
                    break;
                case OpCodes.gtir:
                    //gtir
                    result[argC] = argA > result[argB] ? 1 : 0;
                    //Console.WriteLine("register[" + argC + "] = " + argA + " > register[" + argB + "] ? 1 : 0;");
                    break;
                case OpCodes.gtri:
                    //gtri
                    result[argC] = result[argA] > argB ? 1 : 0;
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] > " + argB + " ? 1 : 0;");
                    break;
                case OpCodes.gtrr:
                    //gtrr
                    result[argC] = result[argA] > result[argB] ? 1 : 0;
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] > register[" + argB + "] ? 1 : 0;");
                    break;
                case OpCodes.eqir:
                    //eqir
                    result[argC] = argA == result[argB] ? 1 : 0;
                    //Console.WriteLine("register[" + argC + "] = " + argA + " == register[" + argB + "] ? 1 : 0;");
                    break;
                case OpCodes.eqri:
                    //eqri
                    result[argC] = result[argA] == argB ? 1 : 0;
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] == " + argB + " ? 1 : 0;");
                    break;
                case OpCodes.eqrr:
                    //eqrr
                    result[argC] = result[argA] == result[argB] ? 1 : 0;
                    //Console.WriteLine("register[" + argC + "] = register[" + argA + "] == register[" + argB + "] ? 1 : 0;");
                    break;
                default:
                    break;
            }
            return result;
        }
        static void print(int[] instruction)
        {
            var opcode = (OpCodes)instruction[0];
            var argA = instruction[1];
            var argB = instruction[2];
            var argC = instruction[3];
            //Console.WriteLine("Instruction: " + opcode + " " + argA + " " + argB + " " + argC);
            switch (opcode)
            {
                case OpCodes.addr:
                    //addr
                    //result[argC] = result[argA] + result[argB];
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] + register[" + argB + "];");
                    break;
                case OpCodes.addi:
                    //addi
                    //result[argC] = result[argA] + argB;
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] + " + argB + ";");
                    break;
                case OpCodes.mulr:
                    //mulr
                    //result[argC] = result[argA] * result[argB];
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] * register[" + argB + "];");
                    break;
                case OpCodes.muli:
                    //muli
                    //result[argC] = result[argA] * argB;
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] * " + argB + ";");
                    break;
                case OpCodes.banr:
                    //banr
                    //result[argC] = result[argA] & result[argB];
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] & register[" + argB + "];");
                    break;
                case OpCodes.bani:
                    //bani
                    //result[argC] = result[argA] & argB;
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] & " + argB + ";");
                    break;
                case OpCodes.borr:
                    //borr
                    //result[argC] = result[argA] | result[argB];
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] | register[" + argB + "];");
                    break;
                case OpCodes.bori:
                    //bori
                    //result[argC] = result[argA] | argB;
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] | " + argB + ";");
                    break;
                case OpCodes.setr:
                    //setr
                    //result[argC] = result[argA];
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "];");
                    break;
                case OpCodes.seti:
                    //seti
                    //result[argC] = argA;
                    Console.WriteLine("register[" + argC + "] = " + argA + ";");
                    break;
                case OpCodes.gtir:
                    //gtir
                    //result[argC] = argA > result[argB] ? 1 : 0;
                    Console.WriteLine("register[" + argC + "] = " + argA + " > register[" + argB + "] ? 1 : 0;");
                    break;
                case OpCodes.gtri:
                    //gtri
                    //result[argC] = result[argA] > argB ? 1 : 0;
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] > " + argB + " ? 1 : 0;");
                    break;
                case OpCodes.gtrr:
                    //gtrr
                    //result[argC] = result[argA] > result[argB] ? 1 : 0;
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] > register[" + argB + "] ? 1 : 0;");
                    break;
                case OpCodes.eqir:
                    //eqir
                    //result[argC] = argA == result[argB] ? 1 : 0;
                    Console.WriteLine("register[" + argC + "] = " + argA + " == register[" + argB + "] ? 1 : 0;");
                    break;
                case OpCodes.eqri:
                    //eqri
                    //result[argC] = result[argA] == argB ? 1 : 0;
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] == " + argB + " ? 1 : 0;");
                    break;
                case OpCodes.eqrr:
                    //eqrr
                    //result[argC] = result[argA] == result[argB] ? 1 : 0;
                    Console.WriteLine("register[" + argC + "] = register[" + argA + "] == register[" + argB + "] ? 1 : 0;");
                    break;
                default:
                    break;
            }
        }
    }
}

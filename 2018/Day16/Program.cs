using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        //const int INSTRUCTION = 0;
        //const int A = 0;
        //const int B = 1;
        //const int C = 2;


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

        static void Main(string[] args)
        {
            var inputMatch = new Regex("(\\d+),? (\\d+),? (\\d+),? (\\d+),?");
            var input = File.ReadAllLines("Input.txt");

            //var identifiedOpCodes = new Dictionary<int, int>();


            //while (identifiedOpCodes.Keys.Count < 16)
            //{
                var possibleMatches = new List<List<int>>();
                //var possibleMatchesAtoOP = new Dictionary<int, HashSet<OpCodes>>();
                var possibleMatchesOPtoA = new Dictionary<OpCodes, HashSet<int>>();
                for (int i = 0; i < input.Length; i++)
                {
                    var possibles = new List<int>();
                    var matches = inputMatch.Match(input[i]);
                    var before = new int[] { int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[3].Value), int.Parse(matches.Groups[4].Value) };
                    matches = inputMatch.Match(input[++i]);
                    var instruction = new int[] { int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[3].Value), int.Parse(matches.Groups[4].Value) };
                    matches = inputMatch.Match(input[++i]);
                    var after = new int[] { int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[3].Value), int.Parse(matches.Groups[4].Value) };

                    //if (!possibleMatchesAtoOP.ContainsKey(instruction[0]))
                    //{
                    //    possibleMatchesAtoOP[instruction[0]] = new HashSet<OpCodes>();
                    //}

                    //if (!identifiedOpCodes.ContainsKey(instruction[0]))
                    {
                        for (int op = 0; op < 16; op++)
                        {
                            if (!possibleMatchesOPtoA.ContainsKey((OpCodes)op))
                            {
                                possibleMatchesOPtoA[(OpCodes)op] = new HashSet<int>();
                            }
                            //if (!identifiedOpCodes.Values.Contains(op))
                            {
                                var result = evaluate(before, new[] { op, instruction[1], instruction[2], instruction[3] });
                                if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                                {
                                    possibles.Add(op);
                                    //possibleMatchesAtoOP[instruction[0]].Add((OpCodes)op);
                                    possibleMatchesOPtoA[(OpCodes)op].Add(instruction[0]);
                                }
                            }
                        }

                        //if (possibles.Count == 1)
                        //{
                        //    identifiedOpCodes[instruction[0]] = possibles[0];
                        //}

                        possibleMatches.Add(possibles);

                    }

                    i++;
                }

                //for (int op = 0; op < 16; op++)
                //{
                //    if (possibleMatchesAtoOP.ContainsKey(op))
                //    {
                //        Console.WriteLine("1: " + op + "=" + String.Join(" ", possibleMatchesAtoOP[op]));
                //    }
                //}

                for (int op = 0; op < 16; op++)
                {
                    if (possibleMatchesOPtoA.ContainsKey((OpCodes)op))
                    {
                        Console.WriteLine("2: " + ((OpCodes)op) + "=" + String.Join(" ", possibleMatchesOPtoA[(OpCodes)op]));
                    }
                }

                var final = possibleMatches.Where(p => p.Count >= 3).ToArray();
                Console.WriteLine("Result: " + final.Length);

                var found = new Dictionary<OpCodes, int>();
                var found2 = new Dictionary<int, OpCodes>();
                while (found.Count < 16)
                {
                    foreach (var possibleMatch in possibleMatchesOPtoA)
                    {
                        if (!found.ContainsKey(possibleMatch.Key))
                        {
                            var possibles = possibleMatch.Value.Where(k => !found.Values.Contains(k));
                            if (possibles.Count() == 1)
                            {
                                found[possibleMatch.Key] = possibles.First();
                                found2[found[possibleMatch.Key]] = possibleMatch.Key;
                            }
                        }
                    }
                    //foreach (var possibleMatch in possibleMatchesAtoOP)
                    //{
                    //    if (!found.Values.Contains(possibleMatch.Key))
                    //    {
                    //        var possibles = possibleMatch.Value.Where(k => !found.Keys.Contains(k));
                    //        if (possibles.Count() == 1)
                    //        {
                    //            found[possibles.First()] = possibleMatch.Key;
                    //        }
                    //    }
                    //}
                }

            //}

            var input2 = File.ReadAllLines("Input2.txt");
            var registers = new[] { 0, 0, 0, 0 };
            for (int i = 0; i < input2.Length; i++)
            {
                var matches = inputMatch.Match(input2[i]);
                var instruction = new int[] { int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[3].Value), int.Parse(matches.Groups[4].Value) };
                registers = evaluate(registers, new[] { (int)found2[instruction[0]], instruction[1], instruction[2], instruction[3] });
            }

            Console.WriteLine("Result: " + registers[0]);

            //232 = too low
            Console.ReadLine();
        }
        static int[] evaluate(int[] input, int[] instruction)
        {
            var result = new[] { input[0], input[1], input[2], input[3] };
            var opcode = instruction[0];
            var argA = instruction[1];
            var argB = instruction[2];
            var argC = instruction[3];
            switch ((OpCodes)opcode)
            {
                case OpCodes.addr:
                    //addr
                    result[argC] = result[argA] + result[argB];
                    break;
                case OpCodes.addi:
                    //addi
                    result[argC] = result[argA] + argB;
                    break;
                case OpCodes.mulr:
                    //mulr
                    result[argC] = result[argA] * result[argB];
                    break;
                case OpCodes.muli:
                    //muli
                    result[argC] = result[argA] * argB;
                    break;
                case OpCodes.banr:
                    //banr
                    result[argC] = result[argA] & result[argB];
                    break;
                case OpCodes.bani:
                    //bani
                    result[argC] = result[argA] & argB;
                    break;
                case OpCodes.borr:
                    //borr
                    result[argC] = result[argA] | result[argB];
                    break;
                case OpCodes.bori:
                    //bori
                    result[argC] = result[argA] | argB;
                    break;
                case OpCodes.setr:
                    //setr
                    result[argC] = result[argA];
                    break;
                case OpCodes.seti:
                    //seti
                    result[argC] = argA;
                    break;
                case OpCodes.gtir:
                    //gtir
                    result[argC] = argA > result[argB] ? 1 : 0;
                    break;
                case OpCodes.gtri:
                    //gtri
                    result[argC] = result[argA] > argB ? 1 : 0;
                    break;
                case OpCodes.gtrr:
                    //gtrr
                    result[argC] = result[argA] > result[argB] ? 1 : 0;
                    break;
                case OpCodes.eqir:
                    //eqir
                    result[argC] = argA == result[argB] ? 1 : 0;
                    break;
                case OpCodes.eqri:
                    //eqri
                    result[argC] = result[argA] == argB ? 1 : 0;
                    break;
                case OpCodes.eqrr:
                    //eqrr
                    result[argC] = result[argA] == result[argB] ? 1 : 0;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}

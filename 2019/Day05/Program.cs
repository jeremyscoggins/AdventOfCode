using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("Input.txt").Split(',').Select(i => int.Parse(i)).ToArray();
            //int start1 = 0;
            //int start2 = 0;
            var state = input.ToArray();
            //input[1] = 12;
            //input[2] = 2;
            //while (state[0] != 19690720)
            {
                //state = input.ToArray();
                //state[1] = start1;
                //state[2] = start2;
                for (var counter = 0; counter < state.Length; counter++)
                {
                    var instruction = state[counter].ToString("D5");
                    switch (instruction.Substring(3))
                    {
                        case "01":
                            {
                                //add
                                var a = instruction[2] == '0' ? state[state[++counter]] : state[++counter];
                                var b = instruction[1] == '0' ? state[state[++counter]] : state[++counter];
                                var c = state[++counter];
                                state[c] = a + b;
                            }
                            break;
                        case "02":
                            {
                                //multiply
                                var a = instruction[2] == '0' ? state[state[++counter]] : state[++counter];
                                var b = instruction[1] == '0' ? state[state[++counter]] : state[++counter];
                                var c = state[++counter];
                                state[c] = a * b;
                            }
                            break;
                        case "03":
                            {
                                //input
                                var a = state[++counter];
                                var value = int.Parse(Console.ReadLine());
                                state[a] = value;
                            }
                            break;
                        case "04":
                            {
                                //output
                                var a = instruction[2] == '0' ? state[state[++counter]] : state[++counter];
                                Console.Write(a);
                            }
                            break;
                        case "05":
                            {
                                //jump if true
                                var a = instruction[2] == '0' ? state[state[++counter]] : state[++counter];
                                var b = instruction[1] == '0' ? state[state[++counter]] : state[++counter];
                                if (a != 0)
                                {
                                    counter = b-1;
                                }
                            }
                            break;
                        case "06":
                            {
                                //jump if false
                                var a = instruction[2] == '0' ? state[state[++counter]] : state[++counter];
                                var b = instruction[1] == '0' ? state[state[++counter]] : state[++counter];
                                if (a == 0)
                                {
                                    counter = b-1;
                                }
                            }
                            break;
                        case "07":
                            {
                                //less than
                                var a = instruction[2] == '0' ? state[state[++counter]] : state[++counter];
                                var b = instruction[1] == '0' ? state[state[++counter]] : state[++counter];
                                var c = state[++counter];
                                state[c] = a < b ? 1 : 0;
                            }
                            break;
                        case "08":
                            {
                                //less than
                                var a = instruction[2] == '0' ? state[state[++counter]] : state[++counter];
                                var b = instruction[1] == '0' ? state[state[++counter]] : state[++counter];
                                var c = state[++counter];
                                state[c] = a == b ? 1 : 0;
                            }
                            break;
                        case "99":
                            counter = state.Length;
                            break;
                    }
                }
                //start1++;
                //if (start1 > 99)
                //{
                //    start1 = 0;
                //    start2++;
                //}
                //if (start2 > 99)
                //{
                //    break;
                //}
            }
            //Console.WriteLine(state[0]);
            //Console.WriteLine((start1--) + " " + start2);
            Console.ReadKey();
        }
    }
}

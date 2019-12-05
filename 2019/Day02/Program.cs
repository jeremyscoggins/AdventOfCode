using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("Input.txt").Split(',').Select(i => int.Parse(i)).ToArray();
            int start1 = 0;
            int start2 = 0;
            var state = input.ToArray();
            //input[1] = 12;
            //input[2] = 2;
            while (state[0] != 19690720)
            {
                state = input.ToArray();
                state[1] = start1;
                state[2] = start2;
                for (var counter = 0; counter < state.Length; counter++)
                {
                    switch (state[counter])
                    {
                        case 1:
                            {
                                var a = state[++counter];
                                var b = state[++counter];
                                var c = state[++counter];
                                state[c] = state[a] + state[b];
                            }
                            break;
                        case 2:
                            {
                                var a = state[++counter];
                                var b = state[++counter];
                                var c = state[++counter];
                                state[c] = state[a] * state[b];
                            }
                            break;
                        case 99:
                            counter = state.Length;
                            break;
                    }
                }
                start1++;
                if (start1 > 99)
                {
                    start1 = 0;
                    start2++;
                }
                if (start2 > 99)
                {
                    break;
                }
            }
            Console.WriteLine(state[0]);
            Console.WriteLine((start1--) + " " + start2);
            Console.ReadKey();
        }
    }
}

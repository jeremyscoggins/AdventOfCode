using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        //Its left and center tiles are traps, but its right tile is not.
        //Its center and right tiles are traps, but its left tile is not.
        //Only its left tile is a trap.
        //Only its right tile is a trap.
        static string[] traps = new string[] { "^^.", ".^^", "^..", "..^" };
        static void Main(string[] args)
        {
            var input = ".^.^..^......^^^^^...^^^...^...^....^^.^...^.^^^^....^...^^.^^^...^^^^.^^.^.^^..^.^^^..^^^^^^.^^^..^";
            var output = new string[400000];
            output[0] = input;

            for (int y = 1; y < output.Length; y++)
            {

                var testLine = "." + output[y - 1] + ".";
                var outLine = new char[output[0].Length];
                for (int x = 0; x < outLine.Length; x++)
                {
                    outLine[x] = traps.Contains(testLine.Substring(x, 3)) ? '^' : '.';
                }
                output[y] = new String(outLine);
            }
            var safe = 0;
            foreach (var line in output)
            {
                //Console.WriteLine(line);
                safe += line.Count(c => c == '.');
            }
            Console.WriteLine("safe tiles: {0}", safe);
        }
    }
}

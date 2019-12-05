using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            var input2 = new string[input.Length];

            for (int i = 0; i < input.Length; i+=3)
            {
                input2[i] =
                    input[i].Substring(0, 5)
                    + input[i+1].Substring(0, 5)
                    + input[i+2].Substring(0, 5);
                input2[i+1] =
                    input[i].Substring(5, 5)
                    + input[i+1].Substring(5, 5)
                    + input[i+2].Substring(5, 5);
                input2[i+2] =
                    input[i].Substring(10, 5)
                    + input[i+1].Substring(10, 5)
                    + input[i+2].Substring(10, 5);
            }

            var validTriangles = new List<int[]>();

            foreach (var line in input2)
            {
                var measures = new[] {
                    int.Parse(line.Substring(0, 5).Trim()),
                    int.Parse(line.Substring(5, 5).Trim()),
                    int.Parse(line.Substring(10, 5).Trim())
                };
                Array.Sort(measures);
                if (measures[0] + measures[1] > measures[2])
                {
                    validTriangles.Add(measures);
                }
            }

            Console.WriteLine("There are ({0}) valid triangles in the input", validTriangles.Count);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            //var chars = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            //var input = "37".Select(c => int.Parse(c.ToString())).ToList();
            var input = new List<int>() { 3, 7 };
            //int iterations = 2018;
            //string input = "170641";
            int[] iterations = 170641.ToString().Select(c => int.Parse(c.ToString())).ToArray();
            //int[] iterations = 59414.ToString().Select(c => int.Parse(c.ToString())).ToArray();
            //int iterations = 59414;
            //NOT: 2888281648 // too high
            //NOT: 2103141159 // too high

            var elf1 = 0;
            var elf2 = 1;
            //int i = 0;
            int index = 0;
            while (true)
            {
                //foreach (var c in input)
                //{
                //    Console.Write(c);
                //}
                //Console.WriteLine();
                var input1 = input[elf1];
                var input2 = input[elf2];
                var output = input1 + input2;
                if (output > 9)
                {
                    input.Add(1);
                    input.Add(output - 10);
                }
                else
                {
                    input.Add(output);
                }
                elf1 = (elf1 + input1 + 1) % input.Count;
                elf2 = (elf2 + input2 + 1) % input.Count;
                //i++;
                //if (i == iterations)
                //    break;

                //if (input.Contains(iterations.ToString()))
                //    break;
                int check = 0;
                //foreach (var c in input)
                for (int iCheck = index; iCheck < input.Count && check < 6; iCheck++)
                {
                    var c = input[iCheck];
                    if (iterations[check] == c)
                    {
                        check++;
                    }
                    else if(check > 0)
                    {
                        check = 0;
                        iCheck--;
                        index=iCheck;
                    }
                }
                if (check == iterations.Length)
                {
                    break;
                }
            }
            //Console.Write(input.Substring(iterations, 10));
            Console.Write(new String(input.Select(c => c.ToString()[0]).ToArray()).IndexOf(new String(iterations.Select(c => c.ToString()[0]).ToArray())));
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("Input.txt");

            var reduce = Reduce(input);
            Console.WriteLine("Result: " + reduce.Length);

            var lengths = new List<int>();

            var characters = input.ToLower().Distinct().ToList();

            int minLength = int.MaxValue;

            foreach(var character in characters)
            {
                var input2 = input.Replace(character.ToString(), "").Replace(character.ToString().ToUpper(), "");
                var reduce2 = Reduce(input2);

                minLength = Math.Max(minLength, reduce2.Length);

                Console.WriteLine(reduce2.Length);
                lengths.Add(reduce2.Length);
            }
            var min = lengths.Min();
            Console.WriteLine("Result2: " + min);
            Console.ReadLine();
        }

        private static string Reduce(string input)
        {
            int i = 0;
            while(i < input.Length - 1)
            {
                var a = input[i];
                var b = input[i + 1];

                if (a != b && a.ToString().ToUpper() == b.ToString().ToUpper())
                {
                    input = input.Remove(i, 2);
                    i = Math.Max(0, i-1);
                }
                else
                {
                    i++;
                }
            }
            return input;
        }
    }
}

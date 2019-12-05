using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            long result = 0;
            long result2 = 0;
            var input = File.ReadAllLines("Input.txt");
            foreach (var item in input)
            {
                long mass = long.Parse(item);
                var fuel = (int)Math.Floor((double)mass / 3.0d) - 2;
                result += fuel;
                long fuel2 = fuel;
                while (true)
                {
                    fuel = (int)Math.Floor((double)fuel / 3.0d) - 2;
                    if (fuel <= 0)
                    {
                        break;
                    }
                    fuel2 += fuel;
                }
                result2 += fuel2;
            }
            Console.WriteLine(result);
            Console.WriteLine(result2);
            Console.ReadKey();
        }
    }
}

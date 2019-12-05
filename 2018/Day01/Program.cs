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
            var input = File.ReadAllLines("Input.txt");
            int frequency = 0;
            var frequencies = new HashSet<string>();
            frequencies.Add("0");
            bool findDupllicates = true;
            while (findDupllicates)
            {
                foreach (var line in input)
                {
                    var change = int.Parse(line.Substring(1));
                    var postive = line[0] == '+';
                    if (postive)
                    {
                        frequency += change;
                    }
                    else
                    {
                        frequency -= change;
                    }
                    //Console.WriteLine("Current: {0}", frequency);
                    if (!frequencies.Add(frequency.ToString()))
                    {
                        Console.WriteLine("Duplicate: {0}", frequency);
                        findDupllicates = false;
                        break;
                    }
                }
            }
            Console.WriteLine("Result: {0}", frequency);
            Console.ReadLine();
        }
    }
}

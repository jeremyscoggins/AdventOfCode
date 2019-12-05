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
            var input = File.ReadAllLines("Input.txt");
            int twos = 0;
            int threes = 0;
            foreach (var line in input)
            {
                var counts = new Dictionary<char, int>();
                foreach (var c in line)
                {
                    if (!counts.ContainsKey(c))
                    {
                        counts[c] = 1;
                    }
                    else
                    {
                        counts[c]++;
                    }
                }
                foreach (var item in counts)
                {
                    if (item.Value == 2)
                    {
                        twos++;
                        break;
                    }
                }
                foreach (var item in counts)
                {
                    if (item.Value == 3)
                    {
                        threes++;
                        break;
                    }
                }
            }
            Console.WriteLine("Twos {0}, Threes {1}, Result: {2}", twos, threes, twos * threes);


            foreach (var line in input)
            {
                bool found = false;
                foreach (var line2 in input)
                {
                    int diff = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] != line2[i])
                        {
                            diff++;
                        }
                    }
                    if (diff == 1)
                    {
                        Console.WriteLine("Box 1: {0}, Box 2: {1}", line, line2);
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}

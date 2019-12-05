using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt").OrderBy(x => UInt32.Parse(x.Split('-')[0])).ToArray();

            UInt32 highestAvail = 0;
            UInt32 ip = 0;

            UInt32 available = 0;

            //var merged = new List<Tuple<UInt32, UInt32>>();

            foreach (var line in input)
            {
                var numbers = line.Split('-');
                var low = UInt32.Parse(numbers[0]);
                var high = UInt32.Parse(numbers[1]);

                if (ip >= low)
                {
                    if (ip <= high)
                    {
                        ip = high + 1;
                    }
                }

                if (highestAvail == 0 || low > highestAvail)
                {
                    if (highestAvail != 0)
                    {
                        available += low - highestAvail;
                    }
                    //merged.Add(Tuple.Create(low, high));
                    if (high == UInt32.MaxValue)
                    {
                        break;
                    }
                    highestAvail = high + (UInt32)1;
                }
                else if(highestAvail < high)
                {
                    //merged[merged.Count - 1] = Tuple.Create(merged[merged.Count - 1].Item1, high);
                    if (high == UInt32.MaxValue)
                    {
                        break;
                    }
                    highestAvail = high + (UInt32)1;
                }
            }

            Console.WriteLine("lowest winner is: {0}", ip);
            Console.WriteLine("total available ips is: {0}", available);
        }
    }
}

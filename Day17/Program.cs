using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static List<int> measuringCups = new List<int>();
        static List<int[]> combos = new List<int[]>();
        static void Main(string[] args)
        {
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        measuringCups.Add(int.Parse(line));
                    }
                }
            }

            //recursive solution
            //for (int i = 0; i <= measuringCups.Count; i++)
            //{
            //    combinations(new int[0], measuringCups.ToArray(), i);
            //}

            //bit shifting solution
            for (int i = 0; i <= 1 << measuringCups.Count; i++)
            {
                List<int> combo = new List<int>();
                for (int i2 = 0; i2 < measuringCups.Count; i2++)
                {
                    if ((1 << i2 & i) != 0)
                    {
                        combo.Add(measuringCups[i2]);
                    }
                }
                if (combo.Sum() == 150)
                {
                    combos.Add(combo.ToArray());
                }
            }

            Console.WriteLine("there are {0} combinations", combos.Count);
            var smallestCombos = combos.GroupBy(c => c.Length).OrderBy(g => g.Key).First();
            Console.WriteLine("the smallest combination is {0}, there are {1} of those combinations", smallestCombos.Key, smallestCombos.Count());
            Console.ReadLine();
        }

        private static void combinations(int[] prefix, int[] list, int count)
        {
            if (count == 0)
            {
                //Console.WriteLine(String.Join(" ", prefix));
                if (prefix.Sum() == 150)
                {
                    combos.Add(prefix);
                }
                return;
            }
            for (int i = 0; i < list.Length; i++)
            {
                combinations(join(prefix, list[i]), rest(list, i+1), count-1);
            }
        }

        private static int[] rest(int[] set, int start)
        {
            var result = new int[set.Length - start];
            for (int i = start; i < set.Length; i++)
            {
                result[i - start] = set[i];
            }
            return result;
        }

        private static int[] join(int[] set, int item)
        {
            var result = new int[set.Length + 1];
            result[0] = item;
            set.CopyTo(result, 1);
            return result;
        }
    }
}

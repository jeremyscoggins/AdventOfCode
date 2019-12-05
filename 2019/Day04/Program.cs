using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new Dictionary<char, int>{ { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 } };
            var input1 = 134792;
            var input2 = 675810;
            var passwords = new List<string>();
            for (int i = input1; i <= input2; i++)
            {
                var value = i.ToString();
                int last = -1;
                var same = false;
                var sets = new List<int>();
                foreach (var c in value)
                {
                    var current = numbers[c];
                    if (last == current)
                    {
                        if (!same)
                        {
                            same = true;
                            sets.Add(2);
                        }
                        else
                        {
                            sets[sets.Count - 1]++;
                        }
                    }
                    else
                    {
                        same = false;
                    }
                    if (last > current)
                    {
                        sets.Clear();
                        break;
                    }
                    last = current;
                }
                //if (sets.Count == 0)
                if (sets.Count == 0 || !sets.Any(s => s == 2))
                {
                    continue;
                }
                passwords.Add(value);
            }
            Console.WriteLine(passwords.Count);
            Console.ReadLine();
            //66564 = too high
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            part1_2();
            Console.ReadLine();
        }

        static void part1_2()
        {
            string[] input = File.ReadAllLines(@"input.txt");

            int sum1 = 0; int sum2 = 0;
            foreach (var line in input)
            {
                if (supportsTLS(line))
                    sum1++;
                if (supportsSSL(line))
                    sum2++;
            }

            Console.WriteLine("Part1: {0}", sum1);
            Console.WriteLine("Part2: {0}", sum2);
        }

        static bool supportsSSL(string input)
        {
            string[] ipv7 = Regex.Split(input, @"\[[^\]]*\]");
            foreach (string ip in ipv7)
            {
                List<string> aba = checkABA(ip);
                foreach (var val in aba)
                {
                    string bab = val[1].ToString() + val[0].ToString() + val[1].ToString();
                    foreach (Match m in Regex.Matches(input, @"\[(\w*)\]"))
                    {
                        if (m.Value.Contains(bab))
                            return true;
                    }

                }
            }
            return false;
        }

        static List<string> checkABA(string input)
        {
            List<string> lst = new List<string>();
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2] && input[i] != input[i + 1])
                    lst.Add(input[i].ToString() + input[i + 1].ToString() + input[i + 2].ToString());
            }

            return lst;
        }

        static bool supportsTLS(string input)
        {
            // Check in hypernet
            foreach (Match m in Regex.Matches(input, @"\[(\w*)\]"))
            {
                if (checkABBA(m.Value))
                    return false;
            }

            string[] ipv7 = Regex.Split(input, @"\[[^\]]*\]");
            foreach (var v in ipv7)
            {
                if (checkABBA(v))
                    return true;
            }
            return false;
        }

        static bool checkABBA(string input)
        {
            for (int i = 0; i < input.Length - 3; i++)
            {
                if (input[i] == input[i + 3] && input[i + 1] == input[i + 2] && input[i] != input[i + 1])
                    return true;
            }

            return false;
        }
    }
    /*
    class Program
    {
        //static Regex regex = new Regex(@"(\w+)(\[(\w+)\])?");
        static Regex regex = new Regex(@"\[?(\w+)\]?");
        static char[] letters = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static string[] abba = new string[26 * 25];
        static string[] aba = new string[26 * 25];
        static string[] bab = new string[26 * 25];
        static void Main(string[] args)
        {


            int i = 0;
            foreach (var first in letters)
            {
                foreach (var second in letters)
                {
                    if (first != second)
                    {
                        abba[i] = new String(new[] { first, second, second, first });
                        aba[i] = new String(new[] { first, second, first });
                        bab[i] = new String(new[] { second, first, second });
                        i++;
                    }
                }
            }



            var input = File.ReadAllLines("Input.txt");

            var matchingLines = new List<string>();
            var matchingLines2 = new List<string>();

            foreach (var line in input)
            {
                bool doesContainABBA = false;
                bool doesContainABBA2 = false;
                List<int> abaIndexes = new List<int>();
                List<int> babIndexes = new List<int>();
                List<string> abas = new List<string>();
                List<string> babs = new List<string>();
                var matches = regex.Matches(line);
                foreach (Match match in matches)
                {
                    var value = match.Groups[1].Value;
                    bool isHypernet = match.Groups[0].Value != match.Groups[1].Value;

                    if (!isHypernet)
                    {
                        if (containsABBA(value))
                        {
                            doesContainABBA = true;
                        }

                        int ABAIndex;
                        if (containsABA(value, out ABAIndex))
                        {
                            abaIndexes.Add(ABAIndex);
                            abas.Add(aba[ABAIndex]);
                        }
                    }
                    else
                    {
                        //var group3 = match.Groups[3].Value;
                        //if (group3.Length > 0)
                        {
                            if (containsABBA(value))
                            {
                                doesContainABBA2 = true;
                            }
                            int BABIndex;
                            if (containsBAB(value, out BABIndex))
                            {
                                babIndexes.Add(BABIndex);
                                babs.Add(bab[BABIndex]);
                            }
                        }
                    }

                }
                if (doesContainABBA && !doesContainABBA2)
                {
                    matchingLines.Add(line);
                }
                if (abaIndexes.Count > 0 && babIndexes.Count > 0)
                {
                    if (abaIndexes.Any(abaIndex => babIndexes.Contains(abaIndex)))
                    {
                        matchingLines2.Add(line);
                    }
                }
            }

            Console.WriteLine("Matches found: {0}", matchingLines.Count);
            Console.WriteLine("2nd Matches found: {0}", matchingLines2.Count);
        }

        static bool containsABBA(string set)
        {
            foreach (var abbaItem in abba)
            {
                if (set.IndexOf(abbaItem) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        static bool containsABA(string set, out int abaIndex)
        {
            for(int i = 0; i < aba.Length; i++)
            {
                var abaItem = aba[i];
                if (set.IndexOf(abaItem) != -1)
                {
                    abaIndex = i;
                    return true;
                }
            }
            abaIndex = -1;
            return false;
        }
        static bool containsBAB(string set, out int babIndex)
        {
            for (int i = 0; i < bab.Length; i++)
            {
                var babItem = bab[i];
                if (set.IndexOf(babItem) != -1)
                {
                    babIndex = i;
                    return true;
                }
            }
            babIndex = -1;
            return false;
        }
    }
    */
}

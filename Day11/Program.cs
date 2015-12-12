using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DayEleven
{
    class Program
    {
        static string letters = "abcdefghijklmnopqrstuvwxyz";
        static string notAllowed = "iol";
        static string increment(string input)
        {
            string result = input.Substring(0, input.Length - 1);
            int lastLetter = letters.IndexOf(input[input.Length - 1]);
            lastLetter++;
            if (lastLetter >= letters.Length)
            {
                lastLetter -= letters.Length;
                result = increment(result);
            }
            result += letters[lastLetter];
            return result;
        }

        static bool test(string input)
        {
            foreach (var naLetter in notAllowed)
            {
                if (input.IndexOf(naLetter) != -1)
                {
                    return false;
                }
            }
            bool test1 = false;
            for (int i = 0; i < input.Length - 3; i++)
            {
                if ((input[i] + 1 == input[i + 1]) && (input[i + 1] + 1 == input[i + 2]))
                {
                    test1 = true;
                }
            }
            bool test2 = Regex.IsMatch(input, "(.)\\1.*(.)\\2");
            return test1 && test2;
        }

        static void Main(string[] args)
        {
            //string input = "hxbxwxba";
            string input = increment("hxbxxyzz");

            Console.WriteLine(input);

            while(!test(input))
            {
                input = increment(input);
            }
            Console.WriteLine(input);
            Console.ReadLine();            
            
        }
    }
}

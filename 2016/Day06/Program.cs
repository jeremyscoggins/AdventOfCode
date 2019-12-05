using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class Program
    {
        static char[] letters = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            var frequencies = new int[8,26];
            foreach (var line in input)
            {
                for (int i = 0; i < 8; i++)
                {
                    var letterIndex = Array.IndexOf(letters, line[i]);
                    frequencies[i,letterIndex]++;
                }
            }

            var message = "";
            var message2 = "";
            for (int i = 0; i < 8; i++)
            {
                int max = 0;
                int min = int.MaxValue;
                char letter = 'a';
                char letter2 = 'a';
                for (int l = 0; l < 26; l++)
                {
                    if (frequencies[i,l] > max)
                    {
                        max = frequencies[i,l];
                        letter = letters[l];
                    }
                    if (frequencies[i,l] < min)
                    {
                        min = frequencies[i,l];
                        letter2 = letters[l];
                    }
                }
                message += letter;
                message2 += letter2;
            }


            Console.WriteLine("the message is {0}", message);
            Console.WriteLine("the 2nd message is {0}", message2);
        }
    }
}

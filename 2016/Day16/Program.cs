using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {

        //Call the data you have at this point "a".
        //Make a copy of "a"; call this copy "b".
        //Reverse the order of the characters in "b".
        //In "b", replace all instances of 0 with 1 and all 1s with 0.
        //The resulting data is "a", then a single 0, then "b".

        static void Main(string[] args)
        {
            //var max = 272;
            var max = 35651584;
            var input = "10111100110001111";
            //Console.WriteLine("result: {0}", input);
            while (input.Length < max)
            {
                var b = swapAndReverse(input);
                input = input + "0" + b;
                //Console.WriteLine("result: {0}", input);
            }
            //Console.WriteLine("result: {0}", input);

            Console.WriteLine(checkSum(input.Substring(0, max)));

        }

        static string swapAndReverse(string input)
        {
            char[] output = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[(input.Length - 1) - i] = input[i] == '1' ? '0' : '1';
            }
            return new String(output);
        }

        static string checkSum(string input)
        {
            string output = input;
            do
            {
                output = checkSumInternal(output);
            }
            while (output.Length % 2 == 0);
            return output;
        }

        static string checkSumInternal(string input)
        {
            char[] output = new char[input.Length/2];
            for (int i = 0; i < input.Length; i+=2)
            {
                output[i/2] = input[i] == input[i+1]  ? '1' : '0';
            }
            return new String(output);
        }
    }
}

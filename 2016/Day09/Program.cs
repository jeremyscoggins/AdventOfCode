using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day09
{
    class Program
    {
        static Regex regex = new Regex(@"\((\d+)x(\d+)\)");
        static void Main(string[] args)
        {
            var input = File.ReadAllText("Input.txt");
            //while (input.Contains('('))
            //{
            //    var stringBuilder = new StringBuilder();
            //    var pos = 0;
            //    foreach (Match match in regex.Matches(input))
            //    {
            //        if (match.Index >= pos)
            //        {
            //            stringBuilder.Append(input.Substring(pos, match.Index - pos));
            //
            //            var length = int.Parse(match.Groups[1].Value);
            //            var count = int.Parse(match.Groups[2].Value);
            //
            //            pos = match.Index + match.Length;
            //
            //            var subStr = input.Substring(pos, length);
            //
            //            for (int i = 0; i < count; i++)
            //            {
            //                stringBuilder.Append(subStr);
            //            }
            //
            //            pos += length;
            //        }
            //    }
            //    input = stringBuilder.ToString();
            //    Console.WriteLine("Total Length: {0}", input.Length);
            //}
            Int64 finalLength = decompressedLength(input);

            Console.WriteLine("Total Length2: {0}", finalLength);

        }

        static Int64 decompressedLength(string input)
        {
            if (!regex.IsMatch(input))
            {
                return input.Length;
            }

            int pos = 0;
            Int64 outputLength = 0;
            
            foreach (Match match in regex.Matches(input))
            {
                if (match.Index >= pos)
                {
                    //stringBuilder.Append(input.Substring(pos, match.Index - pos));
                    outputLength += match.Index - pos;
            
                    var length = int.Parse(match.Groups[1].Value);
                    var count = int.Parse(match.Groups[2].Value);
            
                    pos = match.Index + match.Length;
            
                    var subStr = input.Substring(pos, length);
                    var subStrLength = decompressedLength(subStr);
            
                    for (int i = 0; i < count; i++)
                    {
                        //stringBuilder.Append(subStr);
                        outputLength += subStrLength;
                    }
            
                    pos += length;
                }
            }
            return outputLength;
        }
    }
}

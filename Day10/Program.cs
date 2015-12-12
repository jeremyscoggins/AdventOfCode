using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DayTen
{
    class Program
    {
        static string[] splitRuns(string input)
        {
            List<string> runs = new List<string>();
            string currentRun = "";
            foreach (var c in input)
            {
                if (currentRun == "" || currentRun[currentRun.Length - 1] == c)
                {
                    currentRun += c;
                }
                else
                {
                    runs.Add(currentRun);
                    currentRun = "" + c;
                }
            }
            runs.Add(currentRun);
            return runs.ToArray();
        }
        static void Main(string[] args)
        {
            //Regex inputRegex = new Regex("(\\d)\\1*", RegexOptions.Compiled);
            string input = "3113322113";
            string output = "";
            for (int i = 0; i < 50; i++)
            {
                //var matches = inputRegex.Matches(input);
                //var matches = Regex.Matches(input, inputRegex);
                //foreach (Match match in matches)
                var runs = splitRuns(input);
                var builder = new StringBuilder();
                foreach (var run in runs)
                {
                    //output += match.Value.Length.ToString();
                    //output += match.Value[0];
                    //output += run.Length;
                    //output += run[0];
                    builder.Append(run.Length.ToString());
                    builder.Append(run[0]);
                }
                output = builder.ToString();
                Console.WriteLine("{0} length is {1}",i+1, output.Length);
                input = output;
                output = "";
            }



            //Console.WriteLine("Found {0} matches", matches.Count);
            //Console.WriteLine("Found {0} secondary matches", matches2.Count);
            Console.WriteLine("Final String Length is {0}", output.Length);
            Console.ReadLine();
        }
    }
}

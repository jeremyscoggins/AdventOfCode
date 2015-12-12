using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DayEight
{
    class Program
    {

        static void Main(string[] args)
        {
            //int count = 0;
            //int totalCount = 0;
            string escapedText = "";
            string doubleEscapedText = "";
            string text = "";
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        //totalCount+=line.Length;
                        escapedText += line;
                        int skip = 0;
                        for (int i = 0; i < line.Length; i++)
                        {
                            char c = line[i];
                            if (i == 0 || i == line.Length - 1)
                            {
                                doubleEscapedText += "\"";
                            }
                            if (c == '"')
                            {
                                doubleEscapedText += "\\\"";
                            }
                            else if (c == '\\')
                            {
                                doubleEscapedText += "\\\\";
                            }
                            else
                            {
                                doubleEscapedText += c;
                            }

                            if (skip > 0)
                            {
                                skip--;
                            }
                            else
                            {
                                if (c == '\\')
                                {
                                    char nextChar = i < line.Length ? line[i + 1] : ' ';
                                    if (nextChar == '\\' || nextChar == '"')
                                    {
                                        text += nextChar;
                                        //count++;
                                        skip++;
                                    }
                                    else if (nextChar == 'x')
                                    {
                                        var hexChar = (char)Convert.ToInt32(line.Substring(i + 2, 2), 16);
                                        text += hexChar;
                                        //count++;
                                        skip += 3;
                                    }
                                }
                                else if (c == '"')
                                {
                                }
                                else
                                {
                                    text += c;
                                    //count++;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Escaped text length is {0}", escapedText.Length);
            Console.WriteLine("UnEscaped text length is {0}", text.Length);
            Console.WriteLine("difference is {0}", escapedText.Length - text.Length);
            Console.WriteLine("Double Escaped text length is {0}", doubleEscapedText.Length);
            Console.WriteLine("difference is {0}", doubleEscapedText.Length - escapedText.Length);
            Console.ReadLine();
        }
    }
}

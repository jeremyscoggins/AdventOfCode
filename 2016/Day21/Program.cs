using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static Regex regex1 = new Regex(@"move position (\d+) to position (\d+)");
        static Regex regex2 = new Regex(@"reverse positions (\d+) through (\d+)");
        static Regex regex3 = new Regex(@"rotate based on position of letter (\w)");
        static Regex regex4 = new Regex(@"rotate (left|right) (\d+) steps?");
        static Regex regex5 = new Regex(@"swap letter (\w) with letter (\w)");
        static Regex regex6 = new Regex(@"swap position (\d+) with position (\d+)");

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt").ToArray();

            //var output = "abcde";
            //var output = "abcdefgh";

            var reverse = "fbgdceah";

            var possibleInputs = new List<string>();
            foreach (var letter1 in reverse)
            {
                foreach (var letter2 in reverse)
                {
                    if (letter2 == letter1)
                        continue;

                    foreach (var letter3 in reverse)
                    {
                        if (new[] { letter1, letter2 }.Contains(letter3))
                            continue;

                        foreach (var letter4 in reverse)
                        {
                            if (new[] { letter1, letter2, letter3 }.Contains(letter4))
                                continue;

                            foreach (var letter5 in reverse)
                            {
                                if (new[] { letter1, letter2, letter3, letter4 }.Contains(letter5))
                                    continue;

                                foreach (var letter6 in reverse)
                                {
                                    if (new[] { letter1, letter2, letter3, letter4, letter5 }.Contains(letter6))
                                        continue;

                                    foreach (var letter7 in reverse)
                                    {
                                        if (new[] { letter1, letter2, letter3, letter4, letter5, letter6 }.Contains(letter7))
                                            continue;

                                        foreach (var letter8 in reverse)
                                        {
                                            if (new[] { letter1, letter2, letter3, letter4, letter5, letter6, letter7 }.Contains(letter8))
                                                continue;

                                            possibleInputs.Add(new String(new[] { letter1, letter2, letter3, letter4, letter5, letter6, letter7, letter8 }));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (var possibleInput in possibleInputs)
            {
                var output = possibleInput;
                foreach (var line in input)
                {
                    if (regex1.IsMatch(line))
                    {
                        //move position to position
                        var match = regex1.Match(line);
                        var position1 = int.Parse(match.Groups[1].Value);
                        var position2 = int.Parse(match.Groups[2].Value);
                        var letter = output.Substring(position1, 1);
                        output = output.Substring(0, position1) + output.Substring(position1 + 1, output.Length - (position1 + 1));
                        output = output.Substring(0, position2) + letter + output.Substring(position2, output.Length - position2);
                    }
                    else if (regex2.IsMatch(line))
                    {
                        //reverse positions
                        var match = regex2.Match(line);
                        var position1 = int.Parse(match.Groups[1].Value);
                        var position2 = int.Parse(match.Groups[2].Value);
                        var charArray = output.ToCharArray();
                        for (int i = position1; i <= position2; i++)
                        {
                            charArray[i] = output[position2 - (i - position1)];
                        }
                        output = new String(charArray);
                    }
                    else if (regex3.IsMatch(line))
                    {
                        //rotate based on position of letter
                        var match = regex3.Match(line);
                        var letter = match.Groups[1].Value;
                        var position = output.IndexOf(letter[0]);
                        var charArray = output.ToCharArray();
                        var offset = 1 + position;
                        if (position >= 4)
                        {
                            offset++;
                        }
                        for (int i = 0; i < output.Length; i++)
                        {
                            charArray[i] = output[mod(i - offset, output.Length)];
                        }
                        output = new String(charArray);
                    }
                    else if (regex4.IsMatch(line))
                    {
                        //rotate left/right
                        var match = regex4.Match(line);
                        var direction = match.Groups[1].Value == "left" ? -1 : 1;
                        var steps = int.Parse(match.Groups[2].Value);
                        var offset = steps * direction;
                        var charArray = output.ToCharArray();
                        for (int i = 0; i < output.Length; i++)
                        {
                            charArray[i] = output[mod(i - offset, output.Length)];
                        }
                        output = new String(charArray);
                    }
                    else if (regex5.IsMatch(line))
                    {
                        //swap letter with letter
                        var match = regex5.Match(line);
                        var letter1 = match.Groups[1].Value;
                        var letter2 = match.Groups[2].Value;
                        var position1 = output.IndexOf(letter1[0]);
                        var position2 = output.IndexOf(letter2[0]);
                        var charArray = output.ToCharArray();
                        charArray[position1] = letter2[0];
                        charArray[position2] = letter1[0];
                        output = new String(charArray);
                    }
                    else if (regex6.IsMatch(line))
                    {
                        //swap position with position
                        var match = regex6.Match(line);
                        var position1 = int.Parse(match.Groups[1].Value);
                        var position2 = int.Parse(match.Groups[2].Value);
                        var charArray = output.ToCharArray();
                        charArray[position1] = output[position2];
                        charArray[position2] = output[position1];
                        output = new String(charArray);
                    }
                    else
                    {
                        throw new Exception("shouldn't get here");
                    }
                }
                if (output == reverse)
                {
                    Console.WriteLine("reversed {1} into {0}", output, possibleInput);
                    return;
                }
            }

        }
        static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}

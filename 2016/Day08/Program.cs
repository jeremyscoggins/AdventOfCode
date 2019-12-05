using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day08
{
    class Program
    {
        static Regex rotateRegex = new Regex(@"^rotate (row y|column x)\=(\d+) by (\d+)$");
        static Regex rectRegex = new Regex(@"^rect (\d+)x(\d+)$");
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");

            var screen = new char[50, 6];
            var buffer = new char[50, 6];

            foreach (var line in input)
            {
                //buffer = screen.Clone() as char[,];
                Match rotate = rotateRegex.Match(line);
                if (rotate.Success)
                {
                    if (rotate.Groups[1].Value.StartsWith("row"))
                    {
                        var y = int.Parse(rotate.Groups[2].Value);
                        var amount = int.Parse(rotate.Groups[3].Value);
                        for (int i = 0; i < 50; i++)
                        {
                            buffer[(i + amount) % 50, y] = screen[i, y];
                        }
                    }
                    else
                    {
                        var x = int.Parse(rotate.Groups[2].Value);
                        var amount = int.Parse(rotate.Groups[3].Value);
                        for (int i = 0; i < 6; i++)
                        {
                            buffer[x, (i + amount) % 6] = screen[x, i];
                        }
                    }
                }
                Match rect = rectRegex.Match(line);
                if (rect.Success)
                {
                    var width = int.Parse(rect.Groups[1].Value);
                    var height = int.Parse(rect.Groups[2].Value);
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            buffer[x, y] = '#';
                        }
                    }
                }


                screen = buffer.Clone() as char[,];

                int lit = 0;
                for (int y = 0; y < 6; y++)
                {
                    Console.Write("|");
                    for (int x = 0; x < 50; x++)
                    {
                        Console.Write(screen[x, y]);
                        if (screen[x, y] != 0)
                        {
                            lit++;
                        }
                    }
                    Console.Write("|");
                    Console.WriteLine();
                }
                Console.WriteLine("Lit: {0}", lit);
            }
            Console.WriteLine();
        }
    }
}

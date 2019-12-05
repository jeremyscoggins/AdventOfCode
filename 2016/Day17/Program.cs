using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static string map = "#########\n#S| | | #\n#-#-#-#-#\n# | | | #\n#-#-#-#-#\n# | | | #\n#-#-#-#-#\n# | | |  \n####### V";

        static void Main(string[] args)
        {
            var openChars = new[] { 'b', 'c', 'd', 'e', 'f' };
            var doorChars = new[] { '-', '|' };
            var input = "vkjiggvb";
            var walls = map.Split('\n');

            var queue = new Queue<Tuple<string, int , int>>();
            queue.Enqueue(Tuple.Create("", 1, 1));

            var winners = new List<Tuple<string, int, int>>();

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                var path = item.Item1;
                var x = item.Item2;
                var y = item.Item3;

                var hash = GetMd5Hash(input + path);
                bool upIsOpen = openChars.Contains(hash[0]);
                bool downIsOpen = openChars.Contains(hash[1]);
                bool leftIsOpen = openChars.Contains(hash[2]);
                bool rightIsOpen = openChars.Contains(hash[3]);
                bool upIsDoor = doorChars.Contains(walls[y-1][x]);
                bool downIsDoor = doorChars.Contains(walls[y + 1][x]);
                bool leftIsDoor = doorChars.Contains(walls[y][x - 1]);
                bool rightIsDoor = doorChars.Contains(walls[y][x + 1]);



                if (upIsDoor && upIsOpen)
                {
                    var move = Tuple.Create(path + "U", x, y - 2);
                    if (move.Item2 == 7 && move.Item3 == 7)
                    {
                        //winner winner chicken dinner
                        Console.WriteLine("finished, moves: {0}", move.Item1);
                        winners.Add(move);
                        //return;
                    }
                    else
                    {
                        queue.Enqueue(move);
                    }
                }
                if (downIsDoor && downIsOpen)
                {
                    var move = Tuple.Create(path + "D", x, y + 2);
                    if (move.Item2 == 7 && move.Item3 == 7)
                    {
                        //winner winner chicken dinner
                        Console.WriteLine("finished, moves: {0}", move.Item1);
                        winners.Add(move);
                        //return;
                    }
                    else
                    {
                        queue.Enqueue(move);
                    }
                }
                if (leftIsDoor && leftIsOpen)
                {
                    var move = Tuple.Create(path + "L", x - 2, y);
                    if (move.Item2 == 7 && move.Item3 == 7)
                    {
                        //winner winner chicken dinner
                        Console.WriteLine("finished, moves: {0}", move.Item1);
                        winners.Add(move);
                        //return;
                    }
                    else
                    {
                        queue.Enqueue(move);
                    }
                }
                if (rightIsDoor && rightIsOpen)
                {
                    var move = Tuple.Create(path + "R", x + 2, y);
                    if (move.Item2 == 7 && move.Item3 == 7)
                    {
                        //winner winner chicken dinner
                        Console.WriteLine("finished, moves: {0}", move.Item1);
                        winners.Add(move);
                        //return;
                    }
                    else
                    {
                        queue.Enqueue(move);
                    }
                }

            }


            var winner = winners.OrderByDescending(w => w.Item1.Length).First();
            Console.WriteLine("finished, max moves: {0}", winner.Item1.Length);


        }
        static string GetMd5Hash(string input)
        {
            var md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}

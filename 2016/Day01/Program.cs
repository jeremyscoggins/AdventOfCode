using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Program
    {
        const string input = "R3, R1, R4, L4, R3, R1, R1, L3, L5, L5, L3, R1, R4, L2, L1, R3, L3, R2, R1, R1, L5, L2, L1, R2, L4, R1, L2, L4, R2, R2, L2, L4, L3, R1, R4, R3, L1, R1, L5, R4, L2, R185, L2, R4, R49, L3, L4, R5, R1, R1, L1, L1, R2, L1, L4, R4, R5, R4, L3, L5, R1, R71, L1, R1, R186, L5, L2, R5, R4, R1, L5, L2, R3, R2, R5, R5, R4, R1, R4, R2, L1, R4, L1, L4, L5, L4, R4, R5, R1, L2, L4, L1, L5, L3, L5, R2, L5, R4, L4, R3, R3, R1, R4, L1, L2, R2, L1, R4, R2, R2, R5, R2, R5, L1, R1, L4, R5, R4, R2, R4, L5, R3, R2, R5, R3, L3, L5, L4, L3, L2, L2, R3, R2, L1, L1, L5, R1, L3, R3, R4, R5, L3, L5, R1, L3, L5, L5, L2, R1, L3, L1, L3, R4, L1, R3, L2, L2, R3, R3, R4, R4, R1, L4, R1, L5";
        static void Main(string[] args)
        {
            var x = 0;
            var y = 0;
            var directions = new[] { 'N', 'E', 'S', 'W' };
            var direction = 0;
            var visited = new HashSet<string>();
            var twice = false;
            foreach (var item in input.Split(','))
            {
                var turn = item.Trim()[0];
                var distance = int.Parse(item.Trim().Substring(1));
                switch (turn)
                {
                    case 'R':
                        direction = mod(direction + 1, directions.Length);
                        break;
                    case 'L':
                        direction = mod(direction - 1, directions.Length);
                        break;
                }

                for (int i = 0; i < distance; i++)
                {
                    switch (directions[direction])
                    {
                        case 'N':
                            y -= 1;
                            break;
                        case 'E':
                            x += 1;
                            break;
                        case 'S':
                            y += 1;
                            break;
                        case 'W':
                            x -= 1;
                            break;
                    }

                    if (!twice)
                    {
                        if (!visited.Add(x.ToString() + "," + y.ToString()))
                        {
                            Console.WriteLine("The first location visited twice is ({0}, {1}) {2} blocks away", x, y, Math.Abs(x) + Math.Abs(y));
                            twice = true;
                        }
                    }
                }
            }
            Console.WriteLine("The final location is ({0}, {1}) {2} blocks away ", x, y, Math.Abs(x) + Math.Abs(y));
            Console.ReadLine();
        }

        static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}

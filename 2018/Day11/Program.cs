using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxPower = int.MinValue;
            int maxX = 0;
            int maxY = 0;
            int input = 7165;
            int[,] cells = new int[300,300];
            for(int  x = 1; x <=300; x++)
            {
                for (int y = 1; y <= 300; y++)
                {
                    int rackId = x + 10;
                    int powerLevel = rackId * y;
                    powerLevel += input;
                    powerLevel *= rackId;
                    powerLevel = (int)Math.Floor(powerLevel / 100.0f) % 10;
                    powerLevel -= 5;

                    cells[x - 1, y - 1] = powerLevel;

                    //if (powerLevel > maxPower)
                    //{
                    //    maxPower = powerLevel;
                    //    maxX = x;
                    //    maxY = y;
                    //}
                }
            }
            for (int x = 0; x < 298; x++)
            {
                for (int y = 0; y < 298; y++)
                {
                    //var topLeft = cells[x, y];
                    var totalPower = cells[x, y    ] + cells[x + 1, y    ] + cells[x + 2, y    ];
                    totalPower    += cells[x, y + 1] + cells[x + 1, y + 1] + cells[x + 2, y + 1];
                    totalPower    += cells[x, y + 2] + cells[x + 1, y + 2] + cells[x + 2, y + 2];
                    if (totalPower > maxPower)
                    {
                        maxPower = totalPower;
                        maxX = x + 1;
                        maxY = y + 1;
                    }
                }
            }
            Console.WriteLine("Max power: {0}x{1}", maxX, maxY);
            int maxPower2 = int.MinValue;
            int maxX2 = 0;
            int maxY2 = 0;
            int maxSize2 = 0;
            for (int s = 1; s <= 300; s++)
            {
                for (int x = 0; x < 300 - s + 1; x++)
                {
                    for (int y = 0; y < 300 - s + 1; y++)
                    {
                        //var topLeft = cells[x, y];
                        var totalPower = 0;
                        for (int x2 = 0; x2 < s; x2++)
                        {
                            for (int y2 = 0; y2 < s; y2++)
                            {
                                totalPower += cells[x + x2, y + y2];
                            }
                        }
                        if (totalPower > maxPower2)
                        {
                            maxPower2 = totalPower;
                            maxX2 = x + 1;
                            maxY2 = y + 1;
                            maxSize2 = s;
                        }
                    }
                }
            }
            Console.WriteLine("Max power: {0}x{1},{2}", maxX2, maxY2, maxSize2);
        }
    }
}

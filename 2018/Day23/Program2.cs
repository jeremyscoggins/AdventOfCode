using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program2
    {
        public static void DoWork()
        {
            bool TestMode = false;
            int WhichPart = 2;
            string[] InputSplit = File.ReadAllLines("Input.txt");

            List<Bot> bots = new List<Bot>();
            (int x, int y, int z) bestLocation = (0, 0, 0);
            int inRange = 0, maxInRange = 0, bestSum = 0;
            (int minX, int minY, int minZ, int maxX, int maxY, int maxZ) limits;
            int grain = TestMode ? 1 : (int)Math.Pow(2, 26);

            foreach (string input in InputSplit)
            {
                string[] bits = input.Split(new char[] { '=', '<', '>', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                bots.Add(new Bot((int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[3])), int.Parse(bits[5])));
            }
            Bot biggestBot = bots.OrderByDescending(b => b.Radius).FirstOrDefault();
            limits = (bots.Min(bot => bot.Location.X), bots.Min(bot => bot.Location.Y), bots.Min(bot => bot.Location.Z), bots.Max(bot => bot.Location.X), bots.Max(bot => bot.Location.Y), bots.Max(bot => bot.Location.Z));
            int xRange = limits.maxX - limits.minX, yRange = limits.maxY - limits.minY, zRange = limits.maxZ - limits.minZ;

            int inRangeOfBiggest = bots.Count(bot => biggestBot.InRange(bot));

            if (WhichPart == 2)
                do
                {
                    maxInRange = 0;
                    bestSum = int.MaxValue;
                    for (int x = limits.minX; x < limits.maxX; x += grain)
                        for (int y = limits.minY; y < limits.maxY; y += grain)
                            for (int z = limits.minZ; z < limits.maxZ; z += grain)
                                if ((inRange = bots.Count(bot => bot.InRange(x, y, z))) > maxInRange || inRange == maxInRange && Math.Abs(x) + Math.Abs(y) + Math.Abs(z) < bestSum)
                                {
                                    maxInRange = inRange;
                                    bestLocation = (x, y, z);
                                    bestSum = Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
                                }
                    //Debug.Print("Grain {0}: Location: {1}, {2}, {3} InRange: {4} Sum: {5}", grain, bestLocation.x, bestLocation.y, bestLocation.z, maxInRange, bestSum);
                    grain /= 2; xRange /= 2; yRange /= 2; zRange /= 2;
                    limits = (bestLocation.x - xRange / 2, bestLocation.y - yRange / 2, bestLocation.z - zRange / 2, bestLocation.x + xRange / 2, bestLocation.y + yRange / 2, bestLocation.z + zRange / 2);
                } while (grain >= 1);

            var Output = (WhichPart == 1 ? inRangeOfBiggest : bestSum).ToString();
            Console.WriteLine(Output);
        }

        private class Bot
        {
            public (int X, int Y, int Z) Location { get; private set; }
            public int Radius { get; private set; }
            public Bot((int x, int y, int z) location, int radius) { Location = location; Radius = radius; }
            public bool InRange(int x, int y, int z) => Distance(x, y, z) <= Radius;
            public bool InRange(Bot otherBot) => InRange(otherBot.Location.X, otherBot.Location.Y, otherBot.Location.Z);
            public int Distance(int x, int y, int z) => Math.Abs(Location.X - x) + Math.Abs(Location.Y - y) + Math.Abs(Location.Z - z);
            public int Distance(Bot otherBot) => Distance(otherBot.Location.X, otherBot.Location.Y, otherBot.Location.Z);
        }
    }
}

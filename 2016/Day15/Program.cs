using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        //sample:
        //Disc #1 has 5 positions; at time=0, it is at position 4.
        //Disc #2 has 2 positions; at time=0, it is at position 1.

        //input:
        //Disc #1 has 13 positions; at time=0, it is at position 10.
        //Disc #2 has 17 positions; at time=0, it is at position 15.
        //Disc #3 has 19 positions; at time=0, it is at position 17.
        //Disc #4 has 7 positions; at time=0, it is at position 1.
        //Disc #5 has 5 positions; at time=0, it is at position 0.
        //Disc #6 has 3 positions; at time=0, it is at position 1.

        //Disc #7 has 11 positions; at time=0, it is at position 0.


        static void Main(string[] args)
        {
            long time = 0;
            while (true)
            {
                //if ((time + 1 + 4) % 5 == 0)
                //{
                //    if ((time + 2 + 1) % 2 == 0)
                //    {
                //        Console.WriteLine("Success time: {0}", time);
                //        return;
                //    }
                //}

                if ((time + 1 + 10) % 13 == 0)
                {
                    if ((time + 2 + 15) % 17 == 0)
                    {
                        if ((time + 3 + 17) % 19 == 0)
                        {
                            if ((time + 4 + 1) % 7 == 0)
                            {
                                if ((time + 5 + 0) % 5 == 0)
                                {
                                    if ((time + 6 + 1) % 3 == 0)
                                    {
                                        Console.WriteLine("Success time: {0}", time);
                                        if ((time + 7 + 0) % 11 == 0)
                                        {
                                            Console.WriteLine("Success time: {0}", time);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                time++;
            }
        }
    }
}

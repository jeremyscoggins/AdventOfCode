using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        //The first floor contains a strontium generator, a strontium-compatible microchip, a plutonium generator, and a plutonium-compatible microchip.
        //The second floor contains a thulium generator, a ruthenium generator, a ruthenium-compatible microchip, a curium generator, and a curium-compatible microchip.
        //The third floor contains a thulium-compatible microchip.
        //The fourth floor contains nothing relevant.
        const uint strontium    = 1 << 0;
        const uint strontiumGen = 1 << 1;
        const uint plutonium    = 1 << 2;
        const uint plutoniumGen = 1 << 3;
        const uint thulium      = 1 << 4;
        const uint thuliumGen   = 1 << 5;
        const uint ruthenium    = 1 << 6;
        const uint rutheniumGen = 1 << 7;
        const uint curium       = 1 << 8;
        const uint curiumGen    = 1 << 9;
        const uint elerium      = 1 << 10;
        const uint eleriumGen   = 1 << 11;
        const uint dilithium    = 1 << 12;
        const uint dilithiumGen = 1 << 13;

        //const long floorMask = strontium | strontiumGen | plutonium | plutoniumGen | thulium | thuliumGen | ruthenium | rutheniumGen | curium | curiumGen;
        //const int itemsPerFloor = 10;

        //Upon entering the isolated containment area, however, you notice some extra parts on the first floor that weren't listed on the record outside:
        //
        //An elerium generator.
        //An elerium-compatible microchip.
        //A dilithium generator.
        //A dilithium-compatible microchip.


        const long floorMask = strontium | strontiumGen | plutonium | plutoniumGen | thulium | thuliumGen | ruthenium | rutheniumGen | curium | curiumGen | elerium | eleriumGen | dilithium | dilithiumGen;
        const int itemsPerFloor = 14;

        const int elevatorOffset = 4;
        const long elevatorMask = 15;

        const int floor1Offset = 4;
        const int floor2Offset = floor1Offset + itemsPerFloor;
        const int floor3Offset = floor2Offset + itemsPerFloor;
        const int floor4Offset = floor3Offset + itemsPerFloor;

        const int totalBits = itemsPerFloor * 4 + 4;

        //strontium | strontiumGen | plutonium | plutoniumGen | thulium | thuliumGen | ruthenium | rutheniumGen | curium | curiumGen

        static void Main(string[] args)
        {
            long startState = 0;
            updateElevator(ref startState, 0);
            //Console.WriteLine(Convert.ToString(startState, 2).PadLeft(totalBits, '0'));

            //updateFloor(ref startState, strontiumGen | strontium | plutoniumGen | plutonium, 0);
            updateFloor(ref startState, strontiumGen | strontium | plutoniumGen | plutonium | eleriumGen | elerium | dilithiumGen | dilithium, 0);
            //Console.WriteLine(Convert.ToString(startState, 2).PadLeft(totalBits, '0'));

            updateFloor(ref startState, thuliumGen | rutheniumGen | ruthenium | curiumGen | curium, 1);
            //Console.WriteLine(Convert.ToString(startState, 2).PadLeft(totalBits, '0'));

            updateFloor(ref startState, thulium, 2);
            //Console.WriteLine(Convert.ToString(startState, 2).PadLeft(totalBits, '0'));

            var seenStates = new HashSet<long>();
            var queue = new Queue<Tuple<int, long[]>>();

            queue.Enqueue(Tuple.Create(0, new[] { startState }));
            seenStates.Add(startState);

            while (queue.Count > 0)
            {
                var currentState = queue.Dequeue();
                var prevSteps = currentState.Item1;
                var history = currentState.Item2;
                var items = history[history.Length - 1];

                int elevator = getElevator(items);

                int[] directions = new[] { 1, -1 };
                foreach (var direction in directions)
                {
                    int newFloor = elevator + direction;
                    if (newFloor >= 0 && newFloor < 4)
                    {
                        long sourceFloorState = getFloor(items, elevator);
                        //Console.WriteLine(Convert.ToString(sourceFloorState, 2).PadLeft(itemsPerFloor, '0'));
                        long destFloorState = getFloor(items, newFloor);
                        //Console.WriteLine(Convert.ToString(destFloorState, 2).PadLeft(itemsPerFloor, '0'));

                        for (int i = 0; i < itemsPerFloor; i++)
                        {
                            uint item1 = (uint)1 << i;
                            for (int i2 = 0; i2 < itemsPerFloor; i2++)
                            {
                                uint item2 = (uint)1 << i2;
                                if ((sourceFloorState & item1) != 0 && (sourceFloorState & item2) != 0)
                                {
                                    long newSourceFloorState = sourceFloorState;
                                    long newDestFloorState = destFloorState;

                                    newSourceFloorState = newSourceFloorState & ~item1;
                                    newDestFloorState = newDestFloorState | item1;

                                    if (item1 != item2)
                                    {
                                        newSourceFloorState = newSourceFloorState & ~item2;
                                        newDestFloorState = newDestFloorState | item2;
                                    }


                                    if (isValid(newSourceFloorState) && isValid(newDestFloorState))
                                    {
                                        //Console.WriteLine(Convert.ToString(newSourceFloorState, 2).PadLeft(itemsPerFloor, '0'));
                                        //Console.WriteLine(Convert.ToString(newDestFloorState, 2).PadLeft(itemsPerFloor, '0'));

                                        long newItems = items;
                                        updateElevator(ref newItems, newFloor);
                                        updateFloor(ref newItems, newSourceFloorState, elevator);
                                        updateFloor(ref newItems, newDestFloorState, newFloor);
                                        var newSteps = prevSteps + 1;

                                        if (seenStates.Add(newItems))
                                        {
                                            var newHistory = new long[history.Length + 1];
                                            Array.Copy(history, newHistory, history.Length);
                                            newHistory[newHistory.Length - 1] = newItems;
                                            //if (newFloor == 3 && newDestFloorState == (strontium | strontiumGen | plutonium | plutoniumGen | thulium | thuliumGen | ruthenium | rutheniumGen | curium | curiumGen))
                                            if (newFloor == 3 && newDestFloorState == (strontium | strontiumGen | plutonium | plutoniumGen | thulium | thuliumGen | ruthenium | rutheniumGen | curium | curiumGen | elerium | eleriumGen | dilithium | dilithiumGen))
                                            {
                                                //winner winner chicken dinner
                                                //Console.WriteLine(Convert.ToString(newItems, 2).PadLeft(totalBits, '0'));

                                                foreach (var historyItem in newHistory)
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine(Convert.ToString(historyItem, 2).PadLeft(totalBits, '0'));
                                                }
                                                Console.WriteLine("{0} steps to success", newSteps);
                                                return;

                                            }
                                            else
                                            {
                                                queue.Enqueue(Tuple.Create(newSteps, newHistory));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static void updateFloor(ref long state, long items, int floor)
        {
            if (!isValid(items))
            {
                throw new Exception("shouldn't get here");
            }

            switch (floor)
            {
                case 0:
                    state = state & ~(floorMask << floor1Offset) | (items << floor1Offset);
                    return;
                case 1:
                    state = state & ~(floorMask << floor2Offset) | (items << floor2Offset);
                    return;
                case 2:
                    state = state & ~(floorMask << floor3Offset) | (items << floor3Offset);
                    return;
                case 3:
                    state = state & ~(floorMask << floor4Offset) | (items << floor4Offset);
                    return;
                default:
                    throw new Exception("shouldn't get here");
            }
        }

        static void updateElevator(ref long state, int elevator)
        {
            if (elevator < 0 || elevator > 3)
            {
                throw new Exception("shouldn't get here");
            }
            state = state  & ~elevatorMask | ((long)1 << elevator);
        }

        static int getElevator(long state)
        {
            long elevator = state & elevatorMask;
            switch (elevator)
            {
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 4:
                    return 2;
                case 8:
                    return 3;
                default:
                    throw new Exception("shouldn't get here");
            }

        }

        static long getFloor(long state, int floor)
        {
            switch (floor)
            {
                case 0:
                    return (state & floorMask << floor1Offset) >> floor1Offset;
                case 1:
                    return (state & floorMask << floor2Offset) >> floor2Offset;
                case 2:
                    return (state & floorMask << floor3Offset) >> floor3Offset;
                case 3:
                    return (state & floorMask << floor4Offset) >> floor4Offset;
                default:
                    throw new Exception("shouldn't get here");
            }
        }

        static bool isValid(long state)
        {
            bool hasStrontium    = (state & 1 << 0) != 0;
            bool hasStrontiumGen = (state & 1 << 1) != 0;
            bool hasPlutonium    = (state & 1 << 2) != 0;
            bool hasPlutoniumGen = (state & 1 << 3) != 0;
            bool hasThulium      = (state & 1 << 4) != 0;
            bool hasThuliumGen   = (state & 1 << 5) != 0;
            bool hasRuthenium    = (state & 1 << 6) != 0;
            bool hasRutheniumGen = (state & 1 << 7) != 0;
            bool hasCurium       = (state & 1 << 8) != 0;
            bool hasCuriumGen    = (state & 1 << 9) != 0;

            bool hasElerium      = (state & 1 << 10) != 0;
            bool hasEleriumGen   = (state & 1 << 11) != 0;
            bool hasDilithium    = (state & 1 << 12) != 0;
            bool hasDilithiumGen = (state & 1 << 13) != 0;

            bool hasGen = hasStrontiumGen || hasPlutoniumGen || hasThuliumGen || hasRutheniumGen || hasCuriumGen || hasEleriumGen || hasDilithiumGen;
            bool hasChip = hasStrontium || hasPlutonium || hasThulium || hasRuthenium || hasCurium || hasElerium || hasDilithium;

            if (hasGen && !hasChip)
            {
                // all gens
                return true;
            }

            if (hasChip && !hasGen)
            {
                // all chips
                return true;
            }


            if (hasStrontium && !hasStrontiumGen)
            {
                return false;
            }

            if (hasPlutonium && !hasPlutoniumGen)
            {
                return false;
            }

            if (hasThulium && !hasThuliumGen)
            {
                return false;
            }

            if (hasRuthenium && !hasRutheniumGen)
            {
                return false;
            }

            if (hasCurium && !hasCuriumGen)
            {
                return false;
            }

            if (hasElerium && !hasEleriumGen)
            {
                return false;
            }

            if (hasDilithium && !hasDilithiumGen)
            {
                return false;
            }

            return true;

        }
    }        
}           
             
            
             
            
             
             
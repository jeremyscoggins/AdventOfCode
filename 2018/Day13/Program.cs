using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        class Cart
        {
            public uint row;
            public uint col;
            public uint dir;
            public uint next;
            public bool dead;
        }
        static char[] cartTypes = { '^', '>', 'v', '<' };
        static char[] wallTypes = { '|', '-', '|', '-' };
        static char[] turnTypes = { '/', '\\' };
        static uint[][] turns = { new uint[]{ 1, 3 }, new uint[] { 0, 2 }, new uint[] { 3, 1 }, new uint[] { 2, 0 } }; 
        static List<Cart> carts = new List<Cart>();
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("Input.txt").Select(line => line.ToArray()).ToArray();

            for (uint row = 0; row < data.Length; row++)
            {
                for (uint col = 0; col < data[0].Length; col++)
                {
                    var current = data[row][col];
                    if (cartTypes.Contains(current))
                    {
                        carts.Add(new Cart() { row = row, col = col, dir = (uint)Array.IndexOf(cartTypes, current), next = 0 });
                        data[row][col] = wallTypes[Array.IndexOf(cartTypes, current)];
                    }

                }
            }

            bool collision = false;
            while (!collision)
            {
                foreach (var cart in carts.OrderBy(c => c.row).ThenBy(c => c.col))
                {
                    if (cart.dead)
                        continue;
                    switch (cart.dir)
                    {
                        case 0:
                            cart.row--;
                            break;
                        case 1:
                            cart.col++;
                            break;
                        case 2:
                            cart.row++;
                            break;
                        case 3:
                            cart.col--;
                            break;
                    }
                    if (carts.Any(c => c != cart && !c.dead && c.row == cart.row && c.col == cart.col))
                    {
                        Console.WriteLine("collision X:" + cart.col + " Y:" + cart.row);
                        carts.ForEach(c => { if (c != cart && !c.dead && c.row == cart.row && c.col == cart.col) { c.dead = true; } });
                        cart.dead = true;
                        //collision = true;
                        //break;
                    }
                    var next = data[cart.row][cart.col];
                    if (turnTypes.Contains(next))
                    {
                        cart.dir = turns[cart.dir][Array.IndexOf(turnTypes, next)];
                        //switch (cart.dir)
                        //{
                        //    case 0:
                        //        cart.dir = next == '/' ? (uint)1 : 3;
                        //        break;
                        //    case 1:
                        //        cart.dir = next == '/' ? (uint)0 : 2;
                        //        break;
                        //    case 2:
                        //        cart.dir = next == '/' ? (uint)3 : 1;
                        //        break;
                        //    case 3:
                        //        cart.dir = next == '/' ? (uint)2 : 0;
                        //        break;
                        //}
                    }
                    else if (next == '+')
                    {
                        switch (cart.next)
                        {
                            case 0:
                                cart.dir  = (cart.dir + 3) % 4;
                                cart.next = 1;
                                break;
                            case 1:
                                cart.next = 2;
                                break;
                            case 2:
                                cart.dir = (cart.dir + 1) % 4;
                                cart.next = 0;
                                break;
                        }
                    }
                }
                if (carts.Count(c => !c.dead) == 1)
                {
                    var last = carts.First(c => !c.dead);
                    Console.WriteLine("last X:" + last.col + " Y:" + last.row);
                    collision = true;
                    break;
                }
                //for (int row = 0; row < data.Length; row++)
                //{
                //    for (int col = 0; col < data[0].Length; col++)
                //    {
                //        var cart = carts.FirstOrDefault(c => c.row == row && c.col == col);
                //        if (cart != null)
                //        {
                //            Console.Write(cart.next);
                //        }
                //        else
                //        {
                //            Console.Write(data[row][col]);
                //        }
                //    }
                //    Console.WriteLine();
                //}
            }
            Console.ReadLine();
        }
    }
}

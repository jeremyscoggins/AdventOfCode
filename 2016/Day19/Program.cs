using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    class Elf
    {
        public int Number { get; set; }
        public int Presents { get; set; }
        public Elf Next { get; set; }
        public Elf Prev { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //var size = 5;
            var size = 3014603;

            Elf root = new Elf { Number = 1, Presents = 1 };
            Elf elf = root;
            Elf target = null;

            for (int i = 1; i < size+1; i ++)
            {
                elf.Next = i == size ? root : new Elf { Number = i + 1, Presents = 1, Prev = elf };
                elf = elf.Next;
                if (i == size / 2)
                {
                    target = elf;
                }
            }
            root.Prev = elf;

            var count = size;

            while (elf.Next != elf)
            {
                elf.Presents += target.Presents;

                target.Prev.Next = target.Next;
                target.Next.Prev = target.Prev;

                target = count % 2 == 1 ? target.Next.Next : target.Next;
                count--;

                elf = elf.Next;
            }

            Console.WriteLine("the winner is: {0}", elf.Number);
        }
    }
}

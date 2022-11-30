using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AoC2021
{
    public class Day11 : Day
    {
        private int[,] octopi;
        HashSet<(int, int)> flash;
        private int flashCount;

        public Day11()
            : base(11, false)
        { }

        public override void Solve()
        {
            octopi = new int[input[0].Length, input.Count];

            for (int y = 0; y < input.Count; y++)
                for (int x = 0; x < input[y].Length; x++)
                {
                    octopi[x, y] = input[y][x] - '0';
                }
            //Print(0);

            var result = Step(1);
        }

        private int Step(int step)
        {
            flash = new HashSet<(int, int)>();

            for (int x = 0; x < octopi.GetLength(0); x++)
                for (int y = 0; y < octopi.GetLength(1); y++)
                {
                    Increase(x, y);
                }

            foreach(var (x,y) in flash)
            {
                octopi[x, y] = 0;
            }

            if (flash.Count == octopi.GetLength(0) * octopi.GetLength(1))
                return step;

            flashCount += flash.Count;
            return Step(++step);
            //Print(step);
        }

        private void Increase(int x, int y)
        {
            try
            {
                if (++octopi[x, y] > 9)
                    Flash(x, y);
            }
            catch
            {
                return;
            }
        }

        private void Flash(int x, int y)
        {
            if (flash.Contains((x, y)))
                return;

            flash.Add((x, y));

            Increase(x - 1, y - 1);
            Increase(x, y - 1);
            Increase(x + 1, y - 1);

            Increase(x - 1, y);
            Increase(x, y);
            Increase(x + 1, y);

            Increase(x - 1, y + 1);
            Increase(x, y + 1);
            Increase(x + 1, y + 1);
        }

        private void Print(int step)
        {
            Console.WriteLine();
            Console.WriteLine($"Step {step}");

            for (int y = 0; y < octopi.GetLength(1); y++)
            {
                for (int x = 0; x < octopi.GetLength(0); x++)
                {
                    if (octopi[x, y] == 0)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(octopi[x, y]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

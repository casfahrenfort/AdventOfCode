using System;
using System.Collections.Generic;

namespace AoC2022
{
    public class Day10 : Day
    {
        public Dictionary<int, int> cycles = new Dictionary<int, int>();
        char[] pixels = new char[240];
        public Day10()
            : base(10, false)
        {
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = '.';
        }

        public override void Solve()
        {
            int cycle = 1;
            int X = 1;
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "noop")
                {
                    cycle = Cycle(cycle, X);
                    continue;
                }

                string[] split = input[i].Split(' ');
                int addX = int.Parse(split[1]);

                cycle = Cycle(cycle, X);
                cycle = Cycle(cycle, X);
                X += addX;
            }


            for (int j = 0; j < 240; j += 40)
            {
                for (int i = 0; i < 40; i++)
                {
                    int c = j + i + 1;
                    int x = cycles[j + i + 1];
                    if (i == x - 1 || i == x || i == x + 1)
                        Console.Write('#');
                    else
                        Console.Write('.');

                }
                Console.WriteLine();
            }
        }

        public int Cycle(int cycle, int X)
        {
            cycles.Add(cycle, X);
            return ++cycle;
        }
    }
}

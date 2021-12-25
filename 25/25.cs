using NGenerics.DataStructures.Mathematical;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AoC2021
{
    public class Day25 : Day
    {
        private char[,] cucumbers;
        public int steps = 0;

        public Day25()
            : base(25, false)
        { }

        public override void Solve()
        {
            cucumbers = new char[input[0].Length, input.Count];
            for (int y = 0; y < input.Count; y++)
                for(int x =0; x <input[y].Length; x++)
                {
                    cucumbers[x, y] = input[y][x];
                }

            bool moved = Step();
            while (moved)
            {
                moved = Step();
            }
        }

        public bool Step()
        {
            steps++;
            //Print();
            bool moveEast = MoveEast();
            bool moveSouth = MoveSouth();
            return moveEast || moveSouth;
        }

        public bool MoveEast()
        {
            char[,] nextCucumbers = (char[,])cucumbers.Clone();
            bool anyMoved = false;

            for(int x = 0; x < cucumbers.GetLength(0); x++)
                for(int y = 0; y< cucumbers.GetLength(1); y++)
                {
                    if (cucumbers[x, y] != '>')
                        continue;

                    int nextEast = NextEast(x);
                    if (cucumbers[nextEast, y] == '.')
                    {
                        nextCucumbers[nextEast, y] = '>';
                        nextCucumbers[x, y] = '.';
                        anyMoved = true;
                    }
                }

            cucumbers = nextCucumbers;
            return anyMoved;
        }

        public bool MoveSouth()
        {
            char[,] nextCucumbers = (char[,])cucumbers.Clone();
            bool anyMoved = false;

            for (int x = 0; x < cucumbers.GetLength(0); x++)
                for (int y = 0; y < cucumbers.GetLength(1); y++)
                {
                    if (cucumbers[x, y] != 'v')
                        continue;

                    int nextSouth = NextSouth(y);
                    if (cucumbers[x, nextSouth] == '.')
                    {
                        nextCucumbers[x, nextSouth] = 'v';
                        nextCucumbers[x, y] = '.';
                        anyMoved = true;
                    }
                }

            cucumbers = nextCucumbers;
            return anyMoved;
        }

        public int NextEast(int x)
        {
            if (x == cucumbers.GetLength(0) - 1)
                x = 0;
            else
                x += 1;

            return x;
        }

        public int NextSouth(int y)
        {
            if (y == cucumbers.GetLength(1) - 1)
                y = 0;
            else
                y += 1;

            return y;
        }
        
        public void Print()
        {
            for (int y = 0; y < cucumbers.GetLength(1); y++)
            {
                for (int x = 0; x < cucumbers.GetLength(0); x++)
                {
                    Console.Write(cucumbers[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

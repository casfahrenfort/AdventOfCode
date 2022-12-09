using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day8 : Day
    {
        int[,] grid;
        int[,] distanceGrid;

        public Day8()
            : base(8, false)
        { }

        public override void Solve()
        {
            grid = new int[input[0].Length, input.Count];
            distanceGrid = new int[input[0].Length, input.Count];

            for (int y = 0; y < input.Count; y++)
                for (int x = 0; x < input[0].Length; x++)
                {
                    grid[x, y] = input[y][x] - '0';
                }

            int highestDistance = int.MinValue;
            for (int y = 0; y < input.Count; y++)
                for (int x = 0; x < input[0].Length; x++)
                {
                    int distance = ViewDistance(x, y);
                    distanceGrid[x, y] = distance;
                    if (distance > highestDistance)
                        highestDistance = distance;
                }

            //DrawGrid();
            //DrawDistanceGrid();
            Console.WriteLine(highestDistance);
        }

        public int ViewDistance(int x, int y)
        {
            int size = grid[x, y];

            int left = 0;
            if (x != 0)
                for (int i = x - 1; i >= 0; i--)
                    if (grid[i, y] >= size)
                    {
                        left = x - i;
                        break;
                    }
                    else
                    {
                        left++;
                    }

            int right = 0;
            if (x != grid.GetLength(0))
                for (int i = x + 1; i < grid.GetLength(0); i++)
                    if (grid[i, y] >= size)
                    {
                        right = i - x;
                        break;
                    }
                    else
                    {
                        right++;
                    }


            int up = 0;
            if (y != 0)
                for (int i = y - 1; i >= 0; i--)
                    if (grid[x, i] >= size)
                    {
                        up = y - i;
                        break;
                    }
                    else
                    {
                        up++;
                    }
            int down = 0;
            if (y != grid.GetLength(1))
                for (int i = y + 1; i < grid.GetLength(1); i++)
                    if (grid[x, i] >= size)
                    {
                        down = i - y;
                        break;
                    }
                    else
                    {
                        down++;
                    }

            return up * down * left * right;
        }

        public void DrawGrid()
        {
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void DrawDistanceGrid()
        {
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    Console.Write(distanceGrid[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day9 : Day
    {
        private int[,] heightMap;
        private bool[,] visited;

        public Day9()
            : base(9, false)
        { }

        public override void Solve()
        {
            heightMap = new int[input[0].Length, input.Count];
            visited = new bool[input[0].Length, input.Count];

            for (int i = 0; i < input[0].Length; i++)
                for (int j = 0; j < input.Count; j++)
                {
                    int[] split = input[j].ToCharArray().Select(c => c - '0').ToArray();
                    heightMap[i, j] = split[i];
                }

            int result = BasinSizes();
        }

        private int BasinSizes()
        {
            List<int> basinSizes = new List<int>();

            for (int x = 0; x < heightMap.GetLength(0); x++)
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    if (heightMap[x, y] == 9 || visited[x, y])
                        continue;

                    basinSizes.Add(ExploreBasin(x, y));
                }

            return basinSizes.OrderByDescending(x => x).Take(3).Aggregate(1, (x, y) => x * y);
        }

        private int ExploreBasin(int x, int y)
        {
            //PrintHeightMap(x, y);

            if (GetHeight(x, y) == 9 || GetVisisted(x, y))
                return 0;

            visited[x, y] = true;
            return 1 +
                ExploreBasin(x - 1, y) +
                ExploreBasin(x + 1, y) +
                ExploreBasin(x, y - 1) +
                ExploreBasin(x, y + 1);
        }

        private List<(int,int)> LowPoints()
        {
            List<(int, int)> result = new List<(int, int)>();

            for (int x = 0; x < heightMap.GetLength(0); x++)
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    int up, down, left, right;
                    up = GetHeight(x, y - 1);
                    down = GetHeight(x, y + 1);
                    left = GetHeight(x - 1, y);
                    right = GetHeight(x + 1, y);

                    if (heightMap[x, y] < up
                        && heightMap[x, y] < down
                        && heightMap[x, y] < left
                        && heightMap[x, y] < right)
                    {
                        result.Add((x, y));
                    }
                }

            return result;
        }

        private int GetHeight(int x, int y)
        {
            try
            {
                return heightMap[x, y];
            }
            catch
            {
                return int.MaxValue;
            }
        }

        private bool GetVisisted(int x, int y)
        {
            try
            {
                return visited[x, y];
            }
            catch
            {
                return true;
            }
        }

        private void PrintHeightMap(int currentX, int currentY)
        {
            if (currentX < 0 || currentX > heightMap.GetLength(0) || currentY < 0 || currentY > heightMap.GetLength(1))
                return;

            Console.WriteLine();
            for (int y = 0; y < heightMap.GetLength(1); y++)
            {
                for (int x = 0; x < heightMap.GetLength(0); x++)
                {
                    if (x == currentX && y == currentY)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (visited[x, y])
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(heightMap[x, y]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

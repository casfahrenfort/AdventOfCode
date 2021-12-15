using NGenerics.DataStructures.Queues;
using System;
using System.Collections.Generic;

namespace AoC2021
{
    public class Day15 : Day
    {
        private int[,] cave;
        private PriorityQueue<(int, int), int> vertices;

        public Day15()
            : base(15, false)
        { }

        public override void Solve()
        {
            cave = new int[input[0].Length * 5, input.Count * 5];
            vertices = new PriorityQueue<(int, int), int>(PriorityQueueType.Minimum);
            var distances = new Dictionary<(int, int), int>();
            var previous = new Dictionary<(int, int), (int, int)?>();


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int y = 0; y < input.Count; y++)
                        for (int x = 0; x < input[y].Length; x++)
                        {
                            int xi = x + input[y].Length * i;
                            int yj = y + input.Count * j;

                            if (i == 0 && j == 0)
                                cave[xi, yj] = input[y][x] - '0';
                            else
                            {
                                int prevX = i == 0 ? xi : xi - input[y].Length;
                                int prevY = i == 0 ? yj - input.Count : yj;
                                int prevVal = cave[prevX, prevY];
                                cave[xi, yj] = prevVal == 9 ? 1 : prevVal + 1;
                            }

                            distances.Add((xi, yj), int.MaxValue);
                            previous.Add((xi, yj), null);
                        }
                }
            }

            distances[(0, 0)] = 0;
            vertices.Enqueue((0, 0), int.MaxValue);

            while (vertices.Count != 0)
            {
                (int, int) u = vertices.Dequeue();

                foreach (var v in Neighbours(u))
                {
                    int alt = distances[u] + cave[v.Item1, v.Item2];
                    if (alt < distances[v])
                    {
                        distances[v] = alt;
                        previous[v] = u;
                        if (!vertices.Contains(v))
                        {
                            vertices.Enqueue(v, alt);
                        }
                    }
                }

            }

            int result = distances[(cave.GetLength(0) - 1, cave.GetLength(1) - 1)];
        }

        private List<(int, int)> Neighbours((int, int) v)
        {
            List<(int, int)> neighbours = new List<(int, int)>();
            try
            {
                var x = cave[v.Item1 - 1, v.Item2];
                neighbours.Add((v.Item1 - 1, v.Item2));
            }
            catch
            { }
            try
            {
                var x = cave[v.Item1 + 1, v.Item2];
                neighbours.Add((v.Item1 + 1, v.Item2));
            }
            catch
            { }
            try
            {
                var x = cave[v.Item1, v.Item2 - 1];
                neighbours.Add((v.Item1, v.Item2 - 1));
            }
            catch
            { }
            try
            {
                var x = cave[v.Item1, v.Item2 + 1];
                neighbours.Add((v.Item1, v.Item2 + 1));
            }
            catch
            { }
            return neighbours;

        }
    }
}

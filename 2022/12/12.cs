using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day12 : Day
    {
        char[,] heightMap;
        (int, int) end = (-1, -1);

        public Day12()
            : base(12, false)
        { }

        public override void Solve()
        {
            var vertices = new PriorityQueue<(int, int), long>();
            var distances = new Dictionary<(int, int), long>();
            var previous = new Dictionary<(int, int), (int, int)?>();
            var inQ = new Dictionary<(int, int), bool>();
            heightMap = new char[input[0].Length, input.Count];

            List<(int, int)> possibleStarts = new List<(int, int)>();

            for(int y = 0; y < input.Count; y++)
                for(int x = 0; x < input[0].Length; x++)
                {
                    heightMap[x, y] = input[y][x];

                    if (heightMap[x, y] == 'S' || heightMap[x, y] == 'a')
                        possibleStarts.Add((x, y));
                    if (heightMap[x, y] == 'E')
                        end = (x, y);

                    distances.Add((x, y), int.MaxValue);
                    previous.Add((x, y), null);
                    vertices.Enqueue((x,y), distances[(x,y)]);
                    inQ.Add((x, y), false);
                }

            var result = possibleStarts
                .Select(start => StepsToEnd(start,
                                Clone(vertices),
                                new Dictionary<(int, int), long>(distances),
                                new Dictionary<(int, int), (int, int)?>(previous),
                                new Dictionary<(int, int), bool>(inQ)))
                .OrderBy(x => x)
                .First();


            Console.WriteLine(result);
        }

        public PriorityQueue<(int, int), long> Clone(PriorityQueue<(int, int), long> original)
        {
            var newQ = new PriorityQueue<(int, int), long>();
            foreach (var x in original.UnorderedItems)
                newQ.Enqueue(x.Element, x.Priority);

            return newQ;
        }

        public long StepsToEnd((int, int) start,
            PriorityQueue<(int, int), long> vertices,
            Dictionary<(int, int), long> distances,
            Dictionary<(int, int), (int, int)?> previous,
            Dictionary<(int, int), bool> inQ)
        {
            distances[start] = 0;
            previous.Remove(start);

            inQ[start] = true;

            while (vertices.Count != 0)
            {
                (int, int) u = vertices.Dequeue();
                inQ[u] = false;

                foreach (var v in Neighbours(u))
                {
                    long alt = distances[u] + 1;
                    if (alt < distances[v])
                    {
                        distances[v] = alt;
                        previous[v] = u;
                        if (!inQ[v])
                        {
                            vertices.Enqueue(v, alt);
                            inQ[v] = true;
                        }
                    }
                }
            }

            return distances[end];
        }

        public bool CanMove(char current, char next)
        {
            if (current == 'S')
                current = 'a';
            if (next == 'S')
                next = 'a';
            if (next == 'E')
                next = 'z';
            if (current == 'E')
                current = 'z';

            return next <= current + 1;
        }

        private List<(int, int)> Neighbours((int, int) v)
        {
            List<(int, int)> neighbours = new List<(int, int)>();

            if (v.Item1 > 0 && CanMove(heightMap[v.Item1, v.Item2], heightMap[v.Item1 - 1, v.Item2]))
                neighbours.Add((v.Item1 - 1, v.Item2));

            if (v.Item1 < heightMap.GetLength(0) - 1 && CanMove(heightMap[v.Item1, v.Item2], heightMap[v.Item1 + 1, v.Item2]))
                neighbours.Add((v.Item1 + 1, v.Item2));

            if (v.Item2 > 0 && CanMove(heightMap[v.Item1, v.Item2], heightMap[v.Item1, v.Item2 - 1]))
                neighbours.Add((v.Item1, v.Item2 - 1));

            if (v.Item2  < heightMap.GetLength(1) - 1 && CanMove(heightMap[v.Item1, v.Item2], heightMap[v.Item1, v.Item2 + 1]))
                neighbours.Add((v.Item1, v.Item2 + 1));

            return neighbours;
        }
    }
}

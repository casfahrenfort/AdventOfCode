using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AoC2022
{
    public class Day14 : Day
    {
        char[,] map;
        int maxY;
        int xOffset = 100000;

        public Day14()
            : base(14, false)
        { }

        public override void Solve()
        {
            List<List<(int, int)>> rockLines = new List<List<(int, int)>>();

            foreach(string line in input)
            {
                string[] split = line.Split(" -> ");
                rockLines.Add(split.Select(x =>
                {
                    var split = x.Split(',');
                    return (int.Parse(split[0]), int.Parse(split[1]));
                }).ToList());
            }

            var minX = rockLines.Select(x => x.Min(y => y.Item1)).Min();
            var minY = rockLines.Select(x => x.Min(y => y.Item2)).Min();

            var maxX = rockLines.Select(x => x.Max(y => y.Item1)).Max();
            maxY = rockLines.Select(x => x.Max(y => y.Item2)).Max() + 2;

            map = new char[xOffset * 2, maxY + 1];
            map[500, 0] = '+';

            foreach(var lines in rockLines)
            {
                for(int i = 0; i < lines.Count - 1; i++)
                {
                    // y equal
                    if (lines[i].Item2 == lines[i + 1].Item2)
                    {
                        int startX = Math.Min(lines[i].Item1, lines[i + 1].Item1) + xOffset;
                        int length = Math.Abs(lines[i].Item1 - lines[i + 1].Item1);
                        for (int x = startX; x <= startX + length; x++)
                            map[x, lines[i].Item2] = '#';
                    }
                    // x equal
                    else if (lines[i].Item1 == lines[i + 1].Item1)
                    {
                        int startY = Math.Min(lines[i].Item2, lines[i + 1].Item2);
                        int length = Math.Abs(lines[i].Item2 - lines[i + 1].Item2);
                        for (int y = startY; y <= startY + length; y++)
                            map[lines[i].Item1 + xOffset, y] = '#';
                    }
                }
            }

            for (int x = 0; x < map.GetLength(0); x++)
                map[x, maxY] = '#';

            DrawMap();

            for (int i = 0; i < 10000000; i++)
            {
                try
                {
                    DropSand();
                }
                catch (Exception e)
                {
                    Console.WriteLine(i);
                    break;
                }


                //DrawMap();
            }

            DrawMap();
        }

        public void DropSand()
        {
            (int,int) sand = (500 + xOffset, 0);

            while (true)
            {
                if (map[500 + xOffset, 0] == 'o')
                    throw new Exception();

                (int, int) nextSand = (sand.Item1, sand.Item2 + 1);

                if (map[nextSand.Item1, nextSand.Item2] == '\0')
                {
                    sand = nextSand;
                    continue;
                }

                if (map[nextSand.Item1, nextSand.Item2] == 'o'
                    || map[nextSand.Item1, nextSand.Item2] == '#')
                {
                    if (map[nextSand.Item1 - 1, nextSand.Item2] == '\0')
                    {
                        sand = (nextSand.Item1 - 1, nextSand.Item2);
                        continue;
                    }

                    if (map[nextSand.Item1 + 1, nextSand.Item2] == '\0')
                    {
                        sand = (nextSand.Item1 + 1, nextSand.Item2);
                        continue;
                    }

                    map[sand.Item1, sand.Item2] = 'o';
                    break;
                }
            }
        }

        public void DrawMap()
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 490 + xOffset; x < 510; x++)
                {
                    if (map[x,y] == '\0')
                        Console.Write('.');
                    else
                        Console.Write(map[x, y]);
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

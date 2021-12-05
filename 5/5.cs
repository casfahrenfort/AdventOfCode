using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AoC2021
{
    public class Day5 : Day
    {
        public Day5()
            : base(5, false)
        { }

        public override void Solve()
        {
            List<Line> lines = new List<Line>();

            for (int i = 0; i < input.Count; i ++)
            {
                string[] split = input[i].Split(',');
                string[] split2 = split[1].Split(" -> ");
                lines.Add(new Line(int.Parse(split[0]), int.Parse(split2[0]), int.Parse(split2[1]), int.Parse(split[2])));
            }

            int[,] coveredCoordinates = new int[1000, 1000];
            int overlapping = 0;

            foreach(Line line in lines)
            {
                foreach (Vector2 p in line.CoveredPoints())
                {
                    coveredCoordinates[(int)p.X, (int)p.Y] += 1;
                    if (coveredCoordinates[(int)p.X, (int)p.Y] == 2)
                    {
                        overlapping++;
                    }
                }
                //PrintCoordinates(coveredCoordinates);
            }

            Console.WriteLine(overlapping);
        }

        private void PrintCoordinates(int[,] coordinates)
        {
            for(int i = 0; i < coordinates.GetLength(0); i++)
            {
                for(int j = 0; j < coordinates.GetLength(1); j++)
                {
                    Console.Write(coordinates[i, j] == 0 ? "." : coordinates[i, j].ToString());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public class Line
    {
        Vector2 p1, p2;

        public Line(int x1, int y1, int x2, int y2)
        {
            p1 = new Vector2(x1, y1);
            p2 = new Vector2(x2, y2);
        }

        public Vector2[] CoveredPoints()
        {
            if (IsVertical())
            {
                int d = (int)(p2 - p1).Length() + 1;
                Vector2[] points = new Vector2[d];

                for (int i = 0; i < d; i++)
                {
                    int x = p2.X < p1.X ? (int)p1.X - i : (int)p1.X + i;
                    points[i] = new Vector2(x, p1.Y);
                }
                return points;
            }

            if (IsHorizontal())
            {
                int d = (int)(p2 - p1).Length() + 1;
                Vector2[] points = new Vector2[d];

                for (int i = 0; i < d; i++)
                {
                    int y = p2.Y < p1.Y ? (int)p1.Y - i : (int)p1.Y + i;
                    points[i] = new Vector2(p1.X, y);
                }
                return points;
            }

            List<Vector2> diagonal = new List<Vector2>() { p1 };

            int j = 1;
            while (diagonal.Last() != p2)
            {
                int x = p2.X < p1.X ? (int)p1.X - j : (int)p1.X + j;
                int y = p2.Y < p1.Y ? (int)p1.Y - j : (int)p1.Y + j;
                diagonal.Add(new Vector2(x, y));
                j++;
            }

            return diagonal.ToArray();
        }

        public bool IsHorizontalOrVertical()
        {
            return IsHorizontal() || IsVertical();
        }

        private bool IsHorizontal()
        {
            return p1.X == p2.X;
        }

        private bool IsVertical()
        {
            return p1.Y == p2.Y;
        }
    }
}

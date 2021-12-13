using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day13 : Day
    {
        private bool[,] dots;

        public Day13()
            : base(13, false)
        { }

        public override void Solve()
        {
            int largestX = 0, largestY = 0;
            int splitIndex = input.FindIndex(s => s == "");

            foreach (string line in input.Take(splitIndex))
            {
                string[] split = line.Split(',');
                int x = int.Parse(split[0]);
                int y = int.Parse(split[1]);
                largestX = x > largestX ? x : largestX;
                largestY = y > largestY ? y : largestY;
            }

            dots = new bool[largestX + 1, largestY + 1];

            foreach (string line in input.Take(splitIndex))
            {
                string[] split = line.Split(',');
                int x = int.Parse(split[0]);
                int y = int.Parse(split[1]);
                dots[x, y] = true;
            }

            List<FoldInstruction> foldInstructions = new List<FoldInstruction>();
            foreach (string line in input.Skip(splitIndex + 1))
            {
                string[] split = line.Split(' ');
                string[] split2 = split[2].Split('=');

                foldInstructions.Add(new FoldInstruction(split2[0] == "x", int.Parse(split2[1])));
            }

            foreach(var f in foldInstructions)
            {
                dots = f.Fold(dots);
            }
            Print(dots);

            var result = CountDots(dots);
        }

        public int CountDots(bool[,] dots)
        {
            int result = 0;

            for (int y = 0; y < dots.GetLength(1); y++)
                for (int x = 0; x < dots.GetLength(0); x++)
                    if (dots[x, y])
                        result++;

            return result;
        }

        public void Print(bool[,] dots)
        {
            for (int y = 0; y < dots.GetLength(1); y++)
            {
                for (int x = 0; x < dots.GetLength(0); x++)
                {
                    if (dots[x, y])
                        Console.Write('#');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public class FoldInstruction
        {
            private bool foldX;
            private int foldLine;

            public FoldInstruction(bool foldX, int foldLine)
            {
                this.foldLine = foldLine;
                this.foldX = foldX;
            }

            public bool[,] Fold(bool[,] dots)
            {
                bool[,] result;

                if (foldX)
                {
                    result = Copy(dots, foldLine, dots.GetLength(1));

                    for(int x = foldLine + 1; x < dots.GetLength(0); x++)
                        for(int y = 0; y < dots.GetLength(1); y++)
                        {
                            int newX = foldLine - (x - foldLine);
                            if (dots[x, y])
                                result[newX, y] = true;
                        }

                    return result;
                }

                result = Copy(dots, dots.GetLength(0), foldLine);

                for (int y = foldLine + 1; y < dots.GetLength(1); y++)
                    for (int x = 0; x < dots.GetLength(0); x++)
                    {
                        int newY = foldLine - (y - foldLine);
                        if (dots[x, y])
                            result[x, newY] = true;
                    }

                return result;
            }

            public bool[,] Copy(bool[,] original, int newXLength, int newYLength)
            {
                bool[,] result = new bool[newXLength, newYLength];

                for (int x = 0; x < result.GetLength(0); x++)
                    for (int y = 0; y < result.GetLength(1); y++)
                        result[x, y] = original[x, y];

                return result;
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace AoC2022
{
    public class Day9 : Day
    {
        (int, int) realHead = (0, 0);
        List<(int, int)> tails = new List<(int, int)>()
        {
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
        };
        Dictionary<(int, int), int> tailPositions = new Dictionary<(int, int), int>();

        public Day9()
            : base(9, false)
        { }

        public override void Solve()
        {
            tailPositions.InitializeOrIncrement(tails[8]);

            for (int i = 0; i < input.Count; i++)
            {
                string[] split = input[i].Split(' ');

                for (int j = 0; j < int.Parse(split[1]); j++)
                {
                    if (split[0] == "R") MoveRight();
                    else if (split[0] == "U") MoveUp();
                    else if (split[0] == "L") MoveLeft();
                    else if (split[0] == "D") MoveDown();

                    MoveTails();

                    tailPositions.InitializeOrIncrement(tails[8]);
                }
            }

            Console.WriteLine(tailPositions.Count);
        }

        public void MoveLeft()
        {
            realHead = (realHead.Item1 - 1, realHead.Item2);
        }

        public void MoveRight()
        {
            realHead = (realHead.Item1 + 1, realHead.Item2);
        }

        public void MoveUp()
        {
            realHead = (realHead.Item1, realHead.Item2 - 1);
        }

        public void MoveDown()
        {
            realHead = (realHead.Item1, realHead.Item2 + 1);
        }

        public void MoveTails()
        {
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day5 : Day
    {
        private readonly List<Stack> stacks = new List<Stack>()
        {
            new Stack(),
            new Stack(),
            new Stack(),
            new Stack(),
            new Stack(),
            new Stack(),
            new Stack(),
            new Stack(),
            new Stack(),
        };

        public Day5()
            : base(5, false)
        { }

        public override void Solve()
        {
            ReadInput();

            for (int i = 10; i < input.Count; i++)
            {
                string[] line = input[i].Split(' ');
                Move(int.Parse(line[1]), int.Parse(line[3]), int.Parse(line[5]));
            }

            string answer = "";
            for (int i = 0; i < stacks.Count; i++)
            {
                answer += stacks[i].Pop();
            }

            Console.WriteLine(answer);
        }

        private void Move(int amount, int from, int to)
        {
            var movedCrates = new List<object>();
            for (int i = 0; i < amount; i++)
            {
                movedCrates.Add(stacks[from - 1].Pop());
            }

            for (int i = movedCrates.Count - 1; i >= 0; i--)
            {
                stacks[to - 1].Push(movedCrates[i]);
            }
        }

        private void ReadInput()
        {
            for (int i = 7; i >= 0; i--)
            {
                string line = input[i];
                for (int j = 1; j <= 33; j += 4)
                {
                    if (line[j] != ' ')
                    {
                        stacks[(j - 1) / 4].Push(line[j]);
                    }
                }
            }
        }
    }
}

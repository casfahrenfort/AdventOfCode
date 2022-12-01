using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day1 : Day
    {
        public Day1()
            : base(1, false)
        { }

        public override void Solve()
        {
            int currentCalories = 0;
            List<int> calories = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "")
                {
                    calories.Add(currentCalories);
                    currentCalories = 0;
                    continue;
                }

                currentCalories += int.Parse(input[i]);
            }

            calories.Add(currentCalories);

            var top3 = calories.OrderByDescending(x => x).Take(3).Sum();
            Console.WriteLine(top3);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day7 : Day
    {
        public Day7()
            : base(7, false)
        { }

        public override void Solve()
        {
            List<int> crabs = input[0].Split(',').Select(x => int.Parse(x)).ToList();

            Dictionary<int, int> positions = new Dictionary<int, int>();
            int maxPosition = int.MinValue;
            
            foreach(int crab in crabs)
            {
                if (!positions.ContainsKey(crab))
                    positions.Add(crab, 1);
                else
                    positions[crab]++;

                if (crab > maxPosition)
                    maxPosition = crab;
            }

            int lowestCost = int.MaxValue;
            for (int position = 0; position < maxPosition; position++)
            {
                int cost = 0;
                foreach(int otherPosition in positions.Keys)
                {
                    if (otherPosition == position)
                        continue;
                    int distance = Math.Abs(position - otherPosition);
                    int fuelCost = Enumerable.Range(1, distance).Sum();
                    int occurrences = positions[otherPosition];
                    cost += fuelCost * occurrences;
                }

                if (cost < lowestCost)
                    lowestCost = cost;
            }

            Console.WriteLine(lowestCost);
        }
    }
}

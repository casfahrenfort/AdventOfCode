using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day7 : Day
    {
        private readonly Dictionary<int, int> positions = new Dictionary<int, int>();
        private readonly Dictionary<int, int> costs = new Dictionary<int, int>();
        private int lowestCost = int.MaxValue;

        public Day7()
            : base(7, false)
        { }

        public override void Solve()
        {
            List<int> crabs = input[0].Split(',').Select(int.Parse).ToList();

            foreach(int crab in crabs)
            {
                if (!positions.ContainsKey(crab))
                    positions.Add(crab, 1);
                else
                    positions[crab]++;
            }

            int startPosition = Median(crabs);
            LowestCost(startPosition);
        }

        private int LowestCost(int position)
        {
            int costBefore = Cost(position - 1);
            int costAfter = Cost(position + 1);
            int minCost = Math.Min(costBefore, costAfter);
            lowestCost =  minCost < lowestCost ? minCost : lowestCost;

            if (costBefore > lowestCost && costAfter > lowestCost)
                return Cost(position);

            return Math.Min(LowestCost(position - 1), LowestCost(position + 1));
        }

        private int Cost(int position)
        {
            if (costs.ContainsKey(position))
                return costs[position];

            int cost = 0;
            foreach (int otherPosition in positions.Keys)
            {
                if (otherPosition == position)
                    continue;

                int distance = Math.Abs(position - otherPosition);
                int fuelCost = distance * (distance + 1) / 2;
                int occurrences = positions[otherPosition];
                cost += fuelCost * occurrences;
            }

            costs.Add(position, cost);
            return cost;
        }

        private int Median(IEnumerable<int> numbers)
        {
            var sorted = numbers.OrderBy(n => n).ToArray();

            if (sorted.Length % 2 == 0)
                return (sorted[(sorted.Length / 2) - 1] + sorted[sorted.Length / 2]) / 2;

            return sorted[sorted.Length / 2];
        }
    }
}

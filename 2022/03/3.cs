using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day3 : Day
    {
        public Day3()
            : base(3, false)
        { }

        public override void Solve()
        {
            int answer = input.Sublists(3).Select(x => FindPriority(x.ToList())).Sum();

            Console.WriteLine(answer);
        }

        private int FindPriority(List<string> rucksacks)
        {
            var commonChars = FindCommonChar(rucksacks[0], rucksacks[1]);
            foreach (var c in commonChars)
            {
                var nextC = FindCommonChar(c.First().ToString(), rucksacks[2]);

                if (nextC.Count() != 0)
                {
                    int charVal = nextC.First().First();
                    if (charVal >= 97)
                    {
                        return charVal - 96;
                    }

                    return charVal - 38;
                }
            }

            throw new NotImplementedException();
        }

        private IEnumerable<IEnumerable<char>> FindCommonChar(string left, string right)
        {
            return left.GroupBy(c => c)
                .Join(
                    right.GroupBy(c => c),
                        g => g.Key,
                        g => g.Key,
                        (lg, rg) => lg
                            .Zip(rg, (l, r) => l));

        }
    }
}

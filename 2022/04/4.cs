using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day4 : Day
    {
        public Day4()
            : base(4, false)
        { }

        public override void Solve()
        {
            int answer = input.Where(x => FullyContains(x)).Count();

            Console.WriteLine(answer);
        }

        public bool FullyContains(string pair)
        {
            string[] elves = pair.Split(',');
            string[] elf1 = elves[0].Split('-');
            string[] elf2 = elves[1].Split('-');

            int start1 = int.Parse(elf1[0]);
            int end1 = int.Parse(elf1[1]);
            int start2 = int.Parse(elf2[0]);
            int end2 = int.Parse(elf2[1]);

            if (start2 >= start1 && start2 <= end1
                || start1 >= start2 && start1 <= end2
                || end2 >= start1 && end2 <= end1
                || end1 >= start2 && end1 <= end2)
            {
                return true;
            }

            return false;
        }
    }
}

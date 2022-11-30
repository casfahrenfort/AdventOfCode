using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day6 : Day
    {
        public Day6()
            : base(6, false)
        { }

        public override void Solve()
        {
            List<long> parsedInput = input[0]
                .Split(',')
                .Select(x => long.Parse(x))
                .ToList();

            var model = new LanternfishModel(parsedInput);

            int days = 256;
            while (days > 0)
            {
                model.AdvanceDay();
                days--;
            }

            Console.WriteLine(model.LanternFishCount());
        }
    }

    public class LanternfishModel
    {
        Dictionary<long, long> timers;

        public LanternfishModel(List<long> initialState)
        {
            timers = InitTimers();

            foreach (long i in initialState)
                timers[i] += 1;
        }

        public void AdvanceDay()
        {
            var newTimers = InitTimers();

            foreach (long timer in timers.Keys)
            {
                if (timer == 0)
                {
                    newTimers[8] += timers[timer];
                    newTimers[6] += timers[timer];
                    continue;
                }

                newTimers[timer - 1] += timers[timer];
            }

            timers = newTimers;
        }

        public long LanternFishCount()
        {
            return timers.Sum(x => x.Value);
        }

        public Dictionary<long, long> InitTimers()
        {
            return new Dictionary<long, long>()
            {
                [0] = 0,
                [1] = 0,
                [2] = 0,
                [3] = 0,
                [4] = 0,
                [5] = 0,
                [6] = 0,
                [7] = 0,
                [8] = 0,
            };
        }
    }
}

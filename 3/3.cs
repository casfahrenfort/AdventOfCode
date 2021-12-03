using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day3 : Day
    {
        public Day3()
            : base(3, false)
        { }

        public override void Solve()
        {
            List<string> oxygenInput = new List<string>(input);
            List<string> co2Input = new List<string>(input);

            Console.WriteLine(GetRating(oxygenInput, 0, MostCommon) * GetRating(co2Input, 0, LeastCommon));
        }

        private int GetRating(List<string> reports, int iterations, Func<string, char> commonBit)
        {
            if (reports.Count == 1)
            {
                return Convert.ToInt32(reports[0], 2);
            }

            string bitPos = String.Concat(input.Select(s => s[iterations]));
            reports = reports.Where(s => s[iterations] == commonBit(bitPos)).ToList();
            return GetRating(reports, ++iterations, commonBit);
        }

        private char MostCommon(string input)
        {
            int zeroes = input.Count(c => c == '0');
            int ones = input.Count(c => c == '1');

            if (zeroes == ones)
                return '1';

            return ones > zeroes ? '1' : '0';
        }

        private char LeastCommon(string input)
        {
            int zeroes = input.Count(c => c == '0');
            int ones = input.Count(c => c == '1');

            if (zeroes == ones)
                return '0';

            return ones < zeroes ? '1' : '0';
        }
    }
}

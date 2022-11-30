using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2021
{
    public class Day8 : Day
    {
        public Day8()
            : base(8, false)
        { }

        public override void Solve()
        {
            int totalCount = 0;

            foreach(string line in input)
            {
                totalCount += new Entry(line).OutputValue();
            }

            Console.WriteLine(totalCount);
        }
    }

    public class Entry
    {
        private string[] patterns;
        private string[] outputValues;
        private char[] wireSlots;

        public Entry(string input)
        {
            var split = input.Split(" | ");
            patterns = split[0].Split(' ');
            outputValues = split[1].Split(' ');
        }

        public int OutputValue()
        {
            DetermineWireSlots();

            return int.Parse(new string(outputValues.Select(PatternToDigit).ToArray()));
        }

        public int Count1478()
        {
            return outputValues.Where(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7).Count();
        }

        private void DetermineWireSlots()
        {
            string pattern1 = patterns.First(x => x.Length == 2);
            string pattern4 = patterns.First(x => x.Length == 4);
            string pattern7 = patterns.First(x => x.Length == 3);
            string pattern8 = patterns.First(x => x.Length == 7);

            char topSlot = RemoveString(pattern7, pattern1)[0];

            string bottomleftBottom = RemoveString(pattern8, pattern4 + topSlot);

            string[] pattern3or5or2 = patterns.Where(x => x.Length == 5).ToArray();
            string[] pattern3or5 = pattern3or5or2.Where(x => RemoveAllButString(x, bottomleftBottom).Length == 1).ToArray();

            char bottomSlot = RemoveAllButString(pattern3or5[0], bottomleftBottom)[0];
            char bottomLeftSlot = bottomleftBottom.First(c => c != bottomSlot);

            string[] pattern069 = patterns.Where(x => x.Length == 6).ToArray();
            string pattern0 = pattern069.First(x => RemoveString(x, pattern1 + bottomSlot + bottomLeftSlot + topSlot).Length == 1);
            char topLeftSlot = RemoveString(pattern0, pattern1 + bottomSlot + bottomLeftSlot + topSlot)[0];

            char middleSlot = RemoveString(pattern3or5or2[0], pattern0)[0];

            string pattern2 = pattern3or5or2.First(x => x.Contains(bottomLeftSlot));

            char topRightSlot = RemoveString(pattern2, "" + topSlot + middleSlot + bottomLeftSlot + bottomSlot)[0];
            char bottomRightSlot = RemoveString("abcdefg", "" + topSlot + middleSlot + bottomLeftSlot + bottomSlot + topLeftSlot + topRightSlot)[0];

            wireSlots = new char[] { topSlot, topLeftSlot, topRightSlot, middleSlot, bottomLeftSlot, bottomRightSlot, bottomSlot };
        }

        private char PatternToDigit(string pattern)
        {
            switch (pattern.Length)
            {
                case 2: return '1';
                case 3: return '7';
                case 4: return '4';
                case 7: return '8';
                case 5:
                    {
                        if (pattern.Contains(wireSlots[4]))
                            return '2';
                        if (pattern.Contains(wireSlots[1]))
                            return '5';
                        return '3';
                    }
                case 6:
                    {
                        if (!pattern.Contains(wireSlots[3]))
                            return '0';
                        if (pattern.Contains(wireSlots[2]))
                            return '9';
                        return '6';
                    }
                default: throw new Exception();
            }
        }

        private string RemoveString(string input, string remove)
        {
            foreach (var c in Regex.Split(remove, string.Empty).Where(x => x!= ""))
            {
                input = input.Replace(c, string.Empty);
            }
            return input;
        }

        private string RemoveAllButString(string input, string remove)
        {
            foreach (var c in Regex.Split(input, string.Empty).Where(x => x != ""))
            {
                if (!remove.Contains(c))
                    input = input.Replace(c, string.Empty);
            }
            return input;
        }
    }
}

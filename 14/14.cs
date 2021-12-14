using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day14 : Day
    {
        private Dictionary<(char, char), char> rules;
        private Dictionary<(char, char), long> pairs;

        public Day14()
            : base(14, false)
        { }

        public override void Solve()
        {
            string template = input[0];

            rules = new Dictionary<(char, char), char>();
            pairs = new Dictionary<(char, char), long>();
            var occurences = new Dictionary<char, long>();

            for (int i = 2; i < input.Count; i++)
            {
                string[] split = input[i].Split(" -> ");
                rules.Add((split[0][0], split[0][1]), split[1][0]);
            }

            foreach (var chunk in WholeChunks(template, 2))
            {
                var pair = (chunk[0], chunk[1]);
                if (!pairs.ContainsKey(pair))
                    pairs.Add(pair, 1);
                else
                    pairs[pair]++;
            }

            int step = 1;
            while (step <= 40)
            {
                Dictionary<(char, char), long> newPairs = new Dictionary<(char, char), long>();
                foreach (var item in pairs)
                {
                    var pair = item.Key;
                    var number = item.Value;

                    var newChar = rules[(pair.Item1, pair.Item2)];
                    var firstNewPair = (pair.Item1, newChar);
                    var secondNewPair = (newChar, pair.Item2);

                    if (!newPairs.ContainsKey(firstNewPair))
                        newPairs.Add(firstNewPair, number);
                    else
                        newPairs[firstNewPair] += number;


                    if (!newPairs.ContainsKey(secondNewPair))
                        newPairs.Add(secondNewPair, number);
                    else
                        newPairs[secondNewPair] += number;
                }

                pairs = newPairs;
                step++;
            }


            foreach (var item in pairs)
            {
                var pair = item.Key;
                var number = item.Value;
                if (!occurences.ContainsKey(pair.Item2))
                    occurences.Add(pair.Item2, (long)number);
                else
                    occurences[pair.Item2] += (long)number;
            }
            occurences[template[0]]++;
            long mostCommon = occurences.Where(x => x.Value == occurences.Values.Max()).First().Value;
            long leastCommon = occurences.Where(x => x.Value == occurences.Values.Min()).First().Value;

            long answer = mostCommon - leastCommon;
        }

        /* private string Polymer(string template, int depth)
         {
             if (depth == 10)
                 return template;

             depth++;

             var chunks = WholeChunks(c => Polymer(c, depth), 2);
             return string.Join("", polymers);
         }*/

        private IEnumerable<string> WholeChunks(string str, int chunkSize)
        {
            for (int i = 0; i < str.Length - 1; i += 1)
                yield return str.Substring(i, chunkSize);
        }
    }
}

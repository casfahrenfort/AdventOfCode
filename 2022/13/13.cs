using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AoC2022
{
    public class Day13 : Day
    {
        public Day13()
            : base(13, false)
        { }

        public override void Solve()
        {
           var comparer = new PacketComparer();

            var answerP1 = input
                 .Sublists(3)
                 .Select(lines => (JArray.Parse(lines.First()), JArray.Parse(lines.ElementAt(1))))
                 .Select((item, index) => new { item, index = index + 1 })
                 .Where(x => comparer.Compare(x.item.Item1, x.item.Item2) == -1)
                 .Sum(x => x.index);

            var answerP2 = input
                .Where(x => x != "")
                .Select(x => JArray.Parse(x))
                .Append(JArray.Parse("[[2]]"))
                .Append(JArray.Parse("[[6]]"))
                .OrderBy(x => x, comparer)
                .Select((item, index) => new { item, index = index + 1 })
                .Where(x => IsDividerPacket(x.item))
                .Aggregate(1, (x, y) => x * y.index);
        }

        public bool IsDividerPacket(JArray packet)
        {
            if (packet.Children().Count() != 1)
                return false;

            if (packet.Children().First().Children().Count() != 1)
                return false;

            var dividerNumToken = packet.Children().First().Children().First();

            return dividerNumToken.Type == JTokenType.Integer
                && ((long)((JValue)dividerNumToken).Value == 2 || (long)((JValue)dividerNumToken).Value == 6);
        }
    }

    public class PacketComparer : IComparer<JToken>
    {
        public int Compare(JToken value1, JToken value2)
        {
            if (value1.Type == JTokenType.Integer && value2.Type == JTokenType.Integer)
            {
                if ((int)(JValue)value1 < (int)(JValue)value2)
                    return -1;
                if ((int)(JValue)value1 > (int)(JValue)value2)
                    return 1;
                return 0;
            }

            if (value1.Type == JTokenType.Array && value2.Type == JTokenType.Array)
            {
                var a1 = (JArray)value1;
                var a2 = (JArray)value2;
                for (int i = 0; i < Math.Max(a1.Count, a2.Count); i++)
                {
                    JToken a1val, a2val;
                    try
                    {
                        a1val = a1[i];
                    }
                    catch
                    {
                        return -1;
                    }

                    try
                    {
                        a2val = a2[i];
                    }
                    catch
                    {
                        return 1;
                    }

                    var result = Compare(a1val, a2val);

                    if (result == 0)
                        continue;
                    return result;
                }
            }

            if (value1.Type == JTokenType.Integer)
            {
                value1 = new JArray(value1);
                return Compare(value1, value2);
            }
            if (value2.Type == JTokenType.Integer)
            {
                value2 = new JArray(value2);
                return Compare(value1, value2);
            }

            return 0;
        }
    }
}

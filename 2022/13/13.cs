using Newtonsoft.Json.Linq;
using System;
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
            var answerP1 = input.Sublists(3)
                .Select(lines => (JArray.Parse(lines.First()), JArray.Parse(lines.ElementAt(1))))
                .Select((item, index) => new { order = InOrder(item), index = index + 1 })
                .Where(x => x.order)
                .Select(x => x.index)
                .Sum();
        }

        public bool InOrder((JArray, JArray) packetPair)
        {
            var left = packetPair.Item1;
            var right = packetPair.Item2;

            var result = Compare(left, right);

            if (result == null)
                throw new NotImplementedException();

            return result!.Value;
        }

        public bool? Compare(JToken value1, JToken value2)
        {
            if (value1.Type == JTokenType.Integer && value2.Type == JTokenType.Integer)
            {
                if ((int)(JValue)value1 < (int)(JValue)value2) 
                    return true;
                if ((int)(JValue)value1 > (int)(JValue)value2) 
                    return false;
                return null;
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
                        return true;
                    }

                    try
                    {
                        a2val = a2[i];
                    }
                    catch
                    {
                        return false;
                    }

                    var result = Compare(a1val, a2val);

                    if (result == null)
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

            return null;
        }
    }
}

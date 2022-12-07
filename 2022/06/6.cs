using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day6 : Day
    {
        public Day6()
            : base(6, false)
        { }

        public override void Solve()
        {
            int answer = FirstMarkerPosition(input[0]);

            Console.WriteLine(answer);
        }

        public int FirstMarkerPosition(string data)
        {
            char[] chars = data.Take(14).ToArray();

            if (IsMarker(chars))
                return 14;

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < chars.Length - 1; j++)
                    chars[j] = chars[j + 1];
                chars[13] = data[i];

                if (IsMarker(chars))
                    return i + 1;
            }

            throw new NotImplementedException();
        }

        public bool IsMarker(char[] chars)
        {
            return chars.GroupBy(c => c).All(x => x.Count() == 1);
        }
    }
}

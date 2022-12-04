using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day2 : Day
    {
        public Day2()
            : base(2, false)
        { }

        public override void Solve()
        {
            int answer = input.Select(x => StringToScore(x)).Sum();
            Console.WriteLine(answer);
        }

        private int StringToScore(string str)
        {
            switch (str[2])
            {
                case 'X': // Lose
                    {
                        switch (str[0])
                        {
                            case 'A': return 3;
                            case 'B': return 1;
                            case 'C': return 2;
                        }
                    } break;
                case 'Y': // Draw
                    {
                        switch (str[0])
                        {
                            case 'A': return 3 + 1;
                            case 'B': return 3 + 2;
                            case 'C': return 3 + 3;
                        }
                    } break;
                case 'Z':  // Win
                    {
                        switch (str[0])
                        {
                            case 'A': return 6 + 2;
                            case 'B': return 6 + 3;
                            case 'C': return 6 + 1;
                        }
                    }
                    break;
            }

            throw new NotImplementedException();
        }
    }
}

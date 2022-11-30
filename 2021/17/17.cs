using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day17 : Day
    {
        public static Probe highestProbe;

        public Day17()
            : base(17, false)
        { }

        public override void Solve()
        {
            var s = input[0].Substring(15, input[0].Length - 15).Replace(" y=", "").Split(',');
            var xs = s[0].Split("..");
            var ys = s[1].Split("..");

            int minX = int.Parse(xs[0]);
            var maxX = int.Parse(xs[1]);
            int minY = int.Parse(ys[0]);
            int maxY = int.Parse(ys[1]);

            int result = 0;

            for (int x = 0; x <= maxX; x++)
                for (int y = minY; y <= minY * -1; y++)
                {
                    var probe = new Probe(x, y);
                    for (int i = 0; i < 5000; i++)
                    {
                        if (probe.Step(minX, maxX, minY, maxY))
                        {
                            result++;
                            break;
                        };
                        if (probe.x >= maxX && probe.y <= maxY)
                            break;
                    }
                }
        }
    }

    public class Probe
    {
        public int x, y;
        public int highestY;
        private int xVelocity, yVelocity;

        public Probe(int xVelocity, int yVelocity)
        {
            this.xVelocity = xVelocity;
            this.yVelocity = yVelocity;
        }

        public bool Step(int minX, int maxX, int minY, int maxY)
        {
            x += xVelocity;
            y += yVelocity;

            if (y > highestY)
            {
                highestY = y;
            }

            if (xVelocity > 0)
                xVelocity -= 1;
            else if (xVelocity < 0)
                xVelocity += 1;

            yVelocity -= 1;

            return x >= minX && x <= maxX && y >= minY && y <= maxY;
        }
    }
}

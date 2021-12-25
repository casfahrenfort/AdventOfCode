using NGenerics.DataStructures.Mathematical;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AoC2021
{
    public class Day22 : Day
    {
        Dictionary<((int, int), (int, int), (int, int)), long> cubes;

        public Day22()
            : base(22, false)
        { }

        public override void Solve()
        {
            cubes = new Dictionary<((int, int), (int, int), (int, int)), long>();

            for(int i = 0; i < input.Count; i++)
            {
                string[] split = input[i].Split(' ');
                long value = split[0] == "off" ? -1 : 1;

                string[] split2 = split[1].Replace("x=", "").Replace("y=", "").Replace("z=", "").Split(',');
                var boids = split2.Select(x =>
                {
                    var spl = x.Split("..");
                    return (int.Parse(spl[0]), int.Parse(spl[1]));
                }).ToList();

                var cuboid = (boids[0], boids[1], boids[2]);

                if (i == 0)
                {
                    cubes.Add(cuboid, value);
                    continue;
                }
                SetCuboid(cuboid, value);
            }

            var result = cubes.Sum(a => (a.Key.Item1.Item2 - a.Key.Item1.Item1 + (long)1) * (a.Key.Item2.Item2 - a.Key.Item2.Item1 + 1) * (a.Key.Item3.Item2 - a.Key.Item3.Item1 + 1) * a.Value);
        }

        public void SetCuboid(((int minX,int maxX), (int minY,int maxY), (int minZ,int maxZ)) cuboid, long value)
        {
            /*if (cuboid.Item1.maxX < -50 || cuboid.Item2.maxY < -50 || cuboid.Item3.maxZ < -50)
                return;
            if (cuboid.Item1.minX > 50 || cuboid.Item2.minY > 50 || cuboid.Item3.minZ > 50)
                return;*/

            var newCuboids = new Dictionary<((int, int), (int, int), (int, int)), long>();

            foreach(var c in cubes)
            {
                ((int cminX, int cmaxX), (int cminY, int cmaxY), (int cminZ, int cmaxZ)) = c.Key;
                var cubeValue = c.Value;

                int overlapMinX = Math.Max(cuboid.Item1.minX, cminX);
                int overlapMaxX = Math.Min(cuboid.Item1.maxX, cmaxX);
                int overlapMinY = Math.Max(cuboid.Item2.minY, cminY);
                int overlapMaxY = Math.Min(cuboid.Item2.maxY, cmaxY);
                int overlapMinZ = Math.Max(cuboid.Item3.minZ, cminZ);
                int overlapMaxZ = Math.Min(cuboid.Item3.maxZ, cmaxZ);

                var overlapCuboid = ((overlapMinX, overlapMaxX), (overlapMinY, overlapMaxY), (overlapMinZ, overlapMaxZ));

                if (overlapMinX <= overlapMaxX && overlapMinY <= overlapMaxY && overlapMinZ <= overlapMaxZ)
                {
                    newCuboids[overlapCuboid] = newCuboids.GetValueOrDefault(overlapCuboid, 0) - cubeValue;
                }
            }

            if (value == 1)
            {
                newCuboids[cuboid] = newCuboids.GetValueOrDefault(cuboid, 0) + value;
            }

            foreach (var nc in newCuboids)
            {
                cubes[nc.Key] = cubes.GetValueOrDefault(nc.Key, 0) + nc.Value;
            }
        }
    }
}

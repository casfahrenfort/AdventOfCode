using NGenerics.DataStructures.Mathematical;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AoC2021
{
    public class Day19 : Day
    {
        public static List<Matrix> rotMatrices = new List<Matrix>()
            {
                new Matrix(3, 3, new double[,] {{ 0, 0, 1}, { 0, 1, 0}, {-1, 0, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 0, 1}, { 0,-1, 0}, { 1, 0, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 0, 1}, { 1, 0, 0}, { 0, 1, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 0, 1}, {-1, 0, 0}, { 0,-1, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 0,-1}, { 0, 1, 0}, { 1, 0, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 0,-1}, { 0,-1, 0}, {-1, 0, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 0,-1}, { 1, 0, 0}, { 0,-1, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 0,-1}, {-1, 0, 0}, { 0, 1, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 1, 0}, { 0, 0, 1}, { 1, 0, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 1, 0}, { 0, 0,-1}, {-1, 0, 0}}),
                new Matrix(3, 3, new double[,] {{ 0, 1, 0}, { 1, 0, 0}, { 0, 0,-1}}),
                new Matrix(3, 3, new double[,] {{ 0, 1, 0}, {-1, 0, 0}, { 0, 0, 1}}),
                new Matrix(3, 3, new double[,] {{ 0,-1, 0}, { 0, 0, 1}, {-1, 0, 0}}),
                new Matrix(3, 3, new double[,] {{ 0,-1, 0}, { 0, 0,-1}, { 1, 0, 0}}),
                new Matrix(3, 3, new double[,] {{ 0,-1, 0}, { 1, 0, 0}, { 0, 0, 1}}),
                new Matrix(3, 3, new double[,] {{ 0,-1, 0}, {-1, 0, 0}, { 0, 0,-1}}),
                new Matrix(3, 3, new double[,] {{ 1, 0, 0}, { 0, 0, 1}, { 0,-1, 0}}),
                new Matrix(3, 3, new double[,] {{ 1, 0, 0}, { 0, 0,-1}, { 0, 1, 0}}),
                new Matrix(3, 3, new double[,] {{ 1, 0, 0}, { 0, 1, 0}, { 0, 0, 1}}),
                new Matrix(3, 3, new double[,] {{ 1, 0, 0}, { 0,-1, 0}, { 0, 0,-1}}),
                new Matrix(3, 3, new double[,] {{-1, 0, 0}, { 0, 0, 1}, { 0, 1, 0}}),
                new Matrix(3, 3, new double[,] {{-1, 0, 0}, { 0, 0,-1}, { 0,-1, 0}}),
                new Matrix(3, 3, new double[,] {{-1, 0, 0}, { 0, 1, 0}, { 0, 0,-1}}),
                new Matrix(3, 3, new double[,] {{-1, 0, 0}, { 0,-1, 0}, { 0, 0,-1}}),
            };

        public Dictionary<int, Vector> positions = new Dictionary<int, Vector>()
        {
            [0] = new Vector(0, 0, 0),
            [1] = new Vector(68, -1246, -43)
        };

        public Day19()
            : base(19, true)
        { }

        public override void Solve()
        {
            var scanners = new List<Scanner>();
            var scannerInput = input.TakeWhile(line => line != "").ToList();
            input = input.Skip(scannerInput.Count + 1).ToList();
            while (scannerInput.Count != 0)
            {
                int nr = int.Parse(scannerInput[0].Replace("--- scanner ", "").Replace(" ---", ""));
                var beacons = new List<Vector>();
                for (int i = 1; i < scannerInput.Count; i++)
                {
                    string[] split = scannerInput[i].Split(',');
                    beacons.Add(new Vector(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
                }
                scanners.Add(new Scanner(nr, beacons.ToImmutableHashSet()));
                scannerInput = input.TakeWhile(line => line != "").ToList();
                input = input.Skip(scannerInput.Count + 1).ToList();
            }

            /*var s0 = scanners[0].CreateOrientations(0);
            foreach (var s in s0)
            {
                foreach (var b in s.beacons)
                    Console.WriteLine($"{b.X},{b.Y},{b.Z}");
            }*/

            var only0 = new List<IEnumerable<Scanner>>() { scanners.Take(1).ToList() };
            var allScannerOrientations = only0.Concat(scanners.Skip(1).Select(s => s.CreateOrientations(s.id))).ToList();
            //.Concat(scanners.Skip(1).Select(s => s.CreateOrientations(s.id)).ToList());
            var resultBeacons = new HashSet<Vector>();
            /*for (int i = 0; i < allScannerOrientations.Count; i++)
            {
                for (int j = 0; j < allScannerOrientations.Count; j++)
                {
                    if (i == j) continue;

                    var subOverlapping = new List<Vector>();

                    foreach(Scanner source in allScannerOrientations[i])
                        foreach(Scanner target in allScannerOrientations[j])
                        {
                            var overlapping = OverlappingBeacons(source, target);
                            if (overlapping != null)
                            {
                                subOverlapping.AddRange(overlapping);
                            }
                        }
                }

            }*/
            var subOverlapping = new List<Vector>();
            foreach(var source in allScannerOrientations[1])
                foreach(var target in allScannerOrientations[4])
                {
                    var overlapping = OverlappingBeacons(source, target);
                    if (overlapping != null)
                    {
                        subOverlapping.AddRange(overlapping);
                    }
                }
        }

        public List<Vector> OverlappingBeacons(Scanner source, Scanner target)
        {
            foreach (Vector sbeacon in source.beacons)
                foreach (Vector tbeacon in target.beacons)
                {
                    var offset = sbeacon.Subtract(tbeacon);
                    var intersect = source.AbsoluteBeacons(positions[source.id]).Intersect(target.AbsoluteBeacons(offset));
                    if (intersect.Count() >= 12)
                    {
                        return intersect.ToList();
                    }
                }

            /*foreach (Vector sbeacon in source.beacons)
                foreach (Vector tbeacon in target.beacons)
                {
                    var offset = tbeacon.Subtract(sbeacon);
                    var intersect = target.beacons.Intersect(source.beacons.Select(x => x.Add(offset)));
                    if (intersect.Count() >= 12)
                    {
                        return intersect.ToList();
                    }
                }*/


            return null;
        }
    }

    public class Scanner
    {
        public int id;
        public ImmutableHashSet<Vector> beacons;

        public Scanner(int nr, ImmutableHashSet<Vector> beacons)
        {
            this.id = nr;
            this.beacons = beacons;
        }

        public IEnumerable<Vector> AbsoluteBeacons(Vector position)
            => beacons.Select(x => x.Add(position));

        public IEnumerable<Scanner> CreateOrientations(int id)
        {
            return beacons.SelectMany(b => b.EnumOrientations().Select((v, i) => (index: i, vector: v)))
                          .GroupBy(v => v.index, g => g.vector)
                          .Select(g => new Scanner(id, g.ToImmutableHashSet()));
        }

    }

    public struct Vector
    {
        public int X, Y, Z;
        public Vector(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public Vector Subtract(Vector other)
          => new Vector(X - other.X, Y - other.Y, Z - other.Z);

        public Vector Add(Vector other)
          => new Vector(X + other.X, Y + other.Y, Z + other.Z);

        public int ManhattanDistance(Vector other)
          => Math.Abs(other.X - X) + Math.Abs(other.Y - Y) + Math.Abs(other.Z - Z);

        public IEnumerable<Vector> EnumFacingDirections()
        {
            var current = this;
            for (var i = 0; i < 3; ++i)
            {
                yield return current;
                yield return new Vector(-current.X, -current.Y, current.Z);

                current = new Vector(current.Y, current.Z, current.X);
            }
        }

        public IEnumerable<Vector> EnumRotations()
        {
            var current = this;
            for (var i = 0; i < 4; ++i)
            {
                yield return current;
                current = new Vector(current.X, -current.Z, current.Y);
            }
        }

        public IEnumerable<Vector> EnumOrientations()
        {
            Matrix m1 = new Matrix(3, 3, new double[,] { { X, 0, 0 }, { 0, Y, 0 }, { 0, 0, Z } });

            var result = Day19.rotMatrices.Select(x => m1.Multiply(x)).ToList();
            return Day19.rotMatrices
                .Select(m1.Multiply)
                .Select(m =>
                {
                    int x = (int)m.GetRow(0).First(a => a != 0);
                    int y = (int)m.GetRow(1).First(a => a != 0);
                    int z = (int)m.GetRow(2).First(a => a != 0);
                    return new Vector(x, y, z);
                })
                .ToList();
        }

        //=> EnumFacingDirections().SelectMany(v => v.EnumRotations());
        /*{

            return new List<Vector>() 
            {
                new Vector(X, Y, Z),
                new Vector(X, -Z, Y),
                new Vector(X, -Y, -Z),
                new Vector(X, Z, -Y),
                new Vector(Y, Z, X),
                new Vector(Y, -X, -Z),
                new Vector(Y, -Z, -X),
                new Vector(Y, X, -Z),
                new Vector(Z, X, Y),
                new Vector(Z, -Y, X),
                new Vector(Z, -X, -Y),
                new Vector(Z, Y, -X),
                new Vector(-Z, -Y, -X),
                new Vector(-Z, X, -Y),
                new Vector(-Z, Y, X),
                new Vector(-Z, -X, Y),
                new Vector(-Y, -X, -Z),
                new Vector(-Y, Z, -X),
                new Vector(-Y, X, Z),
                new Vector(-Y, -Z, X),
                new Vector(-X, -Z, -Y),
                new Vector(-X, Y, -Z),
                new Vector(-X, Z, Y),
                new Vector(-X, -Y, Z)
            };
        }*/
    }
}

using System;
using System.Diagnostics;

namespace AoC2022
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            var day = new Day8();

            stopwatch.Start();

            day.Solve();

            stopwatch.Stop();

            Console.WriteLine($"Runtime: {stopwatch.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }
    }
}

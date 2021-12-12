﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();

            new Day13().Solve();

            stopwatch.Stop();

            Console.WriteLine($"Runtime: {stopwatch.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }
    }
}

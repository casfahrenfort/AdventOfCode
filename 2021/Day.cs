using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2021
{
    public abstract class Day
    {
        protected List<string> input;

        protected Day(int dayNr, bool useExampleInput)
        {
            string folder = dayNr >= 10 ? dayNr.ToString() : $"0{dayNr}";
            string path = $"../../../{folder}/{(useExampleInput ? "ex" : dayNr.ToString())}.txt";

            input = File.ReadAllLines(path).ToList();
        }

        public virtual void Solve()
        {
            throw new NotImplementedException();
        }
    }
}

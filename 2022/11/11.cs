using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day11 : Day
    {
        public Day11()
            : base(11, false)
        { }

        public override void Solve()
        {
            var monkeys = new Monkey[] 
            { 
                new Monkey0(), 
                new Monkey1(), 
                new Monkey2(), 
                new Monkey3(),
                new Monkey4(),
                new Monkey5(),
                new Monkey6(),
                new Monkey7()
            };
            
            long factor = monkeys
                .Select(m => m.DivisibleTest)
                .Aggregate((f1, f2) => f1 * f2);

            for (int i = 0; i < 10_000; i++)
            {
                for(int j = 0; j < monkeys.Length; j++)
                {
                    Turn(monkeys[j], monkeys, factor);
                }
            }

            long monkeyBusiness = monkeys
                .Select(x => x.InspectedCount)
                .OrderByDescending(x => x)
                .Take(2)
                .Aggregate((long)1, (x, y) => x * y);

            Console.WriteLine(monkeyBusiness);
        }

        public void Turn(Monkey monkey, Monkey[] monkeys, long factor)
        {
            for (int i = 0; i < monkey.StartingItems.Count; i++)
            {
                monkey.InspectedCount++;
                long worry = monkey.StartingItems[i];
                worry = monkey.Operation(worry);
                worry = worry % factor;
                monkeys[monkey.Throw(worry)].StartingItems.Add(worry);
            }

            monkey.StartingItems = new List<long>();
        }
    }

    public abstract class Monkey
    {
        public int InspectedCount = 0;

        public int DivisibleTest = 0;

        public List<long> StartingItems;

        public abstract long Operation(long old);

        public abstract int Throw(long worryLevel);
    }

    public class Monkey0 : Monkey
    {
        public Monkey0()
        {
            StartingItems = new List<long>() { 74, 64, 74, 63, 53 };
            DivisibleTest = 5;
        }

        public override long Operation(long old)
        {
            return old * 7;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % DivisibleTest == 0 ? 1 : 6;
        }
    }

    public class Monkey1 : Monkey
    {

        public Monkey1()
        {
            StartingItems = new List<long>() { 69, 99, 95, 62 };
            DivisibleTest = 17;
        }

        public override long Operation(long old)
        {
            return old * old;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % DivisibleTest == 0 ? 2 : 5;
        }
    }

    public class Monkey2 : Monkey
    {
        public Monkey2()
        {
            StartingItems = new List<long>() { 59, 81 };
            DivisibleTest = 7;
        }

        public override long Operation(long old)
        {
            return old + 8;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % DivisibleTest == 0 ? 4 : 3;
        }
    }

    public class Monkey3 : Monkey
    {
        public Monkey3()
        {
            StartingItems = new List<long>() { 50, 67, 63, 57, 63, 83, 97 };
            DivisibleTest = 13;
        }

        public override long Operation(long old)
        {
            return old + 4;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % DivisibleTest == 0 ? 0: 7;
        }
    }

    public class Monkey4 : Monkey
    {
        public Monkey4()
        {
            StartingItems = new List<long>() { 61, 94, 85, 52, 81, 90, 94, 70 };
            DivisibleTest = 19;
        }

        public override long Operation(long old)
        {
            return old + 3;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % DivisibleTest == 0 ? 7 : 3;
        }
    }

    public class Monkey5 : Monkey
    {
        public Monkey5()
        {
            StartingItems = new List<long>() { 69 };
            DivisibleTest = 3;
        }

        public override long Operation(long old)
        {
            return old + 5;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % DivisibleTest == 0 ? 4 : 2;
        }
    }

    public class Monkey6 : Monkey
    {
        public Monkey6()
        {
            StartingItems = new List<long>() { 54, 55, 58 };
            DivisibleTest = 11;
        }

        public override long Operation(long old)
        {
            return old + 7;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % DivisibleTest == 0 ? 1 : 5;
        }
    }

    public class Monkey7 : Monkey
    {
        public Monkey7()
        {
            StartingItems = new List<long>() { 79, 51, 83, 88, 93, 76 };
            DivisibleTest = 2;
        }

        public override long Operation(long old)
        {
            return old * 3;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % DivisibleTest == 0 ? 0 : 6;
        }
    }


    public class TMonkey0 : Monkey
    {
        public TMonkey0()
        {
            StartingItems = new List<long>() { 79, 98 };
            DivisibleTest = 23;
        }

        public override long Operation(long old)
        {
            return old * 19;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % 23 == 0 ? 2 : 3;
        }
    }

    public class TMonkey1 : Monkey
    {

        public TMonkey1()
        {
            StartingItems = new List<long>() { 54, 65, 75, 74 };
            DivisibleTest = 19;
        }

        public override long Operation(long old)
        {
            return old + 6;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % 19 == 0 ? 2 : 0;
        }
    }

    public class TMonkey2 : Monkey
    {
        public TMonkey2()
        {
            StartingItems = new List<long>() { 79, 60, 97 };
            DivisibleTest = 13;
        }

        public override long Operation(long old)
        {
            return old * old;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % 13 == 0 ? 1 : 3;
        }
    }

    public class TMonkey3 : Monkey
    {
        public TMonkey3()
        {
            StartingItems = new List<long>() { 74 };
            DivisibleTest = 17;
        }

        public override long Operation(long old)
        {
            return old + 3;
        }

        public override int Throw(long worryLevel)
        {
            return worryLevel % 17 == 0 ? 0 : 1;
        }
    }
}

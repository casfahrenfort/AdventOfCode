using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AoC2021
{
    public class Day10 : Day
    {
        public Day10()
            : base(10, false)
        { }

        public override void Solve()
        {
            List<long> scores = new List<long>();

            foreach (string line in input)
            {
                List<char> added = AutoComplete(line);

                if (added == null)
                    continue;

                scores.Add(added.Select(ClosingTokenScore).Aggregate((total, score) => total * 5 + score));
            }

            var sorted = scores.OrderBy(x => x).ToArray();
            long result = sorted[sorted.Length / 2];
        }

        private List<char>? AutoComplete(string line)
        {
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '{':
                    case '(':
                    case '[':
                    case '<': stack.Push(line[i]); break;
                    case '}':
                    case ')':
                    case ']':
                    case '>':
                        {
                            if (stack.Pop() != OppositeToken(line[i]))
                            {
                                return null;
                            }
                        }
                        break;
                }
            }

            return stack.Select(x => OppositeToken(x)).ToList();
        }

        private char? FirstIllegalCharacter(string line)
        {
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '{':
                    case '(':
                    case '[':
                    case '<': stack.Push(line[i]); break;
                    case '}':
                    case ')':
                    case ']':
                    case '>':
                        {
                            if (stack.Pop() != OppositeToken(line[i]))
                            {
                                return line[i];
                            }
                        }
                        break;
                }
            }

            return null;
        }

        private char OppositeToken(char token)
        {
            switch (token)
            {
                case '{': return '}';
                case '(': return ')';
                case '[': return ']';
                case '<': return '>';
                case '}': return '{';
                case ')': return '(';
                case ']': return '[';
                case '>': return '<';
                default: throw new Exception("Invalid token");
            }
        }

        private long ClosingTokenScore(char token)
        {
            switch (token)
            {
                case ')': return 1;
                case ']': return 2;
                case '}': return 3;
                case '>': return 4;
                default: throw new Exception("Invalid token");
            }
        }
    }
}

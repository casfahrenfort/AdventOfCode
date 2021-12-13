using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day12 : Day
    {
        private Dictionary<string, Node> nodes;
        private List<Stack<Node>> paths;

        public Day12()
            : base(12, false)
        { }

        public override void Solve()
        {
            nodes = new Dictionary<string, Node>();
            paths = new List<Stack<Node>>();

            foreach (string line in input)
            {
                string[] split = line.Split('-');
                Node firstNode = null;
                if (nodes.ContainsKey(split[0]))
                    firstNode = nodes[split[0]];
                else
                {
                    firstNode = new Node(split[0]);
                    nodes.Add(split[0], firstNode);
                }

                Node secondNode = null;
                if (nodes.ContainsKey(split[1]))
                    secondNode = nodes[split[1]];
                else
                {
                    secondNode = new Node(split[1]);
                    nodes.Add(split[1], secondNode);
                }

                firstNode.Connect(secondNode);
                secondNode.Connect(firstNode);
            }

            var stack = new Stack<Node>();
            stack.Push(nodes["start"]);
            FindAllPaths(nodes["start"], nodes["end"], stack, false);

            foreach (var p in paths)
            {
                Console.WriteLine(string.Join(',', p.Reverse().Select(x => x.value)));
            }
        }

        public void FindAllPaths(Node start, Node end, Stack<Node> path, bool twice)
        {
            if (start.value == end.value)
            {
                paths.Add(path);
                return;
            }

            foreach (Node n in start.connected)
            {
                if (n.Start)
                    continue;

                if (n.SmallCave && path.Contains(n))
                {
                    if (twice)
                        continue;
                    path.Push(n);
                    FindAllPaths(n, end, new Stack<Node>(path.Reverse()), true);
                    path.Pop();

                }
                else
                {
                    path.Push(n);
                    FindAllPaths(n, end, new Stack<Node>(path.Reverse()), twice);
                    path.Pop();
                }
            }
            return;
        }
    }

    public class Node
    {
        public string value;
        public List<Node> connected;

        public Node(string value)
        {
            this.value = value;
            connected = new List<Node>();
        }

        public void Connect(Node node)
        {
            this.connected.Add(node);
        }

        public bool SmallCave
        {
            get { return value != "start" && value != "end" && !value.Any(char.IsUpper); }
        }

        public bool Start
        {
            get { return value == "start"; }
        }
    }
}

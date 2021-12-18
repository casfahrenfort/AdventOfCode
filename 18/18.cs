using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day18 : Day
    {
        public SnailNumber found = null;

        public Day18()
            : base(18, false)
        { }

        public override void Solve()
        {
            var snailNumbers = input.Select(line =>
            {
                var obj = Newtonsoft.Json.Linq.JArray.Parse(line);
                return new SnailNumber(null, obj);
            }).ToList();

            long largestMagnitude = long.MinValue;

            for (int i = 0; i < snailNumbers.Count; i++)
            {
                for (int j = 0; j < snailNumbers.Count; j++)
                {
                    if (i == j)
                        continue;

                    var freshNumbers = input.Select(line =>
                    {
                        var obj = Newtonsoft.Json.Linq.JArray.Parse(line);
                        return new SnailNumber(null, obj);
                    }).ToList();

                    var magnitude = Addition(freshNumbers[i], freshNumbers[j]).Magnitude;
                    if (magnitude > largestMagnitude)
                        largestMagnitude = magnitude;
                }

                //InOrder(result);
                //Console.WriteLine();
            }

        }

        public SnailNumber Addition(SnailNumber nr1, SnailNumber nr2)
        {
            var root = nr1.Add(nr2);

            while (true)
            {
                found = null;
                FindToExplode(root);

                if (found != null)
                {
                    found.Explode();
                    //InOrder(root);
                    //Console.WriteLine();
                    continue;
                }

                FindToSplit(root);
                if (found != null)
                {
                    found.Split();
                    //InOrder(root);
                    //Console.WriteLine();
                    continue;
                }

                break;
            }

            return root;
        }

        public void FindToExplode(SnailNumber nr)
        {
            if (found != null)
                return;

            if (!nr.left.IsLeaf)
                FindToExplode(nr.left);

            if (nr.right.IsLeaf
                && nr.left.IsLeaf
                && nr.Depth >= 4
                && found == null)
            {
                found = nr;
                return;
            }

            if (!nr.right.IsLeaf)
                FindToExplode(nr.right);
        }

        public void FindToSplit(SnailNumber nr)
        {
            if (found != null || nr == null)
                return;

            FindToSplit(nr.left);

            if (nr.number >= 10
                && found == null)
            {
                found = nr;
                return;
            }

            FindToSplit(nr.right);
        }

        public void InOrder(SnailNumber nr)
        {
            if (nr == null)
                return;

            InOrder(nr.left);

            if (nr.IsLeaf)
            {
                Console.Write(nr.number + " ");
            }

            InOrder(nr.right);
        }

        public void PostOrder(SnailNumber nr)
        {
            if (nr == null)
                return;

            PostOrder(nr.left);
            PostOrder(nr.right);


            if (nr.IsLeaf)
            {
                Console.Write(nr.number);
            }

        }
    }

    public class SnailNumber
    {
        public SnailNumber parent;
        public SnailNumber left;
        public SnailNumber right;
        public int number;

        public SnailNumber(SnailNumber parent,
            SnailNumber left = null,
            SnailNumber right = null,
            int number = -1)
        {
            this.parent = parent;
            this.left = left;
            this.right = right;
            this.number = number;
        }

        public SnailNumber(SnailNumber parent, JToken array)
        {
            this.parent = parent;
            if (array.First.GetType() == typeof(JArray))
            {
                left = new SnailNumber(this, (JArray)array.First);
            }
            else
                left = new SnailNumber(this, number: (int)(long)((JValue)array.First).Value);


            if (array.Last.GetType() == typeof(JArray))
            {
                right = new SnailNumber(this, (JArray)array.Last);
            }
            else
                right = new SnailNumber(this, number: (int)(long)((JValue)array.Last).Value);
        }

        public bool IsLeaf
        {
            get { return left == null && right == null; }
        }

        public bool IsRoot
        {
            get { return parent == null; }
        }

        public bool IsLeft
        {
            get { return parent.left == this; }
        }
        public bool IsRight
        {
            get { return parent.right == this; }
        }

        public int Depth
        {
            get
            {
                if (IsRoot)
                    return 0;

                int depth = 0;
                var p = parent;
                while (p != null)
                {
                    p = p.parent;
                    depth++;
                }

                return depth;
            }
        }

        public long Magnitude
        {
            get
            {
                if (IsLeaf)
                    return number;

                return 3 * left.Magnitude + 2 * right.Magnitude;
            }
        }

        public SnailNumber Add(SnailNumber number)
        {
            var newParent = new SnailNumber(null, this, number);
            parent = newParent;
            number.parent = newParent;
            return newParent;
        }

        public void Explode()
        {
            if (!left.IsLeaf || !right.IsLeaf)
                return;

            SnailNumber nearestLeft = left.Pre();
            while (nearestLeft != null && !nearestLeft.IsLeaf)
                nearestLeft = nearestLeft.Pre();
            SnailNumber nearestRight = right.Succ();
            while (nearestRight != null && !nearestRight.IsLeaf)
                nearestRight = nearestRight.Succ();

            if (nearestLeft != null)
                nearestLeft.number += left.number;
            if (nearestRight != null)
                nearestRight.number += right.number;

            if (IsLeft)
                parent.left = new SnailNumber(parent, number: 0);
            else
                parent.right = new SnailNumber(parent, number: 0);
        }

        public void Split()
        {
            left = new SnailNumber(this, number: (int)Math.Floor((decimal)this.number / 2));
            right = new SnailNumber(this, number: (int)Math.Ceiling((decimal)this.number / 2));
            number = 0;
        }

        public SnailNumber Succ()
        {
            if (right != null)
            {
                var no = right;
                while (no.left != null)
                    no = no.left;
                return no;
            }

            var p = parent;
            var n = this;
            while (p != null && n == p.right)
            {
                n = p;
                p = p.parent;
            }
            return p;
        }

        public SnailNumber Pre()
        {
            if (left != null)
            {
                var no = left;
                while (no.right != null)
                    no = no.right;
                return no;
            }

            var p = parent;
            var n = this;
            while (p != null && n == p.left)
            {
                n = p;
                p = p.parent;
            }
            return p;
        }
    }
}

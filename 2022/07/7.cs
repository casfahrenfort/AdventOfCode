/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public class Day7 : Day
    {
        public List<Directory> DirectoriesOver100k = new List<Directory>();
        public int RequiredSpace;
        public int SmallestSize = int.MaxValue;

        public Day7()
            : base(7, false)
        { }

        public override void Solve()
        {
            var root = new Directory()
            {
                Parent = null,
                Name = "/",
                Size = 0
            };

            var currentDir = root;

            for(int i = 1; i < input.Count; i++)
            {
                if (input[i][0] != '$')
                    continue;

                var split = input[i].Split(' ');
                if (split[1] == "ls")
                {
                    var lines = new List<string>();
                    for (int j = i + 1; j < input.Count; j++)
                        if (input[j][0] != '$')
                            lines.Add(input[j]);
                        else
                            break;
                    Ls(lines, currentDir);
                }
                else if (split[1] == "cd")
                {
                    if (split[2] == "..")
                        currentDir = currentDir.Parent;
                    else
                        currentDir = (Directory)currentDir.Items[split[2]];
                }
            }

            DirectorySizes(root);

            RequiredSpace =  30000000 - (70000000 - root.Size);
            
            FindSmallest(root);

            Console.WriteLine(SmallestSize);
        }

        public void Ls(IEnumerable<string> lines, Directory dir)
        {
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                if (split[0] == "dir")
                {
                    dir.Items.Add(split[1], new Directory() { Name = split[1], Parent = dir, Size = 0 });
                }
                else
                {
                    int size = int.Parse(split[0]);
                    dir.Items.Add(split[1], new File() { Name = split[1], Size = size });
                }
            }
        }

        public void DirectorySizes(Directory root)
        {
            var directories = root.Items.Where(x => x.Value is Directory);
            if (directories.Count() == 0)
            {
                root.Size = root.Items.Sum(x => x.Value.Size);
                if (root.Size <= 100000)
                {
                    DirectoriesOver100k.Add(root);
                }
                return;
            }

            foreach (var dir in directories)
            {
                DirectorySizes((Directory)dir.Value);
            }

            root.Size = root.Items.Sum(x => x.Value.Size);
            if (root.Size <= 100000)
            {
                DirectoriesOver100k.Add(root);
            }
        }

        public void FindSmallest(Directory root)
        {
            if (root.Size > RequiredSpace
                && root.Size < SmallestSize)
                SmallestSize = root.Size;

            var directories = root.Items.Where(x => x.Value is Directory);

            foreach(var dir in directories)
            {
                FindSmallest((Directory)dir.Value);
            }
        }
    }

    public abstract class FileSystemItem
    {
        public required string Name { get; set; }
        public required int Size { get; set; }
    }

    public class Directory : FileSystemItem
    {
        public Dictionary<string, FileSystemItem> Items = new Dictionary<string, FileSystemItem>();
        public required Directory? Parent { get; set; }
    }

    public class File : FileSystemItem
    {
    }
}
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class U7
    {
        private readonly AocHttpClient _client = new AocHttpClient(7);


        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();

            input = "$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            DirectoryModel root = null;
            DirectoryModel current = null;
            for (int i = 0; i < split.Length; i++)
            {
                string line = split[i];
                string[] commandParts = line.Split(' ');
                if (commandParts[0] == "$")
                {
                    string commandArguments = commandParts[1];
                    switch (commandArguments)
                    {
                        case "cd":
                            string directoryName = commandParts[2];
                            switch (directoryName)
                            {
                                case "/": // Root
                                    root = new DirectoryModel(directoryName, 0, null, DirectoryModelType.Directory);
                                    current = root;
                                    break;
                                case "..": // cd previous
                                    current = current.Previous;
                                    break;
                                default: // cd into
                                    current = FindByName(current, directoryName);
                                    break;
                            }
                            break;
                        case "ls":
                            for (int j = i + 1; j < split.Length; j++)
                            {
                                var lsCommand = split[j].Split(' ');
                                string arg1 = lsCommand[0];
                                string arg2 = lsCommand[1];

                                if (arg1 == "$") // Command
                                {
                                    i = j - 1;
                                    break;
                                }
                                else if (arg1 == "dir") // Directory
                                {
                                    current.Next.Add(new DirectoryModel(arg2, 0, current, DirectoryModelType.Directory));
                                }
                                else // File
                                {
                                    current.Next.Add(new DirectoryModel(arg2, long.Parse(arg1), current, DirectoryModelType.File));
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }

            ComputeTotalSizePerDirectoryUsingDepthFirstSearch(root);
            long result = FindMaxSizePerDirectory(root, 100000);

            Console.WriteLine(result);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine();
        }

        private DirectoryModel FindByName(DirectoryModel root, string name)
        {
            Queue<DirectoryModel> queue = new Queue<DirectoryModel>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                DirectoryModel head = queue.Dequeue();
                foreach (DirectoryModel directory in head.Next)
                {
                    if (directory.Name == name)
                        return directory;

                    queue.Enqueue(directory);
                }
            }

            throw new KeyNotFoundException();
        }


        private void ComputeTotalSizePerDirectoryUsingDepthFirstSearch(DirectoryModel start)
        {
            var visited = new HashSet<DirectoryModel>();

            Stack<DirectoryModel> stack = new Stack<DirectoryModel>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (visited.Contains(current))
                {
                    continue;
                }


                visited.Add(current);

                foreach (DirectoryModel neighhbour in current.Next)
                {
                    if (!visited.Contains(neighhbour))
                    {
                        stack.Push(neighhbour);
                    }
                }

                current.Size += current.Next.Sum(x => x.GetSize());
            }
        }

        private long FindMaxSizePerDirectory(DirectoryModel start, long maxSize)
        {
            long sum = 0;
            var visited = new HashSet<DirectoryModel>();

            Queue<DirectoryModel> queue = new Queue<DirectoryModel>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (visited.Contains(current))
                {
                    continue;
                }


                visited.Add(current);


                if (current.Size <= maxSize)
                {
                    sum += current.Size;
                }
                else
                {
                    foreach (DirectoryModel neighhbour in current.Next)
                    {
                        if (!visited.Contains(neighhbour))
                        {
                            queue.Enqueue(neighhbour);
                        }
                    }
                }
            }
            return sum;
        }
    }

    public class DirectoryModel
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public List<DirectoryModel> Next { get; set; }
        public DirectoryModel Previous { get; set; }
        public DirectoryModelType Type { get; set; }

        public DirectoryModel(string name, long size, DirectoryModel previous, DirectoryModelType type)
        {
            Name = name;
            Size = size;
            Next = new List<DirectoryModel>();
            Previous = previous;
            Type = type;
        }

        public long GetSize()
        {
            if (Type == DirectoryModelType.Directory)
            {
                foreach (DirectoryModel item in Next)
                    return Size + item.GetSize();
            }

            if (Type == DirectoryModelType.File)
            {
                return Size;
            }

            return 0;
        }

        public override string ToString()
        {
            return $"{Name} {Size} {Type}";
        }
    }

    public enum DirectoryModelType
    {
        Directory,
        File
    }

}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class D07
    {
        private readonly AocHttpClient _client = new AocHttpClient(7);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            DirectoryModel root = new DirectoryModel("/", null);
            List<DirectoryModel> directories = BuildTreeOfDirectories(split, root);

            long result = directories
                .Where(dir => dir.GetSize() <= 100000)
                .Sum(dir => dir.GetSize());

            Console.WriteLine(result);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            DirectoryModel root = new DirectoryModel("/", null);
            List<DirectoryModel> directories = BuildTreeOfDirectories(split, root);
            long freeSpace = 70000000 - root.GetSize();
            long result = directories.Where(dir => dir.GetSize() > 30000000 - freeSpace)
                .OrderBy(dir => dir.GetSize())
                .FirstOrDefault()
                .GetSize();

            Console.WriteLine(result);
        }

        private List<DirectoryModel> BuildTreeOfDirectories(string[] split, DirectoryModel root)
        {
            DirectoryModel current = root;
            List<DirectoryModel> result = new List<DirectoryModel>();
            result.Add(root);

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
                                    current = root;
                                    break;
                                case "..": // cd previous
                                    if (current.Parent == null)
                                        throw new NullReferenceException();
                                    current = current.Parent;
                                    break;
                                default: // cd into
                                    current = FindDirectoryByName(current, directoryName);
                                    break;
                            }
                            break;
                        case "ls":
                            for (int j = i + 1; j < split.Length; j++)
                            {
                                string[] lsCommand = split[j].Split(' ');
                                string arg1 = lsCommand[0];
                                string arg2 = lsCommand[1];

                                if (arg1 == "$")
                                {
                                    i = j - 1;
                                    break;
                                }
                                else if (arg1 == "dir") // Directory
                                {
                                    DirectoryModel directory = new DirectoryModel(arg2, current);
                                    current.Directories.Add(arg2, directory);
                                    result.Add(directory);
                                }
                                else // File
                                {
                                    current.Files.Add(arg2, long.Parse(arg1));
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            return result;
        }

        private DirectoryModel FindDirectoryByName(DirectoryModel current, string name)
        {
            if (current.Directories.TryGetValue(name, out var directory))
                return directory;
            throw new KeyNotFoundException();
        }
    }

    public class DirectoryModel
    {
        public string Name { get; set; }
        public DirectoryModel Parent { get; set; }
        public Dictionary<string, DirectoryModel> Directories = new Dictionary<string, DirectoryModel>();
        public Dictionary<string, long> Files = new Dictionary<string, long>();

        public DirectoryModel(string name, DirectoryModel parent)
        {
            Name = name;
            Parent = parent;
        }

        public long GetSize()
        {
            return Directories.Values.Sum(dir => dir.GetSize()) + Files.Values.Sum(file => file);
        }

        public override string ToString()
        {
            return $"{Name} {GetSize()}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class D05
    {
        private readonly AocHttpClient _client = new AocHttpClient(5);
        private readonly List<Stack<char>> _listOfStacks = new List<Stack<char>>()
        {
            new Stack<char>(),
            new Stack<char>(),
            new Stack<char>(),
            new Stack<char>(),
            new Stack<char>(),
            new Stack<char>(),
            new Stack<char>(),
            new Stack<char>(),
            new Stack<char>(),
        };

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 7; i >= 0; i--)
            {
                int index = 0;
                for (int y = 1; y <= 33; y += 4)
                {
                    index++;
                    if (split[i][y] == ' ')
                        continue;

                    _listOfStacks[index - 1].Push(split[i][y]);
                }
            }

            for (int i = 9; i < split.Length; i++)
            {
                string[] fromSplit = split[i].Split("from");
                int number = int.Parse(fromSplit[0].Replace("move", string.Empty));
                string[] toSplit = fromSplit[1].Split("to");
                int indexFrom = int.Parse(toSplit[0]);
                int indexTo = int.Parse(toSplit[1]);
                PopAndPushOneByOne(number, indexFrom, indexTo);
            }

            string result = string.Concat(_listOfStacks.Select(x => x.FirstOrDefault()));
            Console.WriteLine(result);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 7; i >= 0; i--)
            {
                int index = 0;
                for (int y = 1; y <= 33; y += 4)
                {
                    index++;
                    if (split[i][y] == ' ')
                        continue;

                    _listOfStacks[index - 1].Push(split[i][y]);
                }
            }

            for (int i = 9; i < split.Length; i++)
            {
                string[] fromSplit = split[i].Split("from");
                int number = int.Parse(fromSplit[0].Replace("move", string.Empty));
                string[] toSplit = fromSplit[1].Split("to");
                int indexFrom = int.Parse(toSplit[0]);
                int indexTo = int.Parse(toSplit[1]);
                PopAndPushWithStack(number, indexFrom, indexTo);
            }

            string result = string.Concat(_listOfStacks.Select(x => x.FirstOrDefault()));
            Console.WriteLine(result);
        }

        private void PopAndPushOneByOne(int number, int indexFrom, int indexTo)
        {
            for (int i = 0; i < number; i++)
            {
                char pop = _listOfStacks[indexFrom - 1].Pop();
                if (pop == ' ')
                    continue;

                _listOfStacks[indexTo - 1].Push(pop);
            }
        }

        private void PopAndPushWithStack(int number, int indexFrom, int indexTo)
        {
            Stack<char> queue = new Stack<char>();
            for (int i = 0; i < number; i++)
            {
                char pop = _listOfStacks[indexFrom - 1].Pop();
                if (pop == ' ')
                    continue;

                queue.Push(pop);
            }

            foreach (var q in queue)
            {
                _listOfStacks[indexTo - 1].Push(q);
            }
        }
    }
}

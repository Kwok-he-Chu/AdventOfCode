using System;
using System.Linq;

namespace AOC2022
{
    public class D03
    {
        private readonly AocHttpClient _client = new AocHttpClient(3);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int sum = 0;

            foreach (string item in split)
            {
                int halfLength = item.Length / 2;
                string left = item.Substring(0, halfLength);
                string right = item.Substring(halfLength, halfLength);
                foreach (char c in left)
                {
                    if (!right.Contains(c))
                        continue;

                    if (char.IsUpper(c))
                    {
                        int value = (int)c - 64 + 27 - 1;
                        sum += value;
                        break;
                    }
                    
                    if (char.IsLower(c))
                    {
                        int value = (int)c - 97 + 1;
                        sum += value;
                        break;
                    }

                    throw new ArgumentOutOfRangeException();
                }
            }

            Console.WriteLine(sum);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var result = split
                .Chunk(3)
                .Select(line => (First: line[0], Second: line[1], Third: line[2]))
                .Select(line => line.First.Intersect(line.Second).Intersect(line.Third)
                .First())
                .Sum(c =>
                {
                    if (char.IsUpper(c))
                    {
                        return (int)c - 64 + 27 - 1;
                    }

                    if (char.IsLower(c))
                    {
                        return (int)c - 97 + 1;
                    }

                    throw new ArgumentOutOfRangeException();
                });

            Console.WriteLine(result);
        }
    }
}

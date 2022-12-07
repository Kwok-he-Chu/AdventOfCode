using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class U1
    {
        private readonly AocHttpClient _client = new AocHttpClient(1);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var result = GetCaloriesPerLine(input);

            Console.WriteLine(result.Max());
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var result = GetCaloriesPerLine(input);

            Console.WriteLine(result.OrderByDescending(x => x).Take(3).Sum());
        }

        private IEnumerable<int> GetCaloriesPerLine(string input)
        {
            return input.Split("\r\n\r\n")
                .Select(line => line.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
                .Select(list => list.Sum(calories => int.Parse(calories)));
        }
    }
}

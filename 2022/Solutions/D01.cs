using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    /// <summary>
    /// Day 1: Calorie Counting
    /// </summary>
    public class D01
    {
        private readonly AocHttpClient _client = new AocHttpClient(1);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "1000\r\n2000\r\n3000\r\n\r\n4000\r\n\r\n5000\r\n6000\r\n\r\n7000\r\n8000\r\n9000\r\n\r\n10000";
            List<int> result = GetCaloriesPerLine(input).ToList();

            Console.WriteLine(result.Max());
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "1000\r\n2000\r\n3000\r\n\r\n4000\r\n\r\n5000\r\n6000\r\n\r\n7000\r\n8000\r\n9000\r\n\r\n10000";
            List<int> result = GetCaloriesPerLine(input).ToList();

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

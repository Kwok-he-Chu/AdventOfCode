﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class U4
    {
        private readonly AocHttpClient _client = new AocHttpClient(4);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var list = GetListOfFilledNumbers(split);

            int result = list.Count(x =>
            {
                IEnumerable<int> intersect = x.First.Intersect(x.Second);
                return intersect.SequenceEqual(x.First) || intersect.SequenceEqual(x.Second);
            });

            Console.WriteLine(result);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var list = GetListOfFilledNumbers(split);

            int result = list.Count(x => x.First.Any(y => x.Second.Contains(y)) || x.Second.Any(y => x.First.Contains(y)));

            Console.WriteLine(result);
        }

        private IEnumerable<(IEnumerable<int> First, IEnumerable<int> Second)> GetListOfFilledNumbers(string[] split)
        {
            return split.Select(s => s.Split(',')
                .Select(r =>
                {
                    int first = int.Parse(r.Split('-')[0]);
                    int last = int.Parse(r.Split('-')[1]);
                    return Enumerable.Range(first, last - first + 1);
                }).ToList())
                .Select(tuple => (First: tuple[0], Second: tuple[1]));
        }

    }
}

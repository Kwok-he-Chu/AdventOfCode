using System;

namespace AOC2025;

/// <summary>
/// Day 5: Cafeteria
/// </summary>
public class D05
{
    private readonly AOCHttpClient _client = new AOCHttpClient(5);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"3-5
// 10-14
// 16-20
// 12-18
//
// 1
// 5
// 8
// 11
// 17
// 32";

        string[] split = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Ranges> ranges = split[0]
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(range =>
            {
                string[] s = range.Split("-");
                long lowerBound = long.Parse(s[0]);
                long upperbound = long.Parse(s[1]);
                return new Ranges(lowerBound, upperbound);
            })
            .ToList();
        
        List<long> numbers = split[1]
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        List<long> result = numbers
            .Where(number => ranges.Any(r => number >= r.LowerBound && number <= r.UpperBound))
            .ToList();
        
        Console.WriteLine(result.Count);
    }

    private class Ranges(long lowerBound, long upperBound)
    {
        public long LowerBound { get; } = lowerBound;
        public long UpperBound { get; } = upperBound;
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"3-5
// 10-14
// 16-20
// 12-18
//
// 1
// 5
// 8
// 11
// 17
// 32";

        string[] split = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Ranges> ranges = split[0]
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(range =>
            {
                string[] s = range.Split("-");
                long lowerBound = long.Parse(s[0]);
                long upperbound = long.Parse(s[1]);
                return new Ranges(lowerBound, upperbound);
            })
            .OrderBy(range => range.LowerBound)
            .ToList();


        List<Ranges> mergedRages = new List<Ranges>();
        mergedRages.Add(ranges.First());
        
        foreach (Ranges range in ranges)
        {
            Ranges current = mergedRages.Last();
            if (range.LowerBound <= current.UpperBound)
            {
                mergedRages[mergedRages.Count - 1] = new Ranges(current.LowerBound, Math.Max(range.UpperBound, current.UpperBound));
                continue;
            }
            mergedRages.Add(range);
        }

        long result = mergedRages.Sum(range => range.UpperBound - range.LowerBound + 1);
        Console.WriteLine(result);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 5: If You Give A Seed A Fertilizer.
/// </summary>
public class D05
{
    private readonly AOCHttpClient _client = new AOCHttpClient(5);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        /*input = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";*/

        string[] split = input.Split(Environment.NewLine);

        List<long> result = split[0].Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        for (int i = 2; i < split.Length; i++)
        {
            List<long> next = new List<long>();
            List<Map> maps = new List<Map>();

            // Parse `xxx-to-yyy` map.
            for (int j = i; j < split.Length; j++)
            {
                // Read until you see a next '-' and skip null/empty white-spaces.
                if (string.IsNullOrWhiteSpace(split[j]) || split[j].Contains("-"))
                {
                    i = j; // Continue where we left off reading.
                    break;
                }

                List<long> numbers = split[j]
                    .Split(" ")
                    .Select(long.Parse)
                    .ToList();

                maps.Add(new Map(numbers[0], numbers[1], numbers[2]));
            }

            List<Map> orderedMaps = maps
                .OrderBy(x => x.SourceRangeStart)
                .ToList();

            foreach (Map map in orderedMaps)
            {
                // These are all overlapping valid seeds
                List<long> validSeedNumbers = result
                    .Where(map.IsWithinRange)
                    .ToList();

                foreach (long number in validSeedNumbers)
                {
                    result.RemoveAll(seed => seed == number);
                    next.Add(map.Destination + number - map.SourceRangeStart);
                }

            }

            next.AddRange(result);
            result = next;
        }

        Console.WriteLine(result.Min());
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();


        /*input = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";*/

        string[] split = input.Split(Environment.NewLine);

        List<long> inputSeedNumbers = split[0].Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        List<Seed> result = new List<Seed>();
        for (int i = 0; i < inputSeedNumbers.Count; i += 2)
        {
            result.Add(new Seed(inputSeedNumbers[i], inputSeedNumbers[i] + inputSeedNumbers[i + 1]));
        }

        for (int i = 2; i < split.Length; i++)
        {
            List<Seed> next = new List<Seed>();
            List<Map> maps = new List<Map>();

            // Parse `xxx-to-yyy` map.
            for (int j = i; j < split.Length; j++)
            {
                // Read until you see a next '-' and skip null/empty white-spaces.
                if (string.IsNullOrWhiteSpace(split[j]) || split[j].Contains("-"))
                {
                    i = j; // Continue where we left off reading.
                    break;
                }

                List<long> numbers = split[j]
                    .Split(" ")
                    .Select(long.Parse)
                    .ToList();

                maps.Add(new Map(numbers[0], numbers[1], numbers[2]));
            }

            List<Map> orderedMaps = maps
                .OrderBy(x => x.SourceRangeStart)
                .ToList();

            foreach (Map map in orderedMaps)
            {
                long end = map.SourceRangeStart + map.RangeLength;

                // These are all overlapping valid seeds
                List<Seed> valid = result
                    .Where(map.IsWithinRange)
                    .ToList();

                foreach (Seed seed in valid)
                {
                    result.Remove(seed);

                    long diff = map.Destination - map.SourceRangeStart;
                    long nextStart = seed.Start + diff;
                    long nextEnd = (seed.End >= end) ? end - 1 + diff : seed.End + diff;

                    result.Add(new Seed(end, seed.End));
                    next.Add(new Seed(nextStart, nextEnd));
                }

            }

            next.AddRange(result);
            result = next.OrderBy(s => s.Start).ToList();
        }

        Console.WriteLine(result.Min(s => s.Start));
    }


    private record Map(long Destination, long SourceRangeStart, long RangeLength)
    {
        public bool IsWithinRange(long number)
        {
            return number >= SourceRangeStart && number < SourceRangeStart + RangeLength;
        }

        public bool IsWithinRange(Seed s)
        {
            return s.Start < SourceRangeStart + RangeLength && s.End >= SourceRangeStart;
        }

        public override string ToString()
        {
            return $"{Destination} {SourceRangeStart} {RangeLength}";
        }
    }

    private record Seed(long Start, long End);
}
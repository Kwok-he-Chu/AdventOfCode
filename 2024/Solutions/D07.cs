using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 7: Bridge Repair
/// </summary>
public class D07
{
    private readonly AOCHttpClient _client = new AOCHttpClient(7);
    
    public void Part1()
    {
        string input = _client.RetrieveFile();
        
//         input = @"190: 10 19
// 3267: 81 40 27
// 83: 17 5
// 156: 15 6
// 7290: 6 8 6 15
// 161011: 16 10 13
// 192: 17 8 14
// 21037: 9 7 18 13
// 292: 11 6 16 20";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Equation> models = split.Select(line =>
        {
            string[] numbers = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);
            return new Equation()
            {
                Sum = long.Parse(numbers[0]),
                List = numbers[1].Split(" ", StringSplitOptions.TrimEntries)
                    .Select(long.Parse)
                    .ToList()
            };
        }).ToList();
        
        long sum = models
            .Where(result => FindSumOrMultiply(result.Sum, result.List, 0))
            .Sum(result => result.Sum);
        
        Console.WriteLine(sum);
    }
    
    private bool FindSumOrMultiply(long sum, List<long> list, long current)
    {
        if (list.Count == 0)
        {
            return current == sum;
        }
        
        List<long> next = list.Skip(1).ToList();
        return FindSumOrMultiply(sum, next, current + list.First()) || 
               FindSumOrMultiply(sum, next, current * list.First());
    }
    
    public void Part2()
    {
        string input = _client.RetrieveFile();
        
//         input = @"190: 10 19
// 3267: 81 40 27
// 83: 17 5
// 156: 15 6
// 7290: 6 8 6 15
// 161011: 16 10 13
// 192: 17 8 14
// 21037: 9 7 18 13
// 292: 11 6 16 20";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Equation> models = split.Select(line =>
        {
            string[] numbers = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);
            return new Equation()
            {
                Sum = long.Parse(numbers[0]),
                List = numbers[1].Split(" ", StringSplitOptions.TrimEntries)
                    .Select(long.Parse)
                    .ToList()
            };
        }).ToList();

        long sum = models
            .Where(result => FindSumMultiplyOrConcatenation(result.Sum, result.List, 0))
            .Sum(result => result.Sum);

        Console.WriteLine(sum);
    }
    
    private bool FindSumMultiplyOrConcatenation(long sum, List<long> list, long current)
    {
        if (list.Count == 0)
        {
            return current == sum;
        }
        
        List<long> next = list.Skip(1).ToList();
        return FindSumMultiplyOrConcatenation(sum, next, current + list.First()) ||
               FindSumMultiplyOrConcatenation(sum, next, current * list.First()) ||
               FindSumMultiplyOrConcatenation(sum, next, long.Parse(current.ToString() + list.First()));
    }

    private class Equation
    {
        public long Sum { get; set; }
        public List<long> List { get; set; }
        
        public override string ToString()
        {
            return $"{Sum}: {String.Join(" ", List)}";
        }
    }
}
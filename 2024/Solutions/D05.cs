using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 5: Print Queue
/// </summary>
public class D05
{
    private readonly AOCHttpClient _client = new AOCHttpClient(5);

    private static HashSet<(int, int)> _rules;
    
    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"47|53
// 97|13
// 97|61
// 97|47
// 75|29
// 61|13
// 75|53
// 29|13
// 97|29
// 53|29
// 61|53
// 97|53
// 61|29
// 47|13
// 75|47
// 97|75
// 47|61
// 75|61
// 47|29
// 75|13
// 53|13
//
// 75,47,61,53,29
// 97,61,53,29,13
// 75,29,13
// 75,97,47,61,53
// 61,13,29
// 97,13,75,29,47";

        string[] data = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        _rules = data[0]
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                string[] numbers = line.Split("|");
                return (int.Parse(numbers[0]), int.Parse(numbers[1]));
            }).ToHashSet();
        
        List<Update> updates = data[1]
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => new Update()
            {
                Numbers = line.Split(',')
                    .Select(value => new Number(int.Parse(value)))
                    .ToList()
            }).ToList();
        
        List<Update> validUpdates = updates.Where(u => u.IsValid()).ToList();
        long sum = validUpdates.Sum(x => x.GetCenterNumber().Value);
        Console.WriteLine(sum);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"47|53
// 97|13
// 97|61
// 97|47
// 75|29
// 61|13
// 75|53
// 29|13
// 97|29
// 53|29
// 61|53
// 97|53
// 61|29
// 47|13
// 75|47
// 97|75
// 47|61
// 75|61
// 47|29
// 75|13
// 53|13
//
// 75,47,61,53,29
// 97,61,53,29,13
// 75,29,13
// 75,97,47,61,53
// 61,13,29
// 97,13,75,29,47";

        string[] data = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        _rules = data[0]
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                string[] numbers = line.Split("|");
                return (int.Parse(numbers[0]), int.Parse(numbers[1]));
            }).ToHashSet();
        
        List<Update> updates = data[1]
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => new Update()
            {
                Numbers = line.Split(',')
                    .Select(value => new Number(int.Parse(value)))
                    .ToList()
            }).ToList();
        
        List<Update> invalidUpdates = updates.Where(u => !u.IsValid()).ToList();
        long sum = invalidUpdates.Sum(update =>
        {
            update.Numbers.Sort(); // Sorts using a custom comparer `IComparable<Number>`
            return update.GetCenterNumber().Value;
        });
        Console.WriteLine(sum);
    }

    private class Update
    {
        public List<Number> Numbers { get; init; }

        public Number GetCenterNumber()
        {
            return Numbers[Numbers.Count / 2];
        }

        public bool IsValid()
        {
            for (int i = 0; i < Numbers.Count; i++)
            {
                for (int j = 0; j < Numbers.Count; j++)
                {
                    if (i != j)
                    {
                        if (!IsValid(Numbers[i], Numbers[j]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool IsValid(Number x, Number y)
        {
            if (_rules.Contains((x.Value, y.Value)))
            {
                int index = Numbers.IndexOf(x);
                for (int i = 0; i < index; i++)
                {
                    if (Numbers[i].Value == y.Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string ToString()
        {
            return string.Join(",", Numbers.Select(x => x.Value.ToString()));
        }
    }

    private class Number : IComparable<Number>
    {
        public int Value { get; }

        public Number(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public int CompareTo(Number other)
        {
            return _rules.Contains((this.Value, other.Value)) ? -1 : 1;
        }
    }
}
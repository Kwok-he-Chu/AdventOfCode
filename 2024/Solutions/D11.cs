using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 11: Plutonian Pebbles
/// </summary>
public class D11
{
    private readonly AOCHttpClient _client = new AOCHttpClient(11);
    private readonly Dictionary<(int, string), long> _dictionary = new Dictionary<(int, string), long>();
    
    public void Part1()
    {
        string input = _client.RetrieveFile();

        //input = @"0 1 10 99 999";
        //input = "125 17";

        List<string> list = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        long sum = list.Select(stone => Blink( stone, 25)).Sum();
        Console.WriteLine(sum);
    }

    private long Blink(string stone, int steps)
    {
        if (_dictionary.TryGetValue((steps, stone), out long memoizedValue))
        {
            return memoizedValue;
        }

        if (steps == 0)
        {
            _dictionary.TryAdd((steps, stone), 1);
            return 1;
        }

        long sum = 0;
        foreach (string transform in ApplyRules(stone))
        {
            sum += Blink(transform, steps - 1);
        }
        
        _dictionary.TryAdd((steps, stone), sum);
        
        return sum;
    }
    
    private List<string> ApplyRules(string stone)
    {
        if (stone == "0")
        {
            return new List<string> { "1" };
        }

        if (stone.Length % 2 == 0)
        {
            int half = stone.Length / 2;
            string left = long.Parse(stone.Substring(0, half)).ToString();
            string right = long.Parse(stone.Substring(half)).ToString();
            return new List<string>() { left, right };
        }

        return new List<string> { (long.Parse(stone) * 2024L).ToString() };
    }
    
    public void Part2()
    {
        string input = _client.RetrieveFile();

        //input = @"0 1 10 99 999";
        //input = "125 17";

        List<string> list = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        long sum = list.Select(stone => Blink(stone, 75)).Sum();
        Console.WriteLine(sum);
    }
}
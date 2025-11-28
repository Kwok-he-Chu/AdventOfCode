using System;

namespace AOC2025;

/// <summary>
/// Day 
/// </summary>
public class D00
{
    private readonly AOCHttpClient _client = new AOCHttpClient(0);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        input = @"";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine();
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        input = @"";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine();
    }
}
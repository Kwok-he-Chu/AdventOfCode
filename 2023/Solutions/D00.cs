using System;

namespace AOC2023;

/// <summary>
/// Day 
/// </summary>
public class D00
{
    private readonly AOCHttpClient _client = new AOCHttpClient(0);

    public void Execute1()
    {
        string input = _client.RetrieveFile();

        input = @"";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine();
    }

    public void Execute2()
    {
        string input = _client.RetrieveFile();

        input = @"";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine();
    }
}
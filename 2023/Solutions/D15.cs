using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 15: Lens Library.
/// </summary>
public class D15
{
    private readonly AOCHttpClient _client = new AOCHttpClient(15);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        //input = @"rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";

        int sum = input.Split(",", StringSplitOptions.TrimEntries)
            .Select(line =>
            {
                int result = 0;
                foreach (char c in line)
                {
                    result = (result + (int)c) * 17 % 256;
                }
                return result;
            }).Sum();

        Console.WriteLine(sum);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        //input = @"rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
        int sum = input.Split(",", StringSplitOptions.TrimEntries)
            .Select(line =>
            {
                int result = 0;
                foreach (char c in line)
                {
                    result = (result + (int)c) * 17 % 256;
                }
                return result;
            }).Sum();

        Console.WriteLine(sum);
    }
}
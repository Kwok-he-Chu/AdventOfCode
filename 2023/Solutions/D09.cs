using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 9: Mirage Maintenance.
/// </summary>
public class D09
{
    private readonly AOCHttpClient _client = new AOCHttpClient(9);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        /*input = @"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45";*/

        List<List<int>> list = ConvertInputToList(input);
        List<History> histories = ComputePyramidHistory(list);

        // Pad right.
        foreach (History history in histories)
        {
            for (int j = history.List.Count - 1; j > 0; j--)
            {
                int current = history.List[j].Last();
                int above = history.List[j - 1].Last();

                history.List[j].Add(current);
                history.List[j - 1].Add(current + above); // Sum.
            }
        }

        int result = histories.Sum(x => x.List.First().Last());
        Console.WriteLine(result);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        //input = @"10 13 16 21 30 45";

        List<List<int>> list = ConvertInputToList(input);
        List<History> histories = ComputePyramidHistory(list);

        // Uno reverse!
        foreach (History history in histories)
            foreach (List<int> l in history.List)
                l.Reverse();

        // Pad right.
        foreach (History history in histories)
        {
            for (int j = history.List.Count - 1; j > 0; j--)
            {
                int current = history.List[j].Last();
                int above = history.List[j - 1].Last();

                history.List[j].Add(current);
                history.List[j - 1].Add(above - current); // Subtract.
            }
        }

        int result = histories.Sum(x => x.List.First().Last());
        Console.WriteLine(result);
    }


    private List<List<int>> ConvertInputToList(string input)
    {
        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        
        List <List<int>> result = new List<List<int>>();
        foreach (string line in split)
        {
            List<int> history = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            result.Add(history);
        }
        return result;
    }

    private List<History> ComputePyramidHistory(List<List<int>> list)
    {
        List<History> result = new List<History>();
        foreach (List<int> history in list)
        {
            List<List<int>> pyramid = new List<List<int>>();

            List<int> current = history;
            pyramid.Add(current);

            while (current.Count != 1)
            {
                current = GetDifferences(current);
                pyramid.Add(current);
            }

            result.Add(new History(pyramid));
        }
        return result;
    }

    private List<int> GetDifferences(List<int> list)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < list.Count - 1; i++)
            result.Add(list[i + 1] - list[i]);
        return result;
    }

    private record History(List<List<int>> List)
    {
        public override string ToString()
        {
            return string.Join(" | ", List.Select(innerList => string.Join(" ", innerList)));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 
/// </summary>
public class D02
{
    private readonly AOCHttpClient _client = new AOCHttpClient(2);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"7 6 4 2 1
// 1 2 7 8 9
// 9 7 6 2 1
// 1 3 2 4 5
// 8 6 4 4 1
// 1 3 6 7 9";
        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> result = split.Select(x =>
        {
            string[] spl = x.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return spl.Select(int.Parse).ToList();
        }).Select(list => new Model() { OriginalList = list }).ToList();

        int sum = result.Count(model =>
            model.IsDecreasing(new List<int>(model.OriginalList)) ||
            model.IsIncreasing(new List<int>(model.OriginalList)));
        Console.WriteLine(sum);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();
//
//         input = @"7 6 4 2 1
// 1 2 7 8 9
// 9 7 6 2 1
// 1 3 2 4 5
// 8 6 4 4 1
// 1 3 6 7 9";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> result = split.Select(x =>
        {
            string[] spl = x.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return spl.Select(int.Parse).ToList();
        }).Select(list => new Model() { OriginalList = list }).ToList();

        int sum = result.Count(model =>
            model.IsDecreasing() ||
            model.IsIncreasing());
        Console.WriteLine(sum);
    }

    private class Model
    {
        public List<int> OriginalList;

        public bool IsIncreasing()
        {
            var linkedList = new LinkedList<int>(OriginalList);
            var result = new List<bool>();
            for (int i = 0; i < linkedList.Count; i++)
            {
                var temp = new List<int>(OriginalList);
                temp.RemoveAt(i);
                result.Add(IsIncreasing(new List<int>(temp)));
            }

            return result.Any(x => x == true);
        }

        public bool IsIncreasing(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int delta = Math.Abs(list[i] - list[i + 1]);
                if (delta <= 3 && list[i] < list[i + 1])
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public bool IsDecreasing()
        {
            var linkedList = new LinkedList<int>(OriginalList);
            var result = new List<bool>();
            for (int i = 0; i < linkedList.Count; i++)
            {
                var temp = new List<int>(OriginalList);
                temp.RemoveAt(i);
                result.Add(IsDecreasing(new List<int>(temp)));
            }

            return result.Any(x => x == true);
        }

        public bool IsDecreasing(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int delta = Math.Abs(list[i] - list[i + 1]);
                if (delta <= 3 && list[i] > list[i + 1])
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 
/// </summary>
public class D01
{
    private readonly AOCHttpClient _client = new AOCHttpClient(1);
    
    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"3   4
// 4   3
// 2   5
// 1   3
// 3   9
// 3   3";
        string[] array = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> models = new List<Model>();
        
        foreach (string item in array)
        {
            string[] split = item.Split("  ");
            models.Add(new Model()
            {
                Left = int.Parse(split[0]),
                Right = int.Parse(split[1])
            });
        }

        models.Sort((a, b) => a.Left.CompareTo(b.Left));

        long sum = 0;
        foreach (Model current in models)
        {
            Model min = models.Where(x => !x.RightSeen).MinBy(x=> x.Right);
            min.RightSeen = true;
            sum += Math.Abs(current.Left - min.Right);
        }
        
        Console.WriteLine(sum);
    }
    
    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"3   4
// 4   3
// 2   5
// 1   3
// 3   9
// 3   3";
        string[] array = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> models = new List<Model>();
        
        foreach (string item in array)
        {
            string[] split = item.Split("  ");
            models.Add(new Model()
            {
                Left = int.Parse(split[0]),
                Right = int.Parse(split[1])
            });
        }

        models.Sort((a, b) => a.Left.CompareTo(b.Left));

        long sum = 0;
        foreach (Model current in models)
        {
            int totalCount = models.Where(x => !x.RightSeen).Count(x=> x.Right == current.Left);
            sum += current.Left * totalCount;
        }
        
        Console.WriteLine(sum);
    }

    private record Model
    {
        public int Left;
        public int Right;
        public bool RightSeen;
    }
}
using System;
using System.Globalization;

namespace AOC2025;

/// <summary>
/// Day 6: Trash Compactor
/// </summary>
public class D06
{
    private readonly AOCHttpClient _client = new AOCHttpClient(6);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"123 328  51 64 
//  45 64  387 23 
//   6 98  215 314
// *   +   *   +  ";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<List<string>> list = split
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(numbers => new List<string>(numbers))
            .ToList();

        List<WorkSheet> worksheets = new List<WorkSheet>();

        for (int vertical = 0; vertical < list[0].Count; vertical++)
        {
            WorkSheet ws = new WorkSheet();

            int currentIndex = 0;
            while (currentIndex < list.Count)
            {
                string current = list[currentIndex][vertical];
                if (current == "+" || current == "*")
                {
                    ws.Operation = current;
                }
                else
                {
                    ws.Numbers.Add(long.Parse(current));
                }

                currentIndex++;
            }
            worksheets.Add(ws);
        }

        List<long> result = worksheets.Select(x => x.PerformOperation(x.Numbers)).ToList();
        Console.WriteLine(result.Sum());
    }

    private class WorkSheet
    {
        public List<long> Numbers { get; } = new List<long>();
        public List<long> CephalopodsNumbers { get; } = new List<long>();
        public string Operation { get; set; }

        public long PerformOperation(List<long> values)
        {
            switch (Operation)
            {
                case "*":
                    return values.Aggregate((x, y) => x * y);
                case "+":
                    return values.Aggregate((x, y) => x + y);
                default:
                    throw new InvalidOperationException(Operation);
            }
        }
    }
    
    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"123 328  51 64 
//  45 64  387 23 
//   6 98  215 314
// *   +   *   +  ";

        string[] split = input.Split(Environment.NewLine);
        int height = split.Length;
        int width  = split.Max(l => l.Length);
        
        char[][] grid = split
            .Select(l => l.PadRight(width))
            .Select(l => l.ToCharArray())
            .ToArray();
        
        List<WorkSheet> worksheets = new List<WorkSheet>();

        List<string> operators = split[4]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Reverse()
            .ToList();
        
        int operatorIndex = 0;
        for (int vertical = width - 1; vertical >= 0; vertical--)
        {
            if (grid.All(row => row[vertical] == ' '))
            {
                vertical--;
                continue;
            }
            
            List<int> indices = new List<int>();
            while (vertical >= 0 && grid.Any(row => char.IsDigit(row[vertical])))
            {
                indices.Add(vertical);
                vertical--;
            }

            WorkSheet ws = new WorkSheet();
            
            foreach (int c in indices)
            {
                string str = "";
                for (int r = 0; r < height - 1; r++)
                    if (char.IsDigit(grid[r][c]))
                        str += grid[r][c];
                
                ws.CephalopodsNumbers.Add(long.Parse(str));
            }

            ws.Operation = operators[operatorIndex];
            operatorIndex++;
            
            worksheets.Add(ws);
        }
        
        List<long> result = worksheets.Select(x => x.PerformOperation(x.CephalopodsNumbers)).ToList();
        Console.WriteLine(result.Sum());
    }
}
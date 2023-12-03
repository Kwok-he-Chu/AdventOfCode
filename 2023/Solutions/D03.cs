using System;
using System.Collections.Generic;

namespace AOC2023;

/// <summary>
/// Day 3: Gear Ratios.
/// </summary>
public class D03
{
    private readonly AOCHttpClient _client = new AOCHttpClient(3);

    private readonly List<(int X, int Y)> _directions = new List<(int X, int Y)>() {
        (-1,  -1), ( 0, -1), ( 1, -1),
        (-1,   0),           ( 1,  0),
        (-1,   1), ( 0,  1), ( 1,  1),
    };

    public void Execute1()
    {
        string input = _client.RetrieveFile();

        /*input = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";*/

        char[,] array = input.ConvertToCharArray();
        bool[,] seenArray = new bool[array.GetLength(0), array.GetLength(1)];

        List<(int X, int Y)> indices = FindAllSymbolIndices(array);

        int sum = 0;

        foreach ((int X, int Y) tuple in indices)
        {
            foreach ((int X, int Y) direction in _directions)
            {
                string number = GetStringNumber(array, seenArray, tuple.X + direction.X, tuple.Y + direction.Y);

                if (!string.IsNullOrEmpty(number))
                {
                    sum += int.Parse(number);
                }
            }
        }

        Console.WriteLine(sum);
    }

    private string GetStringNumber(char[,] array, bool[,] seenArray, int x, int y)
    {
        // Out of bounds.
        if (!array.IsWithinBounds(x, y))
        {
            return string.Empty;
        }

        // We've seen this digit already.
        if (seenArray[x, y])
        {
            return string.Empty;
        }

        // Empty space, nothing to see here.
        if (array[x, y] == '.')
        {
            return string.Empty;
        }

        // Not a digit.
        if (!char.IsDigit(array[x, y]))
        {
            return string.Empty;
        }

        seenArray[x, y] = true;

        // Reads string literals recursively: left<- 2 ->right
        return GetStringNumber(array, seenArray, x - 1, y) + array[x, y] + GetStringNumber(array, seenArray, x + 1, y);
    }

    private List<(int X, int Y)> FindAllSymbolIndices(char[,] array)
    {
        List<(int X, int Y)> result = new();

        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                if (!char.IsDigit(array[x, y]) && array[x, y] != '.') // Symbol check.
                {
                    result.Add((x, y));
                }
            }
        }
        return result;
    }

    public void Execute2()
    {
        string input = _client.RetrieveFile();

        /*input = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";*/

        char[,] array = input.ConvertToCharArray();
        bool[,] seenArray = new bool[array.GetLength(0), array.GetLength(1)];

        List<(int X, int Y)> indices = FindAsterikSymbolIndices(array);

        int sum = 0;

        foreach ((int X, int Y) tuple in indices)
        {
            List<string> gearNumbers = new List<string>();
            foreach ((int X, int Y) direction in _directions)
            {
                string number = GetStringNumber(array, seenArray, tuple.X + direction.X, tuple.Y + direction.Y);
                
                if (!string.IsNullOrEmpty(number))
                {
                    gearNumbers.Add(number);
                }
            }

            if (gearNumbers.Count == 2)
            {
                sum += int.Parse(gearNumbers[0]) * int.Parse(gearNumbers[1]);
            }
        }

        Console.WriteLine(sum);
    }

    private List<(int X, int Y)> FindAsterikSymbolIndices(char[,] array)
    {
        List<(int X, int Y)> result = new();

        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                if (array[x, y] == '*')
                {
                    result.Add((x, y));
                }
            }
        }
        return result;
    }
}
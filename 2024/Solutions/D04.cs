using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 4: Ceres Search
/// </summary>
public class D04
{
    private readonly AOCHttpClient _client = new AOCHttpClient(4);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"MMMSXXMASM
// MSAMXMSMSA
// AMXSXMAAMM
// MSAMASMSMX
// XMASAMXAMM
// XXAMMXXAMA
// SMSMSASXSS
// SAXAMASAAA
// MAMMMXMMMM
// MXMXAXMASX";

        char[,] array = input.ConvertToCharArray();

        long sum = 0;
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                if (array[x, y] == 'X')
                {
                    var neighbors = GetNeighborIndices(array, x, y);
                    foreach (List<(int X, int Y)> n in neighbors)
                    {
                        char M = array[n[0].X, n[0].Y];
                        char A = array[n[1].X, n[1].Y];
                        char S = array[n[2].X, n[2].Y];
                        if (M == 'M' && A == 'A' && S == 'S')
                        {
                            sum++;
                        }
                    }
                }
            }
        }

        Console.WriteLine(sum);
    }
    
    private readonly List<(int X, int Y)> _directions = new List<(int X, int Y)>() {
        (-1,  -1), ( 0, -1), ( 1, -1),
        (-1,   0),           ( 1,  0),
        (-1,   1), ( 0,  1), ( 1,  1),
    };
    
    private List<List<(int, int)>> GetNeighborIndices(char[,] array, int x, int y)
    {
        var result = new List<List<(int, int)>>();
        foreach ((int X, int Y) dir in _directions)
        {
            var temp = new List<(int, int)>();
            for (int i = 1; i < 4; i++)
            {
                int newX = x + dir.X * i;
                int newY = y + dir.Y * i;
                if (array.IsWithinBounds(newX, newY))
                {
                    temp.Add((newX, newY));
                }
            }

            if (temp.Count == 3) // Only detect word length of 3, e.g. -"MAS"
            {
                result.Add(temp);
            }
        }

        return result;
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"MMMSXXMASM
// MSAMXMSMSA
// AMXSXMAAMM
// MSAMASMSMX
// XMASAMXAMM
// XXAMMXXAMA
// SMSMSASXSS
// SAXAMASAAA
// MAMMMXMMMM
// MXMXAXMASX";

        char[,] array = input.ConvertToCharArray();

        long sum = 0;
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                if (array[x, y] == 'A')
                {
                    List<(int X, int Y)> corners = GetCornerIndices(array, x, y);
                    if (corners.Count != 4)
                    {
                        continue;
                    }
                    
                    char topLeft = array[corners[0].X, corners[0].Y];
                    char topRight = array[corners[1].X, corners[1].Y];
                    char bottomLeft = array[corners[2].X, corners[2].Y];
                    char bottomRight = array[corners[3].X, corners[3].Y];

                    // Beautiful :') <3
                    if (topLeft == 'M' && topRight == 'M' &&
                        bottomLeft == 'S' && bottomRight == 'S')
                    {
                        sum++;
                    }  
                    else if (topLeft == 'S' && topRight == 'M' &&
                        bottomLeft == 'S' && bottomRight == 'M')
                    {
                        sum++;
                    }
                    else if (topLeft == 'S' && topRight == 'S' &&
                        bottomLeft == 'M' && bottomRight == 'M')
                    {
                        sum++;
                    }
                    else if (topLeft == 'M' && topRight == 'S' &&
                        bottomLeft == 'M' && bottomRight == 'S')
                    {
                        sum++;
                    }
                }
            }
        }

        Console.WriteLine(sum);
    }
    
    private readonly List<(int X, int Y)> _corners = new List<(int X, int Y)>() {
        (-1,  -1),           ( 1, -1),
        
        (-1,   1),           ( 1,  1),
    };
    
    private List<(int, int)> GetCornerIndices(char[,] array, int x, int y)
    {
        var result = new List<(int, int)>();
        foreach ((int X, int Y) c in _corners)
        {
            int newX = x + c.X;
            int newY = y + c.Y;
            if (array.IsWithinBounds(newX, newY))
            {
                result.Add((newX, newY));
            }
        }

        return result;
    }
}
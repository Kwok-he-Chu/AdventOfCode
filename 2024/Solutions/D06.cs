using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 6: Guard Gallivant
/// </summary>
public class D06
{
    private readonly AOCHttpClient _client = new AOCHttpClient(6);

    private readonly Dictionary<char, (int, int)> _directions = new Dictionary<char, (int, int)>
    {
        { '^', (0, -1) },
        { '>', (1, 0) },
        { 'v', (0, 1) },
        { '<', (-1, 0) },
    };
    
    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"....#.....
// .........#
// ..........
// ..#.......
// .......#..
// ..........
// .#..^.....
// ........#.
// #.........
// ......#...";

        char[,] array = input.ConvertToCharArray();
        (int X, int Y) startPosition = FindSymbol(array, '^');
        (bool[,] Array, bool _) seen = GetTraversedArray(array, startPosition);
        int sum = seen.Array.Cast<bool>().Count(b => b == true);
        Console.WriteLine(sum);
    }

    private (bool[,] Array, bool IsCycle) GetTraversedArray(char[,] array, (int X, int Y) currentPosition)
    {
        Dictionary<(int, int), int> dictionary = new Dictionary<(int, int), int>();
        bool[,] result = new bool[array.GetLength(0), array.GetLength(1)];
        result[currentPosition.X, currentPosition.Y] = true;
        
        char[] symbols = { '^', '>', 'v', '<' };
        int currentSymbolIndex = Array.IndexOf(symbols, array[currentPosition.X, currentPosition.Y]);

        while (true)
        {
            char currentSymbol = symbols[currentSymbolIndex];
            (int X, int Y) dir = _directions[currentSymbol];
            (int X, int Y) newPosition = (currentPosition.X + dir.X, currentPosition.Y + dir.Y);

            if (ContainsHashtag(array, newPosition))
            {
                currentSymbolIndex = (currentSymbolIndex + 1) % 4;
                continue;
            }
            
            if (array.IsWithinBounds(newPosition.X, newPosition.Y))
            {
                currentPosition = newPosition;
                
                if (dictionary.TryGetValue(currentPosition, out int timesSeen))
                {
                    dictionary[currentPosition] += 1;
                }
                else
                {
                    dictionary.Add(currentPosition, 1);
                }
                
                result[currentPosition.X, currentPosition.Y] = true;

                if (timesSeen >= 100)
                {
                    // You've looped it 100 times... Time to stop :')
                    return (result, true);
                }
                continue;
            }
            
            return (result, false);
        }
    }

    private bool ContainsHashtag(char[,] array, (int X, int Y) currentPosition)
    {
        if (!array.IsWithinBounds(currentPosition.X, currentPosition.Y))
        {
            return false;
        }
        
        char currentSymbol = array[currentPosition.X, currentPosition.Y];

        if (currentSymbol == '#')
        {
            return true;
        }

        return false;
    }

    private (int X, int Y) FindSymbol(char[,] array, char symbol)
    {
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                if (array[x,y] == symbol) // Symbol check.
                {
                    return (x, y);
                }
            }
        }

        throw new ArgumentException("Could not find symbol");
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"....#.....
// .........#
// ..........
// ..#.......
// .......#..
// ..........
// .#..^.....
// ........#.
// #.........
// ......#...";

        char[,] array = input.ConvertToCharArray();

        (int X, int Y) startPosition = FindSymbol(array, '^');
        (bool[,] Array, bool _) seen = GetTraversedArray(array, startPosition);
        
        HashSet<(int i, int j)> set = Enumerable.Range(0, seen.Array.GetLength(0))
            .SelectMany(i => Enumerable.Range(0, seen.Array.GetLength(1))
                .Where(j => seen.Array[i, j])
                .Select(j => (i, j)))
            .ToHashSet();
        set.Remove(startPosition);

        long sum = 0;
        foreach ((int X, int Y) s in set)
        {
            char[,] copy = new char[array.GetLength(0), array.GetLength(1)];
            Array.Copy(array, copy, array.Length);
            copy[s.X, s.Y] = '#';
            
            (bool[,] Array, bool IsCycle) result = GetTraversedArray(copy, startPosition);
            if (result.IsCycle)
            {
                sum++;
            }
        }
        Console.WriteLine(sum);
    }
}
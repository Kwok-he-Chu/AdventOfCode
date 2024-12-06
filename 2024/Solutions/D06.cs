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
    
    private readonly char[] _symbols = { '^', '>', 'v', '<' };

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
        (int X, int Y) startPosition = array.FindFirst('^');
        (HashSet<(int, int)> Set, bool _) seen = GetSeenPositions(array, startPosition);
        Console.WriteLine(seen.Set.Count);
    }

    private (HashSet<(int, int)> Set, bool IsCycle) GetSeenPositions(char[,] array, (int X, int Y) currentPosition)
    {
        HashSet<(int, int)> set = new HashSet<(int, int)>()
        {
            currentPosition
        };

        int currentSymbolIndex = Array.IndexOf(_symbols, array[currentPosition.X, currentPosition.Y]);

        int steps = 0;
        while (true)
        {
            char currentSymbol = _symbols[currentSymbolIndex];
            (int X, int Y) dir = _directions[currentSymbol];
            (int X, int Y) newPosition = (currentPosition.X + dir.X, currentPosition.Y + dir.Y);

            if (!array.IsWithinBounds(newPosition))
            {
                return (set, false);
            }

            if (array[newPosition.X, newPosition.Y] == '#')
            {
                currentSymbolIndex = (currentSymbolIndex + 1) % 4;
                continue;
            }

            currentPosition = newPosition;
            if (set.Contains(currentPosition))
            {
                steps++;
            }

            set.Add(currentPosition);

            // The Guard is tired, let the man rest :')
            if (steps >= 1000)
            {
                return (set, true);
            }
        }
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
        (int X, int Y) startPosition = array.FindFirst('^');
        (HashSet<(int, int)> Set, bool _) initial = GetSeenPositions(array, startPosition);
        initial.Set.Remove(startPosition);

        long sum = 0;
        foreach ((int X, int Y) s in initial.Set)
        {
            char[,] copy = new char[array.GetLength(0), array.GetLength(1)];
            Array.Copy(array, copy, array.Length);
            copy[s.X, s.Y] = '#';

            (HashSet<(int, int)> Set, bool IsCycle) seen = GetSeenPositions(copy, startPosition);
            if (seen.IsCycle)
            {
                sum++;
            }
        }
        Console.WriteLine(sum);
    }
}
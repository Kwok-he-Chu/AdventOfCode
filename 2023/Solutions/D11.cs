using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 11: Cosmic Expansion.
/// </summary>
public class D11
{
    private readonly AOCHttpClient _client = new AOCHttpClient(11);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        /*input = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....";*/

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        List<List<char>> map = split.Select(line => line.ToCharArray().ToList()).ToList();
        InsertRows(map);
        InsertColumns(map);

        List<Coordinate> coordinates = GetGalaxyCoordinates(map);
        long sum = 0;
        for (int i = 0; i < coordinates.Count - 1; i++)
        {
            for (int j = i + 1; j < coordinates.Count; j++)
            {
                sum += Math.Abs(coordinates[i].X - coordinates[j].X) + Math.Abs(coordinates[i].Y - coordinates[j].Y); 
            }
        }

        Console.WriteLine(sum);
    }

    private List<Coordinate> GetGalaxyCoordinates(List<List<char>> map)
    {
        List<Coordinate> galaxies = new List<Coordinate>();
        for (int x = 0; x < map.Count; x++)
        {
            for (int y = 0; y < map[x].Count; y++)
            {
                if (map[x][y] == '#')
                {
                    galaxies.Add(new Coordinate(x, y));
                }
            }
        }
        return galaxies;
    }


    private void InsertRows(List<List<char>> map)
    {
        for (int x = 0; x < map[1].Count; x++)
        {
            if (map.All(c => c[x] == '.'))
            {
                for (int i = 0; i < map.Count; i++)
                {
                    map[i].Insert(x, '.');
                }
                x++;
            }
        }
    }

    private void InsertColumns(List<List<char>> map)
    {
        for (int y = 0; y < map.Count; y++)
        {
            if (map[y].All(spot => spot == '.'))
            {
                map.Insert(y, map[y].ToList());
                y++;
            }
        }
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        /*input = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....";*/

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        List<List<char>> map = split.Select(line => line.ToCharArray().ToList()).ToList();
        List<int> rowIndices = GetInsertedRowIndices(map);
        List<int> columnIndices = GetInsertedColumnIndices(map);

        List<Coordinate> coordinates = GetGalaxyCoordinates(map);
        long sum = 0;
        for (int i = 0; i < coordinates.Count - 1; i++)
        {
            for (int j = i + 1; j < coordinates.Count; j++)
            {
                int minX = Math.Min(coordinates[i].X, coordinates[j].X);
                int maxX = Math.Max(coordinates[i].X, coordinates[j].X);
                int minY = Math.Min(coordinates[i].Y, coordinates[j].Y);
                int maxY = Math.Max(coordinates[i].Y, coordinates[j].Y);
                sum += maxX - minX + maxY - minY
                    + rowIndices.Count(row => row > minY && row < maxY) * (1000000 - 1)
                    + columnIndices.Count(column => column > minX && column < maxX) * (1000000 - 1);
            }
        }

        Console.WriteLine(sum);
    }


    private List<int> GetInsertedRowIndices(List<List<char>> map)
    {
        List<int> result = new List<int>();
        for (int x = 0; x < map[1].Count; x++)
        {
            if (map.All(c => c[x] == '.'))
            {
                result.Add(x);
            }
        }
        return result;
    }

    private List<int> GetInsertedColumnIndices(List<List<char>> map)
    {
        List<int> result = new List<int>();
        for (int y = 0; y < map.Count; y++)
        {
            if (map[y].All(spot => spot == '.'))
            {
                result.Add(y);
            }
        }
        return result;
    }

    private record Coordinate(int X, int Y);
}
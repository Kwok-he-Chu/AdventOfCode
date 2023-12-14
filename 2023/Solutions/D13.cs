﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 13: Point of Incidence.
/// </summary>
public class D13
{
    private readonly AOCHttpClient _client = new AOCHttpClient(13);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        /*input = @"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#";*/

        string[] split = input.Split(Environment.NewLine);
        List<Grids> grids = new List<Grids>();

        List<string> list = new List<string>();
        for (int i = 0; i < split.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(split[i]))
            {
                grids.Add(new Grids(list));
                list = new List<string>();
                i++;
                continue;
            }

            list.Add(split[i]);

            // Append the remaining result
            if (i == split.Length - 1)
            {
                grids.Add(new Grids(list));
            }
        }

        int sum = 0;
        foreach (Grids grid in grids)
        {
            int rowNumber = GetSymmetricalValue(grid.Grid) * 100;
            int columNumber = GetSymmetricalValue(grid.GridFlipped);

            sum += rowNumber + columNumber;
        }
        Console.WriteLine(sum);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        input = @"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#";

        string[] split = input.Split(Environment.NewLine);
        List<Grids> grids = new List<Grids>();

        List<string> list = new List<string>();
        for (int i = 0; i < split.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(split[i]))
            {
                grids.Add(new Grids(list));
                list = new List<string>();
                continue;
            }

            list.Add(split[i]);

            // Append the last grid if we reach end of array.
            if (i == split.Length - 1)
            {
                grids.Add(new Grids(list));
            }
        }
    }

    private int GetSymmetricalValue(List<string> grid)
    {
        int center = grid.Count / 2;

        if (grid.Count % 2 == 0) // Even.
        {
            var top = grid.Splice(0, center).ToList();
            var bot = grid.Splice(center + 1, center).Reverse().ToList();
            
            Console.WriteLine("even");
            Console.WriteLine();
            Console.WriteLine(string.Join("\n", top));
            Console.WriteLine();
            Console.WriteLine(string.Join("\n", bot));
            Console.WriteLine();

            if (top.SequenceEqual(bot))
            {
                return center;
            }
            
            return 0;
        }
        else // Uneven.
        {
            var top = grid.Splice(0, center).ToList();
            var bot = grid.Splice(center + 1, center).Reverse().ToList();
            
            Console.WriteLine("v1");
            Console.WriteLine();
            Console.WriteLine(string.Join("\n", top));
            Console.WriteLine();
            Console.WriteLine(string.Join("\n", bot));
            Console.WriteLine();
            
            if (top.SequenceEqual(bot))
            {
                return center;
            }
            
            top = grid.Splice(1, center).ToList();
            bot = grid.Splice(center + 1, center).Reverse().ToList();

            Console.WriteLine("v2");
            Console.WriteLine(string.Join("\n", top));
            Console.WriteLine();
            Console.WriteLine(string.Join("\n", bot));
            Console.WriteLine();

            if (top.SequenceEqual(bot))
            {
                return center + 1;
            }
            
            return 0;
        }

    }

    private class Grids
    {
        public List<string> Grid { get; }
        public List<string> GridFlipped { get; }

        public Grids(List<string> grid)
        {
            Grid = grid;
            GridFlipped = new List<string>();

            for (int i = 0; i < Grid[0].Length; i++)
            {
                GridFlipped.Add(string.Empty);
            }

            for (int i = 0; i < GridFlipped.Count; i++)
            {
                for (int j = 0; j < Grid.Count; j++)
                {
                    GridFlipped[i] += Grid[j][i];
                }
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Grid) + Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine, GridFlipped);
        }
    }
}
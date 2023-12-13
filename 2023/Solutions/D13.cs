using System;
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

            // Append the remaining result
            if (i == split.Length - 1)
            {
                grids.Add(new Grids(list));
            }
        }

        //foreach (var grid in grids)
        //{
        //    Console.WriteLine(grid);
        //    Console.WriteLine();
        //}

        int sum = 0;
        foreach (Grids grid in grids)
        {
            sum += GetSymmetricalValue(grid, 0);
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

    private int GetSymmetricalValue(Grids grids, int column) // WIP
    {
        for (int i = 3; i < grids.Grid.Count - 3; i += 1)
        {
            string gridA1 = grids.Grid[i - 3];
            string gridB1 = grids.Grid[i - 2];
            string gridC1 = grids.Grid[i - 1];
            string gridC2 = grids.Grid[i];
            string gridB2 = grids.Grid[i + 1];
            string gridA2 = grids.Grid[i + 2];
            if (gridA1 == gridA2 && gridB1 == gridB2 && gridC1 == gridC2)
            {
                return i + 1;
            }
        }

        for (int i = 3; i < grids.GridFlipped.Count - 3; i += 1)
        {
            string gridA1 = grids.GridFlipped[i - 3];
            string gridB1 = grids.GridFlipped[i - 2];
            string gridC1 = grids.GridFlipped[i - 1];
            string gridC2 = grids.GridFlipped[i];
            string gridB2 = grids.GridFlipped[i + 1];
            string gridA2 = grids.GridFlipped[i + 2];
            if (gridA1 == gridA2 && gridB1 == gridB2 && gridC1 == gridC2)
            {
                return (i + 1) * 100;
            }
        }

        return -1;
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
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

        List<Pattern> patterns = ConvertInputToList(input);

        int sum = 0;
        foreach (Pattern grid in patterns)
        {
            List<(int Size, int Index)> columns = GetMirrors(grid.Line)
                .OrderByDescending(x => x.Size)
                .ToList();
            List<(int Size, int Index)> rows = GetMirrors(FlipStringList(grid.Line))
                .OrderByDescending(x => x.Size)
                .ToList();

            if (columns.Count > 0)
            {
                sum += columns.First().Index * 100;
            }
            else if (rows.Count > 0)
            {
                sum += rows.First().Index;
            }
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
#....#..#

.#.##.#.#
.##..##..
.#.##.#..
#......##
#......##
.#.##.#..
.##..##.#

#..#....#
###..##..
.##.#####
.##.#####
###..##..
#..#....#
#..##...#

#.##..##.
..#.##.#.
##..#...#
##...#..#
..#.##.#.
..##..##.
#.#.##.#.";

        /*input = @"###.##.##
##.####.#
##.#..#.#
####..###
....##...
##.#..#.#
...#..#..
##..###.#
##......#
##......#
..#.##.#.
...#..#..
##.####.#
....##...
...####..
....##...
##.####.#

.##.##...##...##.
#####..##..##..##
.....##..##..##..
.##.#.#.####.#.#.
.##...#.#..#.#...
....#..........#.
#..#..#......#..#
....###.....####.
.##...#.#..#.#...
.....#..####..#..
#..#...##..##...#
....#...#..#...#.
#..#.##########.#
#..##...####...##
#####.##.##.##.##";*/

        List<Pattern> patterns = ConvertInputToList(input);

        int sum = 0;
        foreach (Pattern grid in patterns)
        {
            List<(int Size, int Index)> columns = GetMirrors(grid.Line)
                .OrderByDescending(x => x.Size)
                .ToList();
            List<(int Size, int Index)> rows = GetMirrors(FlipStringList(grid.Line))
                .OrderByDescending(x => x.Size)
                .ToList();

            if (columns.Count > 0)
            {
                sum += columns.First().Index * 100;
            }
            else if (rows.Count > 0)
            {
                sum += rows.First().Index;
            }
        }
        Console.WriteLine(sum);
    }

    private List<Pattern> ConvertInputToList(string input)
    {
        string[] split = input.Split(Environment.NewLine);
        List<Pattern> patterns = new List<Pattern>();

        List<string> lines = new List<string>();
        for (int i = 0; i < split.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(split[i]) && lines.Count > 0)
            {
                patterns.Add(new Pattern(lines));
                lines = new List<string>();
                continue;
            }

            //list.Add(split[i].Replace('.', '0').Replace('#', '1'));
            lines.Add(split[i]);

            // Append the remaining result if the input.txt doesn't end with a '\n'
            if (i == split.Length - 1 && lines.Count > 0)
            {
                patterns.Add(new Pattern(lines));
            }
        }

        return patterns;
    }

    private IEnumerable<(int Size, int Index)> GetMirrors(List<string> line)
    {
        List<(int Size, int Index)> mirrors = new List<(int Size, int Index)>();

        for (int i = 0; i < line.Count - 1; i++)
        {
            if (line[i] == line[i + 1])
            {
                int min = Math.Min(i, line.Count - i - 2);

                for (var j = 0; j <= min; j++)
                {
                    if (line[i - j] != line[i + j + 1])
                    {
                        break;
                    }

                    if (j == min)
                    {
                        mirrors.Add((min, i + 1));
                    }
                }
            }
        }

        return mirrors;
    }


    /// <summary>
    /// #### <
    /// ....
    /// ....
    /// ....
    /// 
    /// -> becomes ->
    /// 
    /// v
    /// #...
    /// #...
    /// #...
    /// #...
    /// </summary>
    private List<string> FlipStringList(List<string> line)
    {
        List<string> flippedResult = new List<string>();
        for (int i = 0; i < line[0].Length; i++)
        {
            flippedResult.Add(string.Empty);
        }

        for (int i = 0; i < flippedResult.Count; i++)
        {
            for (int j = 0; j < line.Count; j++)
            {
                flippedResult[i] += line[j][i];
            }
        }

        return flippedResult;
    }

    private class Pattern
    {
        public List<string> Line { get; }

        public Pattern(List<string> line)
        {
            Line = line;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Line);
        }
    }
}
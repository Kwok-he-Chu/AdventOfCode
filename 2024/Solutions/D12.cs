using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 12: Garden Groups
/// </summary>
public class D12
{
    private readonly AOCHttpClient _client = new AOCHttpClient(12);

    private List<Vector> _directions = new List<Vector>()
    {
        Vector.East,
        Vector.North,
        Vector.South,
        Vector.West
    };

    public void Part1()
    {
        string input = _client.RetrieveFile();

        //   input = @"AAAA
        // BBCD
        // BBCC
        // EEEC";

//         input = @"EEEEE
// EXXXX
// EEEEE
// EXXXX
// EEEEE";        

//         input = @"AAAAAA
// AAABBA
// AAABBA
// ABBAAA
// ABBAAA
// AAAAAA";

        char[,] list = input.ConvertToCharArray();

        int height = list.GetLength(0);
        int width = list.GetLength(1);

        Plot[,] plots = new Plot[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                plots[x, y] = new Plot(list[x, y], x, y);
            }
        }

        List<(HashSet<Plot> Plots, int Perimeter)> result = ComputePerimeters(plots);
        int sum = result.Sum(x => x.Perimeter * x.Plots.Count);
        Console.WriteLine(sum);
    }

    private List<(HashSet<Plot> Plots, int Perimeter)> ComputePerimeters(Plot[,] input)
    {
        List<(HashSet<Plot> Plots, int Perimeter)> result = new List<(HashSet<Plot>, int)>();

        Dictionary<char, List<Plot>> temp = new Dictionary<char, List<Plot>>();
        for (int y = 0; y < input.GetLength(0); y++)
        {
            for (int x = 0; x < input.GetLength(1); x++)
            {
                if (temp.TryGetValue(input[x, y].Label, out List<Plot> existing))
                {
                    existing.Add(input[x, y]);
                }
                else
                {
                    temp.Add(input[x, y].Label, new List<Plot>() { input[x, y] });
                }
            }
        }

        List<Plot> plots = temp.Values.SelectMany(list => list).ToList();

        while (plots.Count > 0)
        {
            Plot current = plots.First();
            plots.Remove(current);

            HashSet<Plot> fences = new HashSet<Plot> { current };

            int perimeter = 0;

            Stack<Plot> stack = new Stack<Plot>();
            stack.Push(current);

            while (stack.Count > 0)
            {
                Plot plot = stack.Pop();

                foreach (Vector direction in _directions)
                {
                    Vector newPosition = plot.Location + direction;

                    if (!input.IsWithinBounds(newPosition))
                    {
                        perimeter++;
                        continue;
                    }

                    Plot neighbour = input[newPosition.X, newPosition.Y];

                    if (fences.Contains(neighbour))
                    {
                        continue;
                    }

                    if (neighbour.Label == plot.Label)
                    {
                        plots.Remove(neighbour);
                        stack.Push(neighbour);
                        fences.Add(neighbour);
                        continue;
                    }

                    perimeter++;
                }
            }

            result.Add((fences, perimeter));
        }

        return result;
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        input = @"AAAA
BBCD
BBCC
EEEC";

        input = @"RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE";

        char[,] list = input.ConvertToCharArray();

        int height = list.GetLength(0);
        int width = list.GetLength(1);

        Plot[,] plots = new Plot[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                plots[x, y] = new Plot(list[x, y], x, y);
            }
        }

        List<(HashSet<Plot> Plots, int Perimeter)> result = ComputePerimeters(plots);
        var sides = ComputeSides(result);
        int sum = result.Sum(x => x.Perimeter * x.Plots.Count);
        Console.WriteLine(sum);
    }
    
    private int ComputeSides(List<(HashSet<Plot> Plots, int Perimeter)> result)
    {
        return 1; // TODO
    }

    private class Plot
    {
        public char Label { get; }
        public Vector Location { get; }

        public Plot(char label, int x, int y)
        {
            Label = label;
            Location = new Vector(x, y);
        }

        public override string ToString()
        {
            return $"{Label} {Location}";
        }
    }
}
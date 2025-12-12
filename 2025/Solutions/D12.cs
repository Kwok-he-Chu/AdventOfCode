using System;

namespace AOC2025;

/// <summary>
/// Day 12: Christmas Tree Farm
/// </summary>
public class D12
{
    private readonly AOCHttpClient _client = new AOCHttpClient(12);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        input = @"0:
###
##.
##.

1:
###
##.
.##

2:
.##
###
##.

3:
##.
###
##.

4:
###
#..
###

5:
###
.#.
###

4x4: 0 0 0 0 2 0
12x5: 1 0 1 0 2 2
12x5: 1 0 1 0 3 2";

        string[] split = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Shape> shapes = new List<Shape>();
        for (int i = 0; i < split.Length - 1; i++)
        {
            string shapeLines = split[i];

            string[] kvp = shapeLines.Split(':', StringSplitOptions.RemoveEmptyEntries);
            int index = int.Parse(kvp[0]);
            char[,] grid = kvp[1].ConvertToCharArray();
            shapes.Add(new Shape(index, grid));
        }

        string[] lastLineSplit = split[split.Length - 1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Region> regions = new List<Region>();
        for (int i = 0; i < lastLineSplit.Length; i++)
        {
            string regionLines = lastLineSplit[i];   
            string[] kvp = regionLines.Split(':', StringSplitOptions.RemoveEmptyEntries);

            string[] dimension = kvp[0].Split('x', StringSplitOptions.RemoveEmptyEntries);
            Vector dimensionVector = new Vector(int.Parse(dimension[0]), int.Parse(dimension[1]));
            List<int> quantities = kvp[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList(); 
            
            Region region = new Region(dimensionVector, new Dictionary<int, int>());
            for (int index = 0; index < quantities.Count; index++)
            {
                region.Quantities.Add(index, quantities[index]);
            }
            regions.Add(region);
        }
        
        Console.WriteLine();
    }

    private class Region
    {
        public Region(Vector dimension, Dictionary<int, int> quantities)
        {
            Dimension = dimension;
            Quantities = quantities;
        }

        public Vector Dimension { get; set; }
        public Dictionary<int, int> Quantities { get; set; }
    }

    private class Shape
    {
        public int Index { get; set; } 
        public char[,] Grid { get; set; } 
        
        public Shape(int index, char[,] grid)
        {
            Index = index;
            Grid = grid;
        }
    }
    
    public void Part2()
    {
        string input = _client.RetrieveFile();

        input = @"";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 8: Resonant Collinearity
/// </summary>
public class D08
{
    private readonly AOCHttpClient _client = new AOCHttpClient(8);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"............
// ........0...
// .....0......
// .......0....
// ....0.......
// ......A.....
// ............
// ............
// ........A...
// .........A..
// ............
// ............";

        char[,] array = input.ConvertToCharArray();
        List<Antenna> list = new List<Antenna>();
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                list.Add(new Antenna()
                {
                    Frequency = array[x, y],
                    Location = new Vector(x, y)
                });
            }
        }

        HashSet<Vector> result = CreateAntinodes(
            list: list.Where(x => x.Frequency != '.').ToList(),
            array: array,
            isPart2: false
        );
        
        Console.WriteLine(result.Count);
    }

    private HashSet<Vector> CreateAntinodes(List<Antenna> list, char[,] array, bool isPart2)
    {
        HashSet<Vector> result = new HashSet<Vector>();
        foreach (Antenna a in list)
        {
            foreach (Antenna b in list)
            {
                if (a == b)
                    continue;

                if (a.Frequency != b.Frequency)
                    continue;

                HashSet<Vector> locations = GetLocationsWithinBounds(array, a.Location, b.Location, isPart2);
                foreach (Vector location in locations)
                {
                    result.Add(location);
                }
            }
        }

        return result;
    }

    private HashSet<Vector> GetLocationsWithinBounds(char[,] array, Vector a, Vector b, bool isPart2)
    {
        var set = new HashSet<Vector>();
        
        if (isPart2)
        {
            Vector delta = b - a;
            Vector newA = a + delta;
            while (array.IsWithinBounds(newA))
            {
                set.Add(newA);
                newA += delta;
            }
            
            Vector newB = b - delta;
            while (array.IsWithinBounds(newB))
            {
                set.Add(newB);
                newB -= delta;
            }
        }
        else
        {
            Vector delta = a - b;
            Vector newA = a + delta;
            if (array.IsWithinBounds(newA))
            {
                set.Add(newA);
            }
            
            Vector newB = b - delta;
            if (array.IsWithinBounds(newB))
            {
                set.Add(newB);
            }
        }

        return set;
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"............
// ........0...
// .....0......
// .......0....
// ....0.......
// ......A.....
// ............
// ............
// ........A...
// .........A..
// ............
// ............";

        char[,] array = input.ConvertToCharArray();
        List<Antenna> list = new List<Antenna>();
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                list.Add(new Antenna()
                {
                    Frequency = array[x, y],
                    Location = new Vector(x, y)
                });
            }
        }

        HashSet<Vector> result = CreateAntinodes(
            list: list.Where(x => x.Frequency != '.').ToList(),
            array: array,
            isPart2: true
        );
        
        Console.WriteLine(result.Count);
    }

    private class Antenna
    {
        public char Frequency { get; init; }
        public Vector Location { get; init; }
    }
}
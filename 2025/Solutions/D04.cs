using System;

namespace AOC2025;

/// <summary>
/// Day 4: Printing Department
/// </summary>
public class D04
{
    private readonly AOCHttpClient _client = new AOCHttpClient(4);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"..@@.@@@@.
// @@@.@.@.@@
// @@@@@.@.@@
// @.@@@@..@.
// @@.@@@@.@@
// .@@@@@@@.@
// .@.@.@.@@@
// @.@@@.@@@@
// .@@@@@@@@.
// @.@.@@@.@.";

        char[,] array = input.ConvertToCharArray();
        
        List<Vector> result = new List<Vector>();
        array.ForEach((x, y, value) =>
        {
            if (value == '@')
            {
                List<Vector> neighbours = GetNeighbours8(array, x, y);
                if (neighbours.Count < 4)
                    result.Add(new Vector(x, y));
            }
        });
        
        Console.WriteLine(result.Count);
    }

    private List<Vector> GetNeighbours8(char[,] array, int x, int y)
    {
        List<Vector> result = new List<Vector>();
        foreach (Vector neighbour in Vector.Directions8)
            if (array.IsWithinBounds(neighbour.X + x, neighbour.Y + y) && array[neighbour.X + x, neighbour.Y + y] == '@')
                result.Add(new Vector(neighbour.X + x, neighbour.Y + y));
        return result;
    }
    
    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"..@@.@@@@.
// @@@.@.@.@@
// @@@@@.@.@@
// @.@@@@..@.
// @@.@@@@.@@
// .@@@@@@@.@
// .@.@.@.@@@
// @.@@@.@@@@
// .@@@@@@@@.
// @.@.@@@.@.";

        char[,] array = input.ConvertToCharArray();

        int steps = 0;

        int total = 0;
        while (steps < 99)
        {
            List<Vector> result = new List<Vector>();
            
            array.ForEach((x, y, value) =>
            {
                if (value == '@')
                {
                    List<Vector> neighbours = GetNeighbours8(array, x, y);
                    if (neighbours.Count < 4)
                        result.Add(new Vector(x, y));
                }
            });

            foreach (Vector r in result)
            {
                array[r.X, r.Y] = '.';
            }
            
            steps++;
            
            total += result.Count;
            //Console.WriteLine(result.Count);
        }
        
        Console.WriteLine(total);
    }
}
using System;
using System.Collections.Generic;

namespace AOC2024;

/// <summary>
/// Day 10: Hoof It
/// </summary>
public class D10
{
    private readonly AOCHttpClient _client = new AOCHttpClient(10);
    
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

//         input = @"89010123
// 78121874
// 87430965
// 96549874
// 45678903
// 32019012
// 01329801
// 10456732";

        char[,] array = input.ConvertToCharArray();

        long sum = 0;
        List<(int X, int Y)> startPositions = array.FindAll('0');
        foreach ((int X, int Y) pos in startPositions)
        {
            bool[,] seen = new bool[array.GetLength(0), array.GetLength(1)];
            List<List<Vector>> result = Traverse(array, new Vector(pos), seen);
            sum += result.Count;
        }

        Console.WriteLine(sum);
    }

    private List<List<Vector>> Traverse(char[,] array, Vector position, bool[,] seen)
    { 
        List<List<Vector>> result = new List<List<Vector>>();
        Stack<(Vector, List<Vector>)> stack = new Stack<(Vector, List<Vector>)>();
        
        stack.Push((position, new List<Vector>() { position }));
    
        while (stack.Count > 0)
        {
            (Vector current, List<Vector> path) = stack.Pop();
            seen[current.X, current.Y] = true;
        
            foreach (Vector dir in _directions)
            {
                Vector newPosition = current + dir;

                if (!array.IsWithinBounds(newPosition))
                {
                    continue;
                }

                if (seen[newPosition.X, newPosition.Y])
                {
                    continue;
                    
                }

                if (int.Parse(array[current.X, current.Y].ToString()) + 1 == 
                    int.Parse(array[newPosition.X, newPosition.Y].ToString()))
                {
                    List<Vector> newPath = new List<Vector>(path) { newPosition };
                
                    stack.Push((newPosition, newPath));

                    if (array[newPosition.X, newPosition.Y] == '9')
                    {
                        result.Add((newPath));
                    }
                }
            }
        }

        return result;
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"89010123
// 78121874
// 87430965
// 96549874
// 45678903
// 32019012
// 01329801
// 10456732";

        char[,] array = input.ConvertToCharArray();

        long sum = 0;
        List<(int X, int Y)> startPositions = array.FindAll('0');
        foreach ((int X, int Y) pos in startPositions)
        {
            List<List<Vector>> result = Traverse(array, new Vector(pos));
            sum += result.Count;
        }

        Console.WriteLine(sum);
    }
    
    private List<List<Vector>> Traverse(char[,] array, Vector position)
    {   
        List<List<Vector>> result = new List<List<Vector>>();
        Stack<(Vector, List<Vector>)> stack = new Stack<(Vector, List<Vector>)>();
        
        stack.Push((position, new List<Vector>() { position }));
    
        while (stack.Count > 0)
        {
            bool[,] seen = new bool[array.GetLength(0), array.GetLength(1)]; // One change lol
            (Vector current, List<Vector> path) = stack.Pop();
            seen[current.X, current.Y] = true;
        
            foreach (Vector dir in _directions)
            {
                Vector newPosition = current + dir;

                if (!array.IsWithinBounds(newPosition))
                {
                    continue;
                }

                if (seen[newPosition.X, newPosition.Y])
                {
                    continue;
                    
                }

                if (int.Parse(array[current.X, current.Y].ToString()) + 1 == 
                    int.Parse(array[newPosition.X, newPosition.Y].ToString()))
                {
                    List<Vector> newPath = new List<Vector>(path) { newPosition };
                
                    stack.Push((newPosition, newPath));

                    if (array[newPosition.X, newPosition.Y] == '9')
                    {
                        result.Add((newPath));
                    }
                }
            }
        }

        return result;
    }
}
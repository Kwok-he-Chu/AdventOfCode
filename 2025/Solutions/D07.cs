using System;
using System.Diagnostics.CodeAnalysis;

namespace AOC2025;

/// <summary>
/// Day 7: Laboratories
/// </summary>
public class D07
{
    private readonly AOCHttpClient _client = new AOCHttpClient(7);

    public void Part1()
    {
        string input = _client.RetrieveFile();
        
//         input = @".......S.......
// ...............
// .......^.......
// ...............
// ......^.^......
// ...............
// .....^.^.^.....
// ...............
// ....^.^...^....
// ...............
// ...^.^...^.^...
// ...............
// ..^...^.....^..
// ...............
// .^.^.^.^.^...^.
// ...............";

        char[,] grid = input.ConvertToCharArray();
        
        Vector start = grid.FindFirst('S');
        Queue<Vector> queue = new Queue<Vector>();
        queue.Enqueue(start + Vector.South);
        
        int numberOfSplits = 0;
        while (queue.Count > 0)
        {
            Vector current = queue.Dequeue();
            if (!grid.IsWithinBounds(current))
                continue;
            
            char value = grid[current.X, current.Y];
            switch (value)
            {
                case '^':
                    queue.Enqueue(current + Vector.East);
                    queue.Enqueue(current + Vector.West);
                    numberOfSplits++;
                    break;
                case '.':
                    queue.Enqueue(current + Vector.South);
                    break;
            }
            
            grid[current.X, current.Y] = '|';
        }
        
        Console.WriteLine(numberOfSplits);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();
        
//         input = @".......S.......
// ...............
// .......^.......
// ...............
// ......^.^......
// ...............
// .....^.^.^.....
// ...............
// ....^.^...^....
// ...............
// ...^.^...^.^...
// ...............
// ..^...^.....^..
// ...............
// .^.^.^.^.^...^.
// ...............";

        char[,] grid = input.ConvertToCharArray();
        Vector start = grid.FindFirst('S');
        long total = Count(grid, start, new long?[grid.GetLength(0), grid.GetLength(1)]);
        Console.WriteLine(total);
    }
    
    private long Count(char[,] grid, Vector vector, long?[,] cache)
    {
        if (!grid.IsWithinBounds(vector))
            return 1;

        if (!cache[vector.X, vector.Y].HasValue)
        {
            char current = grid[vector.X, vector.Y];
            long result;

            switch (current)
            {
                case '^':
                    result = Count(grid, vector + Vector.West, cache) + Count(grid, vector + Vector.East, cache);
                    cache[vector.X, vector.Y] = result;
                    return result;
                default:
                    result = Count(grid, vector + Vector.South, cache);
                    cache[vector.X, vector.Y] = result;
                    return result;
            }
        }

        return cache[vector.X, vector.Y].Value;
    }
    
    #region Used for debugging when I created the graph
    private void Print(char[,] grid, Node startNode, int size = 25)
    {
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(startNode);

        char[,] result = new char[size, size];
        
        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            result[current.Vector.X, current.Vector.Y] = current.Value;
            foreach (Node child in current.Children)
            {
                queue.Enqueue(child);
            }
        }
        
        result.PrintArray(true);
    }
    
    private class Node
    {
        public Node Parent { get; private set; }
        public List<Node> Children { get; private set; } = new List<Node>();
        public Vector Vector { get; private set; }
        public char Value { get; private set; }

        public Node(Vector vector, Node parent, char value)
        {
            Vector = vector;
            Parent = parent;
            if (parent != null && parent.Children.FirstOrDefault(x => x.Vector == vector) == null)
                parent.Children.Add(this);
            Value = value;
        }

        public override string ToString()
        {
            return Vector.ToString();
        }
    }
    #endregion
}
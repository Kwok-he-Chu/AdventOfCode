using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 10: Pipe Maze.
/// </summary>
public class D10
{
    private readonly AOCHttpClient _client = new AOCHttpClient(10);

    /*
     *  (-x,  -x) | ( 0, -1) | ( x, -x)
     *  (-1,   0) | ( 0,  0) | ( 1,  0)
     *  (-x,   x) | ( 0,  1) | ( x,  x)
     */
    private static readonly (int X, int Y) Right = new( 1,  0);
    private static readonly (int X, int Y) Left  = new(-1,  0);
    private static readonly (int X, int Y) Up    = new( 0, -1);
    private static readonly (int X, int Y) Down  = new( 0,  1);
    
    private readonly Dictionary<char, List<(int X, int Y)>> _directions = new ()
    {
        { '|', new() { Up, Down } },
        { '-', new() { Left, Right } },
        { 'F', new() { Right, Down  } },
        { 'L', new() { Right, Up  } },
        { '7', new() { Left, Down} },
        { 'J', new() { Left, Up } },
        { '.', new() }
    };

    public void Part1()
    {
        string input = _client.RetrieveFile();

        input = @"
-L|F7
7S-7|
L|7||
-L-J|
L|-JF
";
        char[,] array = input.ConvertToCharArray();

        List<Node> nodes = new List<Node>();
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                nodes.Add(new Node(x, y, array[x, y], null, false, 0));
            }
        }

        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                _directions.TryGetValue(array[x,y], out List<(int, int)> directions);

                var neighbors = new List<Node>();
                foreach ((int X, int Y) dir in directions)
                {
                    int newX = x + dir.X;
                    int newY = y + dir.Y;
                    if (array.IsWithinBounds(newX, newY))
                    {
                        //neighbors.Add();
                    }
                }

                nodes.FirstOrDefault(n => n.X == x && n.Y == y);
            }
        }
        
        Node start = FindNode('S', nodes);

        int steps = 0;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            _directions.TryGetValue(current.Symbol, out List<(int, int)> directions);

            var neighbors = new List<Node>();
            foreach ((int X, int Y) dir in directions)
            {
                int newX = current.X + dir.X;
                int newY = current.Y + dir.Y;
                if (array.IsWithinBounds(newX, newY))
                {
                    //neighbors.Add((newX, newY));
                }
            }
        }


        Console.WriteLine();
    }

    private Node FindNode(char symbol, List<Node> nodes)
    {
        return nodes.FirstOrDefault(x => x.Symbol == symbol);
    }

    private record Node(int X, int Y, char Symbol, List<Node> Neighbors, bool IsSeen = false, int Steps = 0)
    {
        public override string ToString()
        {
            return $"({X}, {Y}): {Symbol.ToString()} {IsSeen} {Steps}";
        }
    }
    
    public void Part2()
    {
        string input = _client.RetrieveFile();

        input = @"-L|F7
7S-7|
L|7||
-L-J|
L|-JF
";
        char[,] array = input.ConvertToCharArray();
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                
            }
        }

        Console.WriteLine();
    }
}
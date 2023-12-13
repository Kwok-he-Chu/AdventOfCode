using System;
using System.Collections.Generic;

namespace AOC2023;

/// <summary>
/// Day 10: Pipe Maze.
/// </summary>
public class D10
{
    private readonly AOCHttpClient _client = new AOCHttpClient(10);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        input = @"-L|F7
7S-7|
L|7||
-L-J|
L|-JF
";
        char[,] array = input.ConvertToCharArray();

        Node[,] nodes = new Node[array.GetLength(0), array.GetLength(1)];
        for (int y = 0; y < nodes.GetLength(0); y++)
        {
            for (int x = 0; x < nodes.GetLength(1); x++)
            {
                nodes[x, y] = new Node(x, y, array[x, y], false, 0);
            }
        }


        Node start = FindNode('S', nodes);

        Console.WriteLine();
    }

    private Dictionary<char, List<Direction>> Lookup = new();

    private Node FindNode(char symbol, Node[,] nodes)
    {
        for (int y = 0; y < nodes.GetLength(0); y++)
        {
            for (int x = 0; x < nodes.GetLength(1); x++)
            {
                if (nodes[x, y].Symbol == symbol)
                {
                    return nodes[x, y];
                }
            }
        }

        return null;
    }

    private record Direction() { }

    private record Node(int X, int Y, char Symbol, bool IsSeen, int Steps)
    {
        public override string ToString()
        {
            return $"({X}, {Y}): {Symbol} {IsSeen} {Steps}";
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
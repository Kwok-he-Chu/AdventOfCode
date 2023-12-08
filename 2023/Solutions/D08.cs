using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023;

/// <summary>
/// Day 8: Haunted Wasteland.
/// </summary>
public class D08
{
    private readonly AOCHttpClient _client = new AOCHttpClient(8);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        /*input = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)";*/

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        string instructions = split[0];
        Dictionary<string, Node> nodes = ConvertInputToDictionary(split);

        int steps = 0;
        Node current = nodes["AAA"];
        while (current.Key != "ZZZ")
        {
            char nextInstruction = instructions[steps % instructions.Length];

            steps++;

            current = nextInstruction switch
            {
                'L' => nodes[current.Left],
                'R' => nodes[current.Right],
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        Console.WriteLine(steps);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        /*input = @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";*/

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        string instructions = split[0];
        Dictionary<string, Node> nodes = ConvertInputToDictionary(split);
        
        long steps = 0;
        List<Node> currentNodes = nodes
            .Where(x => x.Key.EndsWith("A"))
            .Select(x => x.Value)
            .ToList();

        long[] lookup = new long[currentNodes.Count];

        while (lookup.Any(x => x == 0))
        {
            char nextInstruction = instructions[(int)(steps % instructions.Length)];

            steps++;

            for (int i = 0; i < currentNodes.Count; i++)
            {
                currentNodes[i] = nextInstruction switch
                {
                    'L' => nodes[currentNodes[i].Left],
                    'R' => nodes[currentNodes[i].Right],
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (currentNodes[i].Key.EndsWith("Z"))
                {
                    lookup[i] = steps;
                }
            }

        }

        Console.WriteLine(lookup.Aggregate(Extensions.LeastCommonMultiple));
    }
    
    private Dictionary<string, Node> ConvertInputToDictionary(string[] split)
    {
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        for (int i = 1; i < split.Length; i++)
        {
            Regex regex = new Regex(@"([A-Z0-9]+)\s*=\s*\(([A-Z0-9]+),\s*([A-Z0-9]+)\)");
            Match match = regex.Match(split[i]);
            
            string key = match.Groups[1].Value;
            string left = match.Groups[2].Value;
            string right = match.Groups[3].Value;

            nodes.Add(key, new Node(key, left, right));
        }

        return nodes;
    }
    
    private record Node(string Key, string Left, string Right)
    {
        public override string ToString()
        {
            return $"{Key} = ({Left},{Right})";
        }
    }
}
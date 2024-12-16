using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 14: Restroom Redoubt
/// </summary>
public class D14
{
    private readonly AOCHttpClient _client = new AOCHttpClient(14);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"p=0,4 v=3,-3
// p=6,3 v=-1,-3
// p=10,3 v=-1,2
// p=2,0 v=2,-1
// p=0,0 v=1,3
// p=3,0 v=-2,-2
// p=7,6 v=-1,-3
// p=3,0 v=-1,-2
// p=9,3 v=2,3
// p=7,3 v=-1,2
// p=2,4 v=2,-3
// p=9,5 v=-3,-3";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> list = new List<Model>();
        foreach (string s in split)
        {
            string[] temp = s.Split(" ");
            string[] pLine = temp[0].Split("=")[1].Split(",");
            string[] vLine = temp[1].Split("=")[1].Split(",");
            list.Add(new Model()
            {
                Position = new Vector(int.Parse(pLine[0]), int.Parse(pLine[1])),
                Direction = new Vector(int.Parse(vLine[0]), int.Parse(vLine[1]))
            });
        }

        int width = 101;
        int height = 103;

        int seconds = 100;

        for (int second = 0; second < seconds; second++)
        {
            foreach (Model model in list)
            {
                model.Position = new Vector(
                    ((model.Position.X + model.Direction.X) % width + width) % width,
                    ((model.Position.Y + model.Direction.Y) % height + height) % height
                );
            }
        }

        long sum = ComputeSafetyFactor(list, width, height);
        Console.WriteLine(sum);
    }

    private long ComputeSafetyFactor(List<Model> list, int width, int height)
    {
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        int topLeft = 0;
        int topRight = 0;
        int bottomLeft = 0;
        int bottomRight = 0;

        foreach (Model model in list)
        {
            if (model.Position.X == halfWidth || model.Position.Y == halfHeight)
                continue;

            if (model.Position.X < halfWidth && model.Position.Y < halfHeight)
                topLeft++;
            else if (model.Position.X >= halfWidth && model.Position.Y < halfHeight)
                topRight++;
            else if (model.Position.X < halfWidth && model.Position.Y >= halfHeight)
                bottomLeft++;
            else if (model.Position.X >= halfWidth && model.Position.Y >= halfHeight)
                bottomRight++;
        }

        return topLeft * topRight * bottomLeft * bottomRight;
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();
        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> list = new List<Model>();
        foreach (string s in split)
        {
            string[] temp = s.Split(" ");
            string[] pLine = temp[0].Split("=")[1].Split(",");
            string[] vLine = temp[1].Split("=")[1].Split(",");
            list.Add(new Model()
            {
                Position = new Vector(int.Parse(pLine[0]), int.Parse(pLine[1])),
                Direction = new Vector(int.Parse(vLine[0]), int.Parse(vLine[1]))
            });
        }

        int width = 101;
        int height = 103;
        int seconds = 10_000;

        List<(int, int)> result = new List<(int, int)>();
        for (int second = 1; second < seconds; second++)
        {
            foreach (Model model in list)
            {
                model.Position = new Vector(
                    ((model.Position.X + model.Direction.X) % width + width) % width,
                    ((model.Position.Y + model.Direction.Y) % height + height) % height
                );
            }

            HashSet<Vector> set = list.Select(x => x.Position).ToHashSet();

            int sumNeighbours = 0;
            foreach (Vector position in set)
            {
                sumNeighbours += GetNeighboursCount(position, set);
            }

            result.Add((sumNeighbours, second));
            if (sumNeighbours >= 800)
            {
                set.ToList().PrintArray(width, height);
                Console.WriteLine("Step: " + second + " | NeighourCount: " + sumNeighbours);
                Console.ReadKey();
            }
        }
    }

    private readonly List<Vector> _directions = new List<Vector>()
    {
        Vector.North,
        Vector.West, Vector.East,
        Vector.South,
    };

    private int GetNeighboursCount(Vector position, HashSet<Vector> list)
    {
        int result = 0;
        foreach (Vector dir in _directions)
        {
            if (list.Contains(position + dir))
            {
                result++;
            }
        }

        return result;
    }

    private class Model
    {
        public Vector Position { get; set; }
        public Vector Direction { get; set; }

        public override string ToString()
        {
            return $"p={Position} v={Direction}";
        }
    }
}
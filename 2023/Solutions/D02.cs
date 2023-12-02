using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 2: Cube Conundrum.
/// </summary>
public class D02
{
    private readonly AOCHttpClient _client = new AOCHttpClient(2);

    public void Execute1()
    {
        string input = _client.RetrieveFile();

        /*input = @"GameInfo 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
GameInfo 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
GameInfo 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
GameInfo 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
GameInfo 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
";*/

        List<GameInfo> list = ConvertInputToList(input);

        List<GameInfo> result = list
            .Where(game =>
                game.List.All(x => x.Red <= 12) &&
                game.List.All(x => x.Green <= 13) &&
                game.List.All(x => x.Blue <= 14))
            .ToList();

        Console.WriteLine(result.Sum(x => x.GameNumber));
    }

    private List<GameInfo> ConvertInputToList(string input)
    {
        List<GameInfo> list = new List<GameInfo>();
        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in split)
        {
            // Parse "Game 1:"
            string[] prefix = line.Split(":");
            int gameNumber = int.Parse(prefix[0].Split(" ")[1]);
            GameInfo gameInfo = new GameInfo() { GameNumber = gameNumber };

            string[] games = prefix[1].Split(";");
            foreach (string game in games)
            {
                string[] rgb = game.Split(",");
                GameInfoRGB gameInfoRgb = new GameInfoRGB();
                foreach (string t in rgb)
                {
                    string[] elements = t.Split(" ");
                    switch (elements[2]) // Color.
                    {
                        case "red":
                            gameInfoRgb.Red = int.Parse(elements[1]);
                            break;
                        case "green":
                            gameInfoRgb.Green = int.Parse(elements[1]);
                            break;
                        case "blue":
                            gameInfoRgb.Blue = int.Parse(elements[1]);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(elements[2]);
                    }
                }
                gameInfo.List.Add(gameInfoRgb);
            }
            list.Add(gameInfo);
        }

        return list;
    }

    public void Execute2()
    {
        string input = _client.RetrieveFile();

        /*input = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";*/

        List<GameInfo> list = ConvertInputToList(input);

        List<int> result = list.Select(game =>
        {
            int r = game.List.MaxBy(x => x.Red).Red;
            int g = game.List.MaxBy(x => x.Green).Green;
            int b = game.List.MaxBy(x => x.Blue).Blue;
            return (r * g * b);
        }).ToList();

        Console.WriteLine(result.Sum());
    }

    private record GameInfo
    {
        public int GameNumber { get; set; }

        public List<GameInfoRGB> List { get; } = new List<GameInfoRGB>();

        public override string ToString()
        {
            return $"{GameNumber}: R{List.Sum(x => x.Red)} G{List.Sum(x => x.Green)} B{List.Sum(x => x.Blue)}";
        }
    }

    private record GameInfoRGB
    {
        public int Red { get; set; }

        public int Green { get; set; }

        public int Blue { get; set; }

        public override string ToString()
        {
            return $"R{Red} G{Green} B{Blue}";
        }
    }
}
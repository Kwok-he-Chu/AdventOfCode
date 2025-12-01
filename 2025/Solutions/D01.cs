using System;

namespace AOC2025;

/// <summary>
/// Day 1: Secret Entrance
/// </summary>
public class D01
{
    private readonly AOCHttpClient _client = new AOCHttpClient(1);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"L68
// L30
// R48
// L5
// R60
// L55
// L1
// L99
// R14
// L82";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        int currentDial = 50;
        List<int> result = new();

        foreach (string s in split)
        {
            char direction = s[0];
            int amount = int.Parse(s.Substring(1));
            switch (direction)
            {
                case 'L':
                    currentDial = (currentDial - amount) % 100;
                    if (currentDial < 0)
                        currentDial += 100;
                    result.Add(currentDial);
                    break;
                case 'R':
                    currentDial = (currentDial + amount) % 100;
                    result.Add(currentDial);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(direction.ToString());
            }
        }

        Console.WriteLine(result.Count(x => x == 0));
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"L68
// L30
// R48
// L5
// R60
// L55
// L1
// L99
// R14
// L82";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        int currentDial = 50;
        List<(int Dial, int NumberOfZeroCrossings)> result = new();
        foreach (string s in split)
        {
            int count = 0;
            
            char direction = s[0];
            int amount = int.Parse(s.Substring(1));
            switch (direction)
            {
                case 'L':
                    if (currentDial == 0)
                        currentDial = 100;

                    currentDial -= amount;

                    while (currentDial < 0)
                    {
                        currentDial += 100;
                        count++;
                    }

                    if (currentDial == 0)
                        count++;
                    
                    result.Add((currentDial, count));
                    break;
                case 'R':
                    currentDial += amount;
                    while (currentDial > 99)
                    {
                        currentDial -= 100;
                        count++;
                    }
                    
                    result.Add((currentDial, count));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(direction.ToString());
            }
        }

        Console.WriteLine(result.Sum(x => x.NumberOfZeroCrossings));
    }
}
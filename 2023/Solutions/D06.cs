using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023;

/// <summary>
/// Day 6: Wait For It.
/// </summary>
public class D06
{
    private readonly AOCHttpClient _client = new AOCHttpClient(6);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        /*input = @"Time:      7  15   30
Distance:  9  40  200";*/
        
        Match timeMatch = Regex.Match(input, @"Time:\s*(\d+\s*)+");
        List<int> times = Regex.Matches(timeMatch.Value, @"\d+")
            .Select(m => int.Parse(m.Value))
            .ToList();
        
        Match distanceMatch = Regex.Match(input, @"Distance:\s*(\d+\s*)+");
        List<int> distances = Regex.Matches(distanceMatch.Value, @"\d+")
            .Select(m => int.Parse(m.Value))
            .ToList();
        
        List<TimeDistance> timeDistances = times
            .Zip(distances, (time, distance) => new TimeDistance(time, distance))
            .ToList();

        long result = timeDistances
            .Select(CountWinningCombinations)
            .Aggregate((current, next) => current * next);
        
        Console.WriteLine(result);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        /*input = @"Time:      7  15   30
Distance:  9  40  200";*/

        Match timeMatch = Regex.Match(input, @"Time:\s*(\d+\s*)+");
        List<int> times = Regex.Matches(timeMatch.Value, @"\d+")
            .Select(m => int.Parse(m.Value))
            .ToList();
        
        Match distanceMatch = Regex.Match(input, @"Distance:\s*(\d+\s*)+");
        List<int> distances = Regex.Matches(distanceMatch.Value, @"\d+")
            .Select(m => int.Parse(m.Value))
            .ToList();
        
        TimeDistance timeDistance = new TimeDistance(long.Parse(string.Join("", times)), long.Parse(string.Join("", distances)));
        
        long result = CountWinningCombinations(timeDistance);
        Console.WriteLine(result);
    }

    private long CountWinningCombinations(TimeDistance timeDistance)
    {
        long winningCombinations = 0;
        
        for (long speed = 1; speed < timeDistance.Time; speed++)
        {
            long holdTime = timeDistance.Time - speed;
            long totalDistance = holdTime * speed;
            if (totalDistance > timeDistance.Distance)
            {
                winningCombinations++;
            }
        }
        
        return winningCombinations;
    }

    private record TimeDistance(long Time, long Distance)
    {
        public override string ToString()
        {
            return $"Time: {Time} | Distance: {Distance}";
        }
    }
}
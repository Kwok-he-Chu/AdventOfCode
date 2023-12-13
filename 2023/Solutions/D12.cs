using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 
/// </summary>
public class D12
{
    private readonly AOCHttpClient _client = new AOCHttpClient(12);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        /*input = @"#.#.### 1,1,3
.#...#....###. 1,1,3
.#.###.#.###### 1,3,1,6
####.#...#... 4,1,1
#....######..#####. 1,6,5
.###.##....# 3,2,1";*/

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Instruction> instructions = split.Select(line =>
        {
            string[] split = line.Split(" ");
            return new Instruction(split[0],
                split[1].Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray());
        }).ToList();


        Console.WriteLine(instructions.Sum(instruction => Count(instruction, new(), 0)));
    }

    private long Count(Instruction instruction, Dictionary<int, long> cache, int key)
    {
        if (cache.TryGetValue(key, out long count))
            return count;

        if (instruction.Conditions.Length == 0)
            return cache[key] = instruction.Spots.Contains('#') ? 0 : 1;

        int maxLength = instruction.Conditions[0];
        int maxIndex = instruction.Spots.Length - instruction.Conditions.Length - Math.Max(maxLength, instruction.Conditions.Sum() - 1);
        int freeSpotsCount = instruction.Spots.Take(maxLength).Count(c => c != '.');

        for (int currentIndex = 0, lastIndex = maxLength; currentIndex <= maxIndex; currentIndex++)
        {
            char currentChar = instruction.Spots[currentIndex];
            char nextChar = instruction.Spots[lastIndex];
            lastIndex++;

            if (freeSpotsCount == maxLength && nextChar != '#')
            {
                count += Count(new Instruction(instruction.Spots[lastIndex..], instruction.Conditions[1..]), cache, key + lastIndex * 128 + 1);
            }

            if (currentChar == '#')
            {
                return cache[key] = count;
            }

            if (currentChar != '.')
            {
                freeSpotsCount -= 1;
            }

            if (nextChar != '.')
            {
                freeSpotsCount += 1;
            }
        }

        if (freeSpotsCount == maxLength && instruction.Conditions.Length == 1)
        {
            count++;
        }

        return cache[key] = count;
    }


    public void Part2()
    {
        string input = _client.RetrieveFile();

        /*input = @".# 1
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1";*/

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Instruction> instructions = split.Select(line =>
        {
            string[] split = line.Split(" ");
            string spots = string.Concat(Enumerable.Repeat(split[0] + "?", 5));
            string conditions = string.Concat(Enumerable.Repeat(split[1] + ",", 5));
            return new Instruction(spots[..(spots.Length - 1)], // Remove last `?` operator
                conditions.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray());
        }).ToList();

        Console.WriteLine(instructions.Sum(instruction => Count(instruction, new(), 0)));
    }

    private class Instruction
    {
        public string Spots { get; set; }

        public int[] Conditions { get; set; }

        public Instruction(string spots, int[] conditions)
        {
            Spots = spots;
            Conditions = conditions;
        }
    }
}
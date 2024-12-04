using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024;

/// <summary>
/// Day 3: Mull It Over
/// </summary>
public class D03
{
    private readonly AOCHttpClient _client = new AOCHttpClient(3);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        //input = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        
        string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        MatchCollection matches = Regex.Matches(input, mulPattern);

        long sum = 0;
        foreach (Match match in matches)
        {
            sum += int.Parse(match.Groups[1].ToString()) * int.Parse(match.Groups[2].ToString());
        }

        Console.WriteLine(sum);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        string doPattern = @"do\(\)";
        string dontPattern = @"don't\(\)";

        //input = @"xxmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

        MatchCollection matches = Regex.Matches(input, mulPattern);

        var doModels = Regex.Matches(input, doPattern)
            .Select(match => new Do()
            {
                Index = match.Groups[0].Index
            })
            .ToList();

        var dontModels = Regex.Matches(input, dontPattern)
            .Select(match => new Dont()
            {
                Index = match.Groups[0].Index
            })
            .ToList();

        var mulModels = Regex.Matches(input, mulPattern)
            .Select(match => new Mul()
            {
                Index = match.Groups[0].Index,
                A = int.Parse(match.Groups[1].ToString()),
                B = int.Parse(match.Groups[2].ToString())
            })
            .ToList();

        List<Instruction> instructions = new List<Instruction>();
        instructions.AddRange(mulModels);
        instructions.AddRange(doModels);
        instructions.AddRange(dontModels);
        instructions.Sort((a,b) => a.Index.CompareTo(b.Index));

        bool isMulInstructionsEnabled = true;
        long sum = 0;
        foreach (Instruction instr in instructions)
        {
            switch (instr)
            {
                case Do:
                    isMulInstructionsEnabled = true;
                    break;
                case Dont:
                    isMulInstructionsEnabled = false;
                    break;
                case Mul mul when isMulInstructionsEnabled:
                    sum += mul.Multiply();
                    break;
            }
        }

        Console.WriteLine(sum);
    }

    private interface Instruction
    {
        public int Index { get; set; }
    }
    
    private class Mul : Instruction
    {
        public int Index { get; set; }
        public int A;
        public int B;

        public int Multiply()
        {
            return A * B;
        }
    }

    private class Do : Instruction
    {
        public int Index { get; set; }
    }

    private class Dont : Instruction
    {
        public int Index { get; set; }
    }
}
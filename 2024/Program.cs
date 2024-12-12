using System;
using System.Diagnostics;

namespace AOC2024;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("----------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Part One:");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("----------");

        Stopwatch stopwatch = Stopwatch.StartNew();
        new D10().Part1(); // Execute Part 1.
        stopwatch.Stop();

        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(stopwatch.Elapsed);
        Console.ForegroundColor = ConsoleColor.White;

        stopwatch.Reset();

        Console.WriteLine("----------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Part Two:");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("----------");

        stopwatch.Start();
        new D10().Part2(); // Execute Part 2.
        stopwatch.Stop();

        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(stopwatch.Elapsed);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("----------------");
    }
}
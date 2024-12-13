using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 13: Claw Contraption 
/// </summary>
public class D13
{
    private readonly AOCHttpClient _client = new AOCHttpClient(13);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"Button A: X+94, Y+34
// Button B: X+22, Y+67
// Prize: X=8400, Y=5400
//
// Button A: X+26, Y+66
// Button B: X+67, Y+21
// Prize: X=12748, Y=12176
//
// Button A: X+17, Y+86
// Button B: X+84, Y+37
// Prize: X=7870, Y=6450
//
// Button A: X+69, Y+23
// Button B: X+27, Y+71
// Prize: X=18641, Y=10279
// ";
        string[] split = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> models = new List<Model>();
        for (int i = 0; i < split.Length; i++)
        {
            string[] lines = split[i].Split(Environment.NewLine);

            Model model = new Model();
            // Button A
            string[] buttonA = lines[0].Split(": ")[1].Split(", ");
            model.aX = long.Parse(buttonA[0].Split("+")[1]);
            model.aY = long.Parse(buttonA[1].Split("+")[1]);

            // Button B
            string[] buttonB = lines[1].Split(": ")[1].Split(", ");
            model.bX = long.Parse(buttonB[0].Split("+")[1]);
            model.bY = long.Parse(buttonB[1].Split("+")[1]);

            // Prizes
            string[] prizes = lines[2].Split(": ")[1].Split(", ");
            model.prizeX = long.Parse(prizes[0].Split("=")[1]);
            model.prizeY = long.Parse(prizes[1].Split("=")[1]);

            models.Add(model);
        }

        long sum = models.Select(m => m.SolveEquation()).Sum();
        Console.WriteLine(sum);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"Button A: X+94, Y+34
// Button B: X+22, Y+67
// Prize: X=8400, Y=5400
//
// Button A: X+26, Y+66
// Button B: X+67, Y+21
// Prize: X=12748, Y=12176
//
// Button A: X+17, Y+86
// Button B: X+84, Y+37
// Prize: X=7870, Y=6450
//
// Button A: X+69, Y+23
// Button B: X+27, Y+71
// Prize: X=18641, Y=10279
// ";
        string[] split = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> models = new List<Model>();
        for (int i = 0; i < split.Length; i++)
        {
            string[] lines = split[i].Split(Environment.NewLine);

            Model model = new Model();
            // Button A
            string[] buttonA = lines[0].Split(": ")[1].Split(", ");
            model.aX = long.Parse(buttonA[0].Split("+")[1]);
            model.aY = long.Parse(buttonA[1].Split("+")[1]);

            // Button B
            string[] buttonB = lines[1].Split(": ")[1].Split(", ");
            model.bX = long.Parse(buttonB[0].Split("+")[1]);
            model.bY = long.Parse(buttonB[1].Split("+")[1]);

            // Prizes
            string[] prizes = lines[2].Split(": ")[1].Split(", ");
            model.prizeX = long.Parse(prizes[0].Split("=")[1]) + 10_000_000_000_000L;
            model.prizeY = long.Parse(prizes[1].Split("=")[1]) + 10_000_000_000_000L;

            models.Add(model);
        }
        
        long sum = models.Select(m => m.SolveEquation()).Sum();
        Console.WriteLine(sum);
    }

    private class Model
    {
        public long aX { get; set; }
        public long aY { get; set; }
        public long bX { get; set; }
        public long bY { get; set; }
        public long prizeX { get; set; }
        public long prizeY { get; set; }
        
        // (2024) Sigh, it's 14+ years ago... Here's a reminder to future-me: order take away if you visit this.
        
        // For stepsA (X):
        //        (aX * stepsA) + (bX * stepsB) =  prizeX
        //        (aX * stepsA)                 =  prizeX - (bX * stepsB) 
        // [!] -> stepsA                        = (prizeX - (bX * stepsB)) / aX
        
        // Similarly, for stepsA (Y):
        //     (aY * stepsA) + (bY * stepsB) =  prizeY
        //     (aY * stepsA)                 =  prizeY - (bY * stepsB) 
        // ->  stepsA                        = (prizeY - (bY * stepsB)) / aY

        // Solve the equation for stepsB:
        //        (prizeX - (bX * stepsB)) / aX  =  (prizeY - (bY * stepsB)) / aY
        //        (prizeX -  bX * stepsB ) * aY  =  (prizeY -  bY * stepsB ) * aX
        // Solve for `stepsB` on a piece of paper, by getting it to the right side of the equation:
        // [!]->  stepsB = (aY * prizeX - aX * prizeY) / (aY * bX - aX * bY)
        
        public long SolveEquation()
        {
            long stepB = (aY * prizeX - aX * prizeY) / (aY * bX - aX * bY);
            long stepA = (prizeX - (bX * stepB)) / aX;

            if (aX * stepA + bX * stepB == prizeX && 
                aY * stepA + bY * stepB == prizeY)
            {
                return (3 * stepA) + stepB;
            }
            
            return 0L;
        }

        public bool CheckIfEqualWhenMultiplied(long stepsA, long stepsB)
        {
            return aX * stepsA + bX * stepsB == prizeX && aY * stepsA + bY * stepsB == prizeY;
        }
        
        public override string ToString()
        {
            return $"Button A: X {aX}, Y {aY} | " +
                   $"Button B: X {bX}, Y {bY} | " +
                   $"Prize   : X {prizeX}, Y {prizeY}";
        }
    }
}
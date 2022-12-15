using System;
using System.Linq;

namespace AOC2022
{
    /// <summary>
    /// Day 2: Rock Paper Scissors
    /// </summary>
    public class D02
    {
        private readonly AocHttpClient _client = new AocHttpClient(2);

        public void Execute1()
        {
            string input = _client.RetrieveFile();
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int score = split.Select(x => x switch
            {
                "A X" => 4,
                "A Y" => 8,
                "A Z" => 3,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 7,
                "C Y" => 2,
                "C Z" => 6,
                _ => throw new ArgumentException()
            }).Sum();
            Console.WriteLine(score);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile();
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int score = split.Select(x => x switch
            {
                "A X" => 3,
                "A Y" => 4,
                "A Z" => 8,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 2,
                "C Y" => 6,
                "C Z" => 7,
                _ => throw new ArgumentException()
            }).Sum();
            Console.WriteLine(score);
        }
    }
}

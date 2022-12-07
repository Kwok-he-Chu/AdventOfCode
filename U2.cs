using System;

namespace AOC2022
{
    public class U2
    {
        private readonly AocHttpClient _client = new AocHttpClient(2);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n");

            int score = 0;
            foreach (var line in split)
            {
                switch (line)
                {
                    case "A X":
                        score += 4;
                        break;
                    case "A Y":
                        score += 8;
                        break;
                    case "A Z":
                        score += 3;
                        break;
                    case "B X":
                        score += 1;
                        break;
                    case "B Y":
                        score += 5;
                        break;
                    case "B Z":
                        score += 9;
                        break;
                    case "C X":
                        score += 7;
                        break;
                    case "C Y":
                        score += 2;
                        break;
                    case "C Z":
                        score += 6;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(score);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n");

            int score = 0;
            foreach (var line in split)
            {
                switch (line)
                {
                    case "A X":
                        score += 3;
                        break;
                    case "A Y":
                        score += 4;
                        break;
                    case "A Z":
                        score += 8;
                        break;
                    case "B X":
                        score += 1;
                        break;
                    case "B Y":
                        score += 5;
                        break;
                    case "B Z":
                        score += 9;
                        break;
                    case "C X":
                        score += 2;
                        break;
                    case "C Y":
                        score += 6;
                        break;
                    case "C Z":
                        score += 7;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine(score);
        }
    }
}

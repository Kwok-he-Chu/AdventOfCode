using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class D09
    {
        private readonly AocHttpClient _client = new AocHttpClient(9);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int result = GetUniqueTailPositions(2, split, false);
            Console.WriteLine(result);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int result = GetUniqueTailPositions(10, split, false);
            Console.WriteLine(result);
        }

        private int GetUniqueTailPositions(int knotsCount, string[] input, bool showVisualization)
        {
            int startX = 64, startY = 64;
            List<(int X, int Y)> list = Enumerable.Repeat((startX, startY), knotsCount).ToList();

#if DEBUG 
            bool[,] visibilityArray = new bool[128, 128]; // Modify the window size here & startX and startY above.
            if (showVisualization)
            {
                visibilityArray[startX, startY] = true;
            }
#endif

            HashSet<(int X, int Y)> hashSet = new HashSet<(int X, int Y)>();

            foreach (string line in input)
            {
                string[] split = line.Split(' ');
                int steps = int.Parse(split[1]);
                int directionX = 0, directionY = 0;

                switch (split[0])
                {
                    case "U":
                        directionY = -1;
                        break;
                    case "D":
                        directionY = 1;
                        break;
                    case "L":
                        directionX = -1;
                        break;
                    case "R":
                        directionX = 1;
                        break;
                    default:
                        throw new KeyNotFoundException();
                }

                int totalSteps = int.Parse(split[1]);
                for (int step = 0; step < totalSteps; step++)
                {
                    // Update head.
                    list[0] = (list[0].X + directionX, list[0].Y + directionY);

                    // Update tail.
                    for (int i = 1; i < list.Count; i++)
                    {
                        int dX = list[i - 1].X - list[i].X;
                        int dY = list[i - 1].Y - list[i].Y;
                        if (Math.Abs(dX) > 1 || Math.Abs(dY) > 1)
                            list[i] = (list[i].X + Math.Sign(dX), list[i].Y + Math.Sign(dY));
                    }

                    (int X, int Y) last = list.Last();
                    hashSet.Add(last);
#if DEBUG
                    if (showVisualization)
                    {
                        visibilityArray[last.X, last.Y] = true;
                        CustomPrintArray(visibilityArray);
                    }
#endif
                }
            }

            return hashSet.Count;
        }

        // Pretty prints the path that # follows.
        private void CustomPrintArray(bool[,] visibilityArray)
        {
            Console.Clear();
            int rowLength = visibilityArray.GetLength(0);
            int columnLength = visibilityArray.GetLength(1);
            for (int j = 0; j < columnLength; j++)
            {
                for (int i = 0; i < rowLength; i++)
                {
                    if (visibilityArray[i, j])
                        Console.Write('#');
                    else
                        Console.Write('.');

                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
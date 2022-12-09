using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class U8
    {
        private readonly AocHttpClient _client = new AocHttpClient(8);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            int[,] inputArray = input.ConvertToIntArray();
            bool[,] visbilityArray = new bool[inputArray.GetLength(0), inputArray.GetLength(1)];

            // Brute force.
            MarkVisibleTopToDown(inputArray, visbilityArray);
            MarkVisibleDownToTop(inputArray, visbilityArray);
            MarkVisibleLeftToRight(inputArray, visbilityArray);
            MarkVisibleRightToLeft(inputArray, visbilityArray);

            int result = Count(visbilityArray);
            Console.WriteLine(result);
        }

        private int Count(bool[,] array)
        {
            int sum = 0;
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    if (array[i, j])
                        sum++;
            return sum;
        }

        private void MarkVisibleRightToLeft(int[,] array, bool[,] visibilityArray)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                int minHeight = -1;
                for (int i = array.GetLength(0) - 1; i >= 0; i--)
                {
                    if (minHeight < array[i, j])
                    {
                        minHeight = array[i, j];
                        visibilityArray[i, j] = true;
                    }
                }
            }
        }

        private void MarkVisibleLeftToRight(int[,] array, bool[,] visibilityArray)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                int minHeight = -1;
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (minHeight < array[i, j])
                    {
                        minHeight = array[i, j];
                        visibilityArray[i, j] = true;
                    }
                }
            }
        }

        private void MarkVisibleTopToDown(int[,] array, bool[,] visibilityArray)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int minHeight = -1;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (minHeight < array[i, j])
                    {
                        minHeight = array[i, j];
                        visibilityArray[i, j] = true;
                    }
                }
            }
        }

        private void MarkVisibleDownToTop(int[,] array, bool[,] visibilityArray)
        {
            for (int i = array.GetLength(0) - 1; i >= 0; i--)
            {
                int minHeight = -1;
                for (int j = array.GetLength(1) - 1; j >= 0 ; j--)
                {
                    if (minHeight < array[i, j])
                    {
                        minHeight = array[i, j];
                        visibilityArray[i, j] = true;
                    }
                }
            }
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            int[,] array = input.ConvertToIntArray();
            int[,] scenicScoreArray = new int[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    SaveScenicScore(array, ref scenicScoreArray, i, j);

            int result = CountScenicScoreMax(scenicScoreArray);
            Console.WriteLine(result);
        }

        private int CountScenicScoreMax(int[,] array)
        {
            int max = 0;
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    if (array[i, j] > max)
                        max = array[i, j];
            return max;
        }

        private void SaveScenicScore(int[,] array, ref int[,] scenicScoreArray, int x, int y)
        {
            var up = Enumerable.Range(1, array.GetLength(1))
                .Select(nr => (X: x, Y: y - nr))
                .Where(tuple => array.IsWithinBounds(tuple.X, tuple.Y))
                .ToList();
            var left = Enumerable.Range(1, array.GetLength(0))
                .Select(nr => (X: x - nr, Y: y))
                .Where(tuple => array.IsWithinBounds(tuple.X, tuple.Y))
                .ToList();
            var right = Enumerable.Range(1, array.GetLength(0))
                .Select(nr => (X: x + nr, Y: y))
                .Where(tuple => array.IsWithinBounds(tuple.X, tuple.Y))
                .ToList();
            var down = Enumerable.Range(1, array.GetLength(1))
                .Select(nr => (X: x, Y: y + nr))
                .Where(tuple => array.IsWithinBounds(tuple.X, tuple.Y))
                .ToList();

            int scenicScoreUp = GetScenicScore(up, array, x, y);
            int scenicScoreLeft = GetScenicScore(left, array, x, y);
            int scenicScoreRight = GetScenicScore(right, array, x, y);
            int scenicScoreDown = GetScenicScore(down, array, x, y); 

            scenicScoreArray[x, y] = scenicScoreUp * scenicScoreLeft * scenicScoreRight * scenicScoreDown;
        }

        private int GetScenicScore(List<(int, int)> listOfCoordinates, int[,] array, int x, int y)
        {
            int scenicScore = 0;
            foreach ((int i, int j) in listOfCoordinates)
            {
                scenicScore++;
                if (array[x, y] <= array[i, j])
                    return scenicScore;
            }
            return scenicScore;
        }
    }
}
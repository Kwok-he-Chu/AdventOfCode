using System;
using System.Linq;

namespace AOC2022
{
    public class U8
    {
        private readonly AocHttpClient _client = new AocHttpClient(8);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "30373\r\n25512\r\n65332\r\n33549\r\n35390";

            int[,] array = input.ConvertToIntArray();
            bool[,] visbilityArray = new bool[array.GetLength(0), array.GetLength(1)];
            int result = IsVisibleTopToDown(array, visbilityArray)
                + IsVisibleDownToTop(array, visbilityArray)
                + IsVisibleLeftToRight(array, visbilityArray)
                + IsVisibleRightToLeft(array, visbilityArray);

            Console.WriteLine(Count(visbilityArray));
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine();
        }

        private int Count(bool[,] array)
        {
            int sum = 0;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = array.GetLength(0) - 1; i >= 0; i--)
                {
                    if (array[i, j])
                        sum++;
                }
            }
            return sum;
        }

        private int IsVisibleRightToLeft(int[,] array, bool[,] visibilityArray)
        {
            int sum = 0;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                int minHeight = -1;
                for (int i = array.GetLength(0) - 1; i >= 0; i--)
                {
                    if (minHeight < array[i, j])
                    {
                        minHeight = array[i, j];
                        sum += 1;
                        visibilityArray[i, j] = true;
                    }
                }
            }
            return sum;
        }

        private int IsVisibleLeftToRight(int[,] array, bool[,] visibilityArray)
        {
            int sum = 0;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                int minHeight = -1;
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (minHeight < array[i, j])
                    {
                        minHeight = array[i, j];
                        sum += 1;
                        visibilityArray[i, j] = true;
                    }
                }
            }
            return sum;
        }

        private int IsVisibleTopToDown(int[,] array, bool[,] visibilityArray)
        {
            int sum = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int minHeight = -1;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (minHeight < array[i, j])
                    {
                        minHeight = array[i, j];
                        sum += 1;
                        visibilityArray[i, j] = true;
                    }
                }
            }
            return sum;
        }

        private int IsVisibleDownToTop(int[,] array, bool[,] visibilityArray)
        {
            int sum = 0;
            for (int i = array.GetLength(0) - 1; i >= 0; i--)
            {
                int minHeight = -1;
                for (int j = array.GetLength(1) - 1; j >= 0 ; j--)
                {
                    if (minHeight < array[i, j])
                    {
                        minHeight = array[i, j];
                        sum += 1;
                        visibilityArray[i, j] = true;
                    }
                }
            }
            return sum;
        }
    }
}
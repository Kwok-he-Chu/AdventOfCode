using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========");
            Console.WriteLine("Part One: ");
            Console.WriteLine("==========");
            new U8().Execute1();
            Console.WriteLine("==========");
            Console.WriteLine("Part Two: ");
            Console.WriteLine("==========");
            new U8().Execute2();
            Console.ReadKey();
        }
    }

    public static class Extensions
    {
        public static List<string> ConvertToList(this string input)
        {
            return input.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static void PrintArray(this int[,] array)
        {
            int rowLength = array.GetLength(0);
            int columnLength = array.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < columnLength; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static int[,] ConvertToIntArray(this string input)
        {
            string[] list = input.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int rowLength = list[0].Length;
            int columnLength = list.Length;
            int[,] result = new int[rowLength, columnLength];

            for (int i = 0; i < rowLength; i++)
                for (int j = 0; j < columnLength; j++)
                    result[i, j] = int.Parse(list[j][i].ToString());
            return result;
        }

        public static bool IsWithinBounds(this int[,] array, int x, int y)
        {
            return x >= array.GetLowerBound(0) && x <= array.GetUpperBound(0) && y >= array.GetLowerBound(1) && y <= array.GetUpperBound(1);
        }

        public static bool IsUpper(this string inp)
        {
            foreach (char c in inp)
                if (char.IsLower(c))
                    return false;
            return true;
        }
    }
}
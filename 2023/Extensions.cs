using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

public static class Extensions
{
    public static bool IsDebug;
    
    public static string Reverse(this string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static void WriteLine(string line)
    {
        if (IsDebug)
            Console.WriteLine(line);
    }

    public static void PrintArray(this bool[,] array)
    {
        int rowLength = array.GetLength(0);
        int columnLength = array.GetLength(1);
        for (int j = 0; j < columnLength; j++)
        {
            for (int i = 0; i < rowLength; i++)
            {
                if (!array[i, j])
                    Console.Write('.');
                else
                    Console.Write('X');
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void PrintArray(this char[,] array)
    {
        int rowLength = array.GetLength(0);
        int columnLength = array.GetLength(1);
        for (int j = 0; j < columnLength; j++)
        {
            for (int i = 0; i < rowLength; i++)
            {
                Console.Write(array[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void PrintArray(this int[,] array)
    {
        int rowLength = array.GetLength(0);
        int columnLength = array.GetLength(1);
        for (int j = 0; j < columnLength; j++)
            for (int i = 0; i < rowLength; i++)
                Console.Write(array[i, j]);
    }

    public static int[,] ConvertToIntArray(this string input)
    {
        string[] list = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        int rowLength = list[0].Length;
        int columnLength = list.Length;
        int[,] result = new int[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
            for (int j = 0; j < columnLength; j++)
                result[i, j] = int.Parse(list[j][i].ToString());
        return result;
    }

    public static char[,] ConvertToCharArray(this string input)
    {
        string[] list = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        int rowLength = list[0].Length;
        int columnLength = list.Length;
        char[,] result = new char[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
            for (int j = 0; j < columnLength; j++)
                result[i, j] = list[j][i];
        return result;
    }

    public static bool IsWithinBounds<T>(this T[,] array, int x, int y)
    {
        return x >= array.GetLowerBound(0) && x <= array.GetUpperBound(0) && y >= array.GetLowerBound(1) && y <= array.GetUpperBound(1);
    }

    public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T temp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = temp;
        return list;
    }

    public static IEnumerable<T> Splice<T>(this IEnumerable<T> list, int offset, int count)
    {
        return list.Skip(offset).Take(count);
    }

    public static long GreatestCommonDivisor(long a, long b) // GCD
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    public static long LeastCommonMultiple(long a, long b) // LCM
    {
        return a / GreatestCommonDivisor(a, b) * b;
    }
}
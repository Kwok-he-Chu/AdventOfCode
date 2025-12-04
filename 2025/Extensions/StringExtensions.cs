using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2025;

public static class StringExtensions
{
    public static string Reverse(this string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
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
}
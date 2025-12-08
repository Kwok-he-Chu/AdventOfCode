using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2025;

public static class ArrayExtensions
{
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
    }
    
    public static void PrintArray(this int[,] array)
    {
        int rowLength = array.GetLength(0);
        int columnLength = array.GetLength(1);
        for (int y = 0; y < columnLength; y++)
            for (int x = 0; x < rowLength; x++)
                Console.Write(array[x, y]);
    }
    
    public static void PrintArray(this char[,] array, bool isFillEmptyStrings = false)
    {
        int rowLength = array.GetLength(0);
        int columnLength = array.GetLength(1);
        for (int y = 0; y < columnLength; y++)
        {
            for (int x = 0; x < rowLength; x++)
            {
                if (array[x, y] == '\0')
                    Console.Write('.');
                else
                    Console.Write(array[x, y]);
            }
            Console.WriteLine();
        }
    }
    
    public static void PrintArray(this List<Vector> list, int width, int height)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (list.Contains(new Vector(x, y)))
                    Console.Write("#");
                else
                    Console.Write(".");
            }
            Console.WriteLine();
        }
    }
    
    public static void PrintArray(this char[,] array, List<Vector> vectors, char symbol)
    {
        char[,] copy = new char[array.GetLength(0), array.GetLength(1)];
        Array.Copy(array, copy, array.Length);

        foreach (Vector vec in vectors)
            copy[vec.X, vec.Y] = symbol;

        copy.PrintArray();
    }

    public static bool IsWithinBounds<T>(this T[,] array, int x, int y)
    {
        return x >= array.GetLowerBound(0) && 
               x <= array.GetUpperBound(0) && 
               y >= array.GetLowerBound(1) && 
               y <= array.GetUpperBound(1);
    }

    public static bool IsWithinBounds<T>(this T[,] array, (int X, int Y) tuple)
    {
        return IsWithinBounds(array, tuple.X, tuple.Y);
    }
    
    public static bool IsWithinBounds<T>(this T[,] array, Vector vector)
    {
        return IsWithinBounds(array, vector.X, vector.Y);
    }
    
    public static Vector FindFirst(this char[,] array, char symbol)
    {
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                if (array[x, y] == symbol)
                {
                    return new Vector(x, y);
                }
            }
        }

        throw new ArgumentException("Could not find symbol: " + symbol);
    }
    
    public static List<Vector> FindAll(this char[,] array, char symbol)
    {
        var result = new List<Vector>();
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                if (array[x, y] == symbol)
                {
                    result.Add(new Vector(x, y));
                }
            }
        }

        return result;
    }

    public static void ForEach<T>(this T[,] array, Action<int, int, T> action)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                action(x, y, array[x, y]);
    }
    
    /*
     *    array.ForEachNeighbour4((currentX, currentY, currentValue, neighbourX, neighbourY, neighbourValue) =>
            {
                Console.WriteLine($"Current {currentX}, {currentY} = {currentValue}");
                Console.WriteLine($"Neighbour {neighbourX}, {neighbourY} = {neighbourValue}");
            });
     */
    public static void ForEachNeighbour4<T>(this T[,] array, Action<int, int, T, int, int, T> action)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                foreach (Vector neighbour in Vector.Directions4.Where(neighbour => array.IsWithinBounds(neighbour.X + x, neighbour.Y + y)))
                    action(x, y, array[x, y], neighbour.X + x, neighbour.Y + y, array[neighbour.X + x, neighbour.Y + y]);
    }
    
    /*
     *    array.ForEachNeighbour8((currentX, currentY, currentValue, neighbourX, neighbourY, neighbourValue) =>
            {
                Console.WriteLine($"Current {currentX}, {currentY} = {currentValue}");
                Console.WriteLine($"Neighbour {neighbourX}, {neighbourY} = {neighbourValue}");
            });
     */
    public static void ForEachNeighbour8<T>(this T[,] array, Action<int, int, T, int, int, T> action)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                foreach (Vector neighbour in Vector.Directions8.Where(neighbour => array.IsWithinBounds(neighbour.X + x, neighbour.Y + y))) 
                    action(x, y, array[x, y], neighbour.X + x, neighbour.Y + y, array[neighbour.X + x, neighbour.Y + y]);
    }
}
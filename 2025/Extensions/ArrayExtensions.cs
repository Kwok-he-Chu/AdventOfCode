using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2025;

public static class ArrayExtensions
{
    public static void PrintArray(this bool[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!grid[x, y])
                    Console.Write('.');
                else
                    Console.Write('X');
            }
            Console.WriteLine();
        }
    }
    
    public static void PrintArray(this int[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                Console.Write(grid[x, y]);
    }
    
    public static void PrintArray(this char[,] grid, bool isFillEmptyStrings = false)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] == '\0')
                    Console.Write('.');
                else
                    Console.Write(grid[x, y]);
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
    
    public static void PrintArray(this char[,] grid, List<Vector> vectors, char symbol)
    {
        char[,] copy = new char[grid.GetLength(0), grid.GetLength(1)];
        Array.Copy(grid, copy, grid.Length);

        foreach (Vector vec in vectors)
            copy[vec.X, vec.Y] = symbol;

        copy.PrintArray();
    }
    public static char[,] RotateClockwise(char[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        
        char[,] result = new char[height, width];
        
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                result[y, width - 1 - x] = grid[x, y];
        
        return result;
    }
    
    public static char[,] RotateCounterClockwise(char[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        
        char[,] result = new char[height, width];
        
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                result[height - 1 - y, x] = grid[x, y];
        
        return result;
    }

    public static char[,] FlipHorizontal(char[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        
        char[,] result = new char[width, height];
        
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                result[x, height - 1 - y] = grid[x, y];
        
        return result;
    }
    
    public static char[,] FlipVertical(char[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        
        char[,] result = new char[width, height];
        
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                result[width - 1 - x, y] = grid[x, y];
        
        return result;
    }
    
    public static bool IsWithinBounds<T>(this T[,] array, long x, long y)
    {
        return x >= array.GetLowerBound(0) && 
               x <= array.GetUpperBound(0) && 
               y >= array.GetLowerBound(1) && 
               y <= array.GetUpperBound(1);
    }

    public static bool IsWithinBounds<T>(this T[,] array, (long X, long Y) tuple)
    {
        return IsWithinBounds(array, tuple.X, tuple.Y);
    }
    
    public static bool IsWithinBounds<T>(this T[,] array, Vector vector)
    {
        return IsWithinBounds(array, vector.X, vector.Y);
    }
    
    public static Vector FindFirst(this char[,] grid, char symbol)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] == symbol)
                {
                    return new Vector(x, y);
                }
            }
        }

        throw new ArgumentException("Could not find symbol: " + symbol);
    }
    
    public static List<Vector> FindAll(this char[,] grid, char symbol)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        
        List<Vector> result = new List<Vector>();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] == symbol)
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
    public static void ForEachNeighbour4<T>(this T[,] grid, Action<long, long, T, long, long, T> action)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                foreach (Vector neighbour in Vector.Directions4.Where(neighbour => grid.IsWithinBounds(neighbour.X + x, neighbour.Y + y)))
                    action(x, y, grid[x, y], neighbour.X + x, neighbour.Y + y, grid[neighbour.X + x, neighbour.Y + y]);
    }
    
    /*
     *    array.ForEachNeighbour8((currentX, currentY, currentValue, neighbourX, neighbourY, neighbourValue) =>
            {
                Console.WriteLine($"Current {currentX}, {currentY} = {currentValue}");
                Console.WriteLine($"Neighbour {neighbourX}, {neighbourY} = {neighbourValue}");
            });
     */
    public static void ForEachNeighbour8<T>(this T[,] array, Action<long, long, T, long, long, T> action)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                foreach (Vector neighbour in Vector.Directions8.Where(neighbour => array.IsWithinBounds(neighbour.X + x, neighbour.Y + y))) 
                    action(x, y, array[x, y], neighbour.X + x, neighbour.Y + y, array[neighbour.X + x, neighbour.Y + y]);
    }
}
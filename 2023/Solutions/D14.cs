using System;

namespace AOC2023;

/// <summary>
/// Day 14: Parabolic Reflector Dish.
/// </summary>
public class D14
{
    private readonly AOCHttpClient _client = new AOCHttpClient(14);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        /*input = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....";*/

        char[,] array = input.ConvertToCharArray();
        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 1; y < array.GetLength(1); y++)
            {
                MoveUp(array, x, y);
            }
        }

        //array.PrintArray();

        int sum = 0;

        for (int x = 0; x < array.GetLength(0); x++)
        {
            int yLength = array.GetLength(1);
            for (int y = 0; y < yLength; y++)
            {
                if (array[x, y] != 'O')
                {
                    continue;
                }

                sum += yLength - y;
            }
        };

        Console.WriteLine(sum);
    }

    private void MoveUp(char[,] map, int x, int y)
    {
        if (map[x, y] == '.' || map[x, y] == '#')
        {
            return;
        }

        int maxY = y;
        for (int currentY = y - 1; currentY >= 0; currentY--)
        {
            if (map[x, currentY] != '.')
            {
                break;
            }

            maxY = currentY;
        }

        map[x, y] = '.';
        map[x, maxY] = 'O';
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        input = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....";

        char[,] array = input.ConvertToCharArray();
        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 1; y < array.GetLength(1); y++)
            {
                MoveUp(array, x, y);
            }
        }

        // TODO: West, East, South... 

        Console.WriteLine();
    }
}
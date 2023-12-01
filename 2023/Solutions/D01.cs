using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2023;

/// <summary>
/// Day 1: Trebuchet?! 
/// </summary>
public class D01
{
    private readonly AOCHttpClient _client = new AOCHttpClient(1);

    public void Execute1()
    {
        string input = _client.RetrieveFile();

        /*input = @"1abc2
        pqr3stu8vwx
        a1b2c3d4e5f
        treb7uchet";*/

        var split = input.Split("\n");
        int result = split.Sum(x => (ReadLeftToRight(x) * 10) + ReadLeftToRight(x.Reverse()));
        Console.WriteLine(result);
    }

    private int ReadLeftToRight(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                return int.Parse(line[i].ToString());
            }
        }

        return 0;
    }

    private Dictionary<string, int> _lookup = new Dictionary<string, int>()
    {
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9},
    };

    public void Execute2()
    {
        string input = _client.RetrieveFile();

        /*input = @"two1nine
            eightwothree
            abcone2threexyz
            xtwone3four
            4nineeightseven2
            zoneight234
            7pqrstsixteen";*/

        var split = input.Split("\n");
        int result = split.Sum(x => (ReadLeftToRightPart2(x) * 10) + ReadLeftToRightPart2(x.Reverse()));
        Console.WriteLine(result);
    }

    private List<int> _characterLengths = new List<int>() {3, 4, 5};

    private int ReadLeftToRightPart2(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            // Return the digit.
            if (char.IsDigit(line[i]))
            {
                return int.Parse(line[i].ToString());
            }

            // Lookup the character in the dictionary, if it's not a digit.
            // We check the characterLengths of: [3, 4, 5].
            if (char.IsLetter(line[i]))
            {
                foreach (int characterLength in _characterLengths)
                {
                    string key = GetExistingKey(line, i, characterLength);
                    if (key != null)
                    {
                        return _lookup[key];
                    }
                }
            }
        }

        return 0;
    }

    private string GetExistingKey(string currentLine, int currentIndex, int numberOfCharacters)
    {
        // Out of bounds check.
        if (currentIndex + numberOfCharacters > currentLine.Length)
        {
            return null;
        }

        string key = currentLine.Substring(currentIndex, numberOfCharacters);

        if (_lookup.ContainsKey(key))
        {
            return key;
        }

        // Look-up the key in reverse too
        string reversedKey = key.Reverse();
        
        if (_lookup.ContainsKey(reversedKey))
        {
            return reversedKey;
        }

        return null;
    }
}
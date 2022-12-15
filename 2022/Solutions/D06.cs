using System;
using System.Collections.Generic;

namespace AOC2022
{
    /// <summary>
    /// Day 6: Tuning Trouble
    /// </summary>
    public class D06
    {
        private readonly AocHttpClient _client = new AocHttpClient(6);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            //input = "bvwbjplbgvbhsrlpgdmjqwftvncz";
            //input = "nppdvjthqldpwncqszvftbrmjlhg";
            //input = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
            //input = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
            string datastream = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)[0];
            Console.WriteLine(GetMarkerIndex(datastream, 4));
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            //input = "bvwbjplbgvbhsrlpgdmjqwftvncz";
            //input = "nppdvjthqldpwncqszvftbrmjlhg";
            //input = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
            //input = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
            string datastream = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)[0];
            Console.WriteLine(GetMarkerIndex(datastream, 14));
        }

        private int GetMarkerIndex(string datastream, int numberOfDistinctCharacters)
        {
            for (int i = 0; i < datastream.Length - numberOfDistinctCharacters; i++)
            {
                HashSet<char> hashSet = new HashSet<char>();
                for (int j = 0; j < numberOfDistinctCharacters; j++)
                {
                    hashSet.Add(datastream[i + j]);
                    if (hashSet.Count == numberOfDistinctCharacters)
                    {
                        return i + j + 1;
                    }
                }
                hashSet.Clear();
            }
            throw new KeyNotFoundException();
        }
    }
}

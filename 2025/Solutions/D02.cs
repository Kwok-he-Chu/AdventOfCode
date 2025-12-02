using System;

namespace AOC2025;

/// <summary>
/// Day 2: Gift Shop
/// </summary>
public class D02
{
    private readonly AOCHttpClient _client = new AOCHttpClient(2);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        //input = @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

        string[] split = input.Split(",", StringSplitOptions.RemoveEmptyEntries);

        List<IdRange> list = new List<IdRange>();
        foreach (string s in split)
        {
            string[] kvp = s.Split('-');
            long lowerBound = long.Parse(kvp[0]);
            long upperBound = long.Parse(kvp[1]);
            IdRange idRange = new IdRange(lowerBound, upperBound);
            list.Add(idRange);
        }

        long sum = 0;
        foreach (IdRange idRange in list)
        {
            List<(long Id, bool IsInvalid)> ids = idRange.ComputeIdsPart1();
            foreach ((long Id, bool IsInvalid) kvp in ids)
                if (kvp.IsInvalid)
                    sum += kvp.Id;
        }
        
        Console.WriteLine(sum);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        //input = @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

        string[] split = input.Split(",", StringSplitOptions.RemoveEmptyEntries);

        List<IdRange> list = new List<IdRange>();
        foreach (string s in split)
        {
            string[] kvp = s.Split('-');
            long lowerBound = long.Parse(kvp[0]);
            long upperBound = long.Parse(kvp[1]);
            IdRange idRange = new IdRange(lowerBound, upperBound);
            list.Add(idRange);
        }

        long sum = 0;
        foreach (IdRange idRange in list)
        {
            List<(long Id, bool IsInvalid)> ids = idRange.ComputeIdsPart2();
            foreach ((long Id, bool IsInvalid) kvp in ids)
                if (kvp.IsInvalid)
                    sum += kvp.Id;
        }
        
        Console.WriteLine(sum);
    }
    
    private class IdRange(long lowerBound, long upperBound)
    {
        public long LowerBound { get; } = lowerBound;
        public long UpperBound { get; } = upperBound;

        public List<(long Id, bool IsInvalid)> ComputeIdsPart1()
        {
            List<(long Id, bool IsInvalid)> result = new();
            for (long i = LowerBound; i <= UpperBound; i++)
                result.Add((Id: i, IsInvalid: IsDuplicate(i.ToString())));
            return result;
        }
        
        private bool IsDuplicate(string value) 
        {
            char[] chars = value.ToCharArray();
            int half = chars.Length / 2;
            return chars[..half].SequenceEqual(chars[half..]);
        }

        public List<(long Id, bool IsInvalid)> ComputeIdsPart2()
        {
            List<(long Id, bool IsInvalid)> result = new(); 
            for (long i = LowerBound; i <= UpperBound; i++)
                result.Add(( Id: i, IsInvalid: IsSequenceRepeatedAtLeastTwice(i.ToString()) ));
            return result;
        }
        
        private bool IsSequenceRepeatedAtLeastTwice(string value)
        {
            int length = value.Length;

            for (int size = 1; size <= length / 2; size++)
            {
                if (length % size == 0)
                {
                    string current = value[..size];
                    bool isMatch = true;

                    for (int i = size; i < length; i += size)
                    {
                        if (!value.Substring(i, size).SequenceEqual(current))
                        {
                            isMatch = false;
                            break;
                        }
                    }

                    if (isMatch)
                        return true;
                    
                }
            }

            
            return false;
        }
        
        public override string ToString()
        {
            return LowerBound + "-" + UpperBound;
        }
    }
}

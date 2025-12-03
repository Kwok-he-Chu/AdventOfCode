using System;

namespace AOC2025;

/// <summary>
/// Day 3: Lobby
/// </summary>
public class D03
{
    private readonly AOCHttpClient _client = new AOCHttpClient(3);

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"987654321111111
// 811111111111119
// 234234234234278
// 818181911112111";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> models = new List<Model>();
        foreach (string s in split)
        {
            Model model = new Model(s.Select(x => long.Parse(x.ToString())).ToList());
            models.Add(model);
        }

        List<HashSet<long>> result = models.Select(x => x.ComputeHashSet_Two_Characters()).ToList();
        Console.WriteLine(result.Select(x=>x.Max()).Sum());
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"987654321111111
// 811111111111119
// 234234234234278
// 818181911112111";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Model> models = new List<Model>();
        foreach (string s in split)
        {
            Model model = new Model(s.Select(x => long.Parse(x.ToString())).ToList());
            models.Add(model);
        }

        List<long> result = models.Select(x => x.Compute_Largest_Twelve_Characters()).ToList();
        Console.WriteLine(result.Sum());
    }

    private class Model
    {
        public List<long> List { get; private set; }

        public Model(List<long> list)
        {
            List = list;
        }

        public HashSet<long> ComputeHashSet_Two_Characters()
        {
            HashSet<long> result = new HashSet<long>();
            for (int i = 0; i < List.Count; i++)
            {
                for (int j = i; j < List.Count; j++)
                {
                    if (i != j)
                    {
                        long number = long.Parse(List[i].ToString() + List[j].ToString());
                        result.Add(number);
                    }
                }
            }
            return result;
        }
        
        public long Compute_Largest_Twelve_Characters()
        {
            List<long> result = new List<long>();
            int currentIndex = List.Count - 12;


            foreach (long digit in List)
            {
                while (currentIndex > 0 && result.Count > 0)
                {
                    if (result[result.Count - 1] >= digit)
                    {
                        break;
                    }
                        
                    result.RemoveAt(result.Count - 1);
                    currentIndex--;
                }

                result.Add(digit);
            }

            if (result.Count <= 12) 
                return long.Parse(string.Concat(result));
            
            return long.Parse(string.Concat(result.Take(12).ToList()));
        }
    }
}
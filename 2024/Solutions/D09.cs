using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024;

/// <summary>
/// Day 9: Disk Fragmenter
/// </summary>
public class D09
{
    private readonly AOCHttpClient _client = new AOCHttpClient(9);

    public void Part1()
    {
        string input = _client.RetrieveFile().TrimEnd();
        
        //input = @"2333133121414131402";

        List<File> files = new List<File>();
        for (int id = 0; id < input.Length; id++)
        {
            int length = int.Parse(input[id].ToString());
            for (int j = 0; j < length; j++)
            {
                files.Add(new File()
                {
                    Id = id % 2 == 0 ? (id / 2).ToString() : "."
                });
            }
        }

        int lastIndex = files.Count - 1;
        for (int index = 0; index < lastIndex; index++)
        {
            File file = files[index];
            if (file.Id == ".")
            {
                while (files[lastIndex].Id == ".")
                {
                    lastIndex--;
                }
                
                if (index < lastIndex)
                {
                    files.Swap(index, lastIndex);
                }
            }
        }
        
        long sum = 0;
        for (int id = 0; id < files.Count; id++)
        {
            if (files[id].Id == ".")
                break;

            sum += id * (long.Parse(files[id].Id));
        }

        Console.WriteLine(sum);
    }

    public void Part2()
    {
        string input = _client.RetrieveFile().TrimEnd();

        input = @"2333133121414131402"; // 2858

        /*
         * 0...1...2......33333
           0...1...233333......
           0...1...233333......
           02..1....33333......
           021......33333......
           021......33333......
         */
        //input = @"1313165";; // Part2: 169
        //input = @"13345"; // && Part2 – 136
        // 1 3 3 4 5
        // 0 . . . 1 1 1 . . . . 2 2 2 2 2
        // 0 2 2 2 1 1 1 2 2 . . . . . . . 


        List<File> files = new List<File>();
        for (int id = 0; id < input.Length; id++)
        {
            int length = int.Parse(input[id].ToString());
            for (int j = 0; j < length; j++)
            {
                files.Add(new File()
                {
                    Id = id % 2 == 0 ? (id / 2).ToString() : ".",
                    Length = length
                });
            }
        }

        Console.WriteLine(string.Join("", files.Select(x => x.Id)));
        Console.WriteLine();
        
        int lastIndex = files.Count - 1;
        for (int index = 0; index < lastIndex; index++)
        {
            File file = files[index];
            if (file.Id == ".")
            {
                while (files[lastIndex].Id == ".")
                {
                    lastIndex--;
                }

                int newIndex = SwapLastWithCurrent(files, file, index, lastIndex);
                index = newIndex;
                Console.WriteLine(string.Join("", files.Select(x => x.Id)));
            }
        }
        
        long sum = 0;
        for (int id = 0; id < files.Count; id++)
        {
            if (files[id].Id == ".")
                break;

            sum += id * (long.Parse(files[id].Id));
        }

        Console.WriteLine(sum);
    }


    private static (File File, int Index)? FindLastFile(List<File> files, File file, int index, int lastIndex, (File File, int Index)? lastSeenFile)
    {
        if (lastIndex <= index)
        {
            return lastSeenFile;
        }
        
        if (files[lastIndex].Id == ".")
        {
            return FindLastFile(files, file, index, lastIndex - 1, lastSeenFile);
        }

        lastSeenFile = (files[lastIndex], lastIndex);
        
        if (files[lastIndex].Length <= file.Length)
        {
            return lastSeenFile;
        }

        return FindLastFile(files, file, index, lastIndex - 1, lastSeenFile);
    }
    
    private static int SwapLastWithCurrent(List<File> files, File file, int index, int lastIndex)
    {
        (File File, int Index)? lastFile = FindLastFile(files, file, index, lastIndex, null);

        int length = lastFile.Value.File.Length;
        int diff = file.Length - length;

        // ...3344 -> 44.33.. (Shift left with leftover space 'diff')
        if (diff > 0)
        {
            for (int x = 0; x < length; x++)
            {
                files.Swap(index, lastFile.Value.Index - x);
                index++;
            }

            files[index].Length = diff;
            return index - 1;
        }
        
        // ..4..33 -> 334.... (Shift 33 left, exactly 2 spots)
        if (diff == 0)
        {       
            for (int x = 0; x < length; x++)
            {
                files.Swap(index, lastFile.Value.Index - x);
                index++;
            }
            return index;
        }

        // .44... -> 44...   (Shift, cause all empty space)
        // for (int x = 0; x < length; x++)
        // {
        //     files.Swap(index, lastFile.Value.Index - x);
        //     index++;
        // }

        return index;
    }

    private class File
    {
        public string Id { get; init; }

        public int Length { get; set; }
        
        public override string ToString()
        {
            return $"{Id} {Length}";
        }
        
    }
}
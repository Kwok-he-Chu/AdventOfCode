using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023;

/// <summary>
/// Day 4: Scratchcards.
/// </summary>
public class D04
{
    private readonly AOCHttpClient _client = new AOCHttpClient(4);
    
    public void Execute1()
    {
        string input = _client.RetrieveFile();

        /*input = @"ScratchCard 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
ScratchCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
ScratchCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
ScratchCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
ScratchCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
ScratchCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";*/
        
        List<ScratchCard> scratchCards = ConvertInputToList(input);

        int sum = scratchCards
            .Select(card => card.IntersectionSet)
            .Sum(c => (int)Math.Pow(2, c.Count - 1));
        
        Console.WriteLine(sum);
    }

    public void Execute2()
    {
        string input = _client.RetrieveFile();

        /*input = @"ScratchCard 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
ScratchCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
ScratchCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
ScratchCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
ScratchCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
ScratchCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";*/
        
        List<ScratchCard> scratchCards = ConvertInputToList(input);

        int sum = 0;

        Queue<ScratchCard> queue = new Queue<ScratchCard>();
        foreach (ScratchCard card in scratchCards)
        {
            queue.Enqueue(card);
        }

        while (queue.Count > 0)
        {
            ScratchCard scratchCard = queue.Dequeue();
            sum++;
            
            int count = scratchCard.IntersectionSet.Count;
            List<int> newCardNumbers = Enumerable.Range(scratchCard.Number + 1, count).ToList();

            foreach (int number in newCardNumbers)
            {
                queue.Enqueue(scratchCards.First(x => x.Number == number));
            }
        }
        
        Console.WriteLine(sum);
    }

    private List<ScratchCard> ConvertInputToList(string input)
    {
        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return split
            .Select(line =>
                {
                    string[] cardString = line.Split(":");
                    string[] numberString = cardString[1].Split("|");

                    HashSet<int> setA = numberString[0]
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToHashSet();

                    HashSet<int> setB = numberString[1]
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToHashSet();

                    int number = int.Parse(cardString[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
                    return new ScratchCard(number, setA, setB);
                })
            .ToList();
    }
    
    private class ScratchCard
    {
        public int Number { get; }
        public HashSet<int> IntersectionSet { get; }

        private readonly HashSet<int> _setA;
        private readonly HashSet<int> _setB;
        
        public ScratchCard(int number, HashSet<int> setA, HashSet<int> setB)
        {
            _setA = setA;
            _setB = setB;
            Number = number;
            IntersectionSet =  _setA.Intersect(_setB).ToHashSet();
        }
        
        public override string ToString()
        {
            return $"Card {Number}: {string.Join(" ", _setA)} | {string.Join(" ", _setB)}";
        }
    }
}
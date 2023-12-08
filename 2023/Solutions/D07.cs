using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2023;

/// <summary>
/// Day 7: Camel Cards.
/// </summary>
public class D07
{
    private readonly AOCHttpClient _client = new AOCHttpClient(7);

    public void Part1()
    {
        string input = _client.RetrieveFile();

        input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        List<Hand> hands = split.Select(line =>
        {
            string[] hand = line.Split(" ");
            return new Hand(hand[0], int.Parse(hand[1]));
        }).ToList();

        List<Hand> result = new List<Hand>();
        foreach (Hand hand in hands)
        {
            result.Add(hand);
            for (int i = 0; i < result.Count - 1; i++)
            {
                EvaluationResult a = result[i].EvaluationResult;
                EvaluationResult b = result[i + 1].EvaluationResult;
                if (IsHigherHandCategory(a, b))
                {
                    result.Swap(i, i + 1);
                }
            }
        }

        long sum = 0;
        for (var i = 0; i < result.Count; i++)
        {
            var res = result[i];
            long multiply = (i + 1) * res.Bid;
            sum += multiply;
            Console.WriteLine(multiply);
        }

        Console.WriteLine(sum);
    }

    private bool IsHigherHandCategory(EvaluationResult a, EvaluationResult b)
    {
        if (a.HandCategory < b.HandCategory)
            return false;

        if (a.HandCategory == b.HandCategory)
        {
            if (a.HandCategory is HandCategory.FOUR_OF_A_KIND)
            {    
                if (a.XOfAKind < b.XOfAKind)
                    return false;
                return a.NextHighCards.First() > b.NextHighCards.First();
            }

            if (a.HandCategory is HandCategory.FULL_HOUSE)
            {
                if (a.XOfAKind < b.XOfAKind)
                    return false;

                return a.PairOne > b.PairOne;
            }

            if (a.HandCategory is HandCategory.THREE_OF_A_KIND)
            {
                if (a.XOfAKind < b.XOfAKind)
                    return false;
                
                if (a.NextHighCards.First() < b.NextHighCards.First())
                    return false;

                return a.NextHighCards[1] > b.NextHighCards[1];
            }
            
            if (a.HandCategory is HandCategory.TWO_PAIR)
            {
                if (a.PairOne < b.PairOne)
                    return false;
                
                if (a.PairTwo < b.PairTwo)
                    return false;

                return a.NextHighCards.First() > b.NextHighCards.First();
            }
                        
            if (a.HandCategory is HandCategory.ONE_PAIR)
            {
                if (a.PairOne < b.PairOne)
                    return false;
                
                if (a.NextHighCards[0] < b.NextHighCards[0])
                    return false;
                
                if (a.NextHighCards[1] < b.NextHighCards[1])
                    return false;

                return a.NextHighCards[2] > b.NextHighCards[2];
            }
        }

        return true; // a.HandCategory > b.HandCategory
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483
";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine();
    }
    
    private class Hand
    {
        public List<Card> Cards { get; }
        public int Bid { get; }
        public EvaluationResult EvaluationResult { get; }
        
        public Hand(String cards, int bid)
        {
            Cards = cards.Select(c => new Card(c)).ToList(); 
            Bid = bid;
            EvaluationResult = Evaluate();
        }

        private EvaluationResult Evaluate()
        {
            return IsXOfAKind(5) ??
                   IsXOfAKind(4) ??
                   IsFullHouse() ??
                   IsXOfAKind(3) ??
                   IsTwoPair() ??
                   IsOnePair() ??
                   new EvaluationResult()
                   {
                       HandCategory = HandCategory.HIGH_CARD,
                       NextHighCards = new List<int>() { Cards[0].Rank }
                   };
        }

        private EvaluationResult? IsFullHouse()
        {
            var groups = Cards.GroupBy(card => card.Rank).ToList();
            var threeOfAKind = groups.Where(group => group.Count() == 3).ToList();
            var onePair = groups.Where(group => group.Count() == 2).ToList();

            if (threeOfAKind.Count == 1 && onePair.Count == 1)
            {
                return new EvaluationResult()
                {
                    HandCategory = HandCategory.FULL_HOUSE,
                    XOfAKind = threeOfAKind.First().Key,
                    PairOne = onePair.First().Key,
                    NextHighCards = new List<int>()
                };
            }

            return null;
        }

        private EvaluationResult? IsXOfAKind(int number)
        {
            if (number is < 3 or > 5)
                throw new ArgumentOutOfRangeException();
            
            var groups = Cards.GroupBy(card => card.Rank).ToList();
            var result = groups.Where(group => group.Count() == number).ToList();
            if (result.Count == 1)
            {
                return new EvaluationResult
                {
                    HandCategory = number == 5 ? HandCategory.FIVE_OF_A_KIND 
                        : number == 4 ? HandCategory.FOUR_OF_A_KIND 
                        : HandCategory.THREE_OF_A_KIND,
                    XOfAKind = result.First().Key,
                    NextHighCards = GetHighCardWithout(result.First().Key)
                };
            }

            return null;
        }

        private EvaluationResult? IsTwoPair()
        {
            var groups = Cards.GroupBy(card => card.Rank).ToList();
            var result = groups.Where(group => group.Count() == 2).ToList();
            if (result.Count == 2)
            {
                return new EvaluationResult
                {
                    HandCategory = HandCategory.TWO_PAIR,
                    PairOne = result[0].Key,
                    PairTwo = result[1].Key,
                    NextHighCards = GetHighCardWithout(result[0].Key, result[1].Key)
                };
            }

            return null;
        }

        private EvaluationResult? IsOnePair()
        {
            var groups = Cards.GroupBy(card => card.Rank).ToList();
            var result = groups.Where(group => group.Count() == 2).ToList();
            
            if (result.Count == 1)
            {
                return new EvaluationResult
                {
                    HandCategory = HandCategory.ONE_PAIR,
                    PairOne = result.First().Key,
                    NextHighCards = GetHighCardWithout(result.First().Key)
                };
            }

            return null;
        }

        private List<int> GetHighCardWithout(int rankToExclude)
        {
            return Cards.Where(card => card.Rank != rankToExclude)
                .OrderByDescending(card => card.Rank)
                .Select(card => card.Rank)
                .ToList();
        }

        private List<int> GetHighCardWithout(int rankToExcludeA, int rankToExcludeB)
        {
            return Cards.Where(card => card.Rank != rankToExcludeA && card.Rank != rankToExcludeB)
                .OrderByDescending(card => card.Rank)
                .Select(card => card.Rank)
                .ToList();
        }

        public override string ToString()
        {
            return $"{String.Join("", Cards)} {Bid} \n{EvaluationResult}";
        }
    }

    private enum HandCategory
    {
        HIGH_CARD = 1,
        ONE_PAIR = 2,
        TWO_PAIR = 3,
        THREE_OF_A_KIND = 4,
        FULL_HOUSE = 5,
        FOUR_OF_A_KIND = 6,
        FIVE_OF_A_KIND = 7
    }

    private record EvaluationResult(HandCategory? HandCategory = null, int XOfAKind = 0, int PairOne = 0, int PairTwo = 0, List<int> NextHighCards = null)
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{HandCategory} | ");
            if (NextHighCards.Count > 0)
            {
                sb.Append($"NextHighCard: [ {string.Join(", ", NextHighCards)} ] | ");
            }
            if (PairOne != 0) sb.Append($"PairOne: {PairOne} | ");
            if (PairTwo != 0) sb.Append($"PairTwo: {PairTwo} | ");
            if (XOfAKind != 0) sb.Append($"XOfAKind: {XOfAKind} | ");
            return sb.ToString();
        }
    }
    
    private class Card 
    {
        public int Rank { get; }
        private readonly char _rank;

        public Card(char rank)
        {
            _rank = rank; 
            Rank = rank switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ when char.IsDigit(rank) => int.Parse(rank.ToString()),
                _ => throw new ArgumentOutOfRangeException(rank.ToString())
            };
        }

        public override string ToString()
        {
            return $"{_rank}";
        }
    }
}
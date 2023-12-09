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

        /*input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";*/

        List<Hand> hands = ConvertInputToList(input, false);

        List<Hand> result = new List<Hand>();

        foreach (Hand hand in hands)
        {
            result.Add(hand);
            for (int i = result.Count - 1; i > 0; i--)
            {
                EvaluationResult a = result[i].EvaluationResult;
                EvaluationResult b = result[i - 1].EvaluationResult;
                if (IsHigherHandCategory(a, b))
                {
                    result.Swap(i, i - 1);
                }
            }
        }

        long sum = 0;
        for (var i = result.Count - 1; i >= 0; i--)
        {
            Hand res = result[i];
            long multiply = (result.Count - i) * res.Bid;
            sum += multiply;
        }

        Console.WriteLine(sum);
    }

    private bool IsHigherHandCategory(EvaluationResult a, EvaluationResult b)
    {
        if (a.HandCategory == b.HandCategory)
        {
            for (int i = 0; i < 5; i++)
            {
                if (a.Hand.OriginalCards[i].Rank > b.Hand.OriginalCards[i].Rank)
                {
                    return true;
                }
                else if (a.Hand.OriginalCards[i].Rank == b.Hand.OriginalCards[i].Rank)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        return a.HandCategory > b.HandCategory;
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

        /*input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";*/

        List<Hand> hands = ConvertInputToList(input, true);

        List<Hand> result = new List<Hand>();

        foreach (Hand hand in hands)
        {
            result.Add(hand);
            for (int i = result.Count - 1; i > 0; i--)
            {
                EvaluationResult a = result[i].EvaluationResult;
                EvaluationResult b = result[i - 1].EvaluationResult;
                if (IsHigherHandCategory(a, b))
                {
                    result.Swap(i, i - 1);
                }
            }
        }

        foreach (var r in result)
        {
            Console.WriteLine(string.Join("", r.Cards) + " |  " + r.Bid);
        }

        long sum = 0;
        for (var i = result.Count - 1; i >= 0; i--)
        {
            Hand res = result[i];
            long multiply = (result.Count - i) * res.Bid;
            sum += multiply;
        }

        Console.WriteLine(sum);
    }


    private List<Hand> ConvertInputToList(string input, bool useJokers)
    {
        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return split.Select(line =>
        {
            string[] hand = line.Split(" ");
            return new Hand(hand[0], int.Parse(hand[1]), useJokers);
        }).ToList();
    }

    private class Hand
    {
        public List<Card> OriginalCards { get; private set; }
        public List<Card> Cards { get; private set; }
        public int Bid { get; private set; }
        public EvaluationResult EvaluationResult { get; private set; }

        public Hand(string cards, int bid, bool useJokers)
        {
            Bid = bid;
            OriginalCards = cards
                .Select(c => new Card(c, useJokers))
                .ToList();
            Cards = cards
                .Select(c => new Card(c, useJokers))
                .ToList();
            EvaluationResult = useJokers
                ? EvaluateWithJokers()
                : Evaluate();
        }

        private void ReplaceJokerWith(int rank)
        {
            char rankCharacter = rank switch
            {
                1 => 'J',
                14 => 'A',
                13 => 'K',
                12 => 'Q',
                10 => 'T',
                _ when rank >= 2 && rank <= 9 => (char)(rank + '0'), // [2 .. 9]
                _ => throw new ArgumentOutOfRangeException()
            };

            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].IsJoker)
                {
                    Cards[i] = new Card(rankCharacter);
                }
            }
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
                       Hand = this,
                       HandCategory = HandCategory.HIGH_CARD,
                       NextHighCards = new List<Card>() { Cards.MaxBy(x => x.Rank) }
                   };
        }

        private EvaluationResult EvaluateWithJokers()
        {
            EvaluationResult = Evaluate();
            int numberOfJokers = EvaluationResult.Hand.Cards.Count(x => x.IsJoker);
            if (numberOfJokers > 0)
            {
                switch (EvaluationResult.HandCategory)
                {
                    case HandCategory.FIVE_OF_A_KIND:
                        // JJJJJ
                        return EvaluationResult;
                    case HandCategory.FOUR_OF_A_KIND:
                        if (numberOfJokers == 1)
                        {
                            // 4444[J]
                            // 4444[4]
                            this.ReplaceJokerWith(EvaluationResult.XOfAKind); // 4
                        }
                        else if (numberOfJokers == 4)
                        {
                            // [JJJJ]A
                            // [AAAA]A
                            this.ReplaceJokerWith(EvaluationResult.NextHighCards.First().Rank); // A
                        }
                        else
                        {
                            throw new ArgumentException();
                        }

                        break;
                    case HandCategory.FULL_HOUSE:
                        if (numberOfJokers == 3)
                        {
                            /// [JJJ]AA
                            /// [AAA]AA
                            this.ReplaceJokerWith(EvaluationResult.PairOne); // AA
                        }
                        else if (numberOfJokers == 2)
                        {
                            /// 444[JJ]
                            /// 444[44]
                            this.ReplaceJokerWith(EvaluationResult.XOfAKind);  // 44
                        }
                        else
                        {
                            throw new ArgumentException();
                        }

                        break;
                    case HandCategory.THREE_OF_A_KIND:
                        if (numberOfJokers == 3)
                        {
                            /// [JJJ]A1
                            /// [AAA]A1
                            this.ReplaceJokerWith(EvaluationResult.NextHighCards.First().Rank); // AAA
                        }
                        else if (numberOfJokers == 1)
                        {
                            /// 444[J]A 
                            /// 444[4]A
                            this.ReplaceJokerWith(EvaluationResult.XOfAKind); // 4

                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        break;
                    case HandCategory.TWO_PAIR:
                        if (numberOfJokers == 2)
                        {
                            /// [JJ]AA1
                            /// [AA]AA1

                            /// AA[JJ]1
                            /// AA[AA]1
                            var higherTwoPair = Math.Max(EvaluationResult.PairOne, EvaluationResult.PairTwo);
                            this.ReplaceJokerWith(higherTwoPair); // AA
                        }
                        else if (numberOfJokers == 1)
                        {
                            /// 44[22]J
                            /// 44[44]J

                            /// QQ[55]J
                            /// QQ[QQ]J
                            var lowerTwoPair = Math.Min(EvaluationResult.PairOne, EvaluationResult.PairTwo);
                            this.ReplaceJokerWith(lowerTwoPair); // QQ or 44

                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        break;
                    case HandCategory.ONE_PAIR:
                        if (numberOfJokers == 1)
                        {
                            // 88[J]56
                            // 88[8]56
                            this.ReplaceJokerWith(EvaluationResult.PairOne); // 8
                        }
                        else if (numberOfJokers == 2)
                        {
                            // [JJ]234
                            // [44]234
                            this.ReplaceJokerWith(EvaluationResult.NextHighCards.First().Rank); // 44
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        break;
                    case HandCategory.HIGH_CARD:
                        if (numberOfJokers == 1)
                        {
                            // 2345[J]
                            // 2345[5]
                            this.ReplaceJokerWith(EvaluationResult.NextHighCards.First().Rank); // 5
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            EvaluationResult = Evaluate();
            return EvaluationResult;
        }

        private EvaluationResult IsFullHouse()
        {
            var groups = Cards.GroupBy(card => card.Rank).ToList();
            var threeOfAKind = groups.Where(group => group.Count() == 3).ToList();
            var onePair = groups.Where(group => group.Count() == 2).ToList();

            if (threeOfAKind.Count == 1 && onePair.Count == 1)
            {
                return new EvaluationResult()
                {
                    Hand = this,
                    HandCategory = HandCategory.FULL_HOUSE,
                    XOfAKind = threeOfAKind.First().Key,
                    PairOne = onePair.First().Key,
                    NextHighCards = new List<Card>()
                };
            }

            return null;
        }

        private EvaluationResult IsXOfAKind(int number, bool useJokers = false)
        {
            if (number is < 3 or > 5)
                throw new ArgumentOutOfRangeException();

            var groups = Cards.GroupBy(card => card.Rank).ToList();
            var result = groups.Where(group => group.Count() == number).ToList();
            if (result.Count == 1)
            {
                return new EvaluationResult
                {
                    Hand = this,
                    HandCategory = number == 5 ? HandCategory.FIVE_OF_A_KIND
                        : number == 4 ? HandCategory.FOUR_OF_A_KIND
                        : HandCategory.THREE_OF_A_KIND,
                    XOfAKind = result.First().Key,
                    NextHighCards = GetHighCardWithout(result.First().Key)
                };
            }

            return null;
        }

        private EvaluationResult IsTwoPair()
        {
            var groups = Cards.GroupBy(card => card.Rank).ToList();
            var result = groups.Where(group => group.Count() == 2).ToList();
            if (result.Count == 2)
            {
                return new EvaluationResult
                {
                    Hand = this,
                    HandCategory = HandCategory.TWO_PAIR,
                    PairOne = result[0].Key,
                    PairTwo = result[1].Key,
                    NextHighCards = GetHighCardWithout(result[0].Key, result[1].Key)
                };
            }

            return null;
        }

        private EvaluationResult IsOnePair()
        {
            var groups = Cards.GroupBy(card => card.Rank).ToList();
            var result = groups.Where(group => group.Count() == 2).ToList();

            if (result.Count == 1)
            {
                return new EvaluationResult
                {
                    Hand = this,
                    HandCategory = HandCategory.ONE_PAIR,
                    PairOne = result.First().Key,
                    NextHighCards = GetHighCardWithout(result.First().Key)
                };
            }

            return null;
        }

        private List<Card> GetHighCardWithout(int rankToExclude)
        {
            return Cards.Where(card => card.Rank != rankToExclude)
                .OrderByDescending(card => card.Rank)
                .ToList();
        }

        private List<Card> GetHighCardWithout(int rankToExcludeA, int rankToExcludeB)
        {
            return Cards.Where(card => card.Rank != rankToExcludeA && card.Rank != rankToExcludeB)
                .OrderByDescending(card => card.Rank)
                .ToList();
        }

        public override string ToString()
        {
            return $"{string.Join("", Cards)} {Bid} \n{EvaluationResult}";
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

    private record EvaluationResult(Hand Hand = null, HandCategory? HandCategory = null, int XOfAKind = 0, int PairOne = 0, int PairTwo = 0, List<Card> NextHighCards = null)
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{HandCategory} {string.Join("", Hand.OriginalCards)} | ");
            if (NextHighCards.Count > 0) sb.Append($"NextHighCard: [ {string.Join(", ", NextHighCards)} ] | ");
            if (PairOne != 0) sb.Append($"PairOne: {PairOne} | ");
            if (PairTwo != 0) sb.Append($"PairTwo: {PairTwo} | ");
            if (XOfAKind != 0) sb.Append($"XOfAKind: {XOfAKind} | ");
            return sb.ToString();
        }
    }

    private class Card
    {
        public int Rank { get; }

        private char _rank;

        public bool IsJoker => _rank == 'J';

        public Card(char rank, bool useJokers = false)
        {
            _rank = rank;
            Rank = rank switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => useJokers ? 1 : 11,
                'T' => 10,
                _ when char.IsDigit(rank) => int.Parse(rank.ToString()),
                _ => throw new ArgumentOutOfRangeException(rank.ToString())
            };
        }

        public override string ToString()
        {
            return _rank.ToString();
        }
    }
}
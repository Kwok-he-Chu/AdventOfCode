using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2022
{
    /// <summary>
    /// Day 11: Monkey in the Middle
    /// </summary>
    public class D11
    {
        private readonly AocHttpClient _client = new AocHttpClient(11);

        private static Func<long, long, long> _funcMultiply => (x, y) => x * y;
        private static Func<long, long, long> _funcPower => (x, _) => x * x;
        private static Func<long, long, long> _funcPlus => (x, y) => x + y;

        public void Execute1()
        {
            string input = _client.RetrieveFile();
            //input = "Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3\r\n\r\nMonkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0\r\n\r\nMonkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3\r\n\r\nMonkey 3:\r\n  Starting items: 74\r\n  Operation: new = old + 3\r\n  Test: divisible by 17\r\n    If true: throw to monkey 0\r\n    If false: throw to monkey 1";
            string[] split = input.Split("\r\n");
            List<MonkeyModel> monkeys = GetMonkeyList(split);

            for (int round = 0; round < 20; round++)
                foreach (MonkeyModel monkey in monkeys)
                    monkey.InspectItems();

            List<MonkeyModel> result = monkeys.OrderByDescending(x => x.NumberOfInspections).Take(2).ToList();
            Console.WriteLine(result[0].NumberOfInspections * result[1].NumberOfInspections);
        }

        private List<MonkeyModel> GetMonkeyList(string[] split)
        {
            List<MonkeyModel> result = new List<MonkeyModel>();

            // Parse input.
            for (int i = 0; i < split.Length; i += 7)
            {
                if (split[i].StartsWith("Monkey"))
                {
                    (long Number, Func<long, long, long> Func) operationTuple = ParseOperationFunction(split[i + 2]);
                    List<long> items = split[i + 1].Replace("Starting items:", string.Empty).Split(',').Select(long.Parse).ToList();
                    Queue<long> queue = new Queue<long>();
                    foreach (long item in items)
                    {
                        queue.Enqueue(item);
                    }
                    result.Add(new MonkeyModel()
                    {
                        MonkeyId = int.Parse(split[i].Replace("Monkey", string.Empty).Replace(":", string.Empty)),
                        Items = queue,
                        OperationNumber = operationTuple.Number,
                        Func = operationTuple.Func,
                        DivisionNumber = long.Parse(split[i + 3].Replace("Test: divisible by", string.Empty)),
                        IfTrueMonkeyId = int.Parse(split[i + 4].Replace("If true: throw to monkey", string.Empty)),
                        IfFalseMonkeyId = int.Parse(split[i + 5].Replace("If false: throw to monkey", string.Empty))
                    });
                }
            }

            // Set references.
            foreach (MonkeyModel m in result)
            {
                m.TrueMonkey = result.FirstOrDefault(x => x.MonkeyId == m.IfTrueMonkeyId) ?? throw new ArgumentNullException();
                m.FalseMonkey = result.FirstOrDefault(x => x.MonkeyId == m.IfFalseMonkeyId) ?? throw new ArgumentNullException();
            }

            return result;
        }

        private (long Number, Func<long, long, long> Func) ParseOperationFunction(string line)
        {
            string sub = line.Replace("Operation: new = old", string.Empty);
            string[] split = sub.Split("*");
            if (split.Length == 2)
            {
                if (split[1] == " old")
                    return (0, _funcPower);
                return (long.Parse(split[1]), _funcMultiply);
            }

            split = sub.Split('+');
            if (split.Length == 2)
                return (long.Parse(split[1]), _funcPlus);

            throw new ArgumentOutOfRangeException();
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile();
            //input = "Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3\r\n\r\nMonkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0\r\n\r\nMonkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3\r\n\r\nMonkey 3:\r\n  Starting items: 74\r\n  Operation: new = old + 3\r\n  Test: divisible by 17\r\n    If true: throw to monkey 0\r\n    If false: throw to monkey 1";
            string[] split = input.Split("\r\n");

            List<MonkeyModel> monkeys = GetMonkeyList(split);

            long leastCommonMultiple = monkeys
                .Select(m => m.DivisionNumber)
                .Aggregate(FindLeastCommonMultiple);

            for (int round = 0; round < 10000; round++)
                foreach (MonkeyModel monkey in monkeys)
                    monkey.InspectItems(true, leastCommonMultiple);

            List<MonkeyModel> result = monkeys.OrderByDescending(x => x.NumberOfInspections).Take(2).ToList();
            Console.WriteLine(result[0].NumberOfInspections * result[1].NumberOfInspections);
        }

        private long FindGreatestCommonDivisor(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        private long FindLeastCommonMultiple(long a, long b)
        {
            return a / FindGreatestCommonDivisor(a, b) * b;
        }
    }

    public class MonkeyModel
    {
        public int MonkeyId { get; set; }
        public Queue<long> Items { get; set; }
        public Func<long, long, long> Func { get; set; }

        public long OperationNumber { get; set; }

        public long DivisionNumber { get; set; }

        public int IfTrueMonkeyId { get; set; }
        public MonkeyModel TrueMonkey { get; set; }

        public int IfFalseMonkeyId { get; set; }
        public MonkeyModel FalseMonkey { get; set; }

        public long NumberOfInspections;

        public void InspectItems(bool part2 = false, long leastCommonMultiple = 0)
        {
            Extensions.WriteLine($"_____\nMonkey {MonkeyId}");

            while (Items.Count > 0)
            {
                long item = Items.Dequeue();

                long oldItemValue = Func.Invoke(item, OperationNumber);
                Extensions.WriteLine("worry level: " + item);
                Extensions.WriteLine("multiplied level: " + oldItemValue);

                long newItemValue;

                if (part2)
                    newItemValue = oldItemValue % leastCommonMultiple;
                else
                    newItemValue = oldItemValue / 3;

                bool isDivisibleBy = TestDivisibleby(newItemValue);
                Extensions.WriteLine(isDivisibleBy.ToString());
                if (isDivisibleBy)
                {
                    TrueMonkey.Items.Enqueue(newItemValue);
                    Extensions.WriteLine($"Pass to {IfTrueMonkeyId}  | {newItemValue}");
                }
                else
                {
                    FalseMonkey.Items.Enqueue(newItemValue);
                    Extensions.WriteLine($"Pass to {IfFalseMonkeyId} | {newItemValue}");
                }
                NumberOfInspections++;
            }
        }

        private bool TestDivisibleby(long value)
        {
            return value % DivisionNumber == 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (long item in Items)
                sb.Append($"{item}, ");
            return $"{MonkeyId} :{DivisionNumber} o{OperationNumber} #{NumberOfInspections} - {sb}";
        }
    }
}
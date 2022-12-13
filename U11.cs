using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2022
{
    public class U11
    {
        private readonly AocHttpClient _client = new AocHttpClient(11);

        public static Func<ulong, ulong, ulong> _funcMultiply => (x, y) => x * y;
        public static Func<ulong, ulong, ulong> _funcPower => (x, _) => x * x;
        public static Func<ulong, ulong, ulong> _funcPlus => (x, y) => x + y;

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3\r\n\r\nMonkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0\r\n\r\nMonkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3\r\n\r\nMonkey 3:\r\n  Starting items: 74\r\n  Operation: new = old + 3\r\n  Test: divisible by 17\r\n    If true: throw to monkey 0\r\n    If false: throw to monkey 1";
            string[] split = input.Split("\r\n");

            List<MonkeyModel> monkeys = new List<MonkeyModel>();
            for (int i = 0; i < split.Length; i += 7)
            {
                if (split[i].StartsWith("Monkey"))
                {
                    var operationTuple = ParseOperation(split[i + 2]);
                    List<ulong> items = ParseStartingItems(split[i + 1]);
                    Queue<ulong> queue = new Queue<ulong>();
                    foreach (var item in items)
                    {
                        queue.Enqueue(item);
                    }
                    monkeys.Add(new MonkeyModel()
                    {
                        MonkeyId = ParseMonkeyId(split[i]),
                        Items = queue,
                        OperationNumber = operationTuple.Number,
                        Func = operationTuple.Func,
                        DivisionNumber = ParseDivisionNumber(split[i + 3]),
                        IfTrueMonkeyId = ParseIfTrue(split[i + 4]),
                        IfFalseMonkeyId = ParseIfFalse(split[i + 5])
                    });

                }
            }

            foreach (MonkeyModel m in monkeys)
            {
                m.TrueMonkey = FindMonkey(monkeys, m.IfTrueMonkeyId);
                m.FalseMonkey = FindMonkey(monkeys, m.IfFalseMonkeyId);
            }


            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.InspectItems();
                }
            }

            List<MonkeyModel> result = monkeys.OrderByDescending(x => x.NumberOfInspections).Take(2).ToList();
            Console.WriteLine(result[0].NumberOfInspections * result[1].NumberOfInspections);
        }

        private int ParseMonkeyId(string line)
        {
            return int.Parse(line.Replace("Monkey", string.Empty).Replace(":", string.Empty));
        }


        private List<ulong> ParseStartingItems(string line)
        {
            return line.Replace("Starting items:", string.Empty).Split(',').Select(ulong.Parse).ToList();
        }

        private (ulong Number, Func<ulong, ulong, ulong> Func) ParseOperation(string line)
        {
            string sub = line.Replace("Operation: new = old", string.Empty);
            string[] split = sub.Split("*");
            if (split.Length == 2)
            {
                if (split[1] == " old")
                    return (0, _funcPower);
                return (ulong.Parse(split[1]), _funcMultiply);
            }

            split = sub.Split('+');
            if (split.Length == 2)
            {
                return (ulong.Parse(split[1]), _funcPlus);
            }

            throw new ArgumentOutOfRangeException();
        }

        private ulong ParseDivisionNumber(string line)
        {
            return ulong.Parse(line.Replace("Test: divisible by", string.Empty));
        }

        private int ParseIfTrue(string line)
        {
            return int.Parse(line.Replace("If true: throw to monkey", string.Empty));
        }

        private int ParseIfFalse(string line)
        {
            return int.Parse(line.Replace("If false: throw to monkey", string.Empty));
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            input = "Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3\r\n\r\nMonkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0\r\n\r\nMonkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3\r\n\r\nMonkey 3:\r\n  Starting items: 74\r\n  Operation: new = old + 3\r\n  Test: divisible by 17\r\n    If true: throw to monkey 0\r\n    If false: throw to monkey 1";
            string[] split = input.Split("\r\n");

            List<MonkeyModel> monkeys = new List<MonkeyModel>();
            for (int i = 0; i < split.Length; i += 7)
            {
                if (split[i].StartsWith("Monkey"))
                {
                    var operationTuple = ParseOperation(split[i + 2]);
                    List<ulong> items = ParseStartingItems(split[i + 1]);
                    Queue<ulong> queue = new Queue<ulong>();
                    foreach (var item in items)
                    {
                        queue.Enqueue(item);
                    }
                    monkeys.Add(new MonkeyModel()
                    {
                        MonkeyId = ParseMonkeyId(split[i]),
                        Items = queue,
                        OperationNumber = operationTuple.Number,
                        Func = operationTuple.Func,
                        DivisionNumber = ParseDivisionNumber(split[i + 3]),
                        IfTrueMonkeyId = ParseIfTrue(split[i + 4]),
                        IfFalseMonkeyId = ParseIfFalse(split[i + 5])
                    });

                }
            }

            foreach (MonkeyModel m in monkeys)
            {
                m.TrueMonkey = FindMonkey(monkeys, m.IfTrueMonkeyId);
                m.FalseMonkey = FindMonkey(monkeys, m.IfFalseMonkeyId);
            }

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.InspectItems(true);
                }
            }

            List<MonkeyModel> result = monkeys.OrderByDescending(x => x.NumberOfInspections).Take(2).ToList();
            Console.WriteLine(result[0].NumberOfInspections * result[1].NumberOfInspections);
        }

        private MonkeyModel FindMonkey(List<MonkeyModel> list, int monkeyId)
        {
            return list.FirstOrDefault(x => x.MonkeyId == monkeyId) ?? throw new ArgumentNullException();
        }
    }

    public class MonkeyModel
    {
        private readonly bool _isDebug = false;

        public int MonkeyId { get; set; }
        public Queue<ulong> Items { get; set; }
        public Func<ulong, ulong, ulong> Func { get; set; }

        public ulong OperationNumber { get; set; }

        public ulong DivisionNumber { get; set; }

        public int IfTrueMonkeyId { get; set; }
        public MonkeyModel TrueMonkey { get; set; }

        public int IfFalseMonkeyId { get; set; }
        public MonkeyModel FalseMonkey { get; set; }

        public int NumberOfInspections = 0;

        public void InspectItems(bool part2 = false)
        {
            CustomWriteLine($"_____\nMonkey {MonkeyId}");

            while (Items.Count > 0)
            {
                var currentWorryLevel = Items.Dequeue();
                if (_isDebug)
                    Console.ReadKey();

                ulong value = Func.Invoke(currentWorryLevel, OperationNumber);
                CustomWriteLine("worry level: " + currentWorryLevel);
                CustomWriteLine("multiplied level: " + value);

                ulong dividedByThree = value / 3;

                if (part2)
                    dividedByThree = value;

                bool isDivisibleBy = TestDivisibleby(dividedByThree);
                CustomWriteLine((dividedByThree % DivisionNumber == 0).ToString());
                if (isDivisibleBy)
                {
                    TrueMonkey.Items.Enqueue(dividedByThree);
                    CustomWriteLine($"Pass to {dividedByThree}   " + IfTrueMonkeyId);
                }
                else
                {
                    FalseMonkey.Items.Enqueue(dividedByThree);
                    CustomWriteLine($"Pass to {dividedByThree}   " + IfFalseMonkeyId);
                }
                NumberOfInspections++;
            }
        }

        private void CustomWriteLine(string line)
        {
            if (_isDebug)
                Console.WriteLine(line);
        }

        private bool TestDivisibleby(ulong value)
        {
            return value % DivisionNumber == 0;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in Items)
            {
                sb.Append($"{item}, ");
            }
            return $"{MonkeyId} :{DivisionNumber} o{OperationNumber} #{NumberOfInspections} - {sb.ToString()}";
        }
    }
}
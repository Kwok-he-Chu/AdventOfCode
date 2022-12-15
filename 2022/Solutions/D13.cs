using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AOC2022
{
    /// <summary>
    /// Day 13: Distress Signal
    /// </summary>
    public class D13
    {
        private readonly AocHttpClient _client = new AocHttpClient(13);

        public void Execute1()
        {
            string input = _client.RetrieveFile();
            //input = "[1,1,3,1,1]\r\n[1,1,5,1,1]\r\n\r\n[[1],[2,3,4]]\r\n[[1],4]\r\n\r\n[9]\r\n[[8,7,6]]\r\n\r\n[[4,4],4,4]\r\n[[4,4],4,4,4]\r\n\r\n[7,7,7,7]\r\n[7,7,7]\r\n\r\n[]\r\n[3]\r\n\r\n[[[]]]\r\n[[]]\r\n\r\n[1,[2,[3,[4,[5,6,7]]]],8,9]\r\n[1,[2,[3,[4,[5,6,0]]]],8,9]";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            List<int> list = GetComparisonsListResultPart1(split);

            int sum = 0;
            for (int i = 0; i < list.Count; i++)
                if (list[i] < 0)
                    sum += i + 1;
            Console.WriteLine(sum);
        }

        private List<int> GetComparisonsListResultPart1(string[] split)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < split.Length - 1; i += 2)
            {
                JsonElement left = JsonSerializer.Deserialize<JsonElement>(split[i]);
                JsonElement right = JsonSerializer.Deserialize<JsonElement>(split[i + 1]);

                int value = CheckJsonElementValueKind(left, right);
                list.Add(value);
            }

            return list;
        }

        private int CheckJsonElementValueKind(JsonElement left, JsonElement right)
        {
            if (left.ValueKind == JsonValueKind.Number && right.ValueKind == JsonValueKind.Number)
                return left.GetInt32() - right.GetInt32();

            if (left.ValueKind == JsonValueKind.Number && right.ValueKind == JsonValueKind.Array)
                return Compare(JsonSerializer.Deserialize<JsonElement>($"[{left.GetInt32()}]"), right);

            if (left.ValueKind == JsonValueKind.Array && right.ValueKind == JsonValueKind.Number)
                return Compare(left, JsonSerializer.Deserialize<JsonElement>($"[{right.GetInt32()}]"));

            return Compare(left, right);
        }

        private int Compare(JsonElement left, JsonElement right)
        {
            JsonElement.ArrayEnumerator leftEnumerator = left.EnumerateArray();
            JsonElement.ArrayEnumerator rightEnumerator = right.EnumerateArray();
            while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
            {
                int result = CheckJsonElementValueKind(leftEnumerator.Current, rightEnumerator.Current);
                if (result == 0)
                    continue;

                return result;
            }
            return left.GetArrayLength() - right.GetArrayLength();
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile();
            //input = "[1,1,3,1,1]\r\n[1,1,5,1,1]\r\n\r\n[[1],[2,3,4]]\r\n[[1],4]\r\n\r\n[9]\r\n[[8,7,6]]\r\n\r\n[[4,4],4,4]\r\n[[4,4],4,4,4]\r\n\r\n[7,7,7,7]\r\n[7,7,7]\r\n\r\n[]\r\n[3]\r\n\r\n[[[]]]\r\n[[]]\r\n\r\n[1,[2,[3,[4,[5,6,7]]]],8,9]\r\n[1,[2,[3,[4,[5,6,0]]]],8,9]";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int result = GetComparisonsResultPart2(split);
            Console.WriteLine(result);
        }

        private int GetComparisonsResultPart2(string[] split)
        {
            List<JsonElement> elements = new List<JsonElement>();
            JsonElement element2 = JsonSerializer.Deserialize<JsonElement>($"[[2]]");
            JsonElement element6 = JsonSerializer.Deserialize<JsonElement>($"[[6]]");
            elements.Add(element2);
            elements.Add(element6);

            for (int i = 0; i < split.Length; i ++)
                elements.Add(JsonSerializer.Deserialize<JsonElement>(split[i]));

            elements.Sort(Compare);

            return (elements.IndexOf(element2) + 1) * (elements.IndexOf(element6) + 1);
        }
    }
}
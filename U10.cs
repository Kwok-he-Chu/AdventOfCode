using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class U10
    {
        private readonly AocHttpClient _client = new AocHttpClient(10);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "addx 15\r\naddx -11\r\naddx 6\r\naddx -3\r\naddx 5\r\naddx -1\r\naddx -8\r\naddx 13\r\naddx 4\r\nnoop\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx -35\r\naddx 1\r\naddx 24\r\naddx -19\r\naddx 1\r\naddx 16\r\naddx -11\r\nnoop\r\nnoop\r\naddx 21\r\naddx -15\r\nnoop\r\nnoop\r\naddx -3\r\naddx 9\r\naddx 1\r\naddx -3\r\naddx 8\r\naddx 1\r\naddx 5\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\naddx -36\r\nnoop\r\naddx 1\r\naddx 7\r\nnoop\r\nnoop\r\nnoop\r\naddx 2\r\naddx 6\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\naddx 1\r\nnoop\r\nnoop\r\naddx 7\r\naddx 1\r\nnoop\r\naddx -13\r\naddx 13\r\naddx 7\r\nnoop\r\naddx 1\r\naddx -33\r\nnoop\r\nnoop\r\nnoop\r\naddx 2\r\nnoop\r\nnoop\r\nnoop\r\naddx 8\r\nnoop\r\naddx -1\r\naddx 2\r\naddx 1\r\nnoop\r\naddx 17\r\naddx -9\r\naddx 1\r\naddx 1\r\naddx -3\r\naddx 11\r\nnoop\r\nnoop\r\naddx 1\r\nnoop\r\naddx 1\r\nnoop\r\nnoop\r\naddx -13\r\naddx -19\r\naddx 1\r\naddx 3\r\naddx 26\r\naddx -30\r\naddx 12\r\naddx -1\r\naddx 3\r\naddx 1\r\nnoop\r\nnoop\r\nnoop\r\naddx -9\r\naddx 18\r\naddx 1\r\naddx 2\r\nnoop\r\nnoop\r\naddx 9\r\nnoop\r\nnoop\r\nnoop\r\naddx -1\r\naddx 2\r\naddx -37\r\naddx 1\r\naddx 3\r\nnoop\r\naddx 15\r\naddx -21\r\naddx 22\r\naddx -6\r\naddx 1\r\nnoop\r\naddx 2\r\naddx 1\r\nnoop\r\naddx -10\r\nnoop\r\nnoop\r\naddx 20\r\naddx 1\r\naddx 2\r\naddx 2\r\naddx -6\r\naddx -11\r\nnoop\r\nnoop\r\nnoop";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            
            List<int> range = Enumerable.Select(Enumerable.Range(0, 6), x => 20 + x * 40).ToList();
            List<int> result = GetSignalStrength(range, split);
            Console.WriteLine(result.Sum());
        }

        public List<int> GetSignalStrength(List<int> range, string[] instructions)
        {
            List<int> result = new List<int>();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);

            int nextCycleNumber, cycleNumber = 0;

            foreach (string instr in instructions)
            {
                string[] arg = instr.Split(' ');
                string command = arg[0];

                // Check 20th, 60th, 100th, 140th, 180th and 220th cycle.
                if (range.Count > 0)
                {
                    nextCycleNumber = range.Min();
                    if (cycleNumber >= nextCycleNumber)
                    {
                        result.Add(queue.Peek() * nextCycleNumber);
                        range.Remove(nextCycleNumber);
                    }
                }

                // Handle CPU operations.
                while (queue.Count >= 2)
                {
                    int first = queue.Dequeue();
                    int second = queue.Dequeue();
                    first += second;
                    queue.Enqueue(first);
                }

                // Add instructions to queue.
                switch (command)
                {
                    case "noop":
                        cycleNumber++;
                        break;
                    case "addx":
                        queue.Enqueue(int.Parse(arg[1]));
                        cycleNumber += 2;
                        break;
                    default:
                        throw new Exception("Unknown instruction");
                }
            }

            return result;
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            input = "addx 15\r\naddx -11\r\naddx 6\r\naddx -3\r\naddx 5\r\naddx -1\r\naddx -8\r\naddx 13\r\naddx 4\r\nnoop\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx -35\r\naddx 1\r\naddx 24\r\naddx -19\r\naddx 1\r\naddx 16\r\naddx -11\r\nnoop\r\nnoop\r\naddx 21\r\naddx -15\r\nnoop\r\nnoop\r\naddx -3\r\naddx 9\r\naddx 1\r\naddx -3\r\naddx 8\r\naddx 1\r\naddx 5\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\naddx -36\r\nnoop\r\naddx 1\r\naddx 7\r\nnoop\r\nnoop\r\nnoop\r\naddx 2\r\naddx 6\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\naddx 1\r\nnoop\r\nnoop\r\naddx 7\r\naddx 1\r\nnoop\r\naddx -13\r\naddx 13\r\naddx 7\r\nnoop\r\naddx 1\r\naddx -33\r\nnoop\r\nnoop\r\nnoop\r\naddx 2\r\nnoop\r\nnoop\r\nnoop\r\naddx 8\r\nnoop\r\naddx -1\r\naddx 2\r\naddx 1\r\nnoop\r\naddx 17\r\naddx -9\r\naddx 1\r\naddx 1\r\naddx -3\r\naddx 11\r\nnoop\r\nnoop\r\naddx 1\r\nnoop\r\naddx 1\r\nnoop\r\nnoop\r\naddx -13\r\naddx -19\r\naddx 1\r\naddx 3\r\naddx 26\r\naddx -30\r\naddx 12\r\naddx -1\r\naddx 3\r\naddx 1\r\nnoop\r\nnoop\r\nnoop\r\naddx -9\r\naddx 18\r\naddx 1\r\naddx 2\r\nnoop\r\nnoop\r\naddx 9\r\nnoop\r\nnoop\r\nnoop\r\naddx -1\r\naddx 2\r\naddx -37\r\naddx 1\r\naddx 3\r\nnoop\r\naddx 15\r\naddx -21\r\naddx 22\r\naddx -6\r\naddx 1\r\nnoop\r\naddx 2\r\naddx 1\r\nnoop\r\naddx -10\r\nnoop\r\nnoop\r\naddx 20\r\naddx 1\r\naddx 2\r\naddx 2\r\naddx -6\r\naddx -11\r\nnoop\r\nnoop\r\nnoop";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            
            List<int> range = Enumerable.Select(Enumerable.Range(0, 6), x => 20 + x * 40).ToList();
            char[,] result = GetCRTWindowOutput(range, split);

            CustomPrintArray(result);
        }

        public char[,] GetCRTWindowOutput(List<int> range, string[] instructions)
        {
            List<int> result = new List<int>();
            Queue<int> registerX = new Queue<int>();
            registerX.Enqueue(1);

            int nextCycleNumber, cycleNumber = 0;
            int x = 1, y = 0;
            char[,] array = new char[40, 7];
            HashSet<int> hashSet = new HashSet<int>() { 0, 1, 2 };
            array[0, 0] = '#';
            foreach (string instr in instructions)
            {
                string[] arg = instr.Split(' ');
                string command = arg[0];

                int currentRegisterX = registerX.Peek();

                // Check 20th, 60th, 100th, 140th, 180th and 220th cycle.
                if (range.Count > 0)
                {
                    nextCycleNumber = range.Min();
                    if (cycleNumber >= nextCycleNumber)
                    {
                        result.Add(registerX.Peek() * nextCycleNumber);
                        range.Remove(nextCycleNumber);
                    }
                }

                // Handle CPU operations.
                if (registerX.Count >= 2)
                {
                    int first = registerX.Dequeue();
                    int second = registerX.Dequeue();
                    first += second;
                    registerX.Enqueue(first);
                }

                // Add instructions to queue.
                switch (command)
                {
                    case "noop":
                        cycleNumber++;
                        break;
                    case "addx":
                        int value = int.Parse(arg[1]);
                        registerX.Enqueue(value);
                        cycleNumber += 2;

                        if (currentRegisterX >= 0 && currentRegisterX <= 40 && hashSet.Contains(x))
                        {
                            array[x, y] = '#';
                        }
                        else
                        {
                            array[x, y] = '.';
                        }
                        break;
                    default:
                        throw new Exception("Unknown instruction");
                }

                hashSet = new HashSet<int>() { currentRegisterX - 1, currentRegisterX, currentRegisterX + 1 };
                x++;

                if (x >= 40)
                {
                    x = 0;
                    y++;
                }

            }

            return array;
        }

        private void CustomPrintArray(char[,] array)
        {
            int rowLength = array.GetLength(0);
            int columnLength = array.GetLength(1);
            for (int y = 0; y < columnLength; y++)
            {
                for (int x = 0; x < rowLength; x++)
                {
                    Console.Write(array[x, y] == '\0' ? '.' : array[x, y]);

                }
                Console.WriteLine();
            }
        }
    }
}
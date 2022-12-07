using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class U5
    {
        private readonly AocHttpClient _client = new AocHttpClient(5);
        private readonly List<Stack<string>> _stacks = new List<Stack<string>>();

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            

            Console.WriteLine();
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            var split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine();
        }


    }
}

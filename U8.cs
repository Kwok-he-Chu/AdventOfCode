using System;
using System.Linq;

namespace AOC2022
{
    public class U8
    {
        private readonly AocHttpClient _client = new AocHttpClient(8);

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

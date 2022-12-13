using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class D13
    {
        private readonly AocHttpClient _client = new AocHttpClient(13);

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi";
            Console.WriteLine();
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            Console.WriteLine();
        }
    }
}
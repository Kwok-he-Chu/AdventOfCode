using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AOC2022
{
    public class AocHttpClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _sessionToken;
        private readonly int _number;

        public AocHttpClient(int number)
        {
            _number = number;
            _sessionToken = Environment.GetEnvironmentVariable("AdventOfCodeSessionToken", EnvironmentVariableTarget.Machine) ?? throw new ArgumentNullException();
        }

        public async Task<string> RetrieveFile()
        {
            string fileName = $"{_number}.txt";
            if (File.Exists(fileName))
                return File.ReadAllText(fileName);

            string url = $"https://adventofcode.com/2022/day/{_number}/input";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
            message.Headers.Add("Cookie", $"session={_sessionToken}");

            HttpResponseMessage response = await _httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            string output = await response.Content.ReadAsStringAsync();
            File.WriteAllLines(fileName, output.Split('\n'));
            
            return File.ReadAllText(fileName);
        }
    }
}

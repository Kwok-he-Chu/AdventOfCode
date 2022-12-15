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
        private readonly int _year = 2022;
        private readonly int _day;

        public AocHttpClient(int day)
        {
            _day = day;
            _sessionToken = Environment.GetEnvironmentVariable("AdventOfCodeSessionToken", EnvironmentVariableTarget.Machine) ?? throw new ArgumentNullException();
        }

        public async Task<string> RetrieveFile()
        {
            string fileName = $"input_{_year}_{_day.ToString("D2")}.txt";
            string path = $"../../../Input/";
            
            // If exists, read file from the folder "/{year}/Input/" after cd from root executable "/bin/Debug/net6.0/"
            if (File.Exists(path + fileName))
                return File.ReadAllText(path + fileName);

            // Fetch from Advent of Code.
            string url = $"https://adventofcode.com/{_year}/day/{_day}/input";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
            message.Headers.Add("Cookie", $"session={_sessionToken}");

            HttpResponseMessage response = await _httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            // Save the file to "/{path}/{fileName}" so we don't have to fetch it again.
            string output = await response.Content.ReadAsStringAsync();
            File.WriteAllLines(path + fileName, output.Split('\n'));
            
            return File.ReadAllText(path + fileName);
        }
    }
}

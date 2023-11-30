using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AOC2023;

public class AOCHttpClient
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly string _sessionToken;
    private readonly int _year;
    private readonly int _day;

    public AOCHttpClient(int day, int year = 2023)
    {
        _day = day;
        _year = year;
        _sessionToken = Environment.GetEnvironmentVariable("AdventOfCodeSessionToken", EnvironmentVariableTarget.Machine) ?? throw new ArgumentNullException();
    }

    /// <summary>
    /// Retrieve input file using a synchronous (blocking) call.
    /// </summary>
    /// <returns>Input file.</returns>
    public string RetrieveFile()
    {
        return RetrieveFileAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Retrieve input file using an asynchronous call.
    /// </summary>
    /// <returns>Input file.</returns>
    private async Task<string> RetrieveFileAsync()
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

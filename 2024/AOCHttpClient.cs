﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AOC2024;

public class AOCHttpClient
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly string _sessionToken;
    private readonly int _year;
    private readonly int _day;

    public AOCHttpClient(int day, int year = 2024)
    {
        _day = day;
        _year = year;
        // Add your sessionToken from your cookies (adventofcode.com) here.
        // EnvironmentVariableTarget.Process: MacOS (`~/.zshrc`).
        // EnvironmentVariableTarget.Machine: Windows.
        _sessionToken = Environment.GetEnvironmentVariable("AdventOfCodeSessionToken", EnvironmentVariableTarget.Process) ?? throw new ArgumentNullException(); 
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
        
        // If exists, read file from the folder "/{year}/Input/"
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
        File.WriteAllLines(path + fileName, output.Split(Environment.NewLine));
        
        return File.ReadAllText(path + fileName);
    }
}

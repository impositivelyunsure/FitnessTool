using FitnessContracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace FitnessTool
{
    public class FitnessAPIClient : IFitnessContract
    {
        private readonly HttpClient _httpClient;

        public FitnessAPIClient(string baseAddress = "https://localhost:7105")
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }
        public double AddMacros(double proteins, double fats, double carbs)
        {
            return Post<double>("/macros", new { proteins, fats, carbs });
        }

        private T Post<T>(string path, object body)
        {
            // Block on async to satisfy sync contract
            var response = _httpClient.PostAsJsonAsync(path, body).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
            return result!;
        }
    }
}


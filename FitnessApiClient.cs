using FitnessContracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace FitnessTool
{
    public class FitnessApiClient : IFitnessContract
    {
        private readonly HttpClient _httpClient;

        public FitnessApiClient(string baseAddress = "https://localhost:7105")
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public double CalcMacros(double proteins, double fats, double carbs)
        {
            return Post<double>("/calculate-macros", new { proteins, fats, carbs });
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

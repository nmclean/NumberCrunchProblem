using Microsoft.AspNetCore.WebUtilities;
using NumberCrunchApp.Models;

namespace NumberCrunchApp.Services {
    public class NumberCrunchApiService {
        private const int PageSize = 50;

        private readonly HttpClient _httpClient;

        public NumberCrunchApiService(HttpClient httpClient) {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7207/NumberCrunch/");
        }

        public async Task<int> GenerateSamples(GenerateSamplesRequest request, CancellationToken cancellationToken = default) {
            var response = await _httpClient.PostAsJsonAsync("", request, cancellationToken);
            response.EnsureSuccessStatusCode();
            var contentString = await response.Content.ReadAsStringAsync(cancellationToken);
            return int.Parse(contentString);
        }

        public async Task<SampleResponse?> GetSamples(int id, int page, CancellationToken cancellationToken = default) {
            var requestUri = QueryHelpers.AddQueryString($"{id}",
                new KeyValuePair<string, string?>[] {
                    new("start", $"{1 + (page - 1) * PageSize}"),
                    new("count", $"{PageSize}")
                });

            return await _httpClient.GetFromJsonAsync<SampleResponse>(requestUri, cancellationToken);
        }
    }
}

using BlockedCountry.Application.IExternalServices;
using System.Text.Json;


namespace BlockedCountry.Infrastructure.ExternalServices
{
    public class RestCountriesService : ICountryLookupService
    {
        private readonly HttpClient _httpClient;

        public RestCountriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetCountryNameByCodeAsync(string code)
        {
            var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/alpha/{code}");

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.GetArrayLength() > 0 && root[0].TryGetProperty("name", out var nameObj))
            {
                return nameObj.GetProperty("common").GetString();
            }

            return null;
        }
    }
}

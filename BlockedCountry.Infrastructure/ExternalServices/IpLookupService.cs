using BlockedCountry.Application.IExternalServices;
using BlockedCountry.Contracts.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace BlockedCountry.Infrastructure.ExternalServices
{
    public class IpLookupService : IIpLookupService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IpLookupService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IpLookupResponse> LookupAsync(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }

            if (!IPAddress.TryParse(ipAddress, out _))
                throw new ArgumentException("Invalid IP address format.");

            var apiKey = _configuration["IpGeolocation:ApiKey"];
            var url = $"https://api.ipgeolocation.io/ipgeo?apiKey={apiKey}&ip={ipAddress}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Geolocation API error: {error}");
            }

            var content = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            return new IpLookupResponse
            {
                Ip = root.GetProperty("ip").GetString(),
                CountryCode = root.GetProperty("country_code2").GetString(),
                CountryName = root.GetProperty("country_name").GetString(),
                Isp = root.GetProperty("isp").GetString()
            };
        }
    }
}

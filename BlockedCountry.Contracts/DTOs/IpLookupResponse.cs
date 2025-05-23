using System.Text.Json.Serialization;

namespace BlockedCountry.Contracts.DTOs;

public class IpLookupResponse
{
    //[JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;

    //[JsonPropertyName("country_code2")]
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string Isp { get; set; } = string.Empty;
}

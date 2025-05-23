namespace BlockedCountry.Contracts.DTOs;

public class IpLookupResponse
{
    public string Ip { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string Isp { get; set; } = string.Empty;
}

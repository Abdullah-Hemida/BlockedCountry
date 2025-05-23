namespace BlockedCountry.Contracts.DTOs;

public class TemporalBlockRequest
{
    public string CountryCode { get; set; } = string.Empty;
    public int DurationMinutes { get; set; } // Must be 1 - 1440
}


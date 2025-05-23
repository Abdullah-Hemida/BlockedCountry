namespace BlockedCountry.Domain.Entities;

public class BlockedAttempt
{
    public string IpAddress { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string UserAgent { get; set; } = string.Empty;
}


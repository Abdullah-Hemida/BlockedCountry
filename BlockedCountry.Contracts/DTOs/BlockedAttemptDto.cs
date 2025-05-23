
namespace BlockedCountry.Contracts.DTOs
{
    public class BlockedAttemptDto
    {
        public string IpAddress { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserAgent { get; set; } = string.Empty;
    }
}

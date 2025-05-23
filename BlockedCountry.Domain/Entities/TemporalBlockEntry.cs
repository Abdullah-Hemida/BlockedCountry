namespace BlockedCountry.Domain.Entities
{
    public class TemporalBlockEntry
    {
        public string CountryCode { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
    }
}


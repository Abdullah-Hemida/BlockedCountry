
namespace BlockedCountry.Application.IServices
{
    public interface ITemporalBlockService
    {
        Task<bool> BlockTemporarilyAsync(string countryCode, int durationMinutes);
        bool IsTemporarilyBlocked(string countryCode);
        void CleanupExpired();
    }
}

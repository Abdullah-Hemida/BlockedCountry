
namespace BlockedCountry.Application.IRepositories
{
    public interface ITemporalBlockRepository
    {
        Task<bool> AddAsync(string countryCode, DateTime expiryTime);
        void CleanupExpired();
        bool IsBlocked(string countryCode);
    }
}

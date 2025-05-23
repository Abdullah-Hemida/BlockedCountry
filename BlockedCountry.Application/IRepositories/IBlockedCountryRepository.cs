using BlockedCountry.Domain.Entities;

namespace BlockedCountry.Application.IRepositories
{
    public interface IBlockedCountryRepository
    {
        Task<bool> AddAsync(TheBlockedCountry country);
        Task<bool> RemoveAsync(string countryCode);
        Task<IEnumerable<TheBlockedCountry>> GetAllAsync(string? filter = null, int page = 1, int pageSize = 10);
        Task<bool> ExistsAsync(string countryCode);
    }
}

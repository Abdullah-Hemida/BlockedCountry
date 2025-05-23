using BlockedCountry.Domain.Entities;

namespace BlockedCountry.Application.IServices
{
    public interface ICountryService
    {
        Task<bool> BlockCountryAsync(string countryCode);
        Task<bool> UnblockCountryAsync(string countryCode);
        Task<IEnumerable<TheBlockedCountry>> GetBlockedCountriesAsync(string? filter = null, int page = 1, int pageSize = 10);
    }
}

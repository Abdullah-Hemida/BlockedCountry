using BlockedCountry.Application.IRepositories;
using BlockedCountry.Application.IServices;
using BlockedCountry.Domain.Entities;

namespace BlockedCountry.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IBlockedCountryRepository _repository;

        public CountryService(IBlockedCountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> BlockCountryAsync(string countryCode)
        {
            if (await _repository.ExistsAsync(countryCode)) return false;
            return await _repository.AddAsync(new TheBlockedCountry { CountryCode = countryCode });
        }

        public Task<bool> UnblockCountryAsync(string countryCode) => _repository.RemoveAsync(countryCode);

        public Task<IEnumerable<TheBlockedCountry>> GetBlockedCountriesAsync(string? filter, int page, int pageSize)
            => _repository.GetAllAsync(filter, page, pageSize);
    }
}

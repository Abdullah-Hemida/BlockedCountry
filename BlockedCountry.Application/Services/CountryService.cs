using BlockedCountry.Application.IExternalServices;
using BlockedCountry.Application.IRepositories;
using BlockedCountry.Application.IServices;
using BlockedCountry.Domain.Entities;

namespace BlockedCountry.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IBlockedCountryRepository _repository;
        private readonly ICountryLookupService _countryLookup;
        public CountryService(IBlockedCountryRepository repository, ICountryLookupService countryLookup)
        {
            _repository = repository;
            _countryLookup = countryLookup;
        }

        public async Task<bool> BlockCountryAsync(string countryCode)
        {
            if (await _repository.ExistsAsync(countryCode)) return false;

            var CountryName = await _countryLookup.GetCountryNameByCodeAsync(countryCode);
            if (CountryName == null) return false;

            return await _repository.AddAsync(new TheBlockedCountry
            {
                CountryCode = countryCode,
                CountryName = CountryName
            });
        }

        public Task<bool> UnblockCountryAsync(string countryCode) => _repository.RemoveAsync(countryCode);

        public Task<IEnumerable<TheBlockedCountry>> GetBlockedCountriesAsync(string? filter, int page, int pageSize)
            => _repository.GetAllAsync(filter, page, pageSize);
    }
}

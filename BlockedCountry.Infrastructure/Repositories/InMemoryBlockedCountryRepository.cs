using BlockedCountry.Application.IRepositories;
using BlockedCountry.Domain.Entities;
using System.Collections.Concurrent;


namespace BlockedCountry.Infrastructure.Repositories
{
    public class InMemoryBlockedCountryRepository : IBlockedCountryRepository
    {
        private readonly ConcurrentDictionary<string, TheBlockedCountry> _countries = new(StringComparer.OrdinalIgnoreCase);

        public Task<bool> AddAsync(TheBlockedCountry country) => Task.FromResult(_countries.TryAdd(country.CountryCode, country));
        public Task<bool> RemoveAsync(string countryCode) => Task.FromResult(_countries.TryRemove(countryCode, out _));
        public Task<IEnumerable<TheBlockedCountry>> GetAllAsync(string? filter, int page, int pageSize)
        {
            var query = _countries.Values.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(c => c.CountryCode.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                                         (!string.IsNullOrEmpty(c.CountryName) && c.CountryName.Contains(filter, StringComparison.OrdinalIgnoreCase)))
;
            }
            return Task.FromResult(query.Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable());
        }
        public Task<bool> ExistsAsync(string countryCode) => Task.FromResult(_countries.ContainsKey(countryCode));
    }
}

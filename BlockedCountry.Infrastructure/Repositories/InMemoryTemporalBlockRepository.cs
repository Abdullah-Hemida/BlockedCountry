using BlockedCountry.Application.IRepositories;
using System.Collections.Concurrent;


namespace BlockedCountry.Infrastructure.Repositories
{
    public class InMemoryTemporalBlockRepository : ITemporalBlockRepository
    {
        private readonly ConcurrentDictionary<string, DateTime> _temporalBlocks = new(StringComparer.OrdinalIgnoreCase);

        public Task<bool> AddAsync(string countryCode, DateTime expiryTime)
            => Task.FromResult(_temporalBlocks.TryAdd(countryCode, expiryTime));

        public void CleanupExpired()
        {
            var now = DateTime.UtcNow;
            foreach (var kvp in _temporalBlocks)
            {
                if (kvp.Value < now)
                    _temporalBlocks.TryRemove(kvp.Key, out _);
            }
        }

        public bool IsBlocked(string countryCode)
        {
            return _temporalBlocks.TryGetValue(countryCode, out var expiry) && expiry > DateTime.UtcNow;
        }
    }
}

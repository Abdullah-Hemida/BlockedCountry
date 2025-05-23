using BlockedCountry.Application.IRepositories;
using BlockedCountry.Application.IServices;

namespace BlockedCountry.Application.Services
{
    public class TemporalBlockService : ITemporalBlockService
    {
        private readonly ITemporalBlockRepository _repository;

        public TemporalBlockService(ITemporalBlockRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> BlockTemporarilyAsync(string countryCode, int durationMinutes)
        {
            if (durationMinutes < 1 || durationMinutes > 1440) return false;
            var expiry = DateTime.UtcNow.AddMinutes(durationMinutes);
            return await _repository.AddAsync(countryCode, expiry);
        }

        public bool IsTemporarilyBlocked(string countryCode) => _repository.IsBlocked(countryCode);

        public void CleanupExpired() => _repository.CleanupExpired();
    }
}

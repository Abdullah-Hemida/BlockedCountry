using BlockedCountry.Application.IRepositories;
using BlockedCountry.Domain.Entities;

namespace BlockedCountry.Infrastructure.Repositories
{
    public class InMemoryBlockedAttemptRepository : IBlockedAttemptRepository
    {
        private readonly List<BlockedAttempt> _attempts = new();
        private readonly object _lock = new();

        public void Add(BlockedAttempt attempt)
        {
            lock (_lock) { _attempts.Add(attempt); }
        }

        public IEnumerable<BlockedAttempt> GetPaginated(int page, int pageSize)
        {
            lock (_lock)
            {
                return _attempts.OrderByDescending(x => x.Timestamp).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int Count()
        {
            lock (_lock) { return _attempts.Count; }
        }
    }
}
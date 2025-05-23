using BlockedCountry.Domain.Entities;

namespace BlockedCountry.Application.IRepositories
{
    public interface IBlockedAttemptRepository
    {
        void Add(BlockedAttempt attempt);
        IEnumerable<BlockedAttempt> GetPaginated(int page, int pageSize);
        int Count();
    }
}

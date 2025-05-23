using BlockedCountry.Contracts.DTOs;
using BlockedCountry.Domain.Entities;

namespace BlockedCountry.Application.IServices
{
    public interface IBlockedAttemptService
    {
        void LogAttempt(BlockedAttempt attempt);
        PaginatedResult<BlockedAttemptDto> GetBlockedAttempts(int page, int pageSize);
    }
}

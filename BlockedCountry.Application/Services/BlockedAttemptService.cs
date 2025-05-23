using BlockedCountry.Application.IRepositories;
using BlockedCountry.Application.IServices;
using BlockedCountry.Contracts.DTOs;
using BlockedCountry.Domain.Entities;

namespace BlockedCountry.Application.Services
{
    public class BlockedAttemptService : IBlockedAttemptService
    {
        private readonly IBlockedAttemptRepository _repository;

        public BlockedAttemptService(IBlockedAttemptRepository repository)
        {
            _repository = repository;
        }

        public void LogAttempt(BlockedAttempt attempt) => _repository.Add(attempt);

        public PaginatedResult<BlockedAttemptDto> GetBlockedAttempts(int page, int pageSize)
        {
            var items = _repository.GetPaginated(page, pageSize)
                        .Select(a => new BlockedAttemptDto
                        {
                            IpAddress = a.IpAddress,
                            CountryCode = a.CountryCode,
                            IsBlocked = a.IsBlocked,
                            Timestamp = a.Timestamp,
                            UserAgent = a.UserAgent
                        });

            return new PaginatedResult<BlockedAttemptDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = _repository.Count(),
                Items = items
            };
        }
    }
}

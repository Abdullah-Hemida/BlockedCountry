using BlockedCountry.Contracts.DTOs;
namespace BlockedCountry.Application.IExternalServices
{
    public interface IIpLookupService
    {
        Task<IpLookupResponse> LookupAsync(string ipAddress);
    }
}


namespace BlockedCountry.Application.IExternalServices
{
    public interface ICountryLookupService
    {
        Task<string?> GetCountryNameByCodeAsync(string code);
    }
}

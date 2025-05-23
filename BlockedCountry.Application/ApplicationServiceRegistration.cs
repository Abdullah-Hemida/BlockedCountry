using BlockedCountry.Application.IServices;
using BlockedCountry.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace BlockedCountry.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICountryService, CountryService>();
            services.AddSingleton<IBlockedAttemptService, BlockedAttemptService>();
            services.AddSingleton<ITemporalBlockService, TemporalBlockService>();
            return services;
        }
    }
}

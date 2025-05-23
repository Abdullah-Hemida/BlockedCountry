using BlockedCountry.Application.IExternalServices;
using BlockedCountry.Application.IRepositories;
using BlockedCountry.Infrastructure.ExternalServices;
using BlockedCountry.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlockedCountry.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddSingleton<IBlockedCountryRepository, InMemoryBlockedCountryRepository>();
            services.AddSingleton<IBlockedAttemptRepository, InMemoryBlockedAttemptRepository>();
            services.AddSingleton<ITemporalBlockRepository, InMemoryTemporalBlockRepository>();

            // External Services
            services.AddHttpClient<IIpLookupService, IpLookupService>();
            services.AddHttpClient<ICountryLookupService, RestCountriesService>();
            return services;
        }
    }
}

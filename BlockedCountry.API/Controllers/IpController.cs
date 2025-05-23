using BlockedCountry.Application.IExternalServices;
using BlockedCountry.Application.IServices;
using BlockedCountry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlockedCountry.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IpController : ControllerBase
    {
        private readonly IIpLookupService _ipLookupService;
        private readonly ICountryService _countryService;
        private readonly ITemporalBlockService _temporalBlockService;
        private readonly IBlockedAttemptService _attemptService;

        public IpController(IIpLookupService ipLookupService, ICountryService countryService, ITemporalBlockService temporalBlockService, IBlockedAttemptService attemptService)
        {
            _ipLookupService = ipLookupService;
            _countryService = countryService;
            _temporalBlockService = temporalBlockService;
            _attemptService = attemptService;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> Lookup([FromQuery] string? ipAddress)
        {
            var ip = string.IsNullOrWhiteSpace(ipAddress) ? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "" : ipAddress;
            var result = await _ipLookupService.LookupAsync(ip);
            return Ok(result);
        }

        [HttpGet("check-block")]
        public async Task<IActionResult> CheckBlock()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
            var userAgent = Request.Headers.UserAgent.ToString();

            var ipInfo = await _ipLookupService.LookupAsync(ip);
            var isBlocked = (await _countryService.GetBlockedCountriesAsync()).Any(x => x.CountryCode.Equals(ipInfo.CountryCode, StringComparison.OrdinalIgnoreCase))
                             || _temporalBlockService.IsTemporarilyBlocked(ipInfo.CountryCode);

            _attemptService.LogAttempt(new BlockedAttempt
            {
                IpAddress = ip,
                CountryCode = ipInfo.CountryCode,
                IsBlocked = isBlocked,
                UserAgent = userAgent
            });

            return Ok(new { IsBlocked = isBlocked });
        }
    }
}

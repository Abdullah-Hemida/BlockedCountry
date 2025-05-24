using BlockedCountry.Application.IExternalServices;
using BlockedCountry.Application.IServices;
using BlockedCountry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlockedCountry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IpController : ControllerBase
{
    private readonly IIpLookupService _ipLookupService;
    private readonly ICountryService _countryService;
    private readonly ITemporalBlockService _temporalBlockService;
    private readonly IBlockedAttemptService _attemptService;

    public IpController(IIpLookupService ipLookupService, ICountryService countryService,
        ITemporalBlockService temporalBlockService, IBlockedAttemptService attemptService)
    {
        _ipLookupService = ipLookupService;
        _countryService = countryService;
        _temporalBlockService = temporalBlockService;
        _attemptService = attemptService;
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> Lookup([FromQuery] string? ipAddress)
    {
        try
        {
            var ip = string.IsNullOrWhiteSpace(ipAddress)
                ? HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""
                : ipAddress;

            var result = await _ipLookupService.LookupAsync(ip);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(502, $"Geolocation API error: {ex.Message}");
        }
    }

    [HttpGet("check-block")]
    public async Task<IActionResult> CheckBlock([FromQuery] string? ipAddress)
    {
        try
        {
            var ip = string.IsNullOrWhiteSpace(ipAddress)
                ? HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""
                : ipAddress;

            var userAgent = Request.Headers.UserAgent.ToString();
            var ipInfo = await _ipLookupService.LookupAsync(ip);

            var isBlocked =
                (await _countryService.GetBlockedCountriesAsync())
                .Any(x => x.CountryCode.Equals(ipInfo.CountryCode, StringComparison.OrdinalIgnoreCase))
                || _temporalBlockService.IsTemporarilyBlocked(ipInfo.CountryCode);

            _attemptService.LogAttempt(new BlockedAttempt
            {
                IpAddress = ip,
                CountryCode = ipInfo.CountryCode,
                IsBlocked = isBlocked,
                Timestamp = DateTime.UtcNow,
                UserAgent = userAgent
            });

            return Ok(new { IsBlocked = isBlocked });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Unexpected error: {ex.Message}");
        }
    }
}


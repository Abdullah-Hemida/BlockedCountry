using BlockedCountry.Application.IServices;
using BlockedCountry.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlockedCountry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;
    private readonly ITemporalBlockService _temporalBlockService;

    public CountriesController(ICountryService countryService, ITemporalBlockService temporalBlockService)
    {
        _countryService = countryService;
        _temporalBlockService = temporalBlockService;
    }

    [HttpPost("block")]
    public async Task<IActionResult> BlockCountry([FromBody] BlockCountryRequest request)
    {
        if (!IsValidCountryCode(request.CountryCode))
            return BadRequest("Invalid country code. Use ISO 3166-1 alpha-2 format (e.g., 'US').");

        try
        {
            var result = await _countryService.BlockCountryAsync(request.CountryCode);
            if (!result)
                return Conflict("Country already blocked or not found.");
            return Ok("Country blocked successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Unexpected error: {ex.Message}");
        }
    }

    [HttpDelete("block/{countryCode}")]
    public async Task<IActionResult> UnblockCountry(string countryCode)
    {
        if (!IsValidCountryCode(countryCode))
            return BadRequest("Invalid country code.");

        var result = await _countryService.UnblockCountryAsync(countryCode);
        if (!result) return NotFound("Country not found.");
        return Ok("Country unblocked successfully.");
    }

    [HttpGet("blocked")]
    public async Task<IActionResult> GetBlockedCountries([FromQuery] string? filter, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var list = await _countryService.GetBlockedCountriesAsync(filter, page, pageSize);
        return Ok(list);
    }

    [HttpPost("temporal-block")]
    public async Task<IActionResult> TemporarilyBlock([FromBody] TemporalBlockRequest request)
    {
        if (!IsValidCountryCode(request.CountryCode))
            return BadRequest("Invalid country code.");

        if (request.DurationMinutes < 1 || request.DurationMinutes > 1440)
            return BadRequest("Duration must be between 1 and 1440 minutes.");

        try
        {
            var result = await _temporalBlockService.BlockTemporarilyAsync(request.CountryCode, request.DurationMinutes);
            if (!result)
                return Conflict("Country is already temporarily blocked.");

            return Ok("Country temporarily blocked.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Unexpected error: {ex.Message}");
        }
    }

    private bool IsValidCountryCode(string code) =>
        !string.IsNullOrWhiteSpace(code) && code.Length == 2 && code.All(char.IsLetter);
}


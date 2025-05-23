using BlockedCountry.Application.IServices;
using BlockedCountry.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlockedCountry.API.Controllers
{
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
            var result = await _countryService.BlockCountryAsync(request.CountryCode);
            if (!result) return Conflict("Country already blocked.");
            return Ok("Country blocked successfully.");
        }

        [HttpDelete("block/{countryCode}")]
        public async Task<IActionResult> UnblockCountry(string countryCode)
        {
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
            if (request.DurationMinutes < 1 || request.DurationMinutes > 1440)
                return BadRequest("Duration must be between 1 and 1440 minutes.");

            var result = await _temporalBlockService.BlockTemporarilyAsync(request.CountryCode, request.DurationMinutes);
            if (!result) return Conflict("Country is already temporarily blocked.");
            return Ok("Country temporarily blocked.");
        }
    }
}

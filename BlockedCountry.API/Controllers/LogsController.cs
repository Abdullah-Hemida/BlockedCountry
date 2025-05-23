using BlockedCountry.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BlockedCountry.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly IBlockedAttemptService _blockedAttemptService;

        public LogsController(IBlockedAttemptService blockedAttemptService)
        {
            _blockedAttemptService = blockedAttemptService;
        }

        [HttpGet("blocked-attempts")]
        public IActionResult GetBlockedAttempts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = _blockedAttemptService.GetBlockedAttempts(page, pageSize);
            return Ok(result);
        }
    }
}

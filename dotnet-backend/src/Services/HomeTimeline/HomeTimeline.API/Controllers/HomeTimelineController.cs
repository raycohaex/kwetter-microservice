using HomeTimeline.Application.Contracts;
using HomeTimeline.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HomeTimeline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeTimelineController : ControllerBase
    {
        private readonly ILogger<HomeTimelineController> _logger;
        private readonly IHomeTimelineService _homeTimelineService;

        public HomeTimelineController(ILogger<HomeTimelineController> logger, IHomeTimelineService homeTimelineService)
        {
            _logger = logger;
            _homeTimelineService = homeTimelineService;
        }

        [HttpGet("username")]
        public async Task<ActionResult<Timeline>> GetHomeTimelineByUsername(string username)
        {
            var timeline = await _homeTimelineService.GetHomeTimeline(username);

            if (timeline == null) {
                return NotFound();
            }

            return Ok(timeline);
        }
    }
}
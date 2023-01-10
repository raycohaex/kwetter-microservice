using HomeTimeline.Application.Contracts;
using HomeTimeline.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HomeTimeline.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HomeTimelineController : ControllerBase
    {
        private readonly ILogger<HomeTimelineController> _logger;
        private readonly IHomeTimelineService _homeTimelineService;

        public HomeTimelineController(ILogger<HomeTimelineController> logger, IHomeTimelineService homeTimelineService)
        {
            _logger = logger;
            _homeTimelineService = homeTimelineService;
        }

        [HttpGet]
        public async Task<ActionResult<Timeline>> GetHomeTimeline()
        {
            var user = User;

            // Get the username from the user's claims
            var usernameClaim = user.Identity.Name;

            if (user.Identity.IsAuthenticated == null)
            {
                return Unauthorized();
            }

            if(usernameClaim == null)
            {
                return BadRequest();
            }

            var timeline = await _homeTimelineService.GetHomeTimeline(usernameClaim);

            if (timeline == null) {
                return NotFound();
            }

            return Ok(timeline);
        }
    }
}
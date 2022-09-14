using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using UserTimeline.Application.Features.Queries.GetTimeline;

namespace UserTimeline.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimelineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}", Name = "GetTimeline")]
        [ProducesResponseType(typeof(TimelineViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TimelineViewModel>> GetUserTimelineByUsername(string username)
        {
            var query = new GetTimelineQuery(username);
            var timeline = await _mediator.Send(query);
            return Ok(timeline);
        }
    }
}

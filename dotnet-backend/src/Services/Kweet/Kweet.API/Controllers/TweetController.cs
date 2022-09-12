using Kweet.Application.Features.Queries.GetKweet;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;

namespace Kweet.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TweetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tweetId}", Name = "GetTweet")]
        [ProducesResponseType(typeof(IEnumerable<KweetViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<KweetViewModel>>> GetOrdersByUserName(Guid id)
        {
            var query = new GetKweetQuery(id);
            var kweet = await _mediator.Send(query);
            return Ok(kweet);
        }
    }
}

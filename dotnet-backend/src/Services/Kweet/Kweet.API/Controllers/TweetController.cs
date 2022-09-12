using Kweet.Application.Features.Queries.GetKweet;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using Kweet.Domain.Entities;
using Kweet.Application.Features.Commands.PostKweet;

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
        public async Task<ActionResult<IEnumerable<KweetViewModel>>> GetOrdersByUserName(Guid tweetId)
        {
            var query = new GetKweetQuery(tweetId);
            var kweet = await _mediator.Send(query);
            return Ok(kweet);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> PostKweet([FromBody] PostKweetCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
